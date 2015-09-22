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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imegeUploadButton = new System.Windows.Forms.Button();
            this.infoLabelRed = new System.Windows.Forms.Label();
            this.infoLabelGreen = new System.Windows.Forms.Label();
            this.infoLabelBlue = new System.Windows.Forms.Label();
            this.labelRed = new System.Windows.Forms.Label();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelBlue = new System.Windows.Forms.Label();
            this.buttonGetColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureBoxPick = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonDrawChart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPick)).BeginInit();
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
            this.infoLabelRed.Location = new System.Drawing.Point(528, 55);
            this.infoLabelRed.Name = "infoLabelRed";
            this.infoLabelRed.Size = new System.Drawing.Size(30, 13);
            this.infoLabelRed.TabIndex = 2;
            this.infoLabelRed.Text = "Red:";
            // 
            // infoLabelGreen
            // 
            this.infoLabelGreen.AutoSize = true;
            this.infoLabelGreen.Location = new System.Drawing.Point(528, 68);
            this.infoLabelGreen.Name = "infoLabelGreen";
            this.infoLabelGreen.Size = new System.Drawing.Size(39, 13);
            this.infoLabelGreen.TabIndex = 3;
            this.infoLabelGreen.Text = "Green:";
            // 
            // infoLabelBlue
            // 
            this.infoLabelBlue.AutoSize = true;
            this.infoLabelBlue.Location = new System.Drawing.Point(528, 81);
            this.infoLabelBlue.Name = "infoLabelBlue";
            this.infoLabelBlue.Size = new System.Drawing.Size(31, 13);
            this.infoLabelBlue.TabIndex = 4;
            this.infoLabelBlue.Text = "Blue:";
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Location = new System.Drawing.Point(575, 55);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(13, 13);
            this.labelRed.TabIndex = 5;
            this.labelRed.Text = "0";
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Location = new System.Drawing.Point(575, 68);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(13, 13);
            this.labelGreen.TabIndex = 6;
            this.labelGreen.Text = "0";
            // 
            // labelBlue
            // 
            this.labelBlue.AutoSize = true;
            this.labelBlue.Location = new System.Drawing.Point(575, 81);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(13, 13);
            this.labelBlue.TabIndex = 7;
            this.labelBlue.Text = "0";
            // 
            // buttonGetColor
            // 
            this.buttonGetColor.Location = new System.Drawing.Point(531, 414);
            this.buttonGetColor.Name = "buttonGetColor";
            this.buttonGetColor.Size = new System.Drawing.Size(102, 23);
            this.buttonGetColor.TabIndex = 8;
            this.buttonGetColor.Text = "Get color";
            this.buttonGetColor.UseVisualStyleBackColor = true;
            this.buttonGetColor.Click += new System.EventHandler(this.buttonGetColor_Click);
            // 
            // pictureBoxPick
            // 
            this.pictureBoxPick.Location = new System.Drawing.Point(531, 110);
            this.pictureBoxPick.Name = "pictureBoxPick";
            this.pictureBoxPick.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxPick.TabIndex = 9;
            this.pictureBoxPick.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(639, 444);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save image";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // zedGraph
            // 
            this.zedGraph.Location = new System.Drawing.Point(597, 12);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(369, 311);
            this.zedGraph.TabIndex = 11;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(597, 329);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // buttonDrawChart
            // 
            this.buttonDrawChart.Location = new System.Drawing.Point(725, 329);
            this.buttonDrawChart.Name = "buttonDrawChart";
            this.buttonDrawChart.Size = new System.Drawing.Size(75, 23);
            this.buttonDrawChart.TabIndex = 13;
            this.buttonDrawChart.Text = "Draw";
            this.buttonDrawChart.UseVisualStyleBackColor = true;
            this.buttonDrawChart.Click += new System.EventHandler(this.buttonDrawChart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 479);
            this.Controls.Add(this.buttonDrawChart);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.zedGraph);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.pictureBoxPick);
            this.Controls.Add(this.buttonGetColor);
            this.Controls.Add(this.labelBlue);
            this.Controls.Add(this.labelGreen);
            this.Controls.Add(this.labelRed);
            this.Controls.Add(this.infoLabelBlue);
            this.Controls.Add(this.infoLabelGreen);
            this.Controls.Add(this.infoLabelRed);
            this.Controls.Add(this.imegeUploadButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Pouring Picture project";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPick)).EndInit();
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
        private System.Windows.Forms.Button buttonGetColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pictureBoxPick;
        private System.Windows.Forms.Button buttonSave;
        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonDrawChart;
    }
}

