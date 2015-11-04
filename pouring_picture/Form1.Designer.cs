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
            this.buttonDrawChart = new System.Windows.Forms.Button();
            this.zedGraph1 = new ZedGraph.ZedGraphControl();
            this.zedGraph2 = new ZedGraph.ZedGraphControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelRangeSliderMin1 = new System.Windows.Forms.Label();
            this.labelRangeSliderMax1 = new System.Windows.Forms.Label();
            this.labelRangeSliderMax = new System.Windows.Forms.Label();
            this.labelRangeSliderMin = new System.Windows.Forms.Label();
            this.labelRangeSliderMax2 = new System.Windows.Forms.Label();
            this.labelRangeSliderMin2 = new System.Windows.Forms.Label();
            this.selectionRangeSlider2 = new pouring_picture.SelectionRangeSlider();
            this.selectionRangeSlider = new pouring_picture.SelectionRangeSlider();
            this.selectionRangeSlider1 = new pouring_picture.SelectionRangeSlider();
            this.textBoxMarkerWidth = new System.Windows.Forms.TextBox();
            this.textBoxMarkerHeight = new System.Windows.Forms.TextBox();
            this.labelMarkerInfo = new System.Windows.Forms.Label();
            this.buttonLoadBackup = new System.Windows.Forms.Button();
            this.textBoxMagic = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelSensInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPick)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(531, 627);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // imegeUploadButton
            // 
            this.imegeUploadButton.Location = new System.Drawing.Point(1036, 587);
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
            this.infoLabelRed.Location = new System.Drawing.Point(1227, 532);
            this.infoLabelRed.Name = "infoLabelRed";
            this.infoLabelRed.Size = new System.Drawing.Size(30, 13);
            this.infoLabelRed.TabIndex = 2;
            this.infoLabelRed.Text = "Red:";
            // 
            // infoLabelGreen
            // 
            this.infoLabelGreen.AutoSize = true;
            this.infoLabelGreen.Location = new System.Drawing.Point(1227, 545);
            this.infoLabelGreen.Name = "infoLabelGreen";
            this.infoLabelGreen.Size = new System.Drawing.Size(39, 13);
            this.infoLabelGreen.TabIndex = 3;
            this.infoLabelGreen.Text = "Green:";
            // 
            // infoLabelBlue
            // 
            this.infoLabelBlue.AutoSize = true;
            this.infoLabelBlue.Location = new System.Drawing.Point(1227, 558);
            this.infoLabelBlue.Name = "infoLabelBlue";
            this.infoLabelBlue.Size = new System.Drawing.Size(31, 13);
            this.infoLabelBlue.TabIndex = 4;
            this.infoLabelBlue.Text = "Blue:";
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Location = new System.Drawing.Point(1274, 532);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(13, 13);
            this.labelRed.TabIndex = 5;
            this.labelRed.Text = "0";
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Location = new System.Drawing.Point(1274, 545);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(13, 13);
            this.labelGreen.TabIndex = 6;
            this.labelGreen.Text = "0";
            // 
            // labelBlue
            // 
            this.labelBlue.AutoSize = true;
            this.labelBlue.Location = new System.Drawing.Point(1274, 558);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(13, 13);
            this.labelBlue.TabIndex = 7;
            this.labelBlue.Text = "0";
            // 
            // buttonGetColor
            // 
            this.buttonGetColor.Location = new System.Drawing.Point(1036, 558);
            this.buttonGetColor.Name = "buttonGetColor";
            this.buttonGetColor.Size = new System.Drawing.Size(102, 23);
            this.buttonGetColor.TabIndex = 8;
            this.buttonGetColor.Text = "Get color";
            this.buttonGetColor.UseVisualStyleBackColor = true;
            this.buttonGetColor.Click += new System.EventHandler(this.buttonGetColor_Click);
            // 
            // pictureBoxPick
            // 
            this.pictureBoxPick.Location = new System.Drawing.Point(1230, 587);
            this.pictureBoxPick.Name = "pictureBoxPick";
            this.pictureBoxPick.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxPick.TabIndex = 9;
            this.pictureBoxPick.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(1144, 588);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(77, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save image";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // zedGraph
            // 
            this.zedGraph.Location = new System.Drawing.Point(549, 12);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(401, 282);
            this.zedGraph.TabIndex = 11;
            // 
            // buttonDrawChart
            // 
            this.buttonDrawChart.Location = new System.Drawing.Point(1144, 558);
            this.buttonDrawChart.Name = "buttonDrawChart";
            this.buttonDrawChart.Size = new System.Drawing.Size(77, 23);
            this.buttonDrawChart.TabIndex = 13;
            this.buttonDrawChart.Text = "Draw";
            this.buttonDrawChart.UseVisualStyleBackColor = true;
            this.buttonDrawChart.Click += new System.EventHandler(this.buttonDrawChart_Click);
            // 
            // zedGraph1
            // 
            this.zedGraph1.Location = new System.Drawing.Point(956, 12);
            this.zedGraph1.Name = "zedGraph1";
            this.zedGraph1.ScrollGrace = 0D;
            this.zedGraph1.ScrollMaxX = 0D;
            this.zedGraph1.ScrollMaxY = 0D;
            this.zedGraph1.ScrollMaxY2 = 0D;
            this.zedGraph1.ScrollMinX = 0D;
            this.zedGraph1.ScrollMinY = 0D;
            this.zedGraph1.ScrollMinY2 = 0D;
            this.zedGraph1.Size = new System.Drawing.Size(401, 282);
            this.zedGraph1.TabIndex = 14;
            // 
            // zedGraph2
            // 
            this.zedGraph2.Location = new System.Drawing.Point(549, 357);
            this.zedGraph2.Name = "zedGraph2";
            this.zedGraph2.ScrollGrace = 0D;
            this.zedGraph2.ScrollMaxX = 0D;
            this.zedGraph2.ScrollMaxY = 0D;
            this.zedGraph2.ScrollMaxY2 = 0D;
            this.zedGraph2.ScrollMinX = 0D;
            this.zedGraph2.ScrollMinY = 0D;
            this.zedGraph2.ScrollMinY2 = 0D;
            this.zedGraph2.Size = new System.Drawing.Size(401, 282);
            this.zedGraph2.TabIndex = 15;
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Items.AddRange(new object[] {
            "RGB",
            "LAB",
            "HSV"});
            this.comboBox1.Location = new System.Drawing.Point(1036, 532);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(102, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.Text = "RGB";
            // 
            // labelRangeSliderMin1
            // 
            this.labelRangeSliderMin1.AutoSize = true;
            this.labelRangeSliderMin1.Location = new System.Drawing.Point(1000, 317);
            this.labelRangeSliderMin1.Name = "labelRangeSliderMin1";
            this.labelRangeSliderMin1.Size = new System.Drawing.Size(23, 13);
            this.labelRangeSliderMin1.TabIndex = 19;
            this.labelRangeSliderMin1.Text = "min";
            // 
            // labelRangeSliderMax1
            // 
            this.labelRangeSliderMax1.AutoSize = true;
            this.labelRangeSliderMax1.Location = new System.Drawing.Point(1305, 317);
            this.labelRangeSliderMax1.Name = "labelRangeSliderMax1";
            this.labelRangeSliderMax1.Size = new System.Drawing.Size(26, 13);
            this.labelRangeSliderMax1.TabIndex = 20;
            this.labelRangeSliderMax1.Text = "max";
            // 
            // labelRangeSliderMax
            // 
            this.labelRangeSliderMax.AutoSize = true;
            this.labelRangeSliderMax.Location = new System.Drawing.Point(900, 316);
            this.labelRangeSliderMax.Name = "labelRangeSliderMax";
            this.labelRangeSliderMax.Size = new System.Drawing.Size(26, 13);
            this.labelRangeSliderMax.TabIndex = 24;
            this.labelRangeSliderMax.Text = "max";
            // 
            // labelRangeSliderMin
            // 
            this.labelRangeSliderMin.AutoSize = true;
            this.labelRangeSliderMin.Location = new System.Drawing.Point(595, 316);
            this.labelRangeSliderMin.Name = "labelRangeSliderMin";
            this.labelRangeSliderMin.Size = new System.Drawing.Size(23, 13);
            this.labelRangeSliderMin.TabIndex = 23;
            this.labelRangeSliderMin.Text = "min";
            // 
            // labelRangeSliderMax2
            // 
            this.labelRangeSliderMax2.AutoSize = true;
            this.labelRangeSliderMax2.Location = new System.Drawing.Point(900, 660);
            this.labelRangeSliderMax2.Name = "labelRangeSliderMax2";
            this.labelRangeSliderMax2.Size = new System.Drawing.Size(26, 13);
            this.labelRangeSliderMax2.TabIndex = 28;
            this.labelRangeSliderMax2.Text = "max";
            // 
            // labelRangeSliderMin2
            // 
            this.labelRangeSliderMin2.AutoSize = true;
            this.labelRangeSliderMin2.Location = new System.Drawing.Point(595, 660);
            this.labelRangeSliderMin2.Name = "labelRangeSliderMin2";
            this.labelRangeSliderMin2.Size = new System.Drawing.Size(23, 13);
            this.labelRangeSliderMin2.TabIndex = 27;
            this.labelRangeSliderMin2.Text = "min";
            // 
            // selectionRangeSlider2
            // 
            this.selectionRangeSlider2.Location = new System.Drawing.Point(595, 643);
            this.selectionRangeSlider2.Max = 255;
            this.selectionRangeSlider2.Min = 0;
            this.selectionRangeSlider2.Name = "selectionRangeSlider2";
            this.selectionRangeSlider2.SelectedMax = 255;
            this.selectionRangeSlider2.SelectedMin = 0;
            this.selectionRangeSlider2.Size = new System.Drawing.Size(340, 10);
            this.selectionRangeSlider2.TabIndex = 26;
            // 
            // selectionRangeSlider
            // 
            this.selectionRangeSlider.Location = new System.Drawing.Point(595, 299);
            this.selectionRangeSlider.Max = 255;
            this.selectionRangeSlider.Min = 0;
            this.selectionRangeSlider.Name = "selectionRangeSlider";
            this.selectionRangeSlider.SelectedMax = 255;
            this.selectionRangeSlider.SelectedMin = 0;
            this.selectionRangeSlider.Size = new System.Drawing.Size(340, 10);
            this.selectionRangeSlider.TabIndex = 22;
            // 
            // selectionRangeSlider1
            // 
            this.selectionRangeSlider1.Location = new System.Drawing.Point(1003, 300);
            this.selectionRangeSlider1.Max = 255;
            this.selectionRangeSlider1.Min = 0;
            this.selectionRangeSlider1.Name = "selectionRangeSlider1";
            this.selectionRangeSlider1.SelectedMax = 255;
            this.selectionRangeSlider1.SelectedMin = 0;
            this.selectionRangeSlider1.Size = new System.Drawing.Size(337, 10);
            this.selectionRangeSlider1.TabIndex = 18;
            // 
            // textBoxMarkerWidth
            // 
            this.textBoxMarkerWidth.Location = new System.Drawing.Point(1036, 450);
            this.textBoxMarkerWidth.Name = "textBoxMarkerWidth";
            this.textBoxMarkerWidth.Size = new System.Drawing.Size(48, 20);
            this.textBoxMarkerWidth.TabIndex = 31;
            this.textBoxMarkerWidth.Text = "10";
            // 
            // textBoxMarkerHeight
            // 
            this.textBoxMarkerHeight.Location = new System.Drawing.Point(1090, 450);
            this.textBoxMarkerHeight.Name = "textBoxMarkerHeight";
            this.textBoxMarkerHeight.Size = new System.Drawing.Size(48, 20);
            this.textBoxMarkerHeight.TabIndex = 32;
            this.textBoxMarkerHeight.Text = "10";
            // 
            // labelMarkerInfo
            // 
            this.labelMarkerInfo.AutoSize = true;
            this.labelMarkerInfo.Location = new System.Drawing.Point(1036, 431);
            this.labelMarkerInfo.Name = "labelMarkerInfo";
            this.labelMarkerInfo.Size = new System.Drawing.Size(64, 13);
            this.labelMarkerInfo.TabIndex = 33;
            this.labelMarkerInfo.Text = "Marker size:";
            // 
            // buttonLoadBackup
            // 
            this.buttonLoadBackup.Location = new System.Drawing.Point(198, 660);
            this.buttonLoadBackup.Name = "buttonLoadBackup";
            this.buttonLoadBackup.Size = new System.Drawing.Size(104, 23);
            this.buttonLoadBackup.TabIndex = 35;
            this.buttonLoadBackup.Text = "Load backup";
            this.buttonLoadBackup.UseVisualStyleBackColor = true;
            this.buttonLoadBackup.Click += new System.EventHandler(this.buttonLoadBackup_Click);
            // 
            // textBoxMagic
            // 
            this.textBoxMagic.Location = new System.Drawing.Point(1167, 450);
            this.textBoxMagic.Name = "textBoxMagic";
            this.textBoxMagic.Size = new System.Drawing.Size(100, 20);
            this.textBoxMagic.TabIndex = 36;
            this.textBoxMagic.Text = "2";
            this.textBoxMagic.TextChanged += new System.EventHandler(this.textBoxMagic_TextChanged);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(117, 660);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 37;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // labelSensInfo
            // 
            this.labelSensInfo.AutoSize = true;
            this.labelSensInfo.Location = new System.Drawing.Point(1164, 431);
            this.labelSensInfo.Name = "labelSensInfo";
            this.labelSensInfo.Size = new System.Drawing.Size(31, 13);
            this.labelSensInfo.TabIndex = 38;
            this.labelSensInfo.Text = "Sens";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 693);
            this.Controls.Add(this.labelSensInfo);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.textBoxMagic);
            this.Controls.Add(this.buttonLoadBackup);
            this.Controls.Add(this.labelMarkerInfo);
            this.Controls.Add(this.textBoxMarkerHeight);
            this.Controls.Add(this.textBoxMarkerWidth);
            this.Controls.Add(this.labelRangeSliderMax2);
            this.Controls.Add(this.labelRangeSliderMin2);
            this.Controls.Add(this.selectionRangeSlider2);
            this.Controls.Add(this.labelRangeSliderMax);
            this.Controls.Add(this.labelRangeSliderMin);
            this.Controls.Add(this.selectionRangeSlider);
            this.Controls.Add(this.labelRangeSliderMax1);
            this.Controls.Add(this.labelRangeSliderMin1);
            this.Controls.Add(this.selectionRangeSlider1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.zedGraph2);
            this.Controls.Add(this.zedGraph1);
            this.Controls.Add(this.buttonDrawChart);
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
            this.Name = "Form1";
            this.Text = "Pouring Picture project";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.Button buttonDrawChart;
        private ZedGraph.ZedGraphControl zedGraph1;
        private ZedGraph.ZedGraphControl zedGraph2;
        private System.Windows.Forms.ComboBox comboBox1;
        private SelectionRangeSlider selectionRangeSlider1;
        private System.Windows.Forms.Label labelRangeSliderMin1;
        private System.Windows.Forms.Label labelRangeSliderMax1;
        private System.Windows.Forms.Label labelRangeSliderMax;
        private System.Windows.Forms.Label labelRangeSliderMin;
        private SelectionRangeSlider selectionRangeSlider;
        private System.Windows.Forms.Label labelRangeSliderMax2;
        private System.Windows.Forms.Label labelRangeSliderMin2;
        private SelectionRangeSlider selectionRangeSlider2;
        private System.Windows.Forms.TextBox textBoxMarkerWidth;
        private System.Windows.Forms.TextBox textBoxMarkerHeight;
        private System.Windows.Forms.Label labelMarkerInfo;
        private System.Windows.Forms.Button buttonLoadBackup;
        private System.Windows.Forms.TextBox textBoxMagic;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelSensInfo;
    }
}

