using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a singular node in the parse tree with no children and returning a constant value.
    /// </summary>
    public class ConstantParseTreeNode : ParseTreeNode
    {
        /// <summary>
        /// Gets the constant value to return upon evaluation.
        /// </summary>
        public double Value
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the ConstantParseTree node, with the given constant value.
        /// </summary>
        /// <param name="value">The constant value to return upon evaluation.</param>
        public ConstantParseTreeNode(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Evaluate this parse tree node with the given variable values.
        /// </summary>
        /// <param name="variables">The variable values to use in calculation.</param>
        /// <returns>Returns the result of the evaluation.</returns>
        public override double Evaluate(Dictionary<char, double> variables)
        {
            return Value;
        }

        /// <summary>
        /// Convert this parse tree node into a string representation of the syntax it represents.
        /// </summary>
        /// <returns>A string representation of the syntax it represents.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
