using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    /// <summary>
    /// Represents a method that deserializes a resource from its XML representation.
    /// </summary>
    /// <param name="xml">THe XML representation of the resource.</param>
    /// <returns>The deserialized form of the resource.</returns>
    public delegate Resource ResourceDeserializationFactory(XElement xml);

    /// <summary>
    /// Provides methods to assist in (de)serialization of serialized Graphmatic document resources.
    /// </summary>
    public static class ResourceSerializationExtensionMethods
    {
        /// <summary>
        /// A list of deserializing constructors for resources with differing XML element names.
        /// </summary>
        private static Dictionary<string, ResourceDeserializationFactory> ResourceDeserializers = new Dictionary<string, ResourceDeserializationFactory>
        {
            { "Page", xml => new Page(xml) },
            { "Equation", xml => new Equation(xml) },
            { "DataSet", xml => new DataSet(xml) },
            { "Resource", xml => new Resource(xml) }
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
    }
}
