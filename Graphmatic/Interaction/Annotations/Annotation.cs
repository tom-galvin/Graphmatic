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

        public virtual void PlotOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            int x1, y1, x2, y2;

            Brush annotationBrush = new SolidBrush(Color);
            page.Graph.ToImageSpace(graphSize, graphParams, X, Y, out x1, out y1);
            page.Graph.ToImageSpace(graphSize, graphParams, X + Width, Y + Height, out x2, out y2);
            graphics.FillRectangle(annotationBrush, x1, y1, x2 - x1, y2 - y1);
        }

        public virtual void UpdateReferences(Document document)
        {
        }
    }
}
