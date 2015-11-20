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
                SelectionChanged(this, null);
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
                SelectionChanged(this, null);
            }
        }
        int selectedMax = 255;
        private int Width = 0;
        private int Height = 0;
        public Slider(int Width, int Height, Brush brush, int m_max, int m_min)
        {
            this.Width = Width;
            this.Height = Height;
            this.brush = brush;

            selectedMax = m_max;
            selectedMin = m_min;
        }
        [Description("Fired when SelectedMin or SelectedMax changes.")]
        public event EventHandler SelectionChanged;
    }
}