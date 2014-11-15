using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic
{
    public class Settings : IXmlConvertible
    {
        public string UserName
        {
            get;
            set;
        }

        public Color DefaultPageColor
        {
            get;
            set;
        }

        public Settings()
        {
            UserName = Environment.UserName;
            DefaultPageColor = Color.AntiqueWhite;
        }

        public Settings(XElement xml)
        {
            UserName = xml.Element("UserName").Value;
            DefaultPageColor = Color.FromArgb(Int32.Parse(xml.Element("DefaultPageColor").Value));
        }

        public XElement ToXml()
        {
            return new XElement("Settings",
                new XElement("UserName", UserName),
                new XElement("DefaultPageColor", DefaultPageColor.ToArgb()));
        }
    }
}
