using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace pouring_picture
{
    public partial class SelectionRangeSlider : UserControl
    {
        private List<Slider> _sliders = new List<Slider>();

        public List<Slider> Sliders
        {
            get { return _sliders; }
            private set { _sliders = value; Invalidate(); }
        }

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

        public void AddSlider(int Width, int Height, int red, int green, int blue, Label min, Label max)
        {
            Sliders.Add(new Slider(Width, Height,
                new SolidBrush(Color.FromArgb(red, green, blue)), min, max));
            Invalidate();
        }

        void SelectionRangeSlider_Paint(object sender, PaintEventArgs e)
        {
            //paint background in white
            e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            //paint selection range in blue
            foreach (var slider in _sliders)
            {
                Rectangle selectionRect = new Rectangle(
                (slider.SelectedMin - slider.Min) * Width / (slider.Max - slider.Min),
                0,
                (slider.SelectedMax - slider.SelectedMin) * Width / (slider.Max - slider.Min),
                Height);
                e.Graphics.FillRectangle(slider.Brush, selectionRect);
            }

/*            Rectangle selectionRect = new Rectangle(
                (selectedMin - Min) * Width / (Max - Min),
                0,
                (selectedMax - selectedMin) * Width / (Max - Min),
                Height);
            e.Graphics.FillRectangle(Brushes.Blue, selectionRect);
*/
            //draw a black frame around our control
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }

        void SelectionRangeSlider_MouseDown(object sender, MouseEventArgs e)
        {
            int pointedValue = Min + e.X * (Max - Min) / Width;
            int distMin = Math.Abs(pointedValue - SelectedMin);
            int distMax = Math.Abs(pointedValue - SelectedMax);
            int minDist = Math.Min(distMin, distMax);
            if (minDist == distMin)
                movingMode = MovingMode.MovingMin;
            else
                movingMode = MovingMode.MovingMax;
            
            var bmp = new Bitmap(Width,Height);

            foreach (var sl in _sliders)
            {
#warning               if(e.X <= sl.Max && e.X >= sl.Min)
                    //call this to refreh the position of the selected thumb
                    SelectionRangeSlider_MouseMove(sl, e);
            }
            Invalidate();
        }

        void SelectionRangeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            Slider slider = null;
            if (sender is Slider)
                slider = (Slider)sender;

            if (e.Button != MouseButtons.Left)
                return;

            foreach (var sl in _sliders)
            {
                if (e.X <= sl.Max && e.X >= sl.Min)
                    slider = sl;
            }
            if (slider == null && _sliders.Count > 0)
                slider = _sliders[0];
            if (slider == null)
                return;

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
        }

        enum MovingMode { MovingValue, MovingMin, MovingMax }
        MovingMode movingMode;
    }
}