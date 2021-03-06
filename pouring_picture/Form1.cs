﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using ColorMine.ColorSpaces;
using pouring_picture.ColorClasses;

namespace pouring_picture
{
    public partial class Form1 : Form
    {
        private int maxHeight = 2510;
        private int maxWidth = 2455;
        private Bitmap savedBitmap;
        private List<BackStepItem> previousBitmaps;
        ZedGraphWrap redWrap;
        ZedGraphWrap greenWrap;
        ZedGraphWrap blueWrap;

        private int rangeValue;
        private bool Cuted = false;

        List<PixelData> pixelDatas;
        List<PixelInfo> pixelInfo;
        List<LabInfo> labInfo;
        List<Lab> labDatas;

        public Form1()
        {
            InitializeComponent();
            rangeValue = Convert.ToInt32(textBoxSensivity.Text);
            previousBitmaps = new List<BackStepItem>();
            pixelDatas = new List<PixelData>();
            pixelInfo = new List<PixelInfo>();
            labInfo = new List<LabInfo>();
            labDatas = new List<Lab>();

            var lLab = new Lab();
            lLab.L = 100;
            var aLab = new Lab();
            aLab.A = 127;
            var bLab = new Lab();
            bLab.B = 127;

            redWrap = new ZedGraphWrap(zedGraph, Color.Red, lLab);
            greenWrap = new ZedGraphWrap(zedGraph1, Color.Green, aLab);
            blueWrap = new ZedGraphWrap(zedGraph2, Color.Blue, bLab);
            pictureBoxPick.BackColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillColorPickRegion(null);
            FormMethods.PrepareGraph(zedGraph, zedGraph1, zedGraph2);
            comboBox1.Text = "RGB";
        }

        private void imegeUploadButton_Click(object sender, EventArgs e)
        {
            FormMethods.UploadImage(pictureBox1, maxHeight, maxWidth, out savedBitmap);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ImageClick(e);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            FormMethods.SaveImage(pictureBox1);
        }

        private void buttonDrawChart_Click(object sender, EventArgs e)
        {
            PouringImage();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            BackStep();
        }

        private void GetColor()
        {
            colorDialog1.ShowDialog();
            var colorDiag = colorDialog1;
            FillColorPickRegion(colorDiag);
        }

        private void FillColorPickRegion(ColorDialog colorDiag)
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
            if(colorDiag != null)
                pictureBoxPick.BackColor = colorDiag.Color;
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

            var pickColor = pictureBoxPick.BackColor;
            byte blue = Convert.ToByte(pictureBoxPick.BackColor.B);
            byte green = Convert.ToByte(pictureBoxPick.BackColor.G);
            byte red = Convert.ToByte(pictureBoxPick.BackColor.R);

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
                            rgbValues[counter + 1], rgbValues[counter + 2]));
                        rgbValues[counter] = blue;
                        rgbValues[counter + 1] = green;
                        rgbValues[counter + 2] = red;
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            pixelInfo.Add(new PixelInfo(pixelDatas, pickColor));
            redWrap.DrawRGBGraph(pixelInfo);
            greenWrap.DrawRGBGraph(pixelInfo);
            blueWrap.DrawRGBGraph(pixelInfo);

            pictureBox1.Image = bmp;
        }

