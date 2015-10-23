﻿using System;
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

        public unsafe void DrawGraph(List<PixelData> datas)
        {
            const int TINT_COUNT = 255;

            GraphControl.Refresh();
            GraphControl.GraphPane.CurveList.Clear();
            GraphControl.GraphPane.GraphObjList.Clear();

            GraphPane pane = GraphControl.GraphPane;
            pane.CurveList.Clear();

            DrowRGB(TINT_COUNT, GraphControl, datas);

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            GraphControl.GraphPane.XAxis.Scale.Min = 0;
            GraphControl.GraphPane.XAxis.Scale.Max = 255;
            GraphControl.AxisChange();
            GraphControl.Refresh();
        }

        public void DrawGraph(int min, int max)
        {
            GraphPane pane = GraphControl.GraphPane;
            var count = GraphControl.GraphPane.CurveList.Count;

            var barList = new List<UserBar>();
            for (int l = 0; l < count; l++)
            {
                var points = GraphControl.GraphPane.CurveList[l].Points;

                var successColors = new List<Point>();
                for (int i = 0; i < 255; i++)
                {
                    if (points[i].X > min && points[i].X < max)
                        successColors.Add(new Point((int)points[i].X, (int)points[i].Y));
                }

                var XValues = new double[255];
                var YValues = new double[255];

                for (int i = 0; i < XValues.Length; i++)
                {
                    XValues[i] = i;
                }

                for (int i = 0; i < successColors.Count; i++)
                {
                    var x = successColors[i].X;
                    YValues[x] = successColors[i].Y;
                }

                barList.Add(new UserBar(Color, XValues, YValues, Color.ToString()));
            }
            pane.CurveList.Clear();

            for (int i = 0; i < barList.Count; i++)
            {
                var _bar = barList[i];
                BarItem bar = pane.AddBar(_bar.label, _bar.XValues, _bar.YValues, _bar.color);
                bar.Bar.Border.Color = _bar.color;
                GraphControl.GraphPane.CurveList[i] = bar;
                bar.Label.IsVisible = false;
            }

            GraphControl.AxisChange();
            GraphControl.Invalidate();
        }

        private unsafe void DrowRGB(int tintCount, ZedGraphControl izedGraph, List<PixelData> datas)
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
            foreach (var data in datas)
            {
                for (int i = 0; i < tintCount; i++)
                {
                    XValues[i] = i + 1;

                    if (color == Color.Red)
                    {
                        if (data.red == i && data.blue != blue && data.green != green)
                            YValues[i]++;
                    }
                    if (color == Color.Blue)
                    {
                        if (data.blue == i && data.red != red && data.green != green)
                            YValues[i]++;
                    }
                    if (color == Color.Green)
                    {
                        if (data.green == i && data.red != red && data.blue != blue)
                            YValues[i]++;
                    }
                }
            }
            if (color == Color.Red)
            {
                var colR = col.R;
                var colG = col.G;
                var colB = col.B;

                if (colR - 20 >= 0)
                    colR -= 20;
                if (colG + 20 <= 255)
                    colG += 20;
                if (colB + 20 <= 255)
                    colB += 20;

                col = Color.FromArgb(colR, colG, colB);
            }
            if (color == Color.Blue)
            {
                var colR = col.R;
                var colG = col.G;
                var colB = col.B;

                if (colR + 20 <= 255)
                    colR += 20;
                if (colG + 20 <= 255)
                    colG += 20;
                if (colB - 20 >= 0)
                    colB -= 20;

                col = Color.FromArgb(colR, colG, colB);
            }
            if (color == Color.Green)
            {
                var colR = col.R;
                var colG = col.G;
                var colB = col.B;

                if (colR + 20 <= 255)
                    colR += 20;
                if (colG - 20 >= 0)
                    colG -= 20;
                if (colB + 20 <= 255)
                    colB += 20;

                col = Color.FromArgb(colR, colG, colB);
            }

            BarItem bar = pane.AddBar(col.ToString(), XValues, YValues, col);
            bar.Bar.Border.Color = col;
            bar.Label.IsVisible = false;

            pane.BarSettings.MinBarGap = 0.0f;
            pane.BarSettings.MinClusterGap = 0.0f;

            pane.Border.DashOff = 0.0f;
            pane.Border.DashOn = 0.0f;

            izedGraph.AxisChange();
            izedGraph.Invalidate();
        }
    }
}