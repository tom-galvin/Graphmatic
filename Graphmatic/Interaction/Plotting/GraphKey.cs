using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphKey : IPlottable, IXmlConvertible
    {
        public GraphKey()
        {

        }

        public GraphKey(XElement element)
        {

        }

        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams, PlotResolution resolution)
        {
            Font font = SystemFonts.DefaultFont;
            Brush brush = new SolidBrush(plotParams.PlotColor);

            float currentY = graphSize.Height;
            int offset = 5;
            foreach (IPlottable plot in graph.Reverse())
            {
                if (plot is Resource)
                {
                    PlottableParameters parameters = graph[plot];
                    var resource = plot as Resource;
                    string name = resource.Name;
                    var size = graphics.MeasureString(name, font);
                    currentY -= size.Height + offset;
                    float resourceX = graphSize.Width - offset - size.Width;
                    graphics.DrawString(name, font, brush, resourceX, currentY);

                    if (resource is DataSet)
                    {
                        Pen resourcePen = new Pen(parameters.PlotColor, DataSet.DataPointPenWidth);
                        graphics.DrawLine(resourcePen,
                            new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + size.Height / 2),
                            new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + size.Height / 2));
                        graphics.DrawLine(resourcePen,
                            new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + size.Height / 2),
                            new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + size.Height / 2));
                        resourcePen.Dispose();
                    }
                    else if (resource is Equation)
                    {
                        Pen resourcePen = new Pen(parameters.PlotColor, Equation.EquationPenWidth);
                        graphics.DrawLine(resourcePen,
                            new PointF(resourceX - 2, currentY + size.Height / 2),
                            new PointF(resourceX - 15, currentY + size.Height / 2));
                        resourcePen.Dispose();
                    }
                }
            }

            font.Dispose();
            brush.Dispose();
        }

        public XElement ToXml()
        {
            return new XElement("GraphKey");
        }

        public void UpdateReferences(Document document)
        {
        }


        public bool CanPlot(char variable1, char variable2)
        {
            return true;
        }
    }
}