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
        public override string Type
        {
            get
            {
                return "Page";
            }
        }

        public override bool Hidden
        {
            get
            {
                return false;
            }
        }

        public Page(XElement xml)
            : base(xml)
        {
            
        }

        public override System.Drawing.Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.Page :
                Properties.Resources.Document16;
        }

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            return baseElement;
        }
    }
}
