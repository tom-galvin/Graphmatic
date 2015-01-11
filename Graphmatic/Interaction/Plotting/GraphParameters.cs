using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphParameters : IXmlConvertible
    {
        public double CenterHorizontal
        {
            get;
            set;
        }

        public double CenterVertical
        {
            get;
            set;
        }

        public double HorizontalPixelScale
        {
            get;
            set;
        }

        public double VerticalPixelScale
        {
            get;
            set;
        }

        public char HorizontalAxis
        {
            get;
            set;
        }

        public char VerticalAxis
        {
            get;
            set;
        }

        public GraphParameters()
        {
            CenterHorizontal = CenterVertical = 0;
            HorizontalPixelScale = VerticalPixelScale = 0.05;

            HorizontalAxis = Properties.Settings.Default.DefaultVariable1;
            VerticalAxis = Properties.Settings.Default.DefaultVariable2;
        }

        public GraphParameters(XElement xml)
        {
            CenterHorizontal = Double.Parse(xml.Element("CenterHorizontal").Value);
            CenterVertical = Double.Parse(xml.Element("CenterVertical").Value);
            HorizontalPixelScale = Double.Parse(xml.Element("HorizontalPixelScale").Value);
            VerticalPixelScale = Double.Parse(xml.Element("VerticalPixelScale").Value);

            HorizontalAxis = Char.Parse(xml.Element("HorizontalAxis").Value);
            VerticalAxis = Char.Parse(xml.Element("VerticalAxis").Value);
        }

        public XElement ToXml()
        {
            return new XElement("GraphParameters",
                new XElement("CenterHorizontal", CenterHorizontal),
                new XElement("CenterVertical", CenterVertical),
                new XElement("HorizontalPixelScale", HorizontalPixelScale),
                new XElement("VerticalPixelScale", VerticalPixelScale),
                new XElement("HorizontalAxis", HorizontalAxis),
                new XElement("VerticalAxis", VerticalAxis));
        }

        public void UpdateReferences(Document document)
        {
        }
    }
}