#warning need hard optimize this methode. it slow all work
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

            foreach (var pi in pixelInfo)
            {
                byte blue = pi.Color.B;
                byte green = pi.Color.G;
                byte red = pi.Color.R;

                for (int i = 0; i < pi.PixelData.Count; i++)
                {
                    byte b = pi.PixelData[i].blue;      //rgbValues[marker];
                    byte g = pi.PixelData[i].green;     //rgbValues[marker + 1];
                    byte r = pi.PixelData[i].red;       //rgbValues[marker + 2];
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

        void selectionRangeSlider_SelectionChanged(object sender, EventArgs e)
        {
            var pixelInfo = new List<PixelInfo>();
            var labInfo = new List<LabInfo>();

            if (Convert.ToString(comboBox1.SelectedItem) == "RGB")
            {
                foreach (var slider in selectionRangeSlider.Sliders)
                {
                    Color color = Color.Red;
                    if (slider.Brush is SolidBrush)
                    {
                        color = (slider.Brush as SolidBrush).Color;
                    }
                    var p = redWrap.GetPixelDatas(slider.SelectedMin,
                        slider.SelectedMax,
                        pixelDatas);
                    pixelInfo.Add(new PixelInfo(p, color));
                }
                this.pixelInfo = pixelInfo;
            }
            if (Convert.ToString(comboBox1.SelectedItem) == "LAB")
            {
                labDatas = LabInfo.ToLabData(pixelDatas);

                foreach (var slider in selectionRangeSlider.Sliders)
                {
                    Color color = Color.Red;
                    if (slider.Brush is SolidBrush)
                    {
                        color = (slider.Brush as SolidBrush).Color;
                    }
                    var p = redWrap.GetLabDatas(slider.SelectedMin,
                        slider.SelectedMax,
                        labDatas);
                    labInfo.Add(new LabInfo(p, color));
                }
                this.labInfo = labInfo;
            }
        }

        private void selectionRangeSlider1_SelectionChanged(object sender, EventArgs e)
        {
            var pixelInfo = new List<PixelInfo>();
            var labInfo = new List<LabInfo>();

            if (Convert.ToString(comboBox1.SelectedItem) == "RGB")
            {
                foreach (var slider in selectionRangeSlider1.Sliders)
                {
                    Color color = Color.Green;
                    if (slider.Brush is SolidBrush)
                    {
                        color = (slider.Brush as SolidBrush).Color;
                    }
                    var p = greenWrap.GetPixelDatas(slider.SelectedMin,
                        slider.SelectedMax,
                        pixelDatas);
                    pixelInfo.Add(new PixelInfo(p, color));
                }
                this.pixelInfo = pixelInfo;
            }
            if (Convert.ToString(comboBox1.SelectedItem) == "LAB")
            {
                labDatas = LabInfo.ToLabData(pixelDatas);

                foreach (var slider in selectionRangeSlider1.Sliders)
                {
                    Color color = Color.Green;
                    if (slider.Brush is SolidBrush)
                    {
                        color = (slider.Brush as SolidBrush).Color;
                    }
                    var p = redWrap.GetLabDatas(slider.SelectedMin,
                        slider.SelectedMax,
                        labDatas);
                    labInfo.Add(new LabInfo(p, color));
                }
                this.labInfo = labInfo;
            }
        }

        void selectionRangeSlider2_SelectionChanged(object sender, EventArgs e)
        {
            var pixelInfo = new List<PixelInfo>();
            var labInfo = new List<LabInfo>();

            if (Convert.ToString(comboBox1.SelectedItem) == "RGB")
            {
                foreach (var slider in selectionRangeSlider2.Sliders)
                {
                    Color color = Color.Blue;
                    if (slider.Brush is SolidBrush)
                    {
                        color = (slider.Brush as SolidBrush).Color;
                    }
                    var p = blueWrap.GetPixelDatas(slider.SelectedMin,
                        slider.SelectedMax,
                        pixelDatas);
                    pixelInfo.Add(new PixelInfo(p, color));
                }
                this.pixelInfo = pixelInfo;
            }
            if (Convert.ToString(comboBox1.SelectedItem) == "LAB")
            {
                labDatas = LabInfo.ToLabData(pixelDatas);

                foreach (var slider in selectionRangeSlider2.Sliders)
                {
                    Color color = Color.Blue;
                    if (slider.Brush is SolidBrush)
                    {
                        color = (slider.Brush as SolidBrush).Color;
                    }
                    var p = redWrap.GetLabDatas(slider.SelectedMin,
                        slider.SelectedMax,
                        labDatas);
                    labInfo.Add(new LabInfo(p, color));
                }
                this.labInfo = labInfo;
            }
        }

        private void buttonLoadBackup_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = savedBitmap;
            pixelDatas.Clear();
        }

        private void textBoxMagic_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            if(Int32.TryParse(textBoxSensivity.Text, out value))
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

        private void buttonAddRange_Click(object sender, EventArgs e)
        {
            SuperSliderAddeder(selectionRangeSlider, 0);
        }

        private void buttonAddRange1_Click(object sender, EventArgs e)
        {
            SuperSliderAddeder(selectionRangeSlider1, 1);
        }

        private void buttonAddRange2_Click(object sender, EventArgs e)
        {
            SuperSliderAddeder(selectionRangeSlider2, 2);
        }
        private void SuperSliderAddeder(SelectionRangeSlider Slider, Int32 SliderNumber)
        {
            Brush b = new SolidBrush(pictureBoxPick.BackColor);
            
            int m_min = 0;
            int m_max = 255;
            foreach (var sli in Slider.Sliders)
            {
                if (sli.SelectedMin == 0 && sli.SelectedMax == 255)
                {
                    MessageBox.Show("You wonna add slider to full slider's range, realy?", "Error");
                }
                if (sli.SelectedMax != 255)
                    if (sli.SelectedMax > m_min)
                    {
                        m_min = sli.SelectedMax + 1;
                        continue;
                    }
                if (sli.SelectedMin != 0)
                    if (sli.SelectedMin < m_max)
                    {
                        m_max = sli.SelectedMin - 1;
                        continue;
                    }
            }
            var slide = new Slider(Slider.Width, Slider.Height, b, m_max, m_min);
            Slider.Sliders.Add(slide);
            if (SliderNumber == 0)
            {
                slide.SelectionChanged += selectionRangeSlider_SelectionChanged;
                selectionRangeSlider_SelectionChanged(slide, null);
                selectionRangeSlider.Invalidate();
            }
            if (SliderNumber == 1)
            {
                slide.SelectionChanged += selectionRangeSlider1_SelectionChanged;
                selectionRangeSlider1_SelectionChanged(slide, null);
                selectionRangeSlider1.Invalidate();
            }
            if (SliderNumber == 2)
            {
                slide.SelectionChanged += selectionRangeSlider2_SelectionChanged;
                selectionRangeSlider2_SelectionChanged(slide, null);
                selectionRangeSlider2.Invalidate();
            }
        }

        private void pictureBoxPick_Click(object sender, EventArgs e)
        {
            GetColor();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (pixelDatas.Count != 0)
            {
                Cuted = true;
                if (Convert.ToString(comboBox1.SelectedItem) == "RGB")
                {
                    ToRgb();
                }
                if (Convert.ToString(comboBox1.SelectedItem) == "LAB")
                {
                    ToLab();
                }
            }
            else MessageBox.Show("count is zero");
        }

        private void ToRgb()
        {
            redWrap.DrawRGBGraph(pixelInfo);
            greenWrap.DrawRGBGraph(pixelInfo);
            blueWrap.DrawRGBGraph(pixelInfo);
        }

        private void ToLab()
        {
    //        if (pixelInfo.Count != 0 && Cuted)
    //        {
    //            labInfo = LabInfo.ToListLabInfo(pixelInfo);
    //            Cuted = false;
    //        }
            if(labInfo.Count == 0)
                labInfo = LabInfo.ToListLabInfo(pixelInfo);
            redWrap.DrawLABGraph(labInfo);
            greenWrap.DrawLABGraph(labInfo);
            blueWrap.DrawLABGraph(labInfo);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox1.SelectedItem) == "RGB")
            {
                ToRgb();
            }
            if (Convert.ToString(comboBox1.SelectedItem) == "LAB")
            {
                ToLab();
            }
        }
    }
}