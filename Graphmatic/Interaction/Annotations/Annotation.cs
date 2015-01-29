using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction.Annotations
{
    /// <summary>
    /// Represents an annotation on a Graphmatic page.
    /// </summary>
    [GraphmaticObject]
    public class Annotation : IXmlConvertible
    {
        /// <summary>
        /// Gets or sets the X co-ordinate of the annotation on the page, in graph space.
        /// </summary>
        public double X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y co-ordinate of the annotation on the page, in graph space.
        /// </summary>
        public double Y
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the annotation on the page, in graph space.
        /// </summary>
        public double Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the annotation on the page, in graph space.
        /// </summary>
        public double Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Color of the annotation on the page.<para/>
        /// Whether this has an effect on the appearance of the specific annotation or not
        /// depends on the implementation.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Annotations.Annotation</c> class.
        /// </summary>
        public Annotation()
        {

        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Annotations.Annotation</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public Annotation(XElement xml)
        {
            X = Double.Parse(xml.Element("X").Value);
            Y = Double.Parse(xml.Element("Y").Value);
            Width = Double.Parse(xml.Element("Width").Value);
            Height = Double.Parse(xml.Element("Height").Value);
            Color = ResourceSerializationExtensionMethods.XmlStringToColor(xml.Element("Color").Value);
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public virtual XElement ToXml()
        {
            return new XElement("Annotation",
                new XElement("X", X.ToString("0.####")),
                new XElement("Y", Y.ToString("0.####")),
                new XElement("Width", Width.ToString("0.####")),
                new XElement("Height", Height.ToString("0.####")),
                new XElement("Color", Color.ToXmlString()));
        }

        /// <summary>
        /// Creates a Rectangle representing the area of this Annotation on the screen.
        /// </summary>
        /// <param name="page">The Page that this Annotation is on.</param>
        /// <param name="graphSize">The size of the graph on the screen.</param>
        protected Rectangle GetScreenRectangle(Page page, Size graphSize)
        {
            int x1, y1, x2, y2;

            page.Graph.ToImageSpace(graphSize, X, Y, out x1, out y2);
            page.Graph.ToImageSpace(graphSize, X + Width, Y + Height, out x2, out y1);
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Determines whether a point is inside the top-right yellow resizing node on the
        /// visual display of the annotation or not.
        /// </summary>
        /// <param name="page">The Page that this Annotation is on.</param>
        /// <param name="graphSize">The size of the graph on the screen.</param>
        /// <param name="point">The point to check for proximity to the resizing node.</param>
        public bool IsPointInResizeNode(Page page, Size graphSize, Point point)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize);
            int manhattanDistance =
                Math.Abs(screenRectangle.X + screenRectangle.Width - point.X) +
                Math.Abs(screenRectangle.Y - point.Y);
            return manhattanDistance < 9;
        }

        /// <summary>
        /// Draws this Annotation onto <paramref name="page"/>.
        /// </summary>
        /// <param name="page">The Graph to plot this Annotation onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this Annotation.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the page's graph's parameters.</param>
        /// <param name="resolution">The plotting resolution to use. This does not have an effect for data sets.</param>
        public virtual void DrawAnnotationOnto(Page page, Graphics graphics, Size graphSize, PlotResolution resolution)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize);
            using (Brush annotationBrush = new SolidBrush(Color))
            {
                graphics.FillRectangle(annotationBrush, screenRectangle);
            }
        }

        /// <summary>
        /// Draws the selection indicator around this Annotation onto <paramref name="page"/>.
        /// </summary>
        /// <param name="page">The Page to plot this Annotation onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this Annotation.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the page's graph's parameters.</param>
        /// <param name="resolution">The plotting resolution to use. This does not have an effect for data sets.</param>
        public virtual void DrawSelectionIndicatorOnto(Page page, Graphics graphics, Size graphSize, PlotResolution resolution)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize);
            using (Pen selectBoxPen = new Pen(Color.Red, 2f))
            {
                Brush resizeNodeBrush = Brushes.Yellow;
                selectBoxPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                graphics.DrawRectangle(selectBoxPen, screenRectangle);
                graphics.FillEllipse(resizeNodeBrush,
                    screenRectangle.X + screenRectangle.Width - 4,
                    screenRectangle.Y - 4,
                    9, 9);
                graphics.DrawEllipse(selectBoxPen,
                    screenRectangle.X + screenRectangle.Width - 4,
                    screenRectangle.Y - 4,
                    9, 9);
            }
        }

        /// <summary>
        /// Determines whether this Annotation is inside the given selection rectangle. This is determined by
        /// checking whether the display and selection rectangles intersect.
        /// </summary>
        /// <param name="page">The Page to plot this Annotation onto.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the page's graph's parameters.</param>
        /// <param name="screenSelection">The selection rectangle to check.</param>
        /// <returns>Returns true if this Annotation is inside the given selection rectangle; false otherwise.</returns>
        public virtual bool IsAnnotationInSelection(Page page, Size graphSize, Rectangle screenSelection)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize);
            return screenRectangle.IntersectsWith(screenSelection);
        }

        /// <summary>
        /// Determines the distance from this Annotation to a given point on the screen.
        /// This returns the closest distance from one of the edges of the screen rectangle.
        /// </summary>
        /// <param name="page">The Page to plot this Annotation onto.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the page's graph's parameters.</param>
        /// <param name="screenSelection">The selection point to check.</param>
        /// <returns>Returns the closest distance from one of the edges of the screen rectangle.</returns>
        public virtual int DistanceToPointOnScreen(Page page, Size graphSize, Point screenSelection)
        {
            // if selection is in resize node, it's ALWAYS in the boundaries of the selection
            if (IsPointInResizeNode(page, graphSize, screenSelection)) return -1;

            Rectangle screenRectangle = GetScreenRectangle(page, graphSize);
            int distFromBottom = screenSelection.Y - screenRectangle.Bottom;
            int distFromTop = screenRectangle.Top - screenSelection.Y;
            int distFromLeft = screenRectangle.Left - screenSelection.X;
            int distFromRight = screenSelection.X - screenRectangle.Right;

            if (distFromTop < 0 && distFromBottom < 0 &&
                distFromLeft < 0 && distFromRight < 0) return -1;

            int minimum = int.MaxValue;
            if (distFromBottom >= 0 && distFromBottom < minimum) minimum = distFromBottom;
            if (distFromTop >= 0 && distFromTop < minimum) minimum = distFromTop;
            if (distFromLeft >= 0 && distFromLeft < minimum) minimum = distFromLeft;
            if (distFromRight >= 0 && distFromRight < minimum) minimum = distFromRight;
            return minimum;
        }

        public virtual void UpdateReferences(Document document)
        {
        }
    }
}
