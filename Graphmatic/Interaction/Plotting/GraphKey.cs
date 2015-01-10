using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphKey : IPlottable
    {
        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams)
        {
            Font font = SystemFonts.DefaultFont;
            Brush brush = new SolidBrush(plotParams.PlotColor);

            float currentY = graphSize.Height;
            int offset = 5;
            foreach (KeyValuePair<IPlottable, PlottableParameters> plot in graph.Reverse())
            {
                if (plot.Key is Resource)
                {
                    var resource = plot.Key as Resource;
                    string name = resource.Name;
                    var size = graphics.MeasureString(name, font);
                    currentY -= size.Height + offset;
                    float resourceX = graphSize.Width - offset - size.Width;
                    graphics.DrawString(name, font, brush, resourceX, currentY);

                    if (resource is DataSet)
                    {
                        Pen resourcePen = new Pen(plot.Value.PlotColor, DataSet.DataPointPenWidth);
                        graphics.DrawLine(resourcePen,
                            new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + size.Height / 2),
                            new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + size.Height / 2));
                        graphics.DrawLine(resourcePen,
                            new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + size.Height / 2),
                            new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + size.Height / 2));
                    }
                    else if (resource is Equation)
                    {
                        Pen resourcePen = new Pen(plot.Value.PlotColor, Equation.EquationPenWidth);
                        graphics.DrawLine(resourcePen,
                            new PointF(resourceX - 2, currentY + size.Height / 2),
                            new PointF(resourceX - 15, currentY + size.Height / 2));
                    }
                }
            }
        }
    }
}