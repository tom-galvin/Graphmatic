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
            set;
        }

        /// <summary>
        /// Gets the expression containing this token.
        /// </summary>
        Expression Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the expressions contained within this token.
        /// </summary>
        Expression[] Children
        {
            get;
        }

        /// <summary>
        /// Gets the default child expression that the cursor is placed into when the token is inserted, or null to place the cursor after the expression.
        /// </summary>
        Expression DefaultChild
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
