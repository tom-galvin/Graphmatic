using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    internal static class ExpressionEngineExtensionMethods
    {
        /// <summary>
        /// Determines the index of an expression in its parent token's child expression array.
        /// </summary>
        /// <param name="expression">The expression to check.</param>
        /// <returns>The index of <c>expression</c> in its parent token's child expression array.</returns>
        internal static int IndexInParent(this Expression expression)
        {
            if (expression.Parent == null)
                throw new InvalidOperationException("Expression does not have a parent Token.");

            for (int i = 0; i < expression.Parent.Children.Length; i++) // find it's index in the parent
            {
                if (expression.Parent.Children[i] == expression)
                {
                    return i;
                }
            }

            throw new InvalidOperationException("The Expression is not contained within its parent Token - referential integrity lost.");
        }

        /// <summary>
        /// Determines the index of a token in its parent expression.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>The index of <c>token</c> in its parent expression.</returns>
        internal static int IndexInParent(this IToken token)
        {
            if (token.Parent == null)
                throw new InvalidOperationException("Token does not have a parent Expression.");

            for (int i = 0; i < token.Parent.Count; i++) // find it's index in the parent's parent
            {
                if (token.Parent[i] == token)
                {
                    return i;
                }
            }

            throw new InvalidOperationException("The Token is not contained within its parent Expression - referential integrity lost.");
        }

        /// <summary>
        /// Determines if a token's children, if existing, are empty or not.
        /// </summary>
        /// <param name="token">The token to check for emptiness.</param>
        /// <returns>Returns true if a token is empty; otherwise returns false.</returns>
        internal static bool Empty(this IToken token)
        {
            if (token.Children.Length == 0) return true;
            foreach (Expression childExpression in token.Children)
            {
                if (childExpression.Count > 0) return false;
            }
            return true;
        }
    }
}
