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
            this.Reset();
            FileInfo configFile = AppConfig.ExistConfigFile();
            if (configFile == null)
            {
                this.btnTransfer.Enabled = false;
            }
            this.progressBar1.Visible = false;
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
        }

        private string GetRemainTime(DateTime start, double percentage)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(start);
            int daySeconds = 60 * 60 * 24;
            int hourSeconds = 60 * 60;
            int minuteSeconds = 60;
            double currentSeconds = timeSpan.TotalSeconds;
            double totalSeconds = currentSeconds / (percentage * 0.01);
            int remainSeconds = (int)totalSeconds - (int)currentSeconds;
            int days = remainSeconds / daySeconds;
            int seconds = (int)remainSeconds - daySeconds * days;
            int hours = remainSeconds / hourSeconds;
            seconds -= hourSeconds * hours;
            int minutes = remainSeconds / minuteSeconds;
            seconds -= minuteSeconds * minutes;

            if (days > 0)
                return String.Format(Properties.Resources.RemainTimeDay, seconds, minutes, hours, days);
            else if (hours > 0)
                return String.Format(Properties.Resources.RemainTimeHour, seconds, minutes, hours, days);
            else if (minutes > 0)
                return String.Format(Properties.Resources.RemainTimeMinute, seconds, minutes, hours, days);
            return String.Format(Properties.Resources.RemainTimeSecond, seconds, minutes, hours, days);
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
                        continue;
                    }
                    file.CopyTo(path);
                    fileCount++;
                    totalSize += file.Length;

                    double percentage = (double)((double)totalSize / (double)fileTransfer.TotalSize) * 100;
                    this.progressBar1.Value = (int)percentage;
                    this.lblFileName.Text = String.Format(Properties.Resources.FileNameMessage, file.FullName, file.Length / 1000000);
                    fileName = file.FullName;
                    this.lblFileCount.Text = String.Format(Properties.Resources.FileCountMessage, fileCount, fileTransfer.FileCount);
                    this.lblProgress.Text = String.Format(Properties.Resources.ProgressMessage,
                            totalSize / 1000000, fileTransfer.TotalSize / 1000000, (int)percentage, this.GetRemainTime(start, percentage));
                    Refresh();
                    Application.DoEvents();
                };
                this.Reset();
                this.lblFileCount.Text = String.Format(Properties.Resources.FileCountMessage, fileCount, fileTransfer.FileCount);
            }
            catch (IOException ex)
            {
                MessageBox.Show(String.Format(Properties.Resources.Error, ex.Message),
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
            ConfigDialog config = new ConfigDialog();
            config.ShowDialog();
            config.Dispose();
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

    }
}
