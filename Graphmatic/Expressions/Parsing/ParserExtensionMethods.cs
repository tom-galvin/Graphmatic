using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Exposes methods used for parsing and the examination of produced parse trees.
    /// </summary>
    public static class ParserExtensionMethods
    {
        /// <summary>
        /// Determines whether a parse tree contains (refers to) a given variable.
        /// </summary>
        /// <param name="node">The ParseTreeNode to perform the recursive search on.</param>
        /// <param name="variable">The variable to search for.</param>
        /// <returns>Returns true if the ParseTreeNode contains the variable; false otherwise.</returns>
        public static bool ContainsVariable(this ParseTreeNode node, char variable)
        {
            if (node is BinaryParseTreeNode)
            {
                BinaryParseTreeNode binaryNode = node as BinaryParseTreeNode;
                return ContainsVariable(binaryNode.Left, variable) || ContainsVariable(binaryNode.Right, variable);
            }
            else if (node is UnaryParseTreeNode)
            {
                UnaryParseTreeNode unaryNode = node as UnaryParseTreeNode;
                return ContainsVariable(unaryNode.Operand, variable);
            }
            else if (node is ConstantParseTreeNode)
            {
                return false;
            }
            else if (node is VariableParseTreeNode)
            {
                return (node as VariableParseTreeNode).Variable == variable;
            }
            else
            {
                throw new InvalidOperationException("Unknown parse tree node type: " + node.GetType().Name);
            }
        }

        /// <summary>
        /// Determines whether a parse tree can be plotted using a given set of variables.
        /// <para/>
        /// If the ParseTree contains any variables other than those specifiec in <c>variables</c>,
        /// then this function will return false. Otherwise, it will return true.
        /// </summary>
        /// <param name="node">The ParseTreeNode to perform the recursive search on.</param>
        /// <param name="variables">The variables to check for.</param>
        /// <returns>Returns true if the ParseTreeNode can be plotted using the given variables; otherwise false.</returns>
        public static bool CanPlotOverVariables(this ParseTreeNode node, params char[] variables)
        {
            if (node is BinaryParseTreeNode)
            {
                BinaryParseTreeNode binaryNode = node as BinaryParseTreeNode;
                return CanPlotOverVariables(binaryNode.Left, variables) && CanPlotOverVariables(binaryNode.Right, variables);
            }
            else if (node is UnaryParseTreeNode)
            {
                UnaryParseTreeNode unaryNode = node as UnaryParseTreeNode;
                return CanPlotOverVariables(unaryNode.Operand, variables);
            }
            else if (node is ConstantParseTreeNode)
            {
                return true;
            }
            else if (node is VariableParseTreeNode)
            {
                // returns true if the VariableParseTreeNode's variable is in the variables array
                // otherwise return false
                return Array.IndexOf(variables, (node as VariableParseTreeNode).Variable) != -1;
            }
            else
            {
                throw new InvalidOperationException("Unknown parse tree node type: " + node.GetType().Name);
            }
        }
    }
}
