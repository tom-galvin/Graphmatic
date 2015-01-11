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

        public bool Removed
        {
            get;
            set;
        }

        public Resource()
        {
            Removed = false;
            Author = Properties.Settings.Default.Username;
            CreationDate = DateTime.Now;
            Name = "New " + Type;
            Guid = Guid.NewGuid();
        }

        public Resource(XElement xml)
        {
            Removed = false;
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
            return large ?
                Properties.Resources.Data32 :
                Properties.Resources.Data16;
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

        public XElement ToResourceReference()
        {
            return new XElement("Reference",
                new XAttribute("ID", Guid));
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void UpdateReferences(Document document)
        {

        }

        public virtual void ResourceModified(Resource resource, ResourceModifyType type)
        {

        }
    }
}
