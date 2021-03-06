﻿using System;
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
using System.Threading;

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
            //ConfigData.Init();
            Init();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.Text = Properties.Resources.Form1Text;
            this.btnCancel.Text = Properties.Resources.Form1BtnCancel;
            this.Reset();
        }
        private void Reset()
        {
            this.lblFileName.Text = String.Empty;
            this.lblFileCount.Text = String.Empty;
            this.lblProgress.Text = String.Empty;
            this.executing = false;
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
            this.btnCancel.Visible = false;
            this.btnCancel.Enabled = false;
            this.progressBar1.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Execute()
        {
            this.progressBar1.Value = 0;
            this.executing = true;
            this.progressBar1.Visible = true;
            this.btnCancel.Visible = true;
            this.btnCancel.Enabled = true;
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
            int hours = seconds / hourSeconds;
            seconds -= hourSeconds * hours;
            int minutes = seconds / minuteSeconds;
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
        public void TransferFile(FileTransfer fileTransfer)
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
                    Refresh();
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
                if (this.noneCopiedList.Count > 0)
                {
                    SkippedFileList skippedFileList = new SkippedFileList();
                    skippedFileList.SetFileList(String.Join("\r\n", this.noneCopiedList));
                    skippedFileList.Show();
                }
                TimeSpan timeSpan = DateTime.Now.Subtract(start);
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
                throw new Exception(ex.Message, ex);
            }
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

    enum TimeUnit {
        Day, Hour, Minute, Second
    }
}
