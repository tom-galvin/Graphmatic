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
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing a syntax tree
        /// for this token and any children.</returns>
        ParseTreeNode Parse();
    }
}
