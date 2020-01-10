namespace RDSync
{
    partial class SkippedFileList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSkippedFileList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSkippedFileList
            // 
            this.txtSkippedFileList.Location = new System.Drawing.Point(36, 30);
            this.txtSkippedFileList.Multiline = true;
            this.txtSkippedFileList.Name = "txtSkippedFileList";
            this.txtSkippedFileList.ReadOnly = true;
            this.txtSkippedFileList.Size = new System.Drawing.Size(516, 263);
            this.txtSkippedFileList.TabIndex = 0;
            // 
            // SkippedFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 325);
            this.Controls.Add(this.txtSkippedFileList);
            this.Name = "SkippedFileList";
            this.Text = "SkippedFileList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSkippedFileList;
    }
}