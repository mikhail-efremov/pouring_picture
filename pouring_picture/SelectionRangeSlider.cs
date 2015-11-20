using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace pouring_picture
{
    public partial class SelectionRangeSlider : UserControl
    {
        public List<Slider> Sliders = new List<Slider>();

        public SelectionRangeSlider()
        {
            //    InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Paint += new PaintEventHandler(SelectionRangeSlider_Paint);
            MouseDown += new MouseEventHandler(SelectionRangeSlider_MouseDown);
            MouseMove += new MouseEventHandler(SelectionRangeSlider_MouseMove);
        }

        Slider currentSlider = null;

        void SelectionRangeSlider_Paint(object sender, PaintEventArgs e)
        {
            //paint background in white
            e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            if(Sliders.Count > 0)
            foreach (var slider in Sliders)
            {
                Rectangle selectionRect = new Rectangle(
                (slider.SelectedMin - slider.Min) * Width / (slider.Max - slider.Min),
                0,
                (slider.SelectedMax - slider.SelectedMin) * Width / (slider.Max - slider.Min),
                Height);
                e.Graphics.FillRectangle(slider.Brush, selectionRect);
            }
            //draw a black frame around our control
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }

        void SelectionRangeSlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (Sliders.Count < 1)
                return;

            int contMin = 255;
            Slider contrSlider = Sliders[0];

            foreach (var slider in Sliders)
            {
                int pointedValue = slider.Min + e.X * (slider.Max - slider.Min) / Width;
                int distMin = Math.Abs(pointedValue - slider.SelectedMin);
                int distMax = Math.Abs(pointedValue - slider.SelectedMax);
                int minDist = Math.Min(distMin, distMax);
                if (minDist < contMin)
                {
                    contMin = minDist;
                    contrSlider = slider;
                    currentSlider = slider;
                }
                else
                    continue;

                if (minDist == distMin)
                    movingMode = MovingMode.MovingMin;
                else
                    movingMode = MovingMode.MovingMax;
            }
            SelectionRangeSlider_MouseMove(sender, e);
        }

        void SelectionRangeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || currentSlider == null)
                return;
            Slider slider = currentSlider;

            int pointedValue = slider.Min + e.X * (slider.Max - slider.Min) / Width;
            if (movingMode == MovingMode.MovingMin)
            {
                if (pointedValue >= 0 && pointedValue <= 255 && pointedValue <= slider.SelectedMax)
                {
                    bool draw = true;
                    foreach (var s in Sliders)
                    {
                        if (pointedValue >= s.SelectedMin && pointedValue <= s.SelectedMax)
                            if(s != currentSlider)
                                draw = false;
                    }
                    if(draw)
                        slider.SelectedMin = pointedValue;
                }
            }
            else if (movingMode == MovingMode.MovingMax)
            {
                if (pointedValue >= 0 && pointedValue <= 255 && pointedValue >= slider.SelectedMin)
                {
                    bool draw = true;
                    foreach (var s in Sliders)
                    {
                        if (pointedValue >= s.SelectedMin && pointedValue <= s.SelectedMax)
                            if(s != currentSlider)
                                draw = false;
                    }
                    if(draw)
                        slider.SelectedMax = pointedValue;
                }
            }
            Invalidate();
        }

        enum MovingMode { MovingValue, MovingMin, MovingMax }
        MovingMode movingMode;
    }
}