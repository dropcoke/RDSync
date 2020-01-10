namespace RDSync
{
    partial class ConfigDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDestinationAudio = new System.Windows.Forms.Button();
            this.txtDestinationAudio = new System.Windows.Forms.TextBox();
            this.btnDestinationVideo = new System.Windows.Forms.Button();
            this.txtDestinationVideo = new System.Windows.Forms.TextBox();
            this.btnDestinationImage = new System.Windows.Forms.Button();
            this.txtDestinationImage = new System.Windows.Forms.TextBox();
            this.lblExtensionListAudio = new System.Windows.Forms.Label();
            this.lblExtensionListVideo = new System.Windows.Forms.Label();
            this.lblVideoMedia = new System.Windows.Forms.Label();
            this.lblExtensionListImage = new System.Windows.Forms.Label();
            this.lblImageMedia = new System.Windows.Forms.Label();
            this.lblAudioMedia = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(176, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(157, 31);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(368, 257);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 31);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDestinationAudio
            // 
            this.btnDestinationAudio.Location = new System.Drawing.Point(589, 197);
            this.btnDestinationAudio.Name = "btnDestinationAudio";
            this.btnDestinationAudio.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationAudio.TabIndex = 20;
            this.btnDestinationAudio.Text = "参照 ";
            this.btnDestinationAudio.UseVisualStyleBackColor = true;
            this.btnDestinationAudio.Click += new System.EventHandler(this.btnDestinationAudio_Click);
            // 
            // txtDestinationAudio
            // 
            this.txtDestinationAudio.Location = new System.Drawing.Point(88, 199);
            this.txtDestinationAudio.Name = "txtDestinationAudio";
            this.txtDestinationAudio.Size = new System.Drawing.Size(494, 19);
            this.txtDestinationAudio.TabIndex = 19;
            // 
            // btnDestinationVideo
            // 
            this.btnDestinationVideo.Location = new System.Drawing.Point(589, 126);
            this.btnDestinationVideo.Name = "btnDestinationVideo";
            this.btnDestinationVideo.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationVideo.TabIndex = 18;
            this.btnDestinationVideo.Text = "参照 ";
            this.btnDestinationVideo.UseVisualStyleBackColor = true;
            this.btnDestinationVideo.Click += new System.EventHandler(this.btnDestinationVideo_Click);
            // 
            // txtDestinationVideo
            // 
            this.txtDestinationVideo.Location = new System.Drawing.Point(88, 128);
            this.txtDestinationVideo.Name = "txtDestinationVideo";
            this.txtDestinationVideo.Size = new System.Drawing.Size(494, 19);
            this.txtDestinationVideo.TabIndex = 17;
            // 
            // btnDestinationImage
            // 
            this.btnDestinationImage.Location = new System.Drawing.Point(589, 55);
            this.btnDestinationImage.Name = "btnDestinationImage";
            this.btnDestinationImage.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationImage.TabIndex = 16;
            this.btnDestinationImage.Text = "参照 ";
            this.btnDestinationImage.UseVisualStyleBackColor = true;
            this.btnDestinationImage.Click += new System.EventHandler(this.btnDestinationImage_Click);
            // 
            // txtDestinationImage
            // 
            this.txtDestinationImage.Location = new System.Drawing.Point(88, 57);
            this.txtDestinationImage.Name = "txtDestinationImage";
            this.txtDestinationImage.Size = new System.Drawing.Size(494, 19);
            this.txtDestinationImage.TabIndex = 15;
            // 
            // lblExtensionListAudio
            // 
            this.lblExtensionListAudio.AutoSize = true;
            this.lblExtensionListAudio.Location = new System.Drawing.Point(86, 184);
            this.lblExtensionListAudio.Name = "lblExtensionListAudio";
            this.lblExtensionListAudio.Size = new System.Drawing.Size(115, 12);
            this.lblExtensionListAudio.TabIndex = 9;
            this.lblExtensionListAudio.Text = "lblExtensionListAudio";
            // 
            // lblExtensionListVideo
            // 
            this.lblExtensionListVideo.AutoSize = true;
            this.lblExtensionListVideo.Location = new System.Drawing.Point(86, 114);
            this.lblExtensionListVideo.Name = "lblExtensionListVideo";
            this.lblExtensionListVideo.Size = new System.Drawing.Size(115, 12);
            this.lblExtensionListVideo.TabIndex = 11;
            this.lblExtensionListVideo.Text = "lblExtensionListVideo";
            // 
            // lblVideoMedia
            // 
            this.lblVideoMedia.AutoSize = true;
            this.lblVideoMedia.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblVideoMedia.Location = new System.Drawing.Point(63, 96);
            this.lblVideoMedia.Name = "lblVideoMedia";
            this.lblVideoMedia.Size = new System.Drawing.Size(98, 16);
            this.lblVideoMedia.TabIndex = 12;
            this.lblVideoMedia.Text = "lblVideoMedia";
            // 
            // lblExtensionListImage
            // 
            this.lblExtensionListImage.AutoSize = true;
            this.lblExtensionListImage.Location = new System.Drawing.Point(86, 42);
            this.lblExtensionListImage.Name = "lblExtensionListImage";
            this.lblExtensionListImage.Size = new System.Drawing.Size(116, 12);
            this.lblExtensionListImage.TabIndex = 13;
            this.lblExtensionListImage.Text = "lblExtensionListImage";
            // 
            // lblImageMedia
            // 
            this.lblImageMedia.AutoSize = true;
            this.lblImageMedia.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblImageMedia.Location = new System.Drawing.Point(63, 23);
            this.lblImageMedia.Name = "lblImageMedia";
            this.lblImageMedia.Size = new System.Drawing.Size(100, 16);
            this.lblImageMedia.TabIndex = 14;
            this.lblImageMedia.Text = "lblImageMedia";
            // 
            // lblAudioMedia
            // 
            this.lblAudioMedia.AutoSize = true;
            this.lblAudioMedia.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAudioMedia.Location = new System.Drawing.Point(63, 167);
            this.lblAudioMedia.Name = "lblAudioMedia";
            this.lblAudioMedia.Size = new System.Drawing.Size(98, 16);
            this.lblAudioMedia.TabIndex = 10;
            this.lblAudioMedia.Text = "lblAudioMedia";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 320);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDestinationAudio);
            this.Controls.Add(this.txtDestinationAudio);
            this.Controls.Add(this.btnDestinationVideo);
            this.Controls.Add(this.txtDestinationVideo);
            this.Controls.Add(this.btnDestinationImage);
            this.Controls.Add(this.txtDestinationImage);
            this.Controls.Add(this.lblExtensionListAudio);
            this.Controls.Add(this.lblAudioMedia);
            this.Controls.Add(this.lblExtensionListVideo);
            this.Controls.Add(this.lblVideoMedia);
            this.Controls.Add(this.lblExtensionListImage);
            this.Controls.Add(this.lblImageMedia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigDialog";
            this.Text = "ConfigDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDestinationAudio;
        private System.Windows.Forms.TextBox txtDestinationAudio;
        private System.Windows.Forms.Button btnDestinationVideo;
        private System.Windows.Forms.TextBox txtDestinationVideo;
        private System.Windows.Forms.Button btnDestinationImage;
        private System.Windows.Forms.TextBox txtDestinationImage;
        private System.Windows.Forms.Label lblExtensionListAudio;
        private System.Windows.Forms.Label lblExtensionListVideo;
        private System.Windows.Forms.Label lblVideoMedia;
        private System.Windows.Forms.Label lblExtensionListImage;
        private System.Windows.Forms.Label lblImageMedia;
        private System.Windows.Forms.Label lblAudioMedia;
    }
}