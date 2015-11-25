using System;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;

namespace pouring_picture
{
    public class ZedGraphWrap
    {
        public ZedGraphControl GraphControl { get; private set; }
        public Color Color { get; private set; }

        public ZedGraphWrap(ZedGraphControl graphControl, Color color)
        {
            GraphControl = graphControl;
            Color = color;
        }

        public unsafe void DrawGraph(List<PixelInfo> pixelInfo)
        {
            const int TINT_COUNT = 255;

            GraphControl.Refresh();
            GraphControl.GraphPane.CurveList.Clear();
            GraphControl.GraphPane.GraphObjList.Clear();

            GraphPane pane = GraphControl.GraphPane;
            pane.CurveList.Clear();

            DrowRGB(TINT_COUNT, GraphControl, pixelInfo);

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            GraphControl.GraphPane.XAxis.Scale.Min = 0;
            GraphControl.GraphPane.XAxis.Scale.Max = 255;
            GraphControl.AxisChange();
            GraphControl.Refresh();
        }

        public List<PixelData> GetPixelDatas(int min, int max, List<PixelData> inpLixelData)
        {
            GraphPane pane = GraphControl.GraphPane;
            var count = GraphControl.GraphPane.CurveList.Count;
            var pixelData = CutPixels(min, max, inpLixelData);
            return pixelData;
        }

        private List<PixelData> CutPixels(int min, int max, List<PixelData> pixelData)
        {
            var count = pixelData.Count;
            var retData = new List<PixelData>();

            if (Color == Color.Red)
            {
                for (int i = 0; i < count; i++)
                {
                    if (pixelData[i].red >= min && pixelData[i].red <= max)
                    {
                        retData.Add(pixelData[i]);
                    }
                }
            }
            if (Color == Color.Blue)
            {
                for (int i = 0; i < count; i++)
                {
                    if (pixelData[i].blue >= min && pixelData[i].blue <= max)
                    {
                        retData.Add(pixelData[i]);
                    }
                }
            }
            if (Color == Color.Green)
            {
                for (int i = 0; i < count; i++)
                {
                    if (pixelData[i].green >= min && pixelData[i].green <= max)
                    {
                        retData.Add(pixelData[i]);
                    }
                }
            }

            return retData;
        }

        private unsafe void DrowRGB(int tintCount, ZedGraphControl izedGraph, List<PixelInfo> pixelInfo)
        {
            double[] YValues = new double[tintCount];
            double[] XValues = new double[tintCount];

            var col = Color;
            var color = Color;

            int red = Color.R;
            int green = Color.G;
            int blue = Color.B;

            GraphPane pane = izedGraph.GraphPane;

            Array.Clear(XValues, 0, XValues.Length);
            foreach (var data in pixelInfo)
            {
                foreach (var pixData in data.PixelData)
                {
                    for (int i = 0; i < tintCount; i++)
                    {
                        XValues[i] = i + 1;

                        if (color == Color.Red)
                        {
                            if (pixData.red == i && pixData.blue != blue && pixData.green != green)
                                YValues[i]++;
                        }
                        if (color == Color.Blue)
                        {
                            if (pixData.blue == i && pixData.red != red && pixData.green != green)
                                YValues[i]++;
                        }
                        if (color == Color.Green)
                        {
                            if (pixData.green == i && pixData.red != red && pixData.blue != blue)
                                YValues[i]++;
                        }
                    }
                }

                BarItem bar = pane.AddBar(col.ToString(), XValues, YValues, data.Color);

                //           bar.Bar.Border.IsVisible = false;
                bar.Label.IsVisible = false;

                bar.Bar.Border.Color = data.Color;
            }


            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            izedGraph.AxisChange();
            izedGraph.Invalidate();
        }
    }
}