using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Represents a set of parameters used to plot a <c>Graphmatic.Interaction.Plotting.IPlottable</c> to a <c>Graphmatic.Interaction.Plotting.Graph</c>.
    /// </summary>
    [GraphmaticObject]
    public class PlottableParameters : IXmlConvertible
    {
        /// <summary>
        /// The color that this <c>IPlottable</c> will be plotted in on the screen.
        /// </summary>
        public Color PlotColor
        {
            get;
            set;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.PlottableParameters</c> class.
        /// </summary>
        public PlottableParameters()
        {

        }

        /// <summary>
        /// Initialize a new instance of the <c>Graphmatic.Interaction.Plotting.PlottableParameters</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The serialized XML data to use for deserialization.</param>
        public PlottableParameters(XElement xml)
        {
            PlotColor = ResourceSerializationExtensionMethods.XmlStringToColor(xml.Element("PlotColor").Value);
        }

        /// <summary>
        /// Serializes this parameter set into its XML representation.
        /// </summary>
        public XElement ToXml()
        {
            return new XElement("PlottableParameters",
                new XElement("PlotColor", PlotColor.ToXmlString()));
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
        }
    }
}
