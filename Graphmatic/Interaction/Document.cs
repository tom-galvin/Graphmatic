using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Document : IXmlConvertible, IEnumerable<Resource>
    {
        private Dictionary<Guid, Resource> Resources;

        public string Author
        {
            get;
            protected set;
        }

        public DateTime CreationDate
        {
            get;
            protected set;
        }

        public string OriginalVersion
        {
            get;
            protected set;
        }

        public List<Page> PageOrder
        {
            get;
            protected set;
        }

        public Guid Guid
        {
            get;
            protected set;
        }

        public Resource CurrentResource
        {
            get;
            set;
        }

        public string BackupFileName
        {
            get
            {
                return String.Format("{0}.gmd", Guid.ToString());
            }
        }

        public Document()
        {
            Resources = new Dictionary<Guid, Resource>();
            PageOrder = new List<Page>();

            Author = Properties.Settings.Default.Username;
            OriginalVersion = Properties.Resources.VersionString;
            CurrentResource = null;
            CreationDate = DateTime.Now;
            Guid = Guid.NewGuid();
        }

        public Document(XElement xml)
        {
            if(xml.Name != "Document")
                throw new IOException("This file is not a Graphmatic document; it has the wrong root tag name.");
            if(xml.Attribute("Format") == null)
                throw new IOException("This file is an invalid Graphmatic document; it has no format attribute.");

            int formatVersion = Int32.Parse(xml.Attribute("Format").Value);
            if(formatVersion > 1)
                throw new IOException("This file is for a newer version of Graphmatic. " +
                    "Try opening this file in "+ xml.Attribute("Version").Value + " or newer.");
            Resources = new Dictionary<Guid, Resource>();

            Author = xml.Attribute("Author").Value;
            OriginalVersion = xml.Attribute("OriginalVersion").Value;
            CreationDate = DateTime.Parse(xml.Attribute("CreationDate").Value);
            Guid = Guid.Parse(xml.Attribute("ID").Value);

            var resourcesElements = xml.Element("Resources").Elements();
            foreach (XElement resourceElement in resourcesElements)
            {
                Resource resource = ResourceSerializationExtensionMethods.FromXml(resourceElement);
                Resources.Add(resource.Guid, resource);
            }

            PageOrder = new List<Page>();
            var pageOrder = xml.Element("PageOrder").Elements("Reference");
            foreach (XElement pageElement in pageOrder)
            {
                var page = FromGuid(Guid.Parse(pageElement.Attribute("ID").Value)) as Page;
                PageOrder.Add(page);
            }

            if (xml.Element("CurrentResource") != null)
                CurrentResource = FromGuid(Guid.Parse(xml.Element("CurrentResource").Value));
        }

        public Resource this[Guid guid]
        {
            get
            {
                return Resources[guid];
            }
            set
            {
                Resources[guid] = value;
            }
        }

        public void Add(Resource resource)
        {
            Resources.Add(resource.Guid, resource);
        }

        public void Remove(Guid guid)
        {
            Resources.Remove(guid);
        }

        public void Remove(Resource resource)
        {
            Remove(resource.Guid);
        }

        public void Save(string path, bool compressed)
        {
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            if (compressed)
            {
                stream = new GZipStream(stream, CompressionMode.Compress);
            }

            XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                ToXml());
            document.Save(stream);
            stream.Close();
        }

        public static Document Open(string path, bool compressed)
        {
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (compressed)
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }

            XDocument document = XDocument.Load(stream);
            stream.Close();
            return new Document(document.Root);
        }

        public Resource FromGuid(Guid guid)
        {
            return Resources[guid];
        }

        public XElement ToXml()
        {
            return new XElement("Document",
                new XAttribute("Format", 1), // file format version
                new XAttribute("Author", Author), // original author
                new XAttribute("Version", Properties.Resources.VersionString), // most recent editing version
                new XAttribute("OriginalVersion", OriginalVersion), // original editing version
                new XAttribute("CreationDate", CreationDate.ToString()), // date the document was created
                new XAttribute("ID", Guid.ToString()), // guid of the document
                new XElement("Resources",
                    Resources.Values.Select(r => r.ToXml())),
                new XElement("PageOrder",
                    PageOrder.Select(page => new XElement("Reference", new XAttribute("ID", page.Guid)))),
                CurrentResource != null ?
                    new XElement("CurrentResource", CurrentResource.Guid) :
                    null);

        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return ((IEnumerable<Resource>)Resources.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Resources.Values).GetEnumerator();
        }

        public void AddPageToPageOrder(Page page, Page after = null)
        {
            if (PageOrder.IndexOf(page) == -1)
            {
                int insertIndex = after == null ? PageOrder.Count : PageOrder.IndexOf(after);
                PageOrder.Insert(insertIndex, page);
            }
            else
            {
                throw new Exception("Page must not already be in the page order.");
            }
        }

        public void RemovePageFromPageOrder(Page page)
        {
            if (PageOrder.IndexOf(page) != -1)
            {
                PageOrder.Remove(page);
            }
            else
            {
                throw new Exception("Page must be in the page index.");
            }
        }

        public void SwapPages(Page page1, Page page2)
        {
            int page1Index = PageOrder.IndexOf(page1),
                page2Index = PageOrder.IndexOf(page2);

            if (page1Index == -1 || page2Index == -1)
            {
                throw new Exception("Both pages must be in the page order.");
            }
            else
            {
                PageOrder.RemoveAt(page1Index);
                PageOrder.Insert(page1Index, page2);
                PageOrder.RemoveAt(page2Index);
                PageOrder.Insert(page2Index, page1);
            }
        }
    }
}
