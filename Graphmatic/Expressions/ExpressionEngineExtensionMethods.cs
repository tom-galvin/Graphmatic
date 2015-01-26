using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Provides helper methods for the expression engine, including for <c>Graphmatic.ExpressionCursor</c>.
    /// These mainly concern the tree-like hierarchical relationship between tokens and their parent/child expressions, and the
    /// navigation around an expression by a user with an I-beam cursor.
    /// </summary>
    internal static class ExpressionEngineExtensionMethods
    {
        /// <summary>
        /// Determines the index of an expression in its parent token's child expression array.
        /// </summary>
        /// <param name="expression">The expression to check.</param>
        /// <returns>The index of <paramref name="expression"/> in its parent token's child expression array.</returns>
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
        /// <returns>The index of <paramref name="token"/> in its parent expression.</returns>
        internal static int IndexInParent(this Token token)
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
        internal static bool Empty(this Token token)
        {
            if (token.Children.Length == 0) return true;
            foreach (Expression childExpression in token.Children)
            {
                if (childExpression.Count > 0) return false;
            }
            return true;
        }

        /// <summary>
        /// Flattens a token's child expressions out into its parent and removes <paramref name="token"/>.
        /// For example, flattening a <cRootToken</c> <c>"³√x"</c> would result in the tokens <c>3</c> <c>x</c>
        /// being added into the parent expression of the <c>RootToken</c>, with the <c>RootToken</c> (now redundant) being removed in the process.<para/>
        /// Attempting to flatten a token that is not contained within a parent expression will result in an <c>InvalidOperationException</c> being thrown.
        /// </summary>
        /// <param name="token">The token to flatten out.</param>
        /// <returns>Returns the location that the cursor should be placed in <paramref name="token"/>'s parent expression upon the
        /// flattening of the token, or <c>-1</c> if this location could not be determined.</returns>
        internal static int Flatten(this Token token)
        {
            if (token.Parent != null && token.Parent != null)
            {
                int startIndex = token.IndexInParent();
                int cursorReturnIndex = -1;
                Expression parent = token.Parent;
                parent.RemoveAt(startIndex);
                foreach (Expression child in token.Children)
                {
                    if (child == token.DefaultChild)
                    {
                        // if this current child expression is the default child of the token, then the location that
                        // the cursor should be returned to is the location where the default child's tokens are
                        // inserted into the flattened token's parent
                        cursorReturnIndex = startIndex;
                    }
                    foreach (Token childToken in child)
                    {
                        parent.Insert(startIndex++, childToken);
                    }
                }
                return cursorReturnIndex;
            }
            else
            {
                throw new InvalidOperationException("Cannot flatten a token that has no parent.");
            }
        }
    }
}
