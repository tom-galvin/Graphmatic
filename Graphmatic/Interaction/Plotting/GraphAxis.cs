using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphAxis : IPlottable, IXmlConvertible
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

        public GraphAxisType HorizontalType
        {
            get;
            set;
        }

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

        public void PlotOnto(Graph graph, Graphics g, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams, PlotResolution resolution)
        {
            int axisX, axisY;
            graph.ToImageSpace(graphSize, graphParams, 0, 0, out axisX, out axisY);

            PlotGridLinesOnto(g, graphSize, plotParams, graphParams, axisX, axisY);
            PlotAxesOnto(g, graphSize, plotParams, axisX, axisY);
        }

        private void PlotAxesOnto(Graphics g, Size graphSize, PlottableParameters plotParams, int axisX, int axisY)
        {
            using (Pen axisPen = new Pen(plotParams.PlotColor))
            {
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

        private void PlotGridLinesOnto(Graphics g, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams, int axisX, int axisY)
        {
            using (Pen majorPen = new Pen(plotParams.PlotColor.ColorAlpha(0.5)))
            using (Pen minorPen = new Pen(plotParams.PlotColor.ColorAlpha(0.333)))
            using (Brush valueBrush = majorPen.Brush)
            {
                double incrementX = GridSize / graphParams.HorizontalPixelScale,
                       incrementY = GridSize / graphParams.VerticalPixelScale;

                g.DrawString(graphParams.VerticalAxis.ToString(), SystemFonts.DefaultFont, valueBrush, (int)axisX, 2);
                g.DrawString(graphParams.HorizontalAxis.ToString(), SystemFonts.DefaultFont, valueBrush, (int)graphSize.Width - 16, (int)axisY);

                string horizontalAxisSuffix = HorizontalType.AxisTypeExtension();
                string verticalAxisSuffix = VerticalType.AxisTypeExtension();

                double horizontalAxisScale = GridSize * HorizontalType.AxisTypeScale();
                double verticalAxisScale = GridSize * VerticalType.AxisTypeScale();

                double value = axisX;
                int index = 0;
                // plot horizontal grid lines from the origin to the right of the page
                while (value < graphSize.Width)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        g.DrawLine(majorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                        g.DrawString(
                            String.Format("{0:0.####}{1}", index * horizontalAxisScale, horizontalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush, (int)value, (int)axisY);
                    }
                    else
                    {
                        g.DrawLine(minorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                    }
                    value += incrementX; index++;
                }

                value = axisX; index = 0;
                // plot hoz grid from origin to left of page
                while (value >= 0)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        g.DrawLine(majorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                        g.DrawString(
                            String.Format("{0:0.####}{1}", index * horizontalAxisScale, horizontalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush,
                            (int)value, (int)axisY);
                    }
                    else
                    {
                        g.DrawLine(minorPen,
                            (int)value, 0, (int)value, graphSize.Height);
                    }
                    value -= incrementX; index++;
                }

                value = axisY; index = 0;
                // plot vertical grid lines from origin to bottom of page
                while (value < graphSize.Height)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        g.DrawLine(majorPen,
                            0, (int)value, graphSize.Width, (int)value);
                        g.DrawString(
                            String.Format("{0:0.####}{1}", index * verticalAxisScale, verticalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush,
                            (int)axisX, (int)value);
                    }
                    else
                    {
                        g.DrawLine(minorPen,
                            0, (int)value, graphSize.Width, (int)value);
                    }
                    value += incrementY; index++;
                }

                value = axisY; index = 0;
                // plot vert grid from origin to top of page
                while (value >= 0)
                {
                    bool major = index % MajorInterval == 0;
                    if (major)
                    {
                        g.DrawLine(majorPen,
                            0, (int)value, graphSize.Width, (int)value);
                        double vertical = graphParams.VerticalPixelScale * index;
                        g.DrawString(
                            String.Format("{0:0.####}{1}", index * verticalAxisScale, verticalAxisSuffix),
                            SystemFonts.DefaultFont,
                            valueBrush,
                            (int)axisX, (int)value);
                    }
                    else
                    {
                        g.DrawLine(minorPen,
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


        public bool CanPlot(char variable1, char variable2)
        {
            return true;
        }
    }
}