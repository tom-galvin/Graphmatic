using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Interaction.Annotations;

namespace Graphmatic.Interaction
{
    /// <summary>
    /// Represents a method that deserializes a resource from its XML representation.
    /// </summary>
    /// <param name="xml">THe XML representation of the resource.</param>
    /// <returns>The deserialized form of the resource.</returns>
    public delegate Resource ResourceDeserializationFactory(XElement xml);

    /// <summary>
    /// Represents a method that deserializes an annotation from its XML representation.
    /// </summary>
    /// <param name="xml">The XML representation of the annotation.</param>
    /// <returns>The deserialized form of the annotation.</returns>
    public delegate Annotation AnnotationDeserializationFactory(XElement xml);

    /// <summary>
    /// Provides methods to assist in (de)serialization of serialized Graphmatic document resources.
    /// </summary>
    public static class ResourceSerializationExtensionMethods
    {
        /// <summary>
        /// A list of deserializing constructors for resource types with differing XML element names.
        /// </summary>
        private static Dictionary<string, ResourceDeserializationFactory> ResourceDeserializers = new Dictionary<string, ResourceDeserializationFactory>
        {
            { "Page", xml => new Page(xml) },
            { "Equation", xml => new Equation(xml) },
            { "DataSet", xml => new DataSet(xml) },
            { "Picture", xml => new Picture(xml) },
            { "HtmlPage", xml => new HtmlPage(xml) },
            { "Resource", xml => new Resource(xml) }
        };

        /// <summary>
        /// A list of deserializing constructors for annotation types with differing XML element names.
        /// </summary>
        private static Dictionary<string, AnnotationDeserializationFactory> AnnotationDeserializers = new Dictionary<string, AnnotationDeserializationFactory>
        {
            { "Annotation", xml => new Annotation(xml) }
        };

        /// <summary>
        /// Deserializes a resource from its XML representation.
        /// </summary>
        /// <param name="xml">The XML representation of a resource to deserialize.</param>
        /// <returns>The deserialized form of the given XML.</returns>
        public static Resource FromXml(XElement xml)
        {
            string elementName = xml.Name.LocalName;
            if (!ResourceDeserializers.ContainsKey(elementName))
                throw new IOException("Unknown resource type: " + elementName);
            return ResourceDeserializers[elementName](xml);
        }

        /// <summary>
        /// Deserializes an annotation from its XML representation.
        /// </summary>
        /// <param name="xml">The XML representation of an annotation to deserialize.</param>
        /// <returns>The deserialized form of the given XML.</returns>
        public static Annotation AnnotationFromXml(XElement xml)
        {
            string elementName = xml.Name.LocalName;
            if (!AnnotationDeserializers.ContainsKey(elementName))
                throw new IOException("Unknown annotation type: " + elementName);
            return AnnotationDeserializers[elementName](xml);
        }

        public static byte[] ToByteArray(this Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static Image ImageToByteArray(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        public static string ToXmlString(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        public static Color XmlStringToColor(string data)
        {
            return ColorTranslator.FromHtml(data);
        }

        public static string ToBase64(this string s)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }

        public static string FromBase64(this string s)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
    }
}
