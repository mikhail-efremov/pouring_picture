using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace pouring_picture
{
    public partial class SelectionRangeSlider : UserControl
    {
        public static List<Slider> Sliders = new List<Slider>();

        [Description("Minimum value of the slider.")]
        public int Min
        {
            get { return min; }
            set { min = value; Invalidate(); }
        }
        int min = 0;

        [Description("Maximum value of the slider.")]
        public int Max
        {
            get { return max; }
            set { max = value; Invalidate(); }
        }
        int max = 100;

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

        [Description("Fired when SelectedMin or SelectedMax changes.")]
        public event EventHandler SelectionChanged;

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
            if (e.Button != MouseButtons.Left)
                return;
            if (currentSlider == null)
                return;
            Slider slider = currentSlider;

            int pointedValue = slider.Min + e.X * (slider.Max - slider.Min) / Width;
            if (movingMode == MovingMode.MovingMin)
            {
                if (pointedValue >= 0 && pointedValue <= 255 && pointedValue <= slider.SelectedMax)
                    slider.SelectedMin = pointedValue;
            }
            else if (movingMode == MovingMode.MovingMax)
            {
                if (pointedValue >= 0 && pointedValue <= 255 && pointedValue >= slider.SelectedMin)
                    slider.SelectedMax = pointedValue;
            }
            Invalidate();
        }

        enum MovingMode { MovingValue, MovingMin, MovingMax }
        MovingMode movingMode;
    }
}