using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Interaction.Statistics
{
    /// <summary>
    /// Represents a line that fits two variables in a data set.
    /// <para/>
    /// This line takes the form of <c>y=A+Bx</c>, where y is the dependent variable, x is
    /// the dependent variable, and A and B are the intercept and slope parameters of the
    /// curve respectively.
    /// </summary>
    public class LinearCurve : Curve
    {
        /// <summary>
        /// Gets or sets the intercept parameter of the curve.
        /// </summary>
        public double Intercept
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the slope parameter of the curve.
        /// </summary>
        public double Slope
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes this curve with the given variables.
        /// </summary>
        /// <param name="dependentVariable">The dependent variable, represented as a function of the independent variable.</param>
        /// <param name="independentVariable">The independent variable of the curve.</param>
        /// <param name="intercept">The intercept parameter of the curve.</param>
        /// <param name="slope">The slope parameter of the curve.</param>
        public LinearCurve(char dependentVariable, char independentVariable, double intercept, double slope)
            : base(dependentVariable, independentVariable)
        {
            Intercept = intercept;
            Slope = slope;
        }

        /// <summary>
        /// Convert this <c>LinearCurve</c> to a <c>Graphmatic.Interaction.Equation</c> object representing the same line.
        /// </summary>
        public override Equation ToEquation()
        {
            Expression expression = new Expression(null);
            expression.Add(new VariableToken(expression, DependentVariable));
            expression.Add(new SymbolicToken(expression, SymbolicToken.SymbolicType.Equals));
            expression.AddRange(ValueToTokenSequence(expression, Intercept));
            if(Slope >= 0) // avoid adding consecutive +- tokens
                expression.Add(new OperationToken(expression, OperationToken.OperationType.Add));
            expression.AddRange(ValueToTokenSequence(expression, Slope));
            expression.Add(new VariableToken(expression, IndependentVariable));
            Equation equation = new Equation(expression)
            {
                Name = this.ToString()
            };
            equation.Parse();
            return equation;
        }

        /// <summary>
        /// Returns a string which represents this line as an equation.
        /// </summary>
        /// <returns>A string representing this line.</returns>
        public override string ToString()
        {
            return String.Format("{0}={1:0.####}+{2:0.####}{3}",
                DependentVariable,
                Intercept,
                Slope,
                IndependentVariable)
                .Replace("+-", "-");
        }
    }
}
