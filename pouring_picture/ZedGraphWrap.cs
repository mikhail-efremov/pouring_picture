﻿using System;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;
using ColorMine.ColorSpaces;
using pouring_picture.ColorClasses;

namespace pouring_picture
{
    public class ZedGraphWrap
    {
        public ZedGraphControl GraphControl { get; private set; }
        public Color Color { get; private set; }
        public Lab Lab { get; private set; }

        public ZedGraphWrap(ZedGraphControl graphControl, Color color, Lab lab)
        {
            GraphControl = graphControl;
            Color = color;
            Lab = lab;
        }

        public unsafe void DrawRGBGraph(List<PixelInfo> pixelInfo)
        {
            const int TINT_COUNT = 255;

            GraphControl.Refresh();
            GraphControl.GraphPane.CurveList.Clear();
            GraphControl.GraphPane.GraphObjList.Clear();

            GraphPane pane = GraphControl.GraphPane;
            pane.CurveList.Clear();

            DrawRGB(TINT_COUNT, GraphControl, pixelInfo);

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            GraphControl.GraphPane.XAxis.Scale.Min = 0;
            GraphControl.GraphPane.XAxis.Scale.Max = 255;
            GraphControl.AxisChange();
            GraphControl.Refresh();
        }

        public unsafe void DrawLABGraph(List<LabInfo> labInfo)
        {
            const int TINT_COUNT = 255;

            GraphControl.Refresh();
            GraphControl.GraphPane.CurveList.Clear();
            GraphControl.GraphPane.GraphObjList.Clear();

            GraphPane pane = GraphControl.GraphPane;
            pane.CurveList.Clear();

            DrawLAB(TINT_COUNT, GraphControl, labInfo);

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            GraphControl.AxisChange();
            GraphControl.Refresh();
        }

        public List<Lab> GetLabDatas(int min, int max, List<Lab> inpLixelData)
        {
            GraphPane pane = GraphControl.GraphPane;
            var labDatas = CutLabPixels(min, max, inpLixelData);
            return labDatas;
        }

        public List<PixelData> GetPixelDatas(int min, int max, List<PixelData> inpLixelData)
        {
            GraphPane pane = GraphControl.GraphPane;
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

        private List<Lab> CutLabPixels(int min, int max, List<Lab> pixelData)
        {
            var count = pixelData.Count;
            var retData = new List<Lab>();

            if (Color == Color.Red)
            {
                for (int i = 0; i < count; i++)
                {
                    if (pixelData[i].L >= min && pixelData[i].L <= max)
                    {
                        retData.Add(pixelData[i]);
                    }
                }
            }
            if (Color == Color.Blue)
            {
                for (int i = 0; i < count; i++)
                {
                    if (pixelData[i].A >= min && pixelData[i].A <= max)
                    {
                        retData.Add(pixelData[i]);
                    }
                }
            }
            if (Color == Color.Green)
            {
                for (int i = 0; i < count; i++)
                {
                    if (pixelData[i].B >= min && pixelData[i].B <= max)
                    {
                        retData.Add(pixelData[i]);
                    }
                }
            }

            return retData;
        }

        private unsafe void DrawRGB(int tintCount, ZedGraphControl izedGraph, List<PixelInfo> pixelInfo)
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

        private unsafe void DrawLAB(int tintCount, ZedGraphControl izedGraph, List<LabInfo> labInfo)
        {
            //L = 100
            //A = -128 127
            //B = -128 127

            var lab = Lab;

            double[] YValues = null;
            double[] XValues = null;

            if (lab.L == 100)
            {
                YValues = new double[100];
                XValues = new double[100];
            }
            else
            {
                YValues = new double[tintCount];
                XValues = new double[tintCount];
            }

            var col = Color;

            int lightness = Color.R;
            int greenToRed = Color.G;
            int blueToYelow = Color.B;
            
            GraphPane pane = izedGraph.GraphPane;

            Array.Clear(XValues, 0, XValues.Length);
            foreach (var data in labInfo)
            {
                foreach (var pixData in data.LabData)
                {
                    if (lab.L == 100)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            XValues[i] = i;
                            if ((int)pixData.L == i)// && (int)pixData.A != blueToYelow && (int)pixData.B != greenToRed)
                                YValues[i]++;
                        }
                        GraphControl.GraphPane.XAxis.Scale.Min = 0;
                        GraphControl.GraphPane.XAxis.Scale.Max = 100;
                    }
                    else
                    {
                        for (int i = -128; i < 127; i++)
                        {
                            XValues[i + 128] = i + 129;

                            if (lab.A == 127)
                            {
                                if ((int)pixData.A == i)// && (int)pixData.L != lightness && (int)pixData.B != greenToRed)
                                    YValues[i + 128]++;
                            }
                            if (lab.B == 127)
                            {
                                if ((int)pixData.B == i)// && (int)pixData.L != lightness && (int)pixData.A != blueToYelow)
                                    YValues[i + 128]++;
                            }
                        }
                        GraphControl.GraphPane.XAxis.Scale.Min = 0;
                        GraphControl.GraphPane.XAxis.Scale.Max = 255;
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