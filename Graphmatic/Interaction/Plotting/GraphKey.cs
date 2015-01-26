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
        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.GraphKey</c> class.
        /// </summary>
        public GraphKey()
        {

        }

        /// <summary>
        /// Initialize a new instance of the <c>Graphmatic.Interaction.Plotting.GraphKey</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public GraphKey(XElement element)
        {

        }

        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams, PlotResolution resolution)
        {
            using (Brush brush = new SolidBrush(plotParams.PlotColor))
            {
                float currentY = graphSize.Height;
                int offset = 5;
                foreach (IPlottable plot in graph.Reverse())
                {
                    if (plot is Resource)
                    {
                        PlottableParameters parameters = graph[plot];
                        var resource = plot as Resource;
                        string name = resource.Name;
                        var size = graphics.MeasureString(name, SystemFonts.DefaultFont);
                        currentY -= size.Height + offset;
                        float resourceX = graphSize.Width - offset - size.Width;
                        graphics.DrawString(name, SystemFonts.DefaultFont, brush, resourceX, currentY);

                        using (Pen resourcePen = new Pen(parameters.PlotColor))
                        {
                            if (resource is DataSet)
                            {
                                resourcePen.Width = DataSet.DataPointPenWidth;
                                graphics.DrawLine(resourcePen,
                                    new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + size.Height / 2),
                                    new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + size.Height / 2));
                                graphics.DrawLine(resourcePen,
                                    new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + size.Height / 2),
                                    new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + size.Height / 2));
                            }
                            else if (resource is Equation)
                            {
                                resourcePen.Width = Equation.EquationPenWidth;
                                graphics.DrawLine(resourcePen,
                                    new PointF(resourceX - 2, currentY + size.Height / 2),
                                    new PointF(resourceX - 15, currentY + size.Height / 2));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public XElement ToXml()
        {
            return new XElement("GraphKey");
        }

        /// <summary>
        /// Updates any references to other resources in the document to point to the correct resource.<para/>
        /// This method (and related method <c>Graphmatic.Interaction.Resource.ToResourceReference()</c>) are
        /// needed because certain resources, such as the Page resource, can refer to other resources from within
        /// them (for example if a Page contains a plotted DataSet). However, if the Page is deserialized before
        /// the DataSet, then it will not have an object to refer to. Thus, the resource reference system is used,
        /// whereby certain objects (such as the Page) keep track of which resources they need to refer to later on,
        /// and only actually dereference the Resource references (via the resource's GUID) after all resources in
        /// the document have been deserialized.
        /// </summary>
        /// <param name="document">The parent document containing this resource, and any other resources that this
        /// resource may point to.</param>
        public void UpdateReferences(Document document)
        {
        }


        public bool CanPlot(char variable1, char variable2)
        {
            return true;
        }
    }
}