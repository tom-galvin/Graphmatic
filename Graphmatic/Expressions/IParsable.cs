using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions
{
    public interface IParsable
    {
        /// <summary>
        /// Returns a parse tree node that can be evaluated.
        /// </summary>
        /// <returns>A parse tree node that can be evaluated.</returns>
        ParseTreeNode Parse();
    }
}
