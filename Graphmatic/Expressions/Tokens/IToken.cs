using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Exposes methods and properties to manipulate a token (or component) in a mathematical expression.
    /// </summary>
    public interface IToken : IPaintable
    {
        /// <summary>
        /// Gets whether the component is rendered in large or small mode.
        /// </summary>
        DisplaySize Size
        {
            get;
        }

        /// <summary>
        /// Gets the expression containing this token.
        /// </summary>
        Expression Parent
        {
            get;
        }

        /// <summary>
        /// Gets the expressions contained within this token.
        /// </summary>
        Expression[] Children
        {
            get;
        }

        /// <summary>
        /// Converts this token (and all its children) to XML.
        /// </summary>
        /// <returns>The XML representation of this token.</returns>
        XElement ToXml();
    }
}
