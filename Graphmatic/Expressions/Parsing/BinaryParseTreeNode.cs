using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a singular node in the parse tree with two children.
    /// </summary>
    public class BinaryParseTreeNode : ParseTreeNode
    {
        /// <summary>
        /// Gets the binary evaluator to be used upon evaluation of the parse tree.
        /// </summary>
        public BinaryEvaluator Evaluator
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the left child node.
        /// </summary>
        public ParseTreeNode Left
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the right child node.
        /// </summary>
        public ParseTreeNode Right
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initialize a new instance of the BinaryParseTreeNode class, with the given evaluator and children.
        /// </summary>
        /// <param name="evaluator">The binary evaluator to be used upon evaluation of the parse tree.</param>
        /// <param name="left">The left child node.</param>
        /// <param name="right">The right child node.</param>
        public BinaryParseTreeNode(BinaryEvaluator evaluator, ParseTreeNode left, ParseTreeNode right)
        {
            Evaluator = evaluator;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Evaluate this parse tree node with the given variable values.
        /// </summary>
        /// <param name="variables">The variable values to use in calculation.</param>
        /// <returns>Returns the result of the evaluation.</returns>
        public override double Evaluate(VariableSet variables)
        {
            double leftResult = Left.Evaluate(variables);
            double rightResult = Right.Evaluate(variables);
            return Evaluator.Function(leftResult, rightResult);
        }

        /// <summary>
        /// Convert this parse tree node into a string representation of the syntax it represents.
        /// </summary>
        /// <returns>A string representation of the syntax it represents.</returns>
        public override string ToString()
        {
            return String.Format(Evaluator.FormatString, Left.ToString(), Right.ToString());
        }
    }
}
