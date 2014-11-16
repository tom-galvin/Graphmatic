using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    public class Picture : Resource
    {
        public Image ImageData
        {
            get;
            protected set;
        }

        public override string Type
        {
            get
            {
                return "Picture";
            }
        }

        public Picture(Image imageData)
            : base()
        {
            ImageData = imageData;
        }

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

        public override Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.Image32 :
                Properties.Resources.Image16;
        }
    }
}
