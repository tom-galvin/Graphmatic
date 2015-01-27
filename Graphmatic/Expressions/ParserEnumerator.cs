using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Represents an enumerator used by an LL(1) parser for traversing an expression.
    /// </summary>
    public class ParserEnumerator
    {
        /// <summary>
        /// Gets the expression that this parser is parsing.
        /// </summary>
        public Expression Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// The index of the parser in the expression being parsed. This starts at zero and moves
        /// along to <c>(Expression.Count - 1)</c> as the expression is parsed.
        /// </summary>
        private int Index;

        /// <summary>
        /// Gets the current lookahead token for this parser.
        /// </summary>
        public Token Current
        {
            get
            {
                if (Index >= 0 && Index < Expression.Count)
                {
                    return Expression[Index];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets whether this enumerator has reached the final token in the expression or not.
        /// </summary>
        public bool EndReached
        {
            get
            {
                return Index == Expression.Count - 1;
            }
        }

        /// <summary>
        /// Initializes a new Parser with the given expression, with the starting position set to just
        /// before the left-hand side of the expression.
        /// </summary>
        /// <param name="expression">The expression to be parsed.</param>
        public ParserEnumerator(Expression expression)
        {
            Expression = expression;
            Index = -1;
        }

        /// <summary>
        /// Advances the enumerator one token along in the expression. If the enumerator is already at the end of
        /// the expression, this method does nothing.
        /// </summary>
        public void Next()
        {
            if (Index < Expression.Count - 1)
            {
                Index += 1;
            }
        }

        /// <summary>
        /// Accepts a token from the expression, advances the enumerator, and returns true. However, if the parser
        /// is at the end of the expression, the parser will not be advanced, and this method will return false.
        /// </summary>
        /// <returns>Returns false if the parser is at the end of the expression; true otherwise.</returns>
        public bool Accept()
        {
            if (Index < Expression.Count - 1)
            {
                Next();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Looks ahead at the next token in the expression. If the next token is of type <typeparamref name="T"/>,
        /// then this will act the same as <c>Accept()</c>. Otherwise, or if the parser is at the end of the expression,
        /// the parser will not be advanced, and this method will return false.
        /// </summary>
        /// <typeparam name="T">The type of token to accept.</typeparam>
        /// <returns>Returns false if the next token is not of type <typeparamref name="T"/>, or the parser is at the end
        /// of the expression; true otherwise.</returns>
        public bool Accept<T>() where T : Token
        {
            if (Check<T>())
            {
                Next();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Looks ahead at the next token in the expression. If the next token is of type <typeparamref name="T"/> and
        /// <paramref name="acceptCondition"/> returns true for the next expression, then this will act the same as
        /// <c>Accept()</c>. Otherwise, or if the parser is at the end of the expression, the parser will not be advanced,
        /// and this method will return false.
        /// </summary>
        /// <typeparam name="T">The type of token to accept.</typeparam>
        /// <returns>Returns false if the next token is not of type <typeparamref name="T"/>, <paramref name="acceptCondition"/>
        /// is not met, or the parser is at the end of the expression; true otherwise.</returns>
        public bool Accept<T>(Func<T, bool> acceptCondition) where T : Token
        {
            if (Check<T>(acceptCondition))
            {
                Next();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the next token in the expression is of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of token to check for.</typeparam>
        /// <returns>Returns true if the next token in the expression is of type <typeparamref name="T"/>; false otherwise.
        /// If the parser is at the end of the expression, this will also return false.</returns>
        public bool Check<T>() where T : Token
        {
            return Check<T>(t => true);
        }

        /// <summary>
        /// Checks if the next token in the expression is of type <typeparamref name="T"/> and satisfies the given condition.
        /// </summary>
        /// <typeparam name="T">The type of token to check for.</typeparam>
        /// <param name="acceptCondition">The condition that the next token in the expression must satisfy in order
        /// to return true.<para/>If the next token is not of type T, then this condition is not checked.</param>
        /// <returns>Returns true if the next token in the expression is of type <typeparamref name="T"/> and
        /// satisfies <paramref name="acceptCondition"/>; false otherwise. If the parser is at the end of the expression,
        /// this will also return false.</returns>
        public bool Check<T>(Func<T, bool> acceptCondition) where T : Token
        {
            if (Index < Expression.Count - 1 &&
                Expression[Index + 1] is T)
            {
                return acceptCondition(Expression[Index + 1] as T);
            }
            else
            {
                return false;
            }
        }
    }
}
