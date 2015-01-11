using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    public class PlottableParameters : IXmlConvertible
    {
        public Color PlotColor
        {
            get;
            set;
        }

        public PlottableParameters()
        {

        }

        public PlottableParameters(XElement xml)
        {
            PlotColor = ResourceSerializationExtensionMethods.XmlStringToColor(xml.Element("PlotColor").Value);
        }

        public XElement ToXml()
        {
            return new XElement("PlottableParameters",
                new XElement("PlotColor", PlotColor.ToXmlString()));
        }

        public void UpdateReferences(Document document)
        {
        }
    }
}
