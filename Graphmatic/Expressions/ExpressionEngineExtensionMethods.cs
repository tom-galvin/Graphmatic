using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    public static class ExpressionEngineExtensionMethods
    {
        public static int IndexInParent(this Expression expression)
        {
            for (int i = 0; i < expression.Parent.Children.Length; i++) // find it's index in the parent
            {
                if (expression.Parent.Children[i] == expression)
                {
                    return i;
                }
            }

            throw new InvalidOperationException("The Expression is not contained within its parent Token - referential integrity lost.");
        }

        public static int IndexInParent(this IToken token)
        {
            for (int i = 0; i < token.Parent.Count; i++) // find it's index in the parent's parent
            {
                if (token.Parent[i] == token)
                {
                    return i;
                }
            }

            throw new InvalidOperationException("The Token is not contained within its parent Expression - referential integrity lost.");
        }

        public static bool Empty(this IToken token)
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
