using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Interaction.Statistics
{
    /// <summary>
    /// Represents a curve that fits two variables in a data set.
    /// </summary>
    public abstract class Curve
    {
        /// <summary>
        /// A string containing Arabic digits, where each digit's index is equal to its value.
        /// </summary>
        private const string Digits = "0123456789";

        /// <summary>
        /// Gets or sets the dependent variable in this fitted curve.
        /// </summary>
        public char DependentVariable
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the independent variable in this fitted curve.
        /// </summary>
        public char IndependentVariable
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes this curve with the given variables.
        /// </summary>
        /// <param name="dependentVariable">The dependent variable, represented as a function of the independent variable.</param>
        /// <param name="independentVariable">The independent variable of the curve.</param>
        public Curve(char dependentVariable, char independentVariable)
        {
            DependentVariable = dependentVariable;
            IndependentVariable = independentVariable;
        }

        /// <summary>
        /// Converts a numeric value to its equivalent tokens in a <c>Graphmatic.Expressions.Expression</c> object.
        /// </summary>
        /// <param name="parent">The expression in which the tokens are to be in.</param>
        /// <param name="value">The double to convert to a sequence of tokens.</param>
        /// <returns>The sequence of tokens representing <paramref name="value"/>.</returns>
        protected IEnumerable<Token> ValueToTokenSequence(Expression parent, double value)
        {
            string doubleAsString = value.ToString(); // converting to a string, let .NET do the formatting for us
            foreach (char c in doubleAsString)
            {
                if (Digits.IndexOf(c) != -1)
                {
                    yield return new DigitToken(parent, Digits.IndexOf(c));
                }
                else if (c == '.')
                {
                    yield return new SymbolicToken(parent, SymbolicToken.SymbolicType.DecimalPoint);
                }
                else if (c == 'e' || c == 'E')
                {
                    yield return new SymbolicToken(parent, SymbolicToken.SymbolicType.Exp10);
                }
                else if (c == '+')
                {
                    yield return new OperationToken(parent, OperationToken.OperationType.Add);
                }
                else if (c == '-')
                {
                    yield return new OperationToken(parent, OperationToken.OperationType.Subtract);
                }
                else
                {
                    throw new FormatException("Unknown character '" + c.ToString() + "' in stringified double.");
                }
            }
        }

        /// <summary>
        /// Convert this <c>Curve</c> to a <c>Graphmatic.Interaction.Equation</c> object representing the same curve.
        /// </summary>
        public abstract Equation ToEquation();
    }
}
