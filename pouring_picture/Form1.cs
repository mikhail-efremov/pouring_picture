﻿using System;
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

        List<List<PixelData>> datas;

        public Form1()
        {
            InitializeComponent();
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

        private void ImageClick(object sender, EventArgs e)
        {
            var mouse = (MouseEventArgs)e;

            var bmp = new Bitmap(pictureBox1.Image);
            var ad = new PaintEventArgs(Graphics.FromImage(pictureBox1.Image)
                , new Rectangle(new Point(mouse.X, mouse.Y), new Size(new Point(0, 0))));
        //    LockUnlockBitsExample(ad);
            
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            Rectangle myRectangle = new Rectangle();

            myRectangle.Location = new Point(coordinates.X, coordinates.Y);

            myRectangle.Size = new Size(Convert.ToInt32(textBoxMarkerWidth.Text),
                Convert.ToInt32(textBoxMarkerHeight.Text));

            var points = new List<Point>();

            for (int i = 0; i < bmp.Size.Height; i++)
                for (int j = 0; j < bmp.Size.Width; j++)
                {
                    if (myRectangle.Contains(new Point(j, i)))
                    {
                        points.Add(new Point(j, i));
                    }
                }

            PouringImage(points, ad);
             
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

        private unsafe void PouringImage(List<Point> points, PaintEventArgs e)
        {
            chartColors.Clear();
            
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
                    if (rgbValues[counter] == b
                        && rgbValues[counter + 1] == g
                        && rgbValues[counter + 2] == r)
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

        private unsafe void DrawGraph()
        {
            const int TINT_COUNT = 255;

            var color = Color.Red;

            int red = Convert.ToInt32(labelRed.Text);
            int green = Convert.ToInt32(labelGreen.Text);
            int blue = Convert.ToInt32(labelBlue.Text);

            zedGraph.Refresh();
            zedGraph.GraphPane.CurveList.Clear();
            zedGraph.GraphPane.GraphObjList.Clear();
            zedGraph.ZoomStepFraction = 255;

            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            
            if (comboBox1.Text == "RGB")
            {
                DrowRGB(255, zedGraph, Color.Red, color);
            }
            else if (comboBox1.Text == "LAB")
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

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
                bar.Label.IsVisible = false;
            }
            else if (comboBox1.Text == "HSV")
            {
                double[] YValues = new double[360];
                double[] XValues = new double[360];

                Color _color = Color.FromArgb(red, green, blue);
                var hsv = RGBtoHSV(_color);

                var hsvList = new List<Hsv>();

                foreach (var c in chartColors)
                {
                    var __color = Color.FromArgb(c.red, c.blue, c.green);
                    var __hsv = RGBtoHSV(__color);
                    hsvList.Add(__hsv);
                }

                foreach (var l in hsvList)
                {
                    for (int i = 0; i < 360; i++) //0 to 100 - is L range
                    {
                        XValues[i] = i + 1;

                        if ((int)l.H == i)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar("H", XValues, YValues, color);
                bar.Bar.Border.Color = color;
                bar.Label.IsVisible = false;
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

            int red = Convert.ToInt32(labelRed.Text);
            int green = Convert.ToInt32(labelGreen.Text);
            int blue = Convert.ToInt32(labelBlue.Text);

            zedGraph1.Refresh();
            zedGraph1.GraphPane.CurveList.Clear();
            zedGraph1.GraphPane.GraphObjList.Clear();

            GraphPane pane = zedGraph1.GraphPane;
            pane.CurveList.Clear();

            if (comboBox1.Text == "RGB")
            {
                DrowRGB(255, zedGraph1, Color.Green, color);
            }
            else if (comboBox1.Text == "LAB")
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

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
                bar.Label.IsVisible = false;
            }
            else if (comboBox1.Text == "HSV")
            {
                double[] YValues = new double[100];
                double[] XValues = new double[100];

                Color _color = Color.FromArgb(red, green, blue);
                var hsv = RGBtoHSV(_color);

                var hsvList = new List<Hsv>();

                foreach (var c in chartColors)
                {
                    var __color = Color.FromArgb(c.red, c.blue, c.green);
                    var __hsv = RGBtoHSV(__color);
                    hsvList.Add(__hsv);
                }

                foreach (var l in hsvList)
                {
                    for (int i = 0; i < 100; i++) //0 to 1 - is s range
                    {
                        XValues[i] = i + 1;

                        int f = (int)(l.S * 100);
                        double s0 = ((double)i / 100)*100;
                        int s = (int)s0;

                        if (f == s)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar("S", XValues, YValues, color);
                bar.Bar.Border.Color = color;
                bar.Label.IsVisible = false;
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

            int red = Convert.ToInt32(labelRed.Text);
            int green = Convert.ToInt32(labelGreen.Text);
            int blue = Convert.ToInt32(labelBlue.Text);

            zedGraph2.Refresh();
            zedGraph2.GraphPane.CurveList.Clear();
            zedGraph2.GraphPane.GraphObjList.Clear();
            zedGraph2.ZoomStepFraction = 255;

            GraphPane pane = zedGraph2.GraphPane;
            pane.CurveList.Clear();

            if (comboBox1.Text == "RGB")
            {
                DrowRGB(255, zedGraph2, Color.Blue, color);
            }
            else if (comboBox1.Text == "LAB")
            {
                double[] YValues = new double[TINT_COUNT];
                double[] XValues = new double[TINT_COUNT];

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
                            YValues[i]++;
                        j++;
                    }
                }

                BarItem bar = pane.AddBar("B", XValues, YValues, color);
                bar.Bar.Border.Color = color;
                bar.Label.IsVisible = false;
            }
            else if (comboBox1.Text == "HSV")
            {
                double[] YValues = new double[100];
                double[] XValues = new double[100];

                Color _color = Color.FromArgb(red, green, blue);
                var hsv = RGBtoHSV(_color);

                var hsvList = new List<Hsv>();

                foreach (var c in chartColors)
                {
                    var __color = Color.FromArgb(c.red, c.blue, c.green);
                    var __hsv = RGBtoHSV(__color);
                    hsvList.Add(__hsv);
                }

                foreach (var l in hsvList)
                {
                    for (int i = 0; i < 100; i++) //0 to 1 - is V range
                    {
                        XValues[i] = i + 1;

                        int f = (int)(l.V * 100);
                        double s0 = ((double)i / 100) * 100;
                        int s = (int)s0;

                        if (f == s)
                            YValues[i]++;
                    }
                }

                BarItem bar = pane.AddBar("V", XValues, YValues, color);
                bar.Bar.Border.Color = color;
                bar.Label.IsVisible = false;
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

        private Hsv RGBtoHSV(Color color)
        {
            Rgb rgb = new Rgb();
            rgb.R = color.R;
            rgb.G = color.G;
            rgb.B = color.B;
            var hsv = rgb.To<Hsv>();

            return hsv;
        }

        void selectionRangeSlider_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin.Text = Convert.ToString(selectionRangeSlider.SelectedMin);
            labelRangeSliderMax.Text = Convert.ToString(selectionRangeSlider.SelectedMax);
        }

        private void selectionRangeSlider1_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin1.Text = Convert.ToString(selectionRangeSlider1.SelectedMin);
            labelRangeSliderMax1.Text = Convert.ToString(selectionRangeSlider1.SelectedMax);
        }

        void selectionRangeSlider2_SelectionChanged(object sender, EventArgs e)
        {
            labelRangeSliderMin2.Text = Convert.ToString(selectionRangeSlider2.SelectedMin);
            labelRangeSliderMax2.Text = Convert.ToString(selectionRangeSlider2.SelectedMax);
        }

        private void buttonCut1_Click(object sender, EventArgs e)
        {
            var color = Color.Green;

            GraphPane pane = zedGraph1.GraphPane;
            var count = zedGraph1.GraphPane.CurveList.Count;

            var barList = new List<UserBar>();
            for (int l = 0; l < count; l++)
            {
                var points = zedGraph1.GraphPane.CurveList[l].Points;
                var min = selectionRangeSlider1.SelectedMin;
                var max = selectionRangeSlider1.SelectedMax;

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
                zedGraph1.GraphPane.CurveList[i] = bar;
                bar.Label.IsVisible = false;
            }

                zedGraph1.AxisChange();
                zedGraph1.Invalidate();
            }

        private void buttonCut_Click(object sender, EventArgs e)
        {
            var color = Color.Red;

            GraphPane pane = zedGraph.GraphPane;
            var count = zedGraph.GraphPane.CurveList.Count;

            var barList = new List<UserBar>();
            for (int l = 0; l < count; l++)
            {
                var points = zedGraph.GraphPane.CurveList[l].Points;
                var min = selectionRangeSlider.SelectedMin;
                var max = selectionRangeSlider.SelectedMax;

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

        private void buttonCut2_Click(object sender, EventArgs e)
        {
            var color = Color.Blue;

            GraphPane pane = zedGraph2.GraphPane;
            var count = zedGraph2.GraphPane.CurveList.Count;

            var barList = new List<UserBar>();
            for (int l = 0; l < count; l++)
            {
                var points = zedGraph2.GraphPane.CurveList[l].Points;
                var min = selectionRangeSlider2.SelectedMin;
                var max = selectionRangeSlider2.SelectedMax;

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
                zedGraph2.GraphPane.CurveList[i] = bar;
                bar.Label.IsVisible = false;
            }

                zedGraph2.AxisChange();
                zedGraph2.Invalidate();
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