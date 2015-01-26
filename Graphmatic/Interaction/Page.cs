using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Interaction.Annotations;
using Graphmatic.Interaction.Plotting;

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

        public Color BackgroundColor
        {
            get;
            set;
        }

        public Graph Graph
        {
            get;
            protected set;
        }

        public List<Annotation> Annotations
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Page</c> class with nothing on it.
        /// </summary>
        public Page()
        {
            BackgroundColor = Properties.Settings.Default.DefaultPageColor;
            Graph = new Graph();
            Annotations = new List<Annotation>();
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Page</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public Page(XElement xml)
            : base(xml)
        {
            BackgroundColor = ResourceSerializationExtensionMethods.XmlStringToColor(xml.Element("BackgroundColor").Value);
            Graph = new Graph(xml.Element("Graph"));
            Annotations = new List<Annotation>(
                xml
                .Element("Annotations")
                .Elements()
                .Select(x => x.Deserialize<Annotation>(SerializationExtensionMethods.AnnotationName)));
        }

        /// <summary>
        /// Gets the resource icon describing this resource type in the user interface.<para/>
        /// This will return different icons if overriden by a derived type.
        /// </summary>
        /// <param name="large">Whether to return the large icon or not. Large icons are 32*32 and
        /// small icons are 16*16.</param>
        public override Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.Page32 :
                Properties.Resources.Page16;
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Page";
            baseElement.Add(new XElement("BackgroundColor", BackgroundColor.ToXmlString()));
            baseElement.Add(Graph.ToXml());
            baseElement.Add(new XElement("Annotations",
                Annotations.Select(a =>
                a.ToXml())));
            return baseElement;
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
        public override void UpdateReferences(Document document)
        {
            Graph.UpdateReferences(document);
        }

        /// <summary>
        /// Called when another resource in the Document containing this resource is modified.
        /// This is used so certain objects which refer to other Resources can update themselves accordingly, for
        /// example by redrawing themselves.
        /// </summary>
        /// <param name="resource">The other resource which was modified.</param>
        /// <param name="type">The type of resource modification which took place.</param>
        public override void ResourceModified(Resource resource, ResourceModifyType type)
        {
            base.ResourceModified(resource, type);
            if (resource == this)
            {
                Graph.OnUpdate();
            }
            else
            {
                Graph.ResourceModified(resource, type);
            }
        }
    }
}
