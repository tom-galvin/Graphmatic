using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    }
}
