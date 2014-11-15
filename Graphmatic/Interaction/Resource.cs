using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Resource : IXmlConvertible
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public Guid Guid
        {
            get;
            protected set;
        }

        public Resource()
        {
            Author = Program.Settings.UserName;
            CreationDate = DateTime.Now;
            Name = "New " + GetType().Name;
            Guid = Guid.NewGuid();
        }

        public Resource(XElement xml)
        {
            Name = xml.Attribute("Name").Value;
            Author = xml.Attribute("Author").Value;
            CreationDate = DateTime.Parse(xml.Attribute("CreationDate").Value);
            Description = xml.Element("Description").Value;
            Guid = Guid.Parse(xml.Attribute("ID").Value);
        }

        public virtual XElement ToXml()
        {
            return new XElement("Resource",
                new XAttribute("Name", Name),
                new XAttribute("Author", Author),
                new XAttribute("CreationDate", CreationDate),
                new XAttribute("ID", Guid),
                new XElement("Description", Description));
        }
    }
}
