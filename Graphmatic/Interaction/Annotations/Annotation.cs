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
                new XElement("X", X),
                new XElement("Y", Y),
                new XElement("Width", Width),
                new XElement("Height", Height),
                new XElement("Color", Color.ToXmlString()));
        }

        private Rectangle GetScreenRectangle(Page page, Size graphSize, GraphParameters graphParams)
        {
            int x1, y1, x2, y2;

            page.Graph.ToImageSpace(graphSize, graphParams, X, Y, out x1, out y1);
            page.Graph.ToImageSpace(graphSize, graphParams, X + Width, Y + Height, out x2, out y2);
            return new Rectangle(x1, y1, x2 - x1, y1 - y2);
        }

        public virtual void DrawOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            Brush annotationBrush = new SolidBrush(Color);
            graphics.FillRectangle(annotationBrush, screenRectangle);
        }

        public virtual void SelectDrawOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            Pen selectBoxPen = new Pen(Color.Red, 2f);
            selectBoxPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawRectangle(selectBoxPen, screenRectangle);
        }

        public bool InSelection(Page page, Size graphSize, GraphParameters graphParams, Rectangle screenSelection)
        {
            Rectangle screenRectangle = GetScreenRectangle(page, graphSize, graphParams);
            return screenRectangle.IntersectsWith(screenSelection);
        }

        public int ScreenDistance(Page page, Size graphSize, GraphParameters graphParams, Point screenSelection)
        {
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
