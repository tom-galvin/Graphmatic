using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction.Annotations
{
    public class Picture : Annotation
    {
        public Image ImageData
        {
            get;
            protected set;
        }

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

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Picture";
            string base64ImageData = Convert.ToBase64String(ImageData.ToByteArray());
            baseElement.Add(new XElement("ImageData", base64ImageData));
            return baseElement;
        }

        public override void DrawAnnotationOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            graphics.DrawImage(ImageData, GetScreenRectangle(page, graphSize, graphParams));
        }
    }
}
