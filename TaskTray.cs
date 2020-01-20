using RDSync.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDSync
{
    public partial class TaskTray : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TaskTray()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.menuFolder.Text = Properties.Resources.TaskTrayMenuConfig;
            this.menuQuit.Text = Properties.Resources.TaskTrayMenuQuit;
            FileInfo configFile = AppConfig.ExistConfigFile();
            if (configFile == null)
            {
                this.ShowConfigDialog();
            }
            this.RunTimer();
        }
        private void RunTimer()
        {
            Timer timer = new Timer();
            timer.Tick += new EventHandler(this.WatchRemovableDrive);
            timer.Interval = 60000;
            timer.Enabled = true;
        }
        private void WatchRemovableDrive(object sender, EventArgs e)
        {
            bool formError = false;
            try
            {
                var drives = DriveInfo.GetDrives();
                foreach (var drive in drives)
                {
                    FileTransfer fileTransfer = new FileTransfer();
                    fileTransfer.Init();
                    if (fileTransfer.CopyFileList.Count > 0)
                    {
                        this.transferFile(fileTransfer);
                        formError = true;
                        this.form.Dispose();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!formError)
                {
                    MessageBox.Show(ex.Message,
                                Properties.Resources.ErrorCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                    log.Error(ex.Message, ex);
                }
                
            }
            
        }
        
        private void ShowConfigDialog()
        {
            ConfigDialog config = new ConfigDialog();
            config.ShowDialog();
            config.Dispose();
        }
        private void transferFile(FileTransfer fileTransfer)
        {
            if (this.form == null || this.form.IsDisposed)
            {
                this.form = new Form1();
                this.form.Show();
                this.form.TransferFile(fileTransfer);
            }
            this.form.Activate();
        }
        private void menuFolder_Click(object sender, EventArgs e)
        {
            this.ShowConfigDialog();
        }
        private Form1 form = null;
        private void menuQuit_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}
