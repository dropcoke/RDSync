using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDSync
{
    public partial class SkippedFileList : Form
    {
        private string skippedFiles;
        public SkippedFileList()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.Text = Properties.Resources.SkippedFileListText;
            this.txtSkippedFileList.Text = this.skippedFiles;
        }

        public void SetFileList(string list)
        {
            this.txtSkippedFileList.Text = list;
        }


        public string SkippedFiles { get => skippedFiles; set => skippedFiles = value; }
    }
}
