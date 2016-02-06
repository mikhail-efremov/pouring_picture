using System;
using System.Drawing;
using System.Collections.Generic;

namespace pouring_picture.ColorClasses
{
    public class PixelInfo
    {
        public List<PixelData> PixelData;
        public Color Color;

        public PixelInfo(List<PixelData> PixelData, Color Color)
        {
            this.PixelData = PixelData;
            this.Color = Color;
        }
    }
}