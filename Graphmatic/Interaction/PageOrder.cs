using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class PageOrder : Resource
    {
        public override string Type
        {
            get
            {
                return "Page Order Data";
            }
        }

        public List<Page> Pages
        {
            get;
            protected set;
        }

        public PageOrder()
        {
            Pages = new List<Page>();
        }

        public PageOrder(Document document, XElement xml)
            : base(xml)
        {
            XElement pagesElement = xml.Element("Pages");
            foreach (XElement pageElement in pagesElement.Elements("Page"))
            {
                Guid guid = Guid.Parse(pageElement.Attribute("Guid").Value);
                Pages.Add(document.FromGuid(guid) as Page);
            }
        }
    }
}
