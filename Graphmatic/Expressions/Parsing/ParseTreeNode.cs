using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a singular node in the parse tree, from which other nodes may be referenced.
    /// </summary>
    public abstract class ParseTreeNode
    {
        /// <summary>
        /// Evaluate this parse tree node with the given variable values.
        /// </summary>
        /// <param name="variables">The variable values to use in calculation.</param>
        /// <returns>Returns the result of the evaluation.</returns>
        public abstract double Evaluate(Dictionary<char, double> variables);

        /// <summary>
        /// Convert this parse tree node into a string representation of the syntax it represents.
        /// </summary>
        /// <returns>A string representation of the syntax it represents.</returns>
        public abstract override string ToString();
    }
}
