using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction.Annotations
{
    /// <summary>
    /// Represents a pictorial annotation on a Page.
    /// </summary>
    [GraphmaticObject]
    public class Picture : Annotation
    {
        /// <summary>
        /// Gets the .NET <c>System.Drawing.Image</c> class representing the image to display.
        /// </summary>
        public Image ImageData
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Annotations.Picture</c> class with the specified image data.
        /// </summary>
        /// <param name="imageData">The image data that this annotation displays.</param>
        public Picture(Image imageData)
            : base()
        {
            ImageData = imageData;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Annotations.Picture</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public Picture(XElement xml)
            : base(xml)
        {
            string base64ImageData = xml.Element("ImageData").Value;
            ImageData = ResourceSerializationExtensionMethods.ImageToByteArray(Convert.FromBase64String(base64ImageData));
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Picture";
            string base64ImageData = Convert.ToBase64String(ImageData.ToByteArray());
            baseElement.Add(new XElement("ImageData", base64ImageData));
            return baseElement;
        }

        /// <summary>
        /// Draws this Annotation onto <paramref name="page"/>.
        /// </summary>
        /// <param name="page">The Graph to plot this Annotation onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this Annotation.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the page's graph's parameters.</param>
        /// <param name="resolution">The plotting resolution to use. This does not have an effect for data sets.</param>
        public override void DrawAnnotationOnto(Page page, Graphics graphics, Size graphSize, PlotResolution resolution)
        {
            graphics.DrawImage(ImageData, GetScreenRectangle(page, graphSize));
        }
    }
}
