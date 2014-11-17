using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class HtmlPage : Resource
    {
        public override string Type
        {
            get
            {
                return "HTML Page";
            }
        }

        public string HtmlData
        {
            get;
            protected set;
        }

        public override Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.HtmlPage32 :
                Properties.Resources.HtmlPage16;
        }

        public HtmlPage(string htmlData)
            : base()
        {
            HtmlData = htmlData;
        }

        public HtmlPage(XElement xml)
            : base(xml)
        {
            HtmlData = xml.Element("Data").Value.FromBase64();
        }

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "HtmlPage";
            baseElement.Add(new XElement("Data", HtmlData.ToBase64()));
            return baseElement;
        }
    }
}
