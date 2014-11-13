using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Exposes methods and properties to manipulate a token (or component) in a mathematical expression.
    /// </summary>
    public abstract class Token : IPaintable, IXmlConvertible, IEvaluable
    {
        /// <summary>
        /// Gets the expression containing this token.
        /// </summary>
        public Expression Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the expressions contained within this token.
        /// </summary>
        public Expression[] Children
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the default child expression that the cursor is placed into when the token is inserted, or null to place the cursor after the expression.
        /// </summary>
        public virtual Expression DefaultChild
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// The width of the token, in pixels.
        /// </summary>
        public int Width
        {
            get;
            protected set;
        }

        /// <summary>
        /// The height of the token, in pixels.
        /// </summary>
        public int Height
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the display size style (large or small) of the drawable element.
        /// </summary>
        public DisplaySize Size
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public virtual int BaselineOffset
        {
            get
            {
                return 0;
            }
        }

        public Token(Expression parent)
        {
            Parent = parent;
            Children = new Expression[] { };
        }

        public abstract void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y);

        public abstract void RecalculateDimensions(ExpressionCursor expressionCursor);

        public abstract XElement ToXml();

        /// <summary>
        /// Evaluates the expression with a given set of variables.
        /// </summary>
        /// <param name="variables">The value of the variables to evaluate with.</param>
        public abstract double Evaluate(Dictionary<char, double> variables);
    }
}
