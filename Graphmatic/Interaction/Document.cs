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
    /// <summary>
    /// Represents a document, which may contain many Graphmatic resources.
    /// </summary>
    [GraphmaticObject]
    public class Document : IXmlConvertible, IEnumerable<Resource>
    {
        /// <summary>
        /// The resources contained in this document, indexed by their GUIDs.
        /// </summary>
        private Dictionary<Guid, Resource> Resources;

        /// <summary>
        /// Gets the author of this document.
        /// This is set when the document is first created.
        /// </summary>
        public string Author
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the date and time at which this document was first created.
        /// </summary>
        public DateTime CreationDate
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the version string of the version of Graphmatic used to originally
        /// create this document, used to aid in error diagnosis for the end user.
        /// </summary>
        public string OriginalVersion
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a list containing the order of pages in this document.
        /// </summary>
        public List<Page> PageOrder
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the ID of this document, used to uniquely identify the document (for
        /// example in the loading and saving of backup files.)
        /// </summary>
        public Guid Guid
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the resource currently being edited by the user.
        /// </summary>
        public Resource CurrentResource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the file name of the backup document to save, which is in the format
        /// <code>&lt;GUID&gt;.gmd</code>
        /// </summary>
        public string BackupFileName
        {
            get
            {
                return String.Format("{0}.gmd", Guid.ToString());
            }
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Document</c> class with nothing in it.
        /// </summary>
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

        /// <summary>
        /// Initializes the descriptive attributes and properties of this document from the given XML data, or throwing
        /// an <c>IOException</c> if the data contained in the XML is invalid.
        /// </summary>
        /// <param name="xml">The serialized XML data representing this document.</param>
        private void InitializeAttributes(XElement xml)
        {
            if (xml.Name != "Document")
                throw new IOException("This file is not a Graphmatic document; it has the wrong root tag name.");
            if (xml.Attribute("Format") == null)
                throw new IOException("This file is an invalid Graphmatic document; it has no format attribute.");

            int formatVersion = Int32.Parse(xml.Attribute("Format").Value);
            if (formatVersion > 1)
                throw new IOException("This file is for a newer version of Graphmatic. " +
                    "Try opening this file in " + xml.Attribute("Version").Value + " or newer.");

            Resources = new Dictionary<Guid, Resource>();

            Author = xml.Attribute("Author").Value;
            OriginalVersion = xml.Attribute("OriginalVersion").Value;
            CreationDate = DateTime.Parse(xml.Attribute("CreationDate").Value);
            Guid = Guid.Parse(xml.Attribute("ID").Value);
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Document</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public Document(XElement xml)
        {
            InitializeAttributes(xml);

            try
            {
                var resourcesElements = xml.Element("Resources").Elements();
                foreach (XElement resourceElement in resourcesElements)
                {
                    Resource resource = resourceElement.Deserialize<Resource>();
                    Resources.Add(resource.Guid, resource);
                }

                PageOrder = new List<Page>();
                var pageOrder = xml.Element("PageOrder").Elements("Reference");
                foreach (XElement pageElement in pageOrder)
                {
                    var page = this[Guid.Parse(pageElement.Attribute("ID").Value)] as Page;
                    PageOrder.Add(page);
                }

                if (xml.Element("CurrentResource") != null)
                    CurrentResource = this[Guid.Parse(xml.Element("CurrentResource").Value)];

                UpdateReferences(this);
            }
            catch (Exception ex)
            {
                if (OriginalVersion !=
                    Properties.Resources.VersionString)
                {
                    throw new IOException(String.Format(
                        "The file was made in another Graphmatic version \"{0}\" (you are running version \"{1}\"). Try " +
                        "opening the file in Graphmatic \"{0}\", or alternatively, ask the initial author of the " +
                        "file ({2}) for an updated version of the document.\r\n\r\nIf this is urgent, try reporting the " +
                        "issue to the creator of the program. Provide a screenshot of this dialog with the report.\r\n\r\n" +
                        "{3}: {4}\r\n{5}",
                        OriginalVersion,
                        Properties.Resources.VersionString,
                        Author,
                        ex.GetType().Name,
                        ex.Message,
                        ex.StackTrace));
                }
                else
                {
                    throw new IOException(String.Format(
                            "Ask the initial author of the file ({0}) for an updated " +
                            "version of the document.\r\n\r\nIf this is urgent, try reporting the " +
                            "issue to the creator of the program. Provide a screenshot of this dialog with the report.\r\n\r\n" +
                            "{1}: {2}\r\n{3}",
                            Author,
                            ex.GetType().Name,
                            ex.Message,
                            ex.StackTrace));
                }
            }
        }

        /// <summary>
        /// Gets a resource contained within this document.
        /// </summary>
        /// <param name="guid">The GUID of the resource to get from the document.</param>
        /// <returns>The resource with the GUID contained in <paramref name="guid"/>.</returns>
        public Resource this[Guid guid]
        {
            get
            {
                return Resources[guid];
            }
        }

        /// <summary>
        /// Adds a resource to this document.
        /// </summary>
        /// <param name="resource">The resource to add to the document.</param>
        public void Add(Resource resource)
        {
            Resources.Add(resource.Guid, resource);
            foreach (Resource otherResource in Resources.Values)
            {
                otherResource.ResourceModified(resource, ResourceModifyType.Add);
            }
        }

        /// <summary>
        /// Removes a resource from this document.
        /// </summary>
        /// <param name="guid">The GUID of the resource to remove.</param>
        public void Remove(Guid guid)
        {
            Remove(Resources[guid]);
        }

        /// <summary>
        /// Removes a resource from this document.
        /// </summary>
        /// <param name="resource">The resource to remove.</param>
        public void Remove(Resource resource)
        {
            foreach (Resource otherResource in Resources.Values)
            {
                otherResource.ResourceModified(resource, ResourceModifyType.Remove);
            }
            if (PageOrder.Contains(resource as Page))
                PageOrder.Remove(resource as Page);
            Resources.Remove(resource.Guid);
        }

        /// <summary>
        /// Determines whether a resource exists in this document with the specified GUID.
        /// </summary>
        /// <param name="guid">The GUID to check for in this document.</param>
        /// <returns>Returns true if a resource exists in this document with the GUID <paramref name="guid"/>;
        /// false otherwise.</returns>
        public bool Contains(Guid guid)
        {
            return Resources.ContainsKey(guid);
        }

        /// <summary>
        /// Saves this document to the file system at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to save the document to.</param>
        /// <param name="compressed">Whether the document should be compressed with Gzip or not.</param>
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

        /// <summary>
        /// Opens a document from the file system, optionally decompressing it in the process.
        /// </summary>
        /// <param name="path">The path to the file representing the document to be opened.</param>
        /// <param name="compressed">Whether the file should be decompressed with Gzip or not.</param>
        /// <returns>Returns the deserialized and loaded Document representing the file at <paramref name="path"/>.</returns>
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

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
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

        /// <summary>
        /// Adds a page to the list describing the order of pages in the document.
        /// </summary>
        /// <param name="page">The page to add.</param>
        /// <param name="after">The page to add <paramref name="page"/> after, or <c>null</c> to add
        /// the page to the end of the order.</param>
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

        /// <summary>
        /// Removes a page from the list describing the order of pages in the document.
        /// </summary>
        /// <param name="page">The page to remove.</param>
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

        /// <summary>
        /// Swaps the order of two pages in the list describing the order of pages in the document.
        /// </summary>
        /// <param name="page1">The pages to swap.</param>
        /// <param name="page2">The pages to swap.</param>
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

        /// <summary>
        /// Updates any references to other resources in the document to point to the correct resource.<para/>
        /// This method (and related method <c>Graphmatic.Interaction.Resource.ToResourceReference()</c>) are
        /// needed because certain resources, such as the Page resource, can refer to other resources from within
        /// them (for example if a Page contains a plotted DataSet). However, if the Page is deserialized before
        /// the DataSet, then it will not have an object to refer to. Thus, the resource reference system is used,
        /// whereby certain objects (such as the Page) keep track of which resources they need to refer to later on,
        /// and only actually dereference the Resource references (via the resource's GUID) after all resources in
        /// the document have been deserialized.
        /// </summary>
        /// <param name="document">The parent document containing this resource, and any other resources that this
        /// resource may point to.</param>
        public void UpdateReferences(Document document)
        {
            foreach (Resource resource in Resources.Values)
            {
                resource.UpdateReferences(document);
            }
        }

        /// <summary>
        /// Notify resources in the document that a modification has taken place. This allows certain resources
        /// (such as pages) to update any respective user interfaces upon the event that a resource contained
        /// within those resources is modified.
        /// </summary>
        /// <param name="resource">The resource which was modified.</param>
        public void NotifyResourceModified(Resource resource)
        {
            foreach (Resource otherResource in Resources.Values)
            {
                otherResource.ResourceModified(resource, ResourceModifyType.Modify);
            }
        }
    }
}
