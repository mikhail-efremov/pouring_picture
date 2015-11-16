﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace pouring_picture
{
    public partial class Form1 : Form
    {
        private int maxHeight = 2510;
        private int maxWidth = 2455;
        private Bitmap savedBitmap;
        private Bitmap savedBitmap2;
        private List<BackStepItem> previousBitmaps = new List<BackStepItem>();

        ZedGraphWrap redWrap;
        ZedGraphWrap greenWrap;
        ZedGraphWrap blueWrap;

        private int rangeValue;

        List<PixelData> pixelDatas;

        public Form1()
        {
            InitializeComponent();
            rangeValue = Convert.ToInt32(textBoxMagic.Text);
            pixelDatas = new List<PixelData>();
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void buttonDrawChart_Click(object sender, EventArgs e)
        {
            PouringImage();
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
                    savedBitmap2 = new Bitmap(image);
                    return true;
                }
            }
            return false;
        }

        private void ImageClick(EventArgs e)
        {
            var mouse = (MouseEventArgs)e;

            if (pictureBox1.Image == null)
                return;

            var bmp = new Bitmap(pictureBox1.Image);

            SetBackStep(new BackStepItem(bmp, new List<PixelData>(pixelDatas)));
            
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

            foreach (var point in points)
            {
                var color = savedBitmap.GetPixel(point.X, point.Y);

                byte b = color.B;       //rgbValues[marker];
                byte g = color.G;       //rgbValues[marker + 1];
                byte r = color.R;       //rgbValues[marker + 2];

                for (int counter = 0; counter < rgbValues.Length; counter += 4)
                {
                        if(PointContains(rgbValues[counter], 
                            rgbValues[counter + 1],
                            rgbValues[counter + 2],
                            b, g, r))
                    {
                        pixelDatas.Add(new PixelData(rgbValues[counter],
                            rgbValues[counter + 1], rgbValues[counter + 2], point.X, point.Y));
                        rgbValues[counter] = blue;
                        rgbValues[counter + 1] = green;
                        rgbValues[counter + 2] = red;
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            redWrap.DrawGraph(pixelDatas);
            greenWrap.DrawGraph(pixelDatas);
            blueWrap.DrawGraph(pixelDatas);

            pictureBox1.Image = bmp;
        }

        private unsafe void PouringImage()
        {
            pictureBox1.Image = savedBitmap;
            var bmp = new Bitmap(pictureBox1.Image);

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

            var points = new List<Point>();

            for (int i = 0; i < pixelDatas.Count; i++ )
            {
                byte b = pixelDatas[i].blue;       //rgbValues[marker];
                byte g = pixelDatas[i].green;       //rgbValues[marker + 1];
                byte r = pixelDatas[i].red;       //rgbValues[marker + 2];
                {
                    for (int counter = 0; counter < rgbValues.Length; counter += 4)
                    {
                        if (PointContainsWR(rgbValues[counter],
                            rgbValues[counter + 1],
                            rgbValues[counter + 2],
                            b, g, r))
                        {
                            rgbValues[counter] = blue;
                            rgbValues[counter + 1] = green;
                            rgbValues[counter + 2] = red;
                        }
                    }
                }
            }
        
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

        private bool PointContainsWR(byte keyb, byte keyg, byte keyr, byte b, byte g, byte r)
        {
            if (keyb == b
                && keyg == g
                && keyr == r)
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
            var list = new List<PixelData>();
            foreach (var slider in selectionRangeSlider.Sliders)
            {
                var p = redWrap.GetPixelDatas(slider.SelectedMin,
                    slider.SelectedMax,
                    pixelDatas);
                if (p.Count != 0)
                {

                    list.AddRange(p);
                }
            }
            pixelDatas = list;
        }

        private void selectionRangeSlider1_SelectionChanged(object sender, EventArgs e)
        {
            var list = new List<PixelData>();
            foreach (var slider in selectionRangeSlider1.Sliders)
            {
                var p = redWrap.GetPixelDatas(slider.SelectedMin,
                    slider.SelectedMax,
                    pixelDatas);
                if (p.Count != 0)
                {
                    redWrap.DrawGraph(p);
                    greenWrap.DrawGraph(p);
                    blueWrap.DrawGraph(p);
                    list.AddRange(p);
                }
            }
            pixelDatas = list;
        }

        void selectionRangeSlider2_SelectionChanged(object sender, EventArgs e)
        {
            var list = new List<PixelData>();
            foreach (var slider in selectionRangeSlider2.Sliders)
            {
                var p = redWrap.GetPixelDatas(slider.SelectedMin,
                    slider.SelectedMax,
                    pixelDatas);
                if (p.Count != 0)
                {
                    redWrap.DrawGraph(p);
                    greenWrap.DrawGraph(p);
                    blueWrap.DrawGraph(p);
                    list.AddRange(p);
                }
            }
            pixelDatas = list;
        }

        private void buttonLoadBackup_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = savedBitmap;
            pixelDatas.Clear();
        }

        private void textBoxMagic_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            if(Int32.TryParse(textBoxMagic.Text, out value))
                rangeValue = value;
        }

        private void BackStep()
        {
            var item = previousBitmaps[previousBitmaps.Count - 1];
            pictureBox1.Image = item.Bitmap;
            pixelDatas = item.PixelData;
            previousBitmaps.Remove(item);
        }

        private void SetBackStep(BackStepItem bitmaps)
        {
            previousBitmaps.Add(bitmaps);
        }

        static int i = 0;
        private void buttonAddRange_Click(object sender, EventArgs e)
        {
            AddSlider();
        }

        private void AddSlider()
        {
            Brush b = PickRandomBrush();
            i++;

            int m_min = 0;
            int m_max = 255;
            foreach (var slider in selectionRangeSlider.Sliders)
            {
                if (slider.SelectedMax != 255)
                    if (slider.SelectedMax > m_min)
                    {
                        m_min = slider.SelectedMax;
                        continue;
                    }
                if (slider.SelectedMin != 0)
                    if (slider.SelectedMin < m_max)
                    {
                        m_max = slider.SelectedMin;
                        continue;
                    }
            }
            var slide = new Slider(selectionRangeSlider.Width, selectionRangeSlider.Height, b,
                m_max, m_min);
            slide.SelectionChanged += selectionRangeSlider_SelectionChanged;
            selectionRangeSlider.Sliders.Add(slide);
            selectionRangeSlider.Invalidate();
        }

        private void AddSlider1()
        {
            Brush b = PickRandomBrush();
            i++;

            int m_min = 0;
            int m_max = 255;
            foreach (var slider in selectionRangeSlider1.Sliders)
            {
                if (slider.SelectedMax != 255)
                    if (slider.SelectedMax > m_min)
                    {
                        m_min = slider.SelectedMax;
                        continue;
                    }
                if (slider.SelectedMin != 0)
                    if (slider.SelectedMin < m_max)
                    {
                        m_max = slider.SelectedMin;
                        continue;
                    }
            }

            var slide = new Slider(selectionRangeSlider1.Width, selectionRangeSlider1.Height, b,
                m_max, m_min);
            slide.SelectionChanged += selectionRangeSlider1_SelectionChanged;
            selectionRangeSlider1.Sliders.Add(slide);
            selectionRangeSlider1.Invalidate();
        }

        private void AddSlider2()
        {
            Brush b = PickRandomBrush();
            i++;

            int m_min = 0;
            int m_max = 255;
            foreach (var slider in selectionRangeSlider2.Sliders)
            {
                if (slider.SelectedMax != 255)
                    if (slider.SelectedMax > m_min)
                    {
                        m_min = slider.SelectedMax;
                        continue;
                    }
                if (slider.SelectedMin != 0)
                    if (slider.SelectedMin < m_max)
                    {
                        m_max = slider.SelectedMin;
                        continue;
                    }
            }

            var slide = new Slider(selectionRangeSlider2.Width, selectionRangeSlider2.Height, b,
                m_max, m_min);
            slide.SelectionChanged += selectionRangeSlider2_SelectionChanged;
            selectionRangeSlider2.Sliders.Add(slide);
            selectionRangeSlider2.Invalidate();
        }

        private void pictureBoxPick_Click(object sender, EventArgs e)
        {
            GetColor();
        }

        private void buttonAddRange1_Click(object sender, EventArgs e)
        {
            AddSlider1();
        }

        private void buttonAddRange2_Click(object sender, EventArgs e)
        {
            AddSlider2();
        }

        private Brush PickRandomBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (pixelDatas.Count != 0)
            {
                redWrap.DrawGraph(pixelDatas);
                greenWrap.DrawGraph(pixelDatas);
                blueWrap.DrawGraph(pixelDatas);
            }
            else MessageBox.Show("count is zero");
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