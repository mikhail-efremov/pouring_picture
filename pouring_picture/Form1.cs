using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using ZedGraph;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using ColorMine.ColorSpaces;

namespace pouring_picture
{
    public partial class Form1 : Form
    {
        private int maxHeight = 510;
        private int maxWidth = 455;
        List<PixelData> chartColors;

        public Form1()
        {
            InitializeComponent();
            chartColors = new List<PixelData>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillColorPickRegion();
            PrepareGraph();
            selectionRangeSlider1.SelectionChanged += selectionRangeSlider1_SelectionChanged;
        }

        private void selectionRangeSlider1_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin.Text = Convert.ToString(selectionRangeSlider1.SelectedMin);
            labelRangeSliderMax.Text = Convert.ToString(selectionRangeSlider1.SelectedMax);
        }

        private void imegeUploadButton_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ImageClick(sender, e);
        }

        private void buttonGetColor_Click(object sender, EventArgs e)
        {
            GetColor();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void buttonDrawChart_Click(object sender, EventArgs e)
        {
            DrawGraph();
#warning            chartColors.Clear();
        }

        private void FillColorPickRegion()
        {
            Bitmap flag = new Bitmap(30, 30);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int iterator = 0;
            while (iterator <= 30)
            {
                var myBrush = new SolidBrush(colorDialog1.Color);
                flagGraphics.FillRectangle(myBrush, 0, iterator, 30, 30);
                iterator++;
            }
            pictureBoxPick.Image = flag;
        }

        private bool UploadImage()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var image = new Bitmap(open.FileName);

                if (VerifyImage(image))
                {
                    pictureBox1.Image = image;
                    return true;
                }
            }
            return false;
        }

        private void ImageClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            Rectangle myRectangle = new Rectangle();

            myRectangle.Location = new Point(coordinates.X, coordinates.Y);

            myRectangle.Size = new Size(20, 10);

            var points = new List<Point>();

            var bmp = new Bitmap(pictureBox1.Image);

            for (int i = 0; i < bmp.Size.Height; i++)
                for (int j = 0; j < bmp.Size.Width; j++)
                {
                    if (myRectangle.Contains(new Point(j, i)))
                    {
                        points.Add(new Point(j, i));
                    }
                }

            PouringImage(points);
        }

        private void GetColor()
        {
            colorDialog1.ShowDialog();
            var color = colorDialog1;

            Bitmap flag = new Bitmap(30, 30);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int iterator = 0;
            while (iterator <= 30)
            {
                var myBrush = new SolidBrush(colorDialog1.Color);
                flagGraphics.FillRectangle(myBrush, 0, iterator, 30, 30);
                iterator++;
            }
            pictureBoxPick.Image = flag;

            labelRed.Text = color.Color.R.ToString();
            labelGreen.Text = color.Color.G.ToString();
            labelBlue.Text = color.Color.B.ToString();
        }

        private void SaveImage()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }

                    fs.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in SaveImage()",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private unsafe void PouringImage(List<Point> points)
        {
            try
            {
#warning        chartColors.Clear();

                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);
                
                var setColor = Color.FromArgb(red, green, blue);
                var bmp = new Bitmap(pictureBox1.Image);

                UnsafeBitmap uBitMap = new UnsafeBitmap(bmp);
                
                uBitMap.LockBitmap();
                for (int i = 0; i < bmp.Size.Height; i++)
                {
                    for (int j = 0; j < bmp.Size.Width; j++)
                    {
                        foreach (var point in points)
                        {
                            var color = uBitMap.PixelAt(point.X, point.Y);
                            
                            var _color = uBitMap.PixelAt(j, i);
                            
                            if (color->red == _color->red
                                && color->green == _color->green
                                && color->blue == _color->blue)
                            {
                                chartColors.Add(new PixelData(color->blue, color->green, color->red));
                                uBitMap.SetPixel(j, i, new PixelData(setColor.B, setColor.G, setColor.R));
                            }
                        }
                    }
                }
                pictureBox1.Image = uBitMap.Bitmap;
                uBitMap.UnlockBitmap();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in PouringImage()",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VerifyImage(Bitmap image)
        {
            var result = image.Size.Height < maxHeight && image.Size.Width < maxWidth;
            if (!result)
            MessageBox.Show("Size of your image have to be smaller then " + maxHeight + "/" + maxWidth, "Huge size error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        private void PrepareGraph()
        {
            zedGraph.IsEnableZoom = false;
            zedGraph.IsEnableVPan = false;
            zedGraph.IsEnableHPan = false;

            zedGraph1.IsEnableZoom = false;
            zedGraph1.IsEnableVPan = false;
            zedGraph1.IsEnableHPan = false;

            zedGraph2.IsEnableZoom = false;
            zedGraph2.IsEnableVPan = false;
            zedGraph2.IsEnableHPan = false;
        }

        private unsafe void DrawGraph()
        {
            const int TINT_COUNT = 255;

            var color = Color.Red;

            zedGraph.Refresh();
            zedGraph.GraphPane.CurveList.Clear();
            zedGraph.GraphPane.GraphObjList.Clear();
            zedGraph.ZoomStepFraction = 255;

            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();

            double[] YValues = new double[TINT_COUNT];
            double[] XValues = new double[TINT_COUNT];

            if (comboBox1.Text == "RGB")
            {
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);

                foreach (var pixel in chartColors)
                {
                    for (int i = 0; i < TINT_COUNT; i++)
                    {
                        XValues[i] = i + 1;

                        if (pixel.red == i && pixel.blue != blue && pixel.green != green)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar(color.ToString(), XValues, YValues, color);
                bar.Bar.Border.Color = color;
            }
            else
            { 
                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);

                Color _color = Color.FromArgb(red, green, blue);
                var lab = RGBtoLAB(_color);

                var labList = new List<Lab>();

                foreach (var c in chartColors)
                {
                    var __color = Color.FromArgb(c.red, c.blue, c.green);
                    var __lab = RGBtoLAB(__color);
                    labList.Add(__lab);
                }

                foreach (var l in labList)
                {
                    for (int i = 0; i < 100; i++) //0 to 100 - is L range
                    {
                        XValues[i] = i + 1;

                        if ((int)l.L == i && (int)lab.A != (int)l.A && (int)lab.B != (int)l.B)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar("L", XValues, YValues, color);
                bar.Bar.Border.Color = color;
            }
            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            zedGraph.AxisChange();
            zedGraph.Invalidate();
            DrawGraph1();
            DrawGraph2();
        }

        private unsafe void DrawGraph1()
        {
            const int TINT_COUNT = 255;

            var color = Color.Green;

            zedGraph1.Refresh();
            zedGraph1.GraphPane.CurveList.Clear();
            zedGraph1.GraphPane.GraphObjList.Clear();

            GraphPane pane = zedGraph1.GraphPane;
            pane.CurveList.Clear();

            if (comboBox1.Text == "RGB")
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

                int blue = Convert.ToInt32(labelBlue.Text);
                int red = Convert.ToInt32(labelRed.Text);

                foreach (var pixel in chartColors)
                {
                    for (int i = 0; i < TINT_COUNT; i++)
                    {
                        XValues[i] = i + 1;

                        if (pixel.green == i && pixel.red != red && pixel.blue != blue)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar(color.ToString(), XValues, YValues, color);
                bar.Bar.Border.Color = color;
            }
            else
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);

                Color _color = Color.FromArgb(red, green, blue);
                var lab = RGBtoLAB(_color);

                var labList = new List<Lab>();

                foreach (var c in chartColors)
                {
                    var __color = Color.FromArgb(c.red, c.blue, c.green);
                    var __lab = RGBtoLAB(__color);
                    labList.Add(__lab);
                }

                foreach (var l in labList)
                {
                    int j = -128;
                    for (int i = 0; i < TINT_COUNT; i++) //-128 to 128 - is A range
                    {
                        XValues[i] = i + 1;

                        if((int)l.A == j && (int)lab.L != (int)l.L && (int)lab.B != (int)l.B)
                            YValues[i]++;
                        j++;
                    }
                }

                BarItem bar = pane.AddBar("A", XValues, YValues, color);
                bar.Bar.Border.Color = color;
            }

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            zedGraph1.AxisChange();
            zedGraph1.Invalidate();
        }

        private unsafe void DrawGraph2()
        {
            const int TINT_COUNT = 255;

            var color = Color.Blue;

            zedGraph2.Refresh();
            zedGraph2.GraphPane.CurveList.Clear();
            zedGraph2.GraphPane.GraphObjList.Clear();
            zedGraph2.ZoomStepFraction = 255;

            GraphPane pane = zedGraph2.GraphPane;
            pane.CurveList.Clear();

            if (comboBox1.Text == "RGB")
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

                int green = Convert.ToInt32(labelGreen.Text);
                int red = Convert.ToInt32(labelRed.Text);

                foreach (var pixel in chartColors)
                {
                    for (int i = 0; i < TINT_COUNT; i++)
                    {
                        XValues[i] = i + 1;

                        if (pixel.blue == i + 128 && pixel.red != red && pixel.green != green)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar(color.ToString(), XValues, YValues, color);
                bar.Bar.Border.Color = color;
            }
            else
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);

                Color _color = Color.FromArgb(red, green, blue);
                var lab = RGBtoLAB(_color);

                var labList = new List<Lab>();

                foreach (var c in chartColors)
                {
                    var __color = Color.FromArgb(c.red, c.blue, c.green);
                    var __lab = RGBtoLAB(__color);
                    labList.Add(__lab);
                }

                foreach (var l in labList)
                {
                    int j = -128;
                    for (int i = 0; i < TINT_COUNT; i++) //-128 to 128 - is B range
                    {
                        XValues[i] = i + 1;

                        if ((int)l.B == j && (int)lab.L != (int)l.L && (int)lab.B != (int)l.B)
                            YValues[i]++; //dosnt correct
                        j++;
                    }
                }

                BarItem bar = pane.AddBar("B", XValues, YValues, color);
                bar.Bar.Border.Color = color;
            }

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            zedGraph2.AxisChange();
            zedGraph2.Invalidate();
        }

        private Lab RGBtoLAB(Color color)
        {
            Rgb rgb = new Rgb();
            rgb.R = color.R;
            rgb.G = color.G;
            rgb.B = color.B;
            var lab = rgb.To<Lab>();

            return lab;
        }
    }
    ///TODO:
    ///http://www.akadia.com/services/dotnet_user_controls.html
    /// <summary>
    /// Very basic slider control with selection range.
    /// </summary>
    [Description("Very basic slider control with selection range.")]
    public partial class SelectionRangeSlider : UserControl
    {
        /// <summary>
        /// Minimum value of the slider.
        /// </summary>
        [Description("Minimum value of the slider.")]
        public int Min
        {
            get { return min; }
            set { min = value; Invalidate(); }
        }
        int min = 0;
        /// <summary>
        /// Maximum value of the slider.
        /// </summary>
        [Description("Maximum value of the slider.")]
        public int Max
        {
            get { return max; }
            set { max = value; Invalidate(); }
        }
        int max = 100;
        /// <summary>
        /// Minimum value of the selection range.
        /// </summary>
        [Description("Minimum value of the selection range.")]
        public int SelectedMin
        {
            get { return selectedMin; }
            set
            {
                selectedMin = value;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
                Invalidate();
            }
        }
        int selectedMin = 0;
        /// <summary>
        /// Maximum value of the selection range.
        /// </summary>
        [Description("Maximum value of the selection range.")]
        public int SelectedMax
        {
            get { return selectedMax; }
            set
            {
                selectedMax = value;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
                Invalidate();
            }
        }
        int selectedMax = 100;
        /// <summary>
        /// Current value.
        /// </summary>
        [Description("Current value.")]
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                if (ValueChanged != null)
                    ValueChanged(this, null);
                Invalidate();
            }
        }
        int value = 50;
        /// <summary>
        /// Fired when SelectedMin or SelectedMax changes.
        /// </summary>
        [Description("Fired when SelectedMin or SelectedMax changes.")]
        public event EventHandler SelectionChanged;
        /// <summary>
        /// Fired when Value changes.
        /// </summary>
        [Description("Fired when Value changes.")]
        public event EventHandler ValueChanged;

        public SelectionRangeSlider()
        {
        //    InitializeComponent();
            //avoid flickering
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Paint += new PaintEventHandler(SelectionRangeSlider_Paint);
            MouseDown += new MouseEventHandler(SelectionRangeSlider_MouseDown);
            MouseMove += new MouseEventHandler(SelectionRangeSlider_MouseMove);
        }

        void SelectionRangeSlider_Paint(object sender, PaintEventArgs e)
        {
            //paint background in white
            e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            //paint selection range in blue
            Rectangle selectionRect = new Rectangle(
                (selectedMin - Min) * Width / (Max - Min),
                0,
                (selectedMax - selectedMin) * Width / (Max - Min),
                Height);
            e.Graphics.FillRectangle(Brushes.Blue, selectionRect);
            //draw a black frame around our control
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            //draw a simple vertical line at the Value position
            e.Graphics.DrawLine(Pens.Black,
                (Value - Min) * Width / (Max - Min), 0,
                (Value - Min) * Width / (Max - Min), Height);
        }

        void SelectionRangeSlider_MouseDown(object sender, MouseEventArgs e)
        {
            //check where the user clicked so we can decide which thumb to move
            int pointedValue = Min + e.X * (Max - Min) / Width;
            int distValue = Math.Abs(pointedValue - Value);
            int distMin = Math.Abs(pointedValue - SelectedMin);
            int distMax = Math.Abs(pointedValue - SelectedMax);
            int minDist = Math.Min(distValue, Math.Min(distMin, distMax));
            if (minDist == distValue)
                movingMode = MovingMode.MovingValue;
            else if (minDist == distMin)
                movingMode = MovingMode.MovingMin;
            else
                movingMode = MovingMode.MovingMax;
            //call this to refreh the position of the selected thumb
            SelectionRangeSlider_MouseMove(sender, e);
        }

        void SelectionRangeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            //if the left button is pushed, move the selected thumb
            if (e.Button != MouseButtons.Left)
                return;
            int pointedValue = Min + e.X * (Max - Min) / Width;
            if (movingMode == MovingMode.MovingValue)
                Value = pointedValue;
            else if (movingMode == MovingMode.MovingMin)
                SelectedMin = pointedValue;
            else if (movingMode == MovingMode.MovingMax)
                SelectedMax = pointedValue;
        }

        /// <summary>
        /// To know which thumb is moving
        /// </summary>
        enum MovingMode { MovingValue, MovingMin, MovingMax }
        MovingMode movingMode;
    }
}