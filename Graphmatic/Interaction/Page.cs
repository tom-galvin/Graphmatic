using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Page : IXmlConvertible
    {
        public string PageName
        {
            get;
            set;
        }

        public Page(string pageName)
        {
            PageName = pageName;
        }

        public XElement ToXml()
        {
            return null;
        }
    }
}
