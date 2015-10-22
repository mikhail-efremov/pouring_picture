using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using ZedGraph;
using ColorMine.ColorSpaces;

namespace pouring_picture
{
    public partial class Form1 : Form
    {
        private int maxHeight = 2510;
        private int maxWidth = 2455;
        List<PixelData> chartColors;
        private Bitmap savedBitmap;
        private Bitmap previousBitmap;

        private int rangeValue;

        List<List<PixelData>> datas;

        public Form1()
        {
            InitializeComponent();
            rangeValue = Convert.ToInt32(textBoxMagic.Text);
            chartColors = new List<PixelData>();
            datas = new List<List<PixelData>>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillColorPickRegion();
            PrepareGraph();
            Subscribe();
        }

        private void imegeUploadButton_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ImageClick(e);
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
            DrawGraph(zedGraph, Color.Red);
            DrawGraph(zedGraph1, Color.Green);
            DrawGraph(zedGraph2, Color.Blue);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            BackStep();
        }

        private void Subscribe()
        {
            selectionRangeSlider.SelectionChanged += selectionRangeSlider_SelectionChanged;
            selectionRangeSlider1.SelectionChanged += selectionRangeSlider1_SelectionChanged;
            selectionRangeSlider2.SelectionChanged += selectionRangeSlider2_SelectionChanged;
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
                    savedBitmap = image;
                    return true;
                }
            }
            return false;
        }

        private void ImageClick(EventArgs e)
        {
            var mouse = (MouseEventArgs)e;

            var bmp = new Bitmap(pictureBox1.Image);
            previousBitmap = bmp;

            var ad = new PaintEventArgs(Graphics.FromImage(pictureBox1.Image)
                , new Rectangle(new Point(mouse.X, mouse.Y), new Size(new Point(0, 0))));
            
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            Rectangle myRectangle = new Rectangle();

            myRectangle.Location = new Point(coordinates.X, coordinates.Y);

            myRectangle.Size = new Size(Convert.ToInt32(textBoxMarkerWidth.Text),
                Convert.ToInt32(textBoxMarkerHeight.Text));

            PouringImage(ad);
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
                               ImageFormat.Jpeg);
                            break;

                        case 2:
                            pictureBox1.Image.Save(fs,
                               ImageFormat.Bmp);
                            break;

                        case 3:
                            pictureBox1.Image.Save(fs,
                               ImageFormat.Gif);
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

        private unsafe void PouringImage(PaintEventArgs e)
        {
            chartColors.Clear();

            var points = new List<Point>();

            Rectangle myRectangle = new Rectangle();

            myRectangle.Location = new Point(e.ClipRectangle.X, e.ClipRectangle.Y);

            myRectangle.Size = new Size(Convert.ToInt32(textBoxMarkerWidth.Text),
                Convert.ToInt32(textBoxMarkerHeight.Text));

            var bmp = new Bitmap(pictureBox1.Image);

            for (int i = 0; i < bmp.Size.Height; i++)
                for (int j = 0; j < bmp.Size.Width; j++)
                {
                    if (myRectangle.Contains(new Point(j, i)))
                    {
                        points.Add(new Point(j, i));
                    }
                }

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
                bmp.LockBits(rect, ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            byte blue = Convert.ToByte(labelBlue.Text);
            byte green = Convert.ToByte(labelGreen.Text);
            byte red = Convert.ToByte(labelRed.Text);

            var pe = points[0];

            var x = e.ClipRectangle.X;
            var y = e.ClipRectangle.Y;

            int success = 0;

            foreach (var point in points)
            {
                var marker = point.X * 4 + point.Y * 4;

                //TODO get byte from array lol

                var color = savedBitmap.GetPixel(point.X, point.Y);

                byte b = color.B;       //rgbValues[marker];
                byte g = color.G;       //rgbValues[marker + 1];
                byte r = color.R;       //rgbValues[marker + 2];

                byte b1 = rgbValues[marker];
                byte g1 = rgbValues[marker + 1];
                byte r1 = rgbValues[marker + 2];

                for (int counter = 0; counter < rgbValues.Length; counter += 4)
                {
                        if(PointContains(rgbValues[counter], 
                            rgbValues[counter + 1],
                            rgbValues[counter + 2],
                            b, g, r))
                    {
                        chartColors.Add(new PixelData(rgbValues[counter],
                            rgbValues[counter + 1], rgbValues[counter + 2]));
                        success++;
                        rgbValues[counter] = blue;
                        rgbValues[counter + 1] = green;
                        rgbValues[counter + 2] = red;
                    }
                }
            }
            datas.Add(new List<PixelData>(chartColors));

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            pictureBox1.Image = bmp;
        }

        private bool PointContains(byte keyb, byte keyg, byte keyr, byte b, byte g, byte r)
        {
            if (keyb > b - rangeValue && keyb < b + rangeValue
                && keyg > g - rangeValue && keyg < g + rangeValue 
                && keyr > r - rangeValue && keyr < r + rangeValue)
                return true;
            return false;
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

        private unsafe void DrowRGB(int tintCount, ZedGraphControl izedGraph, Color color, Color col)
        {
            double[] YValues = new double[tintCount];
            double[] XValues = new double[tintCount];

            int red = Convert.ToInt32(labelRed.Text);
            int green = Convert.ToInt32(labelGreen.Text);
            int blue = Convert.ToInt32(labelBlue.Text);

            GraphPane pane = izedGraph.GraphPane;

            foreach (var data in datas)
            {
                Array.Clear(XValues, 0, XValues.Length);
                foreach (var pixel in data)
                {
                    for (int i = 0; i < tintCount; i++)
                    {
                        XValues[i] = i + 1;

                        if (color == Color.Red)
                        {
                            if (pixel.red == i && pixel.blue != blue && pixel.green != green)
                                YValues[i]++;
                        }
                        if (color == Color.Blue)
                        {
                            if (pixel.blue == i && pixel.red != red && pixel.green != green)
                                YValues[i]++;
                        }
                        if (color == Color.Green)
                        {
                            if (pixel.green == i && pixel.red != red && pixel.blue != blue)
                                YValues[i]++;
                        }
                    }
                }
                if (color == Color.Red)
                {
                    var colR = col.R;
                    var colG = col.G;
                    var colB = col.B;

                    if (colR - 20 >= 0)
                        colR -= 20;
                    if (colG + 20 <= 255)
                        colG += 20;
                    if (colB + 20 <= 255)
                        colB += 20;

                    col = Color.FromArgb(colR, colG, colB);
                }
                if (color == Color.Blue)
                {
                    var colR = col.R;
                    var colG = col.G;
                    var colB = col.B;

                    if (colR + 20 <= 255)
                        colR += 20;
                    if (colG + 20 <= 255)
                        colG += 20;
                    if (colB - 20 >= 0)
                        colB -= 20;

                    col = Color.FromArgb(colR, colG, colB);
                }
                if (color == Color.Green)
                {
                    var colR = col.R;
                    var colG = col.G;
                    var colB = col.B;

                    if (colR + 20 <= 255)
                        colR += 20;
                    if (colG - 20 >= 0)
                        colG -= 20;
                    if (colB + 20 <= 255)
                        colB += 20;

                    col = Color.FromArgb(colR, colG, colB);
                }

                BarItem bar = pane.AddBar(col.ToString(), XValues, YValues, col);
                bar.Bar.Border.Color = col;
                bar.Label.IsVisible = false;

                pane.BarSettings.MinBarGap = 0.0f;
                pane.BarSettings.MinClusterGap = 0.0f;

                pane.Border.DashOff = 0.0f;
                pane.Border.DashOn = 0.0f;

                izedGraph.AxisChange();
                izedGraph.Invalidate();
            }
        }

        void selectionRangeSlider_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin.Text = Convert.ToString(selectionRangeSlider.SelectedMin);
            labelRangeSliderMax.Text = Convert.ToString(selectionRangeSlider.SelectedMax);
            DrawGraph(zedGraph, Color.Red, selectionRangeSlider.SelectedMin, selectionRangeSlider.SelectedMax);
        }

        private void selectionRangeSlider1_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin1.Text = Convert.ToString(selectionRangeSlider1.SelectedMin);
            labelRangeSliderMax1.Text = Convert.ToString(selectionRangeSlider1.SelectedMax);
            DrawGraph(zedGraph1, Color.Green, selectionRangeSlider1.SelectedMin, selectionRangeSlider1.SelectedMax);
        }

        void selectionRangeSlider2_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin2.Text = Convert.ToString(selectionRangeSlider2.SelectedMin);
            labelRangeSliderMax2.Text = Convert.ToString(selectionRangeSlider2.SelectedMax);
            DrawGraph(zedGraph2, Color.Blue, selectionRangeSlider2.SelectedMin, selectionRangeSlider2.SelectedMax);
        }

        private unsafe void DrawGraph(ZedGraphControl zedGraph, Color color)
        {
            const int TINT_COUNT = 255;

            int red = Convert.ToInt32(labelRed.Text);
            int green = Convert.ToInt32(labelGreen.Text);
            int blue = Convert.ToInt32(labelBlue.Text);

            zedGraph.Refresh();
            zedGraph.GraphPane.CurveList.Clear();
            zedGraph.GraphPane.GraphObjList.Clear();

            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();

            if (comboBox1.Text == "RGB")
            {
                DrowRGB(TINT_COUNT, zedGraph, color, color);
            }

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            zedGraph.GraphPane.XAxis.Scale.Min = 0;
            zedGraph.GraphPane.XAxis.Scale.Max = 255;
            zedGraph.AxisChange();
            zedGraph.Refresh();
        }

        private void DrawGraph(ZedGraphControl zedGraph, Color color, int min, int max)
        {
            GraphPane pane = zedGraph.GraphPane;
            var count = zedGraph.GraphPane.CurveList.Count;

            var barList = new List<UserBar>();
            for (int l = 0; l < count; l++)
            {
                var points = zedGraph.GraphPane.CurveList[l].Points;

                var successColors = new List<Point>();
                for (int i = 0; i < 255; i++)
                {
                    if (points[i].X > min && points[i].X < max)
                        successColors.Add(new Point((int)points[i].X, (int)points[i].Y));
                }

                var XValues = new double[255];
                var YValues = new double[255];

                for (int i = 0; i < XValues.Length; i++)
                {
                    XValues[i] = i;
                }

                for (int i = 0; i < successColors.Count; i++)
                {
                    var x = successColors[i].X;
                    YValues[x] = successColors[i].Y;
                }

                var colR = color.R;
                var colG = color.G;
                var colB = color.B;

                if (colR - 20 >= 0)
                    colR -= 20;
                if (colG + 20 <= 255)
                    colG += 20;

                color = Color.FromArgb(colR, colG, colB);

                barList.Add(new UserBar(color, XValues, YValues, color.ToString()));
            }
            pane.CurveList.Clear();

            for (int i = 0; i < barList.Count; i++)
            {
                var _bar = barList[i];
                BarItem bar = pane.AddBar(_bar.label, _bar.XValues, _bar.YValues, _bar.color);
                bar.Bar.Border.Color = _bar.color;
                zedGraph.GraphPane.CurveList[i] = bar;
                bar.Label.IsVisible = false;
            }

            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private void buttonSaveGraphColors_Click(object sender, EventArgs e)
        {  
            double[] XValues = new double[255];
            double[] RedValues = new double[255];
            double[] GreenValues = new double[255];
            double[] BlueValues = new double[255];

            for (int i = 0; i < 255; i++)
            {
                XValues[i] = i;
            }

            var count = zedGraph.GraphPane.CurveList.Count;
            for (int i = 0; i < count; i++)
            {
                var points = zedGraph.GraphPane.CurveList[i].Points;

                for (int j = 0; j < 255; j++)
                {
                    RedValues[j] = RedValues[j] + points[j].Y;
                }
            }

            var count1 = zedGraph1.GraphPane.CurveList.Count;
            for (int i = 0; i < count1; i++)
            {
                var points = zedGraph1.GraphPane.CurveList[i].Points;

                for (int j = 0; j < 255; j++)
                {
                    GreenValues[j] = GreenValues[j] + points[j].Y;
                }
            }

            var count2 = zedGraph2.GraphPane.CurveList.Count;
            for (int i = 0; i < count2; i++)
            {
                var points = zedGraph2.GraphPane.CurveList[i].Points;

                for (int j = 0; j < 255; j++)
                {
                    BlueValues[j] = BlueValues[j] + points[j].Y;
                }
            }

            datas.Clear();
            for (int i = 0; i < 255; i++)
            {
                chartColors.Add(new PixelData((byte)RedValues[i], (byte)GreenValues[i], (byte)BlueValues[i]));
            }
            datas.Add(new List<PixelData>(chartColors));
        }

        private void buttonLoadBackup_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = savedBitmap;
            datas.Clear();
        }

        private void textBoxMagic_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            if(Int32.TryParse(textBoxMagic.Text, out value))
                rangeValue = value;
        }

        private void BackStep()
        {
            pictureBox1.Image = previousBitmap;
        }
    }

    public class UserBar
    {
        public Color color;
        public double[] XValues;
        public double[] YValues;
        public string label;

        public UserBar(Color color, double[] XValues, double[] YValues, string label)
        {
            this.color = color;
            this.XValues = XValues;
            this.YValues = YValues;
            this.label = label;
        }
    }
}