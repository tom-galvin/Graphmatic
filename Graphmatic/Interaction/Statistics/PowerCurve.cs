using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Interaction.Statistics
{
    /// <summary>
    /// Represents a power-law curve that fits two variables in a data set.
    /// <para/>
    /// This curve takes the form of <c>y=Ax^B</c>, where y is the dependent variable, x is
    /// the dependent variable, and A and B are the scale and power parameters of the
    /// curve respectively.
    /// </summary>
    public class PowerCurve : Curve
    {
        /// <summary>
        /// Gets or sets the scale parameter of the curve.
        /// </summary>
        public double Scale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the power parameter of the curve.
        /// </summary>
        public double Power
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes this curve with the given variables.
        /// </summary>
        /// <param name="dependentVariable">The dependent variable, represented as a function of the independent variable.</param>
        /// <param name="independentVariable">The independent variable of the curve.</param>
        /// <param name="scale">The scale parameter of the curve.</param>
        /// <param name="power">The power parameter of the curve.</param>
        public PowerCurve(char dependentVariable, char independentVariable, double scale, double power)
            : base(dependentVariable, independentVariable)
        {
            Scale = scale;
            Power = power;
        }

        /// <summary>
        /// Convert this <c>PowerCurve</c> to a <c>Graphmatic.Interaction.Equation</c> object representing the same curve.
        /// </summary>
        public override Equation ToEquation()
        {
            Expression expression = new Expression(null);
            expression.Add(new VariableToken(expression, DependentVariable));
            expression.Add(new SymbolicToken(expression, SymbolicToken.SymbolicType.Equals));
            expression.AddRange(ValueToTokenSequence(expression, Scale));
            ExpToken exp = new ExpToken(expression);
            exp.Base.Add(new VariableToken(exp.Base, IndependentVariable));
            exp.Power.AddRange(ValueToTokenSequence(exp.Power, Power));
            expression.Add(exp);
            Equation equation = new Equation(expression)
            {
                Name = this.ToString()
            };
            equation.Parse();
            return equation;
        }

        /// <summary>
        /// Returns a string which represents this curve as an equation.
        /// </summary>
        /// <returns>A string representing this curve.</returns>
        public override string ToString()
        {
            return String.Format("{0}={1:0.####}{2}^{3:0.####}",
                DependentVariable,
                Scale,
                IndependentVariable,
                Power);
        }
    }
}
