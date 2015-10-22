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

        ZedGraphWrap redWrap;
        ZedGraphWrap greenWrap;
        ZedGraphWrap blueWrap;

        private int rangeValue;

        List<List<PixelData>> datas;

        public Form1()
        {
            InitializeComponent();
            rangeValue = Convert.ToInt32(textBoxMagic.Text);
            chartColors = new List<PixelData>();
            datas = new List<List<PixelData>>();
            redWrap = new ZedGraphWrap(zedGraph, Color.Red);
            greenWrap = new ZedGraphWrap(zedGraph1, Color.Green);
            blueWrap = new ZedGraphWrap(zedGraph2, Color.Blue);
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
            redWrap.DrawGraph(datas);
            greenWrap.DrawGraph(datas);
            blueWrap.DrawGraph(datas);
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

        void selectionRangeSlider_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin.Text = Convert.ToString(selectionRangeSlider.SelectedMin);
            labelRangeSliderMax.Text = Convert.ToString(selectionRangeSlider.SelectedMax);
            redWrap.DrawGraph(selectionRangeSlider.SelectedMin, selectionRangeSlider.SelectedMax);
        }

        private void selectionRangeSlider1_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin1.Text = Convert.ToString(selectionRangeSlider1.SelectedMin);
            labelRangeSliderMax1.Text = Convert.ToString(selectionRangeSlider1.SelectedMax);
            greenWrap.DrawGraph(selectionRangeSlider1.SelectedMin, selectionRangeSlider1.SelectedMax);
        }

        void selectionRangeSlider2_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin2.Text = Convert.ToString(selectionRangeSlider2.SelectedMin);
            labelRangeSliderMax2.Text = Convert.ToString(selectionRangeSlider2.SelectedMax);
            blueWrap.DrawGraph(selectionRangeSlider2.SelectedMin, selectionRangeSlider2.SelectedMax);
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