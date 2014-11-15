using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Page : Resource
    {
        public Page(XElement xml)
            : base(xml)
        {
            
        }

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            return baseElement;
        }
    }
}
