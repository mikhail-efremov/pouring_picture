using System;
using System.Drawing;
using System.Collections.Generic;
using ColorMine.ColorSpaces;

namespace pouring_picture.ColorClasses
{
    public class LabInfo
    {
        public List<Lab> LabData;
        public Color Color;

        public LabInfo(List<Lab> LabData, Color Color)
        {
            this.LabData = LabData;
            this.Color = Color;
        }

        public LabInfo() { }

        public static List<LabInfo> ToListLabInfo(List<PixelInfo> pixelInfo)
        {
            var labInfo = new List<LabInfo>();

            var labList = new List<Lab>();

            var listColors = new List<Color>();
            var listLabLists = new List<List<Lab>>();

            foreach (var pixInfo in pixelInfo)
            {
                foreach (var pix in pixInfo.PixelData)
                {
                    var rgb = new Rgb();
                    rgb.R = pix.red;
                    rgb.G = pix.green;
                    rgb.B = pix.blue;
                    labList.Add(rgb.To<Lab>());
                }
                listLabLists.Add(new List<Lab>(labList));
                listColors.Add(pixInfo.Color);
                labList.Clear();
            }

            for (int i = 0; i < listColors.Count; i++)
            {
                labInfo.Add(new LabInfo(listLabLists[i], listColors[i]));
            }

            return labInfo;
        }
    }
}