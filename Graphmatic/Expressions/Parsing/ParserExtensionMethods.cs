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
    }
}
