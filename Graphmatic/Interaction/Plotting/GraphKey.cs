using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Represents the key on a Graphmatic graph.
    /// </summary>
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

        /// <summary>
        /// Plots this IPlottable onto a given graph. The specific method of plotting will depend on the implementation of the
        /// IPlottable.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="plotParams">The parameters used to plot this IPlottable.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, PlotResolution resolution)
        {
            using (Brush brush = new SolidBrush(plotParams.PlotColor))
            {
                float currentY = graphSize.Height;
                int offset = 5;

                foreach (IPlottable plot in graph.Reverse())
                {
                    if (plot is Resource) // only plot Resources, so we don't plot the key on the key
                    {
                        PlottableParameters parameters = graph[plot];
                        Resource resource = plot as Resource;
                        string name = resource.Name;
                        SizeF textSize = graphics.MeasureString(name, SystemFonts.DefaultFont);
                        currentY -= textSize.Height + offset;
                        float resourceX = graphSize.Width - offset - textSize.Width;
                        graphics.DrawString(name, SystemFonts.DefaultFont, brush, resourceX, currentY);

                        using (Pen resourcePen = new Pen(parameters.PlotColor)) // plot in the color of the resource
                        {
                            if (resource is DataSet)
                            { // draw a cross for a data set
                                resourcePen.Width = DataSet.DataPointPenWidth;
                                graphics.DrawLine(resourcePen,
                                    new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + textSize.Height / 2),
                                    new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + textSize.Height / 2));
                                graphics.DrawLine(resourcePen,
                                    new PointF(resourceX - DataSet.DataPointCrossSize - 5, currentY + DataSet.DataPointCrossSize + textSize.Height / 2),
                                    new PointF(resourceX + DataSet.DataPointCrossSize - 5, currentY - DataSet.DataPointCrossSize + textSize.Height / 2));
                            }
                            else if (resource is Equation)
                            { // draw a line for an equation
                                resourcePen.Width = Equation.EquationPenWidth;
                                graphics.DrawLine(resourcePen,
                                    new PointF(resourceX - 2, currentY + textSize.Height / 2),
                                    new PointF(resourceX - 15, currentY + textSize.Height / 2));
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

        /// <summary>
        /// Always returns true, as the key is not affected by the variables being plotted.
        /// </summary>
        /// <returns>Returns true.</returns>
        public bool CanPlot(char variable1, char variable2)
        {
            return true;
        }
    }
}