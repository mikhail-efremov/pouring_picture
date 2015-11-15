using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace pouring_picture
{
    public class Slider
    {
        private Brush brush;
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        [Description("Minimum value of the slider.")]
        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        int min = 0;

        [Description("Maximum value of the slider.")]
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        int max = 255;

        [Description("Minimum value of the selection range.")]
        public int SelectedMin
        {
            get { return selectedMin; }
            set
            {
                selectedMin = value;
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
            }
        }
        int selectedMax = 255;
        private int Width = 0;
        private int Height = 0;
        public Slider(int Width, int Height, Brush brush)
        {
            this.Width = Width;
            this.Height = Height;
            this.brush = brush;

            int m_min = 0;
            int m_max = 255;
            foreach (var slider in SelectionRangeSlider.Sliders)
            {
                if (slider.SelectedMin > 0)
                {
                    m_min = 0;
                    m_max = slider.selectedMin - 1;
                    break;
                }
                else if (slider.selectedMax < 255)
                {
                    m_min = slider.SelectedMin + 1;
                    m_max = 255;
                    break;
                }
            }
            selectedMax = m_max;
            selectedMin = m_min;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(
                (selectedMin - Min) * Width / (Max - Min),
                0,
                (selectedMax - selectedMin) * Width / (Max - Min),
                Height);
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