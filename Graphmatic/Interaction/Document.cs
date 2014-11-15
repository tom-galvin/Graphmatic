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

        public Document()
        {
            Resources = new Dictionary<Guid, Resource>();
        }

        public Document(XElement xml)
        {
            Resources = new Dictionary<Guid, Resource>();

            var resourcesElements = xml.Element("Resources").Elements();
            foreach (XElement resourceElement in resourcesElements)
            {
                Resource resource = ResourceSerializationExtensionMethods.FromXml(resourceElement);
                Resources.Add(resource.Guid, resource);
            }
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

        public XElement ToXml()
        {
            return new XElement("Document",
                new XElement("Resources",
                    Resources.Values.Select(r => r.ToXml())));
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return ((IEnumerable<Resource>)Resources.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Resources.Values).GetEnumerator();
        }
    }
}
