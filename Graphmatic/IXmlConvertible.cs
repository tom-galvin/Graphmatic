using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Interaction;

namespace Graphmatic
{
    /// <summary>
    /// Provides methods to convert to and from XML.
    /// </summary>
    public interface IXmlConvertible
    {
        /// <summary>
        /// Converts this object, and any contained objects, to XML.
        /// </summary>
        /// <returns>The XML representation of this object.</returns>
        XElement ToXml();

        /// <summary>
        /// Resolves any resource references which need to be resolved after the document is loaded.
        /// <para/>
        /// For example, if a resource A references another resource B, but A is loaded before B, then
        /// it cannot refer to it. The way around this is to resolve such issues after everything else
        /// is loaded.
        /// </summary>
        /// <param name="document">The Document in which this element resides.</param>
        void UpdateReferences(Document document);
    }
}
