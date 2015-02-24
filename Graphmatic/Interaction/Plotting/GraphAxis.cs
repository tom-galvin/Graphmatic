using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Represents the axis pair on a graph.
    /// </summary>
    [GraphmaticObject]
    public class GraphAxis : IPlottable, IXmlConvertible
    {
        /// <summary>
        /// Gets or sets the size of the grid squares, in graph space, of each grid line.<para/>
        /// This can be scaled on a per-axis basis depending on the values of the properties
        /// <c>HorizontalType</c> and <c>VerticalType</c>.
        /// </summary>
        public double GridSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the major axis interval on the grid. For example, an interval of 5 means
        /// that every 5 grid lines is a major interval.
        /// </summary>
        public double MajorInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the horizontal axis.
        /// </summary>
        public GraphAxisType HorizontalType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the vertical axis.
        /// </summary>
        public GraphAxisType VerticalType
        {
            get;
            set;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.GraphAxis</c> class with the specified grid parameters.
        /// </summary>
        /// <param name="gridSize">The graph-space distance between grid lines.</param>
        /// <param name="majorInterval">The interval between major (darker, numbered) grid lines.</param>
        public GraphAxis(double gridSize, int majorInterval)
        {
            GridSize = gridSize;
            MajorInterval = majorInterval;

            HorizontalType = GraphAxisType.Radians;
            VerticalType = GraphAxisType.Radians;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.GraphAxis</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public GraphAxis(XElement xml)
        {
            GridSize = Double.Parse(xml.Element("GridSize").Value);
            MajorInterval = Double.Parse(xml.Element("MajorInterval").Value);

            HorizontalType = (GraphAxisType)Enum.Parse(typeof(GraphAxisType), xml.Element("HorizontalType").Value);
            VerticalType = (GraphAxisType)Enum.Parse(typeof(GraphAxisType), xml.Element("VerticalType").Value);
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
        public void PlotOnto(Graph graph, Graphics g, Size graphSize, PlottableParameters plotParams, PlotResolution resolution)
        {
            int originX, originY;
            graph.ToImageSpace(graphSize, 0, 0, out originX, out originY);

            PlotGridLinesOnto(graph, g, graphSize, plotParams, originX, originY);
            PlotAxesOnto(g, graphSize, plotParams, originX, originY);
        }

        /// <summary>
        /// Plots the main axis lines onto the graph, not including the grid lines.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="plotParams">The parameters used to plot this IPlottable.</param>
        /// <param name="originX">The X position of the origin on the screen.</param>
        /// <param name="originY">The Y position of the origin on the screen.</param>
        private void PlotAxesOnto(Graphics graphics, Size graphSize, PlottableParameters plotParams, int originX, int originY)
        {
            using (Pen axisPen = new Pen(plotParams.PlotColor))
            {
                if (originY >= 0 && originY < graphSize.Height)
                {
                    graphics.DrawLine(axisPen, 0, originY, graphSize.Width, originY);
                }
                if (originX >= 0 && originX < graphSize.Width)
                {
                    graphics.DrawLine(axisPen, originX, 0, originX, graphSize.Height);
                }
            }
        }

        /// <summary>
        /// Plots grid lines onto the graph.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="plotParams">The parameters used to plot this IPlottable.</param>
        /// <param name="originX">The X position of the origin on the screen.</param>
        /// <param name="originY">The Y position of the origin on the screen.</param>
        private void PlotGridLinesOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, int originX, int originY)
        {
            using (Pen majorPen = new Pen(plotParams.PlotColor.ColorAlpha(0.25)))
            using (Pen minorPen = new Pen(plotParams.PlotColor.ColorAlpha(0.15)))
            using (Brush valueBrush = new SolidBrush(plotParams.PlotColor.ColorAlpha(0.5)))
            {
                // the increment, in pixels, of each grid space
                double incrementX = HorizontalType.AxisTypeGridScale() * GridSize / graph.Parameters.HorizontalPixelScale,
                       incrementY = VerticalType.AxisTypeGridScale() * GridSize / graph.Parameters.VerticalPixelScale;

                // draw the variable names onto the axis
                graphics.DrawString(graph.Parameters.VerticalAxis.ToString(), SystemFonts.DefaultFont, valueBrush, (int)originX, 2);
                graphics.DrawString(graph.Parameters.HorizontalAxis.ToString(), SystemFonts.DefaultFont, valueBrush, (int)graphSize.Width - 16, (int)originY);

                // the unit suffix of the grid labels, for example the little circle for degrees
                string horizontalAxisSuffix = HorizontalType.AxisTypeExtension();
                string verticalAxisSuffix = VerticalType.AxisTypeExtension();
                
                // the axis label scale for the numbers along each axis
                // for example, axes plotted in degree mode are scaled such that 2*pi is displayed
                // as 360 degrees instead
                double horizontalAxisScale = GridSize * HorizontalType.AxisTypeLabelScale() * HorizontalType.AxisTypeGridScale();
                double verticalAxisScale = GridSize * VerticalType.AxisTypeLabelScale() * VerticalType.AxisTypeGridScale();

                // the remainder of the code in this method plots the gridlines from the origin outward.
                // this is potentially not the most efficient method of doing it, but it avoids floating-
                // point rounding errors resulting in axis labels like 2.00000000000017 without adding in
                // even more horrible code.
                // some of this code looks redundant, and it is. However as drawing the grid lines is a
                // (surprisingly) CPU intensive operation this method needs to be as fast as possible as
                // the grid is drawn every time the graph is

                double value = originX;
                int index = 0;
                // plot horizontal grid lines from the origin to the right of the page
                while (value < graphSize.Width)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        graphics.DrawLine(majorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                        graphics.DrawString(
                            String.Format("{0:0.####}{1}", index * horizontalAxisScale, horizontalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush, (int)value, (int)originY);
                    }
                    else
                    {
                        graphics.DrawLine(minorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                    }
                    value += incrementX; index++;
                }

                value = originX; index = 0;
                // plot hoz grid from origin to left of page
                while (value >= 0)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        graphics.DrawLine(majorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                        graphics.DrawString(
                            String.Format("{0:0.####}{1}", index * horizontalAxisScale, horizontalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush,
                            (int)value, (int)originY);
                    }
                    else
                    {
                        graphics.DrawLine(minorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                    }
                    value -= incrementX; index++;
                }

                value = originY; index = 0;
                // plot vertical grid lines from origin to bottom of page
                while (value < graphSize.Height)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        graphics.DrawLine(majorPen,
                            0, (int)value, graphSize.Width, (int)value);
                        graphics.DrawString(
                            String.Format("{0:0.####}{1}", index * verticalAxisScale, verticalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush,
                            (int)originX, (int)value);
                    }
                    else
                    {
                        graphics.DrawLine(minorPen,
                            0, (int)value, graphSize.Width, (int)value);
                    }
                    value += incrementY; index++;
                }

                value = originY; index = 0;
                // plot vert grid from origin to top of page
                while (value >= 0)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        graphics.DrawLine(majorPen,
                            0, (int)value, graphSize.Width, (int)value);
                        double vertical = graph.Parameters.VerticalPixelScale * index;
                        graphics.DrawString(
                            String.Format("{0:0.####}{1}", index * verticalAxisScale, verticalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush,
                            (int)originX, (int)value);
                    }
                    else
                    {
                        graphics.DrawLine(minorPen,
                            0, (int)value, graphSize.Width, (int)value);
                    }
                    value -= incrementY; index++;
                }
            }
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public XElement ToXml()
        {
            return new XElement("GraphAxis",
                new XElement("GridSize", GridSize),
                new XElement("MajorInterval", MajorInterval),
                new XElement("HorizontalType", HorizontalType.ToString()),
                new XElement("VerticalType", VerticalType.ToString()));
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
        /// Always returns true, as the appearance of the graph axes is not affected by the variables being plotted.
        /// </summary>
        /// <returns>Returns true.</returns>
        public bool CanPlot(char variable1, char variable2)
        {
            return true;
        }
    }
}