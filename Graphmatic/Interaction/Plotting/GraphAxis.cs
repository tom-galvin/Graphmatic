using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphAxis : IPlottable
    {
        public void PlotOnto(Graph graph, Graphics g, Size graphSize, Color color, PlotParameters parameters)
        {
            Pen axisPen = new Pen(color);
            int axisX, axisY;
            graph.ToImageSpace(graphSize, parameters, 0, 0, out axisX, out axisY);

            if (axisY >= 0 && axisY < graphSize.Height)
            {
                g.DrawLine(axisPen, 0, axisY, graphSize.Width, axisY);
            }
            if (axisX >= 0 && axisX < graphSize.Width)
            {
                g.DrawLine(axisPen, axisX, 0, axisX, graphSize.Height);
            }
        }
    }
}