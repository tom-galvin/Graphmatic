using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Defines a token for expression tree elements that can be parsed to form a parse tree.
    /// </summary>
    public interface IParsable
    {
        /// <summary>
        /// Returns a parse tree node that can be evaluated.
        /// </summary>
        /// <returns>A parse tree node that can be evaluated.</returns>
        ParseTreeNode Parse();
    }
}
