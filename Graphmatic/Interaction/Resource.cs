using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Resource : IXmlConvertible
    {
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual string Author
        {
            get;
            set;
        }

        public virtual DateTime CreationDate
        {
            get;
            set;
        }

        public Guid Guid
        {
            get;
            protected set;
        }

        public bool Hidden
        {
            get;
            set;
        }

        public virtual string Type
        {
            get
            {
                return "Resource";
            }
        }

        public Resource()
        {
            Author = Properties.Settings.Default.Username;
            CreationDate = DateTime.Now;
            Name = "New " + Type;
            Guid = Guid.NewGuid();
        }

        public Resource(XElement xml)
        {
            Name = xml.Attribute("Name").Value;
            Author = xml.Attribute("Author").Value;
            CreationDate = DateTime.Parse(xml.Attribute("CreationDate").Value);
            Description = xml.Element("Description").Value;
            Guid = Guid.Parse(xml.Attribute("ID").Value);
            Hidden = xml.Attribute("Hidden") != null ?
                Boolean.Parse(xml.Attribute("Hidden").Value) :
                false;
        }

        public virtual Image GetResourceIcon(bool large)
        {
            return
                Properties.Resources.Data32;
        }

        public virtual XElement ToXml()
        {
            return new XElement("Resource",
                new XAttribute("Name", Name),
                new XAttribute("Author", Author),
                new XAttribute("CreationDate", CreationDate),
                new XAttribute("ID", Guid),
                new XAttribute("Hidden", Hidden),
                new XElement("Description", Description));
        }
    }
}
