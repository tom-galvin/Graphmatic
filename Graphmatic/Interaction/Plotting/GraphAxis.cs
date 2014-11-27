using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphAxis : IPlottable
    {
        public double GridSize
        {
            get;
            set;
        }

        public double MajorInterval
        {
            get;
            set;
        }

        public Pen AxisPen
        {
            get;
            set;
        }

        public Pen MajorPen
        {
            get;
            set;
        }

        public Pen MinorPen
        {
            get;
            set;
        }

        public GraphAxis(double gridSize, int majorInterval)
        {
            GridSize = gridSize;
            MajorInterval = majorInterval;

            AxisPen = Pens.Black;
            MajorPen = Pens.Gray;
            MinorPen = Pens.Silver;
        }

        public void PlotOnto(Graph graph, Graphics g, Size graphSize, Color color, PlotParameters parameters)
        {
            int axisX, axisY;
            graph.ToImageSpace(graphSize, parameters, 0, 0, out axisX, out axisY);

            PlotGridLinesOnto(g, graphSize, parameters, axisX, axisY);
            PlotAxesOnto(g, graphSize, axisX, axisY);
        }

        private void PlotAxesOnto(Graphics g, Size graphSize, int axisX, int axisY)
        {
            if (axisY >= 0 && axisY < graphSize.Height)
            {
                g.DrawLine(AxisPen, 0, axisY, graphSize.Width, axisY);
            }
            if (axisX >= 0 && axisX < graphSize.Width)
            {
                g.DrawLine(AxisPen, axisX, 0, axisX, graphSize.Height);
            }
        }

        private void PlotGridLinesOnto(Graphics g, Size graphSize, PlotParameters parameters, int axisX, int axisY)
        {
            double incrementX = GridSize / parameters.HorizontalPixelScale,
                   incrementY = GridSize / parameters.VerticalPixelScale;

            double value = axisX;
            int index = 0;
            while (value < graphSize.Width)
            {
                g.DrawLine(index % MajorInterval == 0 ? MajorPen : MinorPen,
                    (int)value, 0, (int)value, graphSize.Height);
                value += incrementX; index++;
            }

            value = axisX; index = 0;
            while (value >= 0)
            {
                g.DrawLine(index % MajorInterval == 0 ? MajorPen : MinorPen,
                    (int)value, 0, (int)value, graphSize.Height);
                value -= incrementX; index++;
            }

            value = axisY; index = 0;
            while (value < graphSize.Height)
            {
                g.DrawLine(index % MajorInterval == 0 ? MajorPen : MinorPen,
                    0, (int)value, graphSize.Width, (int)value);
                value += incrementY; index++;
            }

            value = axisY; index = 0;
            while (value >= 0)
            {
                g.DrawLine(index % MajorInterval == 0 ? MajorPen : MinorPen,
                    0, (int)value, graphSize.Width, (int)value);
                value -= incrementY; index++;
            }
        }
    }
}