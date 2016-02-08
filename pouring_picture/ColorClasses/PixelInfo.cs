using System;
using System.Drawing;
using System.Collections.Generic;
using ColorMine.ColorSpaces;

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
        
        public static List<PixelInfo> ToListPixelInfo(List<LabInfo> labInfo, Color color)
        {
            var pixelData = new List<PixelData>();
            var pixList = new List<PixelInfo>();
            
            foreach (var lab in labInfo)
            {
                foreach (var pix in lab.LabData)
                {
                    var rgb = pix.To<Rgb>();
                    pixelData.Add(new PixelData((byte)((int)rgb.B), (byte)((int)rgb.G), (byte)((int)rgb.R)));
                }
            }
            pixList.Add(new PixelInfo(pixelData, color));
            return pixList;
        }
    }
}