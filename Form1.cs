using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RDSync.Engines;

namespace RDSync
{
    public partial class Form1 : Form
    {
        private List<string> noneCopiedList;
        private bool executing = false;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Form1()
        {
            InitializeComponent();
            ConfigData.Init();
            Init();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.Text = Properties.Resources.Form1Text;
            this.menuFile.Text = Properties.Resources.Form1MenuFile;
            this.menuFileOption.Text = Properties.Resources.Form1MenuFileOption;
            this.menuFileQuit.Text = Properties.Resources.Form1MenuFileQuit;
            this.btnCancel.Text = Properties.Resources.Form1BtnCancel;
            this.btnTransfer.Text = Properties.Resources.Form1BtnTransferText;
            this.btnFileSkipped.Text = Properties.Resources.Form1BtnSkippedFile;
            this.Reset();
            FileInfo configFile = AppConfig.ExistConfigFile();
            if (configFile == null)
            {
                this.btnTransfer.Enabled = false;
                this.ShowConfigDialog();
            }
        }
        private void Reset()
        {
            this.lblFileName.Text = String.Empty;
            this.lblFileCount.Text = String.Empty;
            this.lblProgress.Text = String.Empty;
            FileInfo configFile = AppConfig.ExistConfigFile();
            this.executing = false;
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
            this.btnCancel.Visible = false;
            this.btnCancel.Enabled = false;
            this.menuFile.Enabled = true;
            this.progressBar1.Visible = false;
            this.btnFileSkipped.Enabled = false;
            this.btnFileSkipped.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Execute()
        {
            this.progressBar1.Value = 0;
            this.btnTransfer.Enabled = false;
            this.menuFile.Enabled = false;
            this.executing = true;
            this.progressBar1.Visible = true;
            this.btnTransfer.Visible = false;
            this.btnCancel.Visible = true;
            this.btnCancel.Enabled = true;
            this.btnFileSkipped.Enabled = false;
            this.btnFileSkipped.Visible = false;
        }

        private string GetRemainTime(DateTime start, double percentage)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(start);
            double currentSeconds = timeSpan.TotalSeconds;
            double totalSeconds = currentSeconds / (percentage * 0.01);
            int remainSeconds = (int)totalSeconds - (int)currentSeconds;
            return this.GetTimeText(this.CreateTimeData(remainSeconds));
        }

        private string GetTimeText(Dictionary<TimeUnit, int> timeData)
        {
            if (timeData[TimeUnit.Day] > 0)
                return String.Format(Properties.Resources.RemainTimeDay, timeData[TimeUnit.Second], timeData[TimeUnit.Minute], timeData[TimeUnit.Hour], timeData[TimeUnit.Day]);
            else if (timeData[TimeUnit.Hour] > 0)
                return String.Format(Properties.Resources.RemainTimeHour, timeData[TimeUnit.Second], timeData[TimeUnit.Minute], timeData[TimeUnit.Hour], timeData[TimeUnit.Day]);
            else if (timeData[TimeUnit.Minute] > 0)
                return String.Format(Properties.Resources.RemainTimeMinute, timeData[TimeUnit.Second], timeData[TimeUnit.Minute], timeData[TimeUnit.Hour], timeData[TimeUnit.Day]);
            return String.Format(Properties.Resources.RemainTimeSecond, timeData[TimeUnit.Second], timeData[TimeUnit.Minute], timeData[TimeUnit.Hour], timeData[TimeUnit.Day]);
        }

        private Dictionary<TimeUnit, int> CreateTimeData(int remainSeconds)
        {
            int daySeconds = 60 * 60 * 24;
            int hourSeconds = 60 * 60;
            int minuteSeconds = 60;
            int days = remainSeconds / daySeconds;
            int seconds = (int)remainSeconds - daySeconds * days;
            int hours = remainSeconds / hourSeconds;
            seconds -= hourSeconds * hours;
            int minutes = remainSeconds / minuteSeconds;
            seconds -= minuteSeconds * minutes;
            Dictionary<TimeUnit, int> returnValue =
                    new Dictionary<TimeUnit, int>{ { TimeUnit.Day, days }, { TimeUnit.Hour, hours },
                        { TimeUnit.Minute, minutes }, {TimeUnit.Second, seconds } };
            return returnValue;

        }

        private void Cancel()
        {
            this.Reset();
            this.lblFileName.Text = Properties.Resources.FileNameMessageCancel;
            this.progressBar1.Visible = false;
        }
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            this.executing = true;
            this.Execute();
            int fileCount = 0;
            long totalSize = 0;
            string fileName = String.Empty;
            this.noneCopiedList = new List<string>();
            try
            {
                DateTime start = DateTime.Now;
                FileTransfer fileTransfer = new FileTransfer();
                fileTransfer.Init();
                foreach (FileInfo file in fileTransfer.CopyFileList)
                {
                    if (!this.executing)
                    {
                        this.Cancel();
                        return;
                    }
                    MediaConfig mediaConfig = fileTransfer.GetMediaType(file);
                    if (mediaConfig == null)
                        continue;
                    string path = fileTransfer.GetDestinationDirectory(mediaConfig, file);
                    fileTransfer.CreateDestinationDirectory(path);
                    path += Path.DirectorySeparatorChar + file.Name;
                    FileInfo target = new FileInfo(path);
                    if (target.Exists)
                    {
                        log.Info(String.Format(Properties.Resources.NotCopiedFile, path));
                        this.noneCopiedList.Add(file.FullName);
                        continue;
                    }
                    this.lblFileName.Text = String.Format(Properties.Resources.FileNameMessage, file.FullName, file.Length / 1000000, path);
                    file.CopyTo(path);
                    fileCount++;
                    totalSize += file.Length;

                    double percentage = (double)((double)totalSize / (double)fileTransfer.TotalSize) * 100;
                    this.progressBar1.Value = (int)percentage;
                    
                    fileName = file.FullName;
                    this.lblFileCount.Text = String.Format(Properties.Resources.FileCountMessage, fileCount, fileTransfer.FileCount);
                    this.lblProgress.Text = String.Format(Properties.Resources.ProgressMessage,
                            totalSize / 1000000, fileTransfer.TotalSize / 1000000, (int)percentage, this.GetRemainTime(start, percentage));
                    Refresh();
                    Application.DoEvents();
                };
                this.Reset();
                if (this.noneCopiedList.Count > 0)
                {
                    this.btnFileSkipped.Enabled = true;
                    this.btnFileSkipped.Visible = true;
                }
                TimeSpan timeSpan = DateTime.Now.Subtract(start);
                this.lblProgress.Text = this.GetTimeText(this.CreateTimeData((int)timeSpan.TotalSeconds))
                            .Replace(Properties.Resources.TimeRemainText, Properties.Resources.TimeExecutedText);
                this.lblFileCount.Text = String.Format(Properties.Resources.FileCountMessage, fileCount, fileTransfer.FileCount);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message,
                                Properties.Resources.ErrorCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                log.Error(ex.Message, ex);
                this.Init();
                this.lblFileName.Text = String.Format(Properties.Resources.FileNameMessageError, fileName);
            }

        }

        private void menuFileOption_Click(object sender, EventArgs e)
        {
            this.ShowConfigDialog();
        }

        private void ShowConfigDialog()
        {
            ConfigDialog config = new ConfigDialog();
            config.ShowDialog();
            config.Dispose();
            Init();
        }

        private void menuFileQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.executing = false;
            this.progressBar1.Value = 0;
        }

        private void btnFileSkipped_Click(object sender, EventArgs e)
        {
            SkippedFileList skippedFileList = new SkippedFileList();
            skippedFileList.SetFileList(String.Join(",", this.noneCopiedList));
            skippedFileList.Show();
        }
                
    }

    enum TimeUnit {
        Day, Hour, Minute, Second
    }
}
