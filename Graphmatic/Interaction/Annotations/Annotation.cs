using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction.Annotations
{
    public class Annotation : IXmlConvertible
    {
        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public Annotation()
        {

        }

        public Annotation(XElement xml)
        {
            X = Double.Parse(xml.Element("X").Value);
            Y = Double.Parse(xml.Element("Y").Value);
            Width = Double.Parse(xml.Element("Width").Value);
            Height = Double.Parse(xml.Element("Height").Value);
            Color = ResourceSerializationExtensionMethods.XmlStringToColor(xml.Element("Color").Value);
        }

        public virtual XElement ToXml()
        {
            return new XElement("Annotation",
                new XElement("X", X.ToString("0.####")),
                new XElement("Y", Y.ToString("0.####")),
                new XElement("Width", Width.ToString("0.####")),
                new XElement("Height", Height.ToString("0.####")),
                new XElement("Color", Color.ToXmlString()));
        }

        protected Rectangle GetScreenRectangle(Page page, Size graphSize, GraphParameters graphParams)
        {
            int x1, y1, x2, y2;

            page.Graph.ToImageSpace(graphSize, graphParams, X, Y, out x1, out y2);
            page.Graph.ToImageSpace(graphSize, graphParams, X + Width, Y + Height, out x2, out y1);
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public bool IsPointInResizeNode(Page page, Size graphSize, GraphParameters graphParams, Point point)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            int manhattanDistance =
                Math.Abs(screenRectangle.X + screenRectangle.Width - point.X) +
                Math.Abs(screenRectangle.Y - point.Y);
            return manhattanDistance < 9;
        }

        public virtual void DrawAnnotationOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            using (Brush annotationBrush = new SolidBrush(Color))
            {
                graphics.FillRectangle(annotationBrush, screenRectangle);
            }
        }

        public virtual void DrawSelectionIndicatorOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            using (Pen selectBoxPen = new Pen(Color.Red, 2f))
            {
                Brush resizeNotchBrush = Brushes.Yellow;
                selectBoxPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                graphics.DrawRectangle(selectBoxPen, screenRectangle);
                graphics.FillEllipse(resizeNotchBrush,
                    screenRectangle.X + screenRectangle.Width - 4,
                    screenRectangle.Y - 4,
                    9, 9);
                graphics.DrawEllipse(selectBoxPen,
                    screenRectangle.X + screenRectangle.Width - 4,
                    screenRectangle.Y - 4,
                    9, 9);
            }
        }

        public virtual bool IsAnnotationInSelection(Page page, Size graphSize, GraphParameters graphParams, Rectangle screenSelection)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            return screenRectangle.IntersectsWith(screenSelection);
        }

        public virtual int DistanceToPointOnScreen(Page page, Size graphSize, GraphParameters graphParams, Point screenSelection)
        {
            // if selection is in resize node, it's ALWAYS in the boundaries of the selection
            if (IsPointInResizeNode(page, graphSize, graphParams, screenSelection)) return -1;

            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
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
