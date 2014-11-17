using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Page : Resource
    {
        public override string Type
        {
            get
            {
                return "Page";
            }
        }

        public Color BackgroundColor
        {
            get;
            set;
        }

        public Page()
        {
            BackgroundColor = Properties.Settings.Default.DefaultPageColor;
        }

        public Page(XElement xml)
            : base(xml)
        {
            BackgroundColor = ResourceSerializationExtensionMethods.XmlStringToColor(xml.Element("BackgroundColor").Value);
        }

        public override Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.Page :
                Properties.Resources.Document16;
        }

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Page";
            baseElement.Add(new XElement("BackgroundColor", BackgroundColor.ToXmlString()));
            return baseElement;
        }
    }
}
