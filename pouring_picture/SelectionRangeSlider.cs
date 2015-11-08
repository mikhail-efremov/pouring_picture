using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace pouring_picture
{
    public partial class SelectionRangeSlider : UserControl
    {
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

        void SelectionRangeSlider_Paint(object sender, PaintEventArgs e)
        {
            //paint background in white
            e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            //paint selection range in blue
            Rectangle selectionRect = new Rectangle(
                (selectedMin - Min + 50) * Width / (Max - Min - 50),
                0,
                (selectedMax - selectedMin) * Width / (Max - Min),
                Height);
            e.Graphics.FillRectangle(Brushes.Blue, selectionRect);
            Rectangle selectionRect1 = new Rectangle(
            (selectedMin - Min) * Width / (Max - Min),
            0,
            (selectedMax - selectedMin) * Width / (Max - Min),
            Height);
            e.Graphics.FillRectangle(Brushes.Blue, selectionRect);
            e.Graphics.FillRectangle(Brushes.Red, selectionRect1);
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
            //call this to refreh the position of the selected thumb
            SelectionRangeSlider_MouseMove(sender, e);
        }

        void SelectionRangeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            int pointedValue = Min + e.X * (Max - Min) / Width;
            if (movingMode == MovingMode.MovingMin)
            {
                if (pointedValue >= 0 && pointedValue <= 255 && pointedValue <= SelectedMax)
                    SelectedMin = pointedValue;
            }
            else if (movingMode == MovingMode.MovingMax)
            {
                if(pointedValue >= 0 && pointedValue <= 255 && pointedValue >= SelectedMin)
                    SelectedMax = pointedValue;
            }
        }

        enum MovingMode { MovingValue, MovingMin, MovingMax }
        MovingMode movingMode;
    }
}