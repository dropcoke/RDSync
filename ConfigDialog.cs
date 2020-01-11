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
    public partial class ConfigDialog : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ConfigDialog()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            FileInfo configFile = AppConfig.ExistConfigFile();
            if (configFile == null)
            {
                this.txtDestinationImage.Text = String.Empty;
                this.txtDestinationVideo.Text = String.Empty;
                this.txtDestinationAudio.Text = String.Empty;
            }
            else
            {
                this.txtDestinationImage.Text = ConfigData.MediaTypeList[MediaType.Image].MediaRootPath;
                this.txtDestinationVideo.Text = ConfigData.MediaTypeList[MediaType.Video].MediaRootPath;
                this.txtDestinationAudio.Text = ConfigData.MediaTypeList[MediaType.Audio].MediaRootPath;
            }
            this.lblExtensionListImage.Text = "(" + String.Join(", ", ConfigData.MediaTypeList[MediaType.Image].Extensions) + ")";
            this.lblExtensionListVideo.Text = "(" + String.Join(", ", ConfigData.MediaTypeList[MediaType.Video].Extensions) + ")";
            this.lblExtensionListAudio.Text = "(" + String.Join(", ", ConfigData.MediaTypeList[MediaType.Audio].Extensions) + ")";
            this.lblImageMedia.Text = Properties.Resources.ConfigDialogLblImageMedia;
            this.lblVideoMedia.Text = Properties.Resources.ConfigDialogLblVideoMedia;
            this.lblAudioMedia.Text = Properties.Resources.ConfigDialogLblAudioMedia;
            this.Text = Properties.Resources.ConfigDialogText;

        }

        private void btnDestinationImage_Click(object sender, EventArgs e)
        {
            string title = Properties.Resources.ImageFolder;
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = Properties.Resources.ConfigDialogOpenFIleFilter,
                CheckFileExists = false,
                InitialDirectory = ConfigData.MediaTypeList[MediaType.Image].MediaRootPath,
                FileName = title,
                Title = title
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(openFile.FileName);
                this.txtDestinationImage.Text = path;
                ConfigData.MediaTypeList[MediaType.Image].MediaRootPath = path;
            }
            openFile.Dispose();
        }

        private void btnDestinationVideo_Click(object sender, EventArgs e)
        {
            string title = Properties.Resources.VideoFolder;
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = Properties.Resources.ConfigDialogOpenFIleFilter,
                CheckFileExists = false,
                InitialDirectory = ConfigData.MediaTypeList[MediaType.Video].MediaRootPath,
                FileName = title,
                Title = title
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(openFile.FileName);
                this.txtDestinationVideo.Text = path;
                ConfigData.MediaTypeList[MediaType.Video].MediaRootPath = path;
            }
            openFile.Dispose();
        }

        private void btnDestinationAudio_Click(object sender, EventArgs e)
        {
            string title = Properties.Resources.AudioFolder;
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = Properties.Resources.ConfigDialogOpenFIleFilter,
                CheckFileExists = false,
                InitialDirectory = ConfigData.MediaTypeList[MediaType.Audio].MediaRootPath,
                FileName = title,
                Title = title
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(openFile.FileName);
                this.txtDestinationAudio.Text = path;
                ConfigData.MediaTypeList[MediaType.Audio].MediaRootPath = path;
            }
            openFile.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigData.MediaTypeList[MediaType.Image].MediaRootPath = this.txtDestinationImage.Text;
                ConfigData.MediaTypeList[MediaType.Video].MediaRootPath = this.txtDestinationVideo.Text;
                ConfigData.MediaTypeList[MediaType.Audio].MediaRootPath = this.txtDestinationAudio.Text;
                AppConfig.Store(ConfigData.AppConfig);
                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message,
                                Properties.Resources.ErrorCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                log.Error(ex.Message, ex);
                Application.Exit();
            }
        }
    }
}
