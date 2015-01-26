using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphParameters : IXmlConvertible
    {
        public double CenterHorizontal
        {
            get;
            set;
        }

        public double CenterVertical
        {
            get;
            set;
        }

        public double HorizontalPixelScale
        {
            get;
            set;
        }

        public double VerticalPixelScale
        {
            get;
            set;
        }

        public char HorizontalAxis
        {
            get;
            set;
        }

        public char VerticalAxis
        {
            get;
            set;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.GraphParameters</c> class.
        /// </summary>
        public GraphParameters()
        {
            CenterHorizontal = CenterVertical = 0;
            HorizontalPixelScale = VerticalPixelScale = 0.05;

            HorizontalAxis = Properties.Settings.Default.DefaultVariable1;
            VerticalAxis = Properties.Settings.Default.DefaultVariable2;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Graphmatic.Interaction.Plotting.GraphParameters</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The serialized XML data representing this parameter set.</param>
        public GraphParameters(XElement xml)
        {
            CenterHorizontal = Double.Parse(xml.Element("CenterHorizontal").Value);
            CenterVertical = Double.Parse(xml.Element("CenterVertical").Value);
            HorizontalPixelScale = Double.Parse(xml.Element("HorizontalPixelScale").Value);
            VerticalPixelScale = Double.Parse(xml.Element("VerticalPixelScale").Value);

            HorizontalAxis = Char.Parse(xml.Element("HorizontalAxis").Value);
            VerticalAxis = Char.Parse(xml.Element("VerticalAxis").Value);
        }

        /// <summary>
        /// Converts this parameter set to its serialized XML form.
        /// </summary>
        public XElement ToXml()
        {
            return new XElement("GraphParameters",
                new XElement("CenterHorizontal", CenterHorizontal),
                new XElement("CenterVertical", CenterVertical),
                new XElement("HorizontalPixelScale", HorizontalPixelScale),
                new XElement("VerticalPixelScale", VerticalPixelScale),
                new XElement("HorizontalAxis", HorizontalAxis),
                new XElement("VerticalAxis", VerticalAxis));
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
