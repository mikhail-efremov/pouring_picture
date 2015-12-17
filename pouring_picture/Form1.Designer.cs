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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureBoxPick = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.buttonDrawChart = new System.Windows.Forms.Button();
            this.zedGraph1 = new ZedGraph.ZedGraphControl();
            this.zedGraph2 = new ZedGraph.ZedGraphControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxMarkerWidth = new System.Windows.Forms.TextBox();
            this.textBoxMarkerHeight = new System.Windows.Forms.TextBox();
            this.labelMarkerInfo = new System.Windows.Forms.Label();
            this.buttonLoadBackup = new System.Windows.Forms.Button();
            this.textBoxSensivity = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelSensInfo = new System.Windows.Forms.Label();
            this.buttonAddRange = new System.Windows.Forms.Button();
            this.buttonAddRange1 = new System.Windows.Forms.Button();
            this.buttonAddRange2 = new System.Windows.Forms.Button();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.selectionRangeSlider2 = new pouring_picture.SelectionRangeSlider();
            this.selectionRangeSlider = new pouring_picture.SelectionRangeSlider();
            this.selectionRangeSlider1 = new pouring_picture.SelectionRangeSlider();
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
            // pictureBoxPick
            // 
            this.pictureBoxPick.Location = new System.Drawing.Point(1117, 379);
            this.pictureBoxPick.Name = "pictureBoxPick";
            this.pictureBoxPick.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxPick.TabIndex = 9;
            this.pictureBoxPick.TabStop = false;
            this.pictureBoxPick.Click += new System.EventHandler(this.pictureBoxPick_Click);
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
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Items.AddRange(new object[] {
            "RGB",
            "LAB",
            "HSV"});
            this.comboBox1.Location = new System.Drawing.Point(1036, 532);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(102, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
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
            // textBoxSensivity
            // 
            this.textBoxSensivity.Location = new System.Drawing.Point(1167, 450);
            this.textBoxSensivity.Name = "textBoxSensivity";
            this.textBoxSensivity.Size = new System.Drawing.Size(100, 20);
            this.textBoxSensivity.TabIndex = 36;
            this.textBoxSensivity.Text = "2";
            this.textBoxSensivity.TextChanged += new System.EventHandler(this.textBoxMagic_TextChanged);
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
            // buttonAddRange
            // 
            this.buttonAddRange.Location = new System.Drawing.Point(683, 316);
            this.buttonAddRange.Name = "buttonAddRange";
            this.buttonAddRange.Size = new System.Drawing.Size(75, 23);
            this.buttonAddRange.TabIndex = 39;
            this.buttonAddRange.Text = "Add range";
            this.buttonAddRange.UseVisualStyleBackColor = true;
            this.buttonAddRange.Click += new System.EventHandler(this.buttonAddRange_Click);
            // 
            // buttonAddRange1
            // 
            this.buttonAddRange1.Location = new System.Drawing.Point(1090, 317);
            this.buttonAddRange1.Name = "buttonAddRange1";
            this.buttonAddRange1.Size = new System.Drawing.Size(75, 23);
            this.buttonAddRange1.TabIndex = 41;
            this.buttonAddRange1.Text = "Add range";
            this.buttonAddRange1.UseVisualStyleBackColor = true;
            this.buttonAddRange1.Click += new System.EventHandler(this.buttonAddRange1_Click);
            // 
            // buttonAddRange2
            // 
            this.buttonAddRange2.Location = new System.Drawing.Point(683, 660);
            this.buttonAddRange2.Name = "buttonAddRange2";
            this.buttonAddRange2.Size = new System.Drawing.Size(75, 23);
            this.buttonAddRange2.TabIndex = 42;
            this.buttonAddRange2.Text = "Add range";
            this.buttonAddRange2.UseVisualStyleBackColor = true;
            this.buttonAddRange2.Click += new System.EventHandler(this.buttonAddRange2_Click);
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(1036, 379);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(75, 23);
            this.buttonDraw.TabIndex = 43;
            this.buttonDraw.Text = "Cut graph";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // selectionRangeSlider2
            // 
            this.selectionRangeSlider2.Location = new System.Drawing.Point(595, 643);
            this.selectionRangeSlider2.Name = "selectionRangeSlider2";
            this.selectionRangeSlider2.Size = new System.Drawing.Size(340, 10);
            this.selectionRangeSlider2.TabIndex = 26;
            // 
            // selectionRangeSlider
            // 
            this.selectionRangeSlider.Location = new System.Drawing.Point(595, 299);
            this.selectionRangeSlider.Name = "selectionRangeSlider";
            this.selectionRangeSlider.Size = new System.Drawing.Size(340, 10);
            this.selectionRangeSlider.TabIndex = 22;
            // 
            // selectionRangeSlider1
            // 
            this.selectionRangeSlider1.Location = new System.Drawing.Point(1003, 300);
            this.selectionRangeSlider1.Name = "selectionRangeSlider1";
            this.selectionRangeSlider1.Size = new System.Drawing.Size(337, 10);
            this.selectionRangeSlider1.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 693);
            this.Controls.Add(this.buttonDraw);
            this.Controls.Add(this.buttonAddRange2);
            this.Controls.Add(this.buttonAddRange1);
            this.Controls.Add(this.buttonAddRange);
            this.Controls.Add(this.labelSensInfo);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.textBoxSensivity);
            this.Controls.Add(this.buttonLoadBackup);
            this.Controls.Add(this.labelMarkerInfo);
            this.Controls.Add(this.textBoxMarkerHeight);
            this.Controls.Add(this.textBoxMarkerWidth);
            this.Controls.Add(this.selectionRangeSlider2);
            this.Controls.Add(this.selectionRangeSlider);
            this.Controls.Add(this.selectionRangeSlider1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.zedGraph2);
            this.Controls.Add(this.zedGraph1);
            this.Controls.Add(this.buttonDrawChart);
            this.Controls.Add(this.zedGraph);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.pictureBoxPick);
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
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pictureBoxPick;
        private System.Windows.Forms.Button buttonSave;
        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.Button buttonDrawChart;
        private ZedGraph.ZedGraphControl zedGraph1;
        private ZedGraph.ZedGraphControl zedGraph2;
        private System.Windows.Forms.ComboBox comboBox1;
        private SelectionRangeSlider selectionRangeSlider1;
        private SelectionRangeSlider selectionRangeSlider;
        private SelectionRangeSlider selectionRangeSlider2;
        private System.Windows.Forms.TextBox textBoxMarkerWidth;
        private System.Windows.Forms.TextBox textBoxMarkerHeight;
        private System.Windows.Forms.Label labelMarkerInfo;
        private System.Windows.Forms.Button buttonLoadBackup;
        private System.Windows.Forms.TextBox textBoxSensivity;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelSensInfo;
        private System.Windows.Forms.Button buttonAddRange;
        private System.Windows.Forms.Button buttonAddRange1;
        private System.Windows.Forms.Button buttonAddRange2;
        private System.Windows.Forms.Button buttonDraw;
    }
}

