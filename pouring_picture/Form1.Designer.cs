namespace pouring_picture
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imegeUploadButton = new System.Windows.Forms.Button();
            this.infoLabelRed = new System.Windows.Forms.Label();
            this.infoLabelGreen = new System.Windows.Forms.Label();
            this.infoLabelBlue = new System.Windows.Forms.Label();
            this.labelRed = new System.Windows.Forms.Label();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelBlue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(513, 455);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // imegeUploadButton
            // 
            this.imegeUploadButton.Location = new System.Drawing.Point(531, 443);
            this.imegeUploadButton.Name = "imegeUploadButton";
            this.imegeUploadButton.Size = new System.Drawing.Size(102, 23);
            this.imegeUploadButton.TabIndex = 1;
            this.imegeUploadButton.Text = "Upload image";
            this.imegeUploadButton.UseVisualStyleBackColor = true;
            this.imegeUploadButton.Click += new System.EventHandler(this.imegeUploadButton_Click);
            // 
            // infoLabelRed
            // 
            this.infoLabelRed.AutoSize = true;
            this.infoLabelRed.Location = new System.Drawing.Point(531, 59);
            this.infoLabelRed.Name = "infoLabelRed";
            this.infoLabelRed.Size = new System.Drawing.Size(30, 13);
            this.infoLabelRed.TabIndex = 2;
            this.infoLabelRed.Text = "Red:";
            // 
            // infoLabelGreen
            // 
            this.infoLabelGreen.AutoSize = true;
            this.infoLabelGreen.Location = new System.Drawing.Point(531, 72);
            this.infoLabelGreen.Name = "infoLabelGreen";
            this.infoLabelGreen.Size = new System.Drawing.Size(40, 13);
            this.infoLabelGreen.TabIndex = 3;
            this.infoLabelGreen.Text = "Green:";
            // 
            // infoLabelBlue
            // 
            this.infoLabelBlue.AutoSize = true;
            this.infoLabelBlue.Location = new System.Drawing.Point(531, 85);
            this.infoLabelBlue.Name = "infoLabelBlue";
            this.infoLabelBlue.Size = new System.Drawing.Size(31, 13);
            this.infoLabelBlue.TabIndex = 4;
            this.infoLabelBlue.Text = "Blue:";
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Location = new System.Drawing.Point(578, 59);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(0, 13);
            this.labelRed.TabIndex = 5;
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Location = new System.Drawing.Point(578, 72);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(0, 13);
            this.labelGreen.TabIndex = 6;
            // 
            // labelBlue
            // 
            this.labelBlue.AutoSize = true;
            this.labelBlue.Location = new System.Drawing.Point(578, 85);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(0, 13);
            this.labelBlue.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 479);
            this.Controls.Add(this.labelBlue);
            this.Controls.Add(this.labelGreen);
            this.Controls.Add(this.labelRed);
            this.Controls.Add(this.infoLabelBlue);
            this.Controls.Add(this.infoLabelGreen);
            this.Controls.Add(this.infoLabelRed);
            this.Controls.Add(this.imegeUploadButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button imegeUploadButton;
        private System.Windows.Forms.Label infoLabelRed;
        private System.Windows.Forms.Label infoLabelGreen;
        private System.Windows.Forms.Label infoLabelBlue;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelBlue;
    }
}

