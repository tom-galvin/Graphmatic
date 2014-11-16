using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a singular node in the parse tree with one child.
    /// </summary>
    public class UnaryParseTreeNode : ParseTreeNode
    {
        /// <summary>
        /// Gets the unary evaluator to be used upon evaluation of the parse tree.
        /// </summary>
        public UnaryEvaluator Evaluator
        {
            get;
            protected set;
        }
        
        /// <summary>
        /// Gets the child node.
        /// </summary>
        public ParseTreeNode Operand
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the UnaryParseTreeNode class, with the given evaluator and child node.
        /// </summary>
        /// <param name="evaluator">The unary evaluator to be used upon evaluation of the parse tree.</param>
        /// <param name="operand">The child node.</param>
        public UnaryParseTreeNode(UnaryEvaluator evaluator, ParseTreeNode operand)
        {
            Evaluator = evaluator;
            Operand = operand;
        }

        /// <summary>
        /// Evaluate this parse tree node with the given variable values.
        /// </summary>
        /// <param name="variables">The variable values to use in calculation.</param>
        /// <returns>Returns the result of the evaluation.</returns>
        public override double Evaluate(Dictionary<char, double> variables)
        {
            double operandResult = Operand.Evaluate(variables);
            return Evaluator.Function(operandResult);
        }

        /// <summary>
        /// Convert this parse tree node into a string representation of the syntax it represents.
        /// </summary>
        /// <returns>A string representation of the syntax it represents.</returns>
        public override string ToString()
        {
            return String.Format(Evaluator.FormatString, Operand.ToString());
        }
    }
}