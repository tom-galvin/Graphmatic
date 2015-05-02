using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a token whose value is obtained by applying a function to some other evaluated value.
    /// </summary>
    [GraphmaticObject]
    public class FunctionToken : Token, IParsable
    {
        /// <summary>
        /// Evaluators for the functions that FunctionToken can represent.<para/>
        /// These include functions such as the trigonometric ratios.
        /// </summary>
        public static readonly Dictionary<string, UnaryEvaluator> Evaluators = new Dictionary<string, UnaryEvaluator>()
        {
            { "sin", new UnaryEvaluator(x => Math.Sin(x), "sin({0})") },
            { "cos", new UnaryEvaluator(x => Math.Cos(x), "cos({0})") },
            { "tan", new UnaryEvaluator(x => Math.Tan(x), "tan({0})") },
            { "sin`", new UnaryEvaluator(x => Math.Asin(x), "arcsin({0})") },
            { "cos`", new UnaryEvaluator(x => Math.Acos(x), "arccos({0})") },
            { "tan`", new UnaryEvaluator(x => Math.Atan(x), "arctan({0})") },
            { "", new UnaryEvaluator(x => x, "({0})") },
        };

        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                return Operand.BaselineOffset;
            }
        }

        /// <summary>
        /// Gets the textual name of the function represented by this token.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Expression containing the operand of this function.
        /// </summary>
        public Expression Operand
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the default child expression that the cursor is placed into when the token is inserted, or null to place the cursor after the expression.
        /// </summary>
        public override Expression DefaultChild
        {
            get
            {
                return Operand;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>FunctionToken</c> class.
        /// </summary>
        /// <param name="name">The textual name of the function represented by this token.</param>
        public FunctionToken(string name)
            : base()
        {
            Text = name;
            Operand = new Expression(this);
            Children = new Expression[] { Operand };
        }

        /// <summary>
        /// Initializes a new instance of the <c>FunctionToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public FunctionToken(XElement xml)
            : base()
        {
            Text = xml.Attribute("Name").Value;
            Operand = new Expression(this, xml.Element("Operand").Elements());
            Children = new Expression[] { Operand };
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("FunctionToken",
                new XAttribute("Name", Text),
                new XElement("Operand", Operand.ToXml()));
        }

        /// <summary>
        /// Paint this token onto the specified GDI+ drawing surface.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to draw this token onto.</param>
        /// <param name="expressionCursor">The expression cursor to draw inside the token.</param>
        /// <param name="x">The X co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        /// <param name="y">The y co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        public override void Paint(Graphics graphics, ExpressionCursor expressionCursor, int x, int y)
        {
            graphics.DrawExpressionString(Text, Size == DisplaySize.Small, x, y + Operand.BaselineOffset);

            // draw bracket lines
            if (Size == DisplaySize.Large)
            {
                Operand.Paint(graphics, expressionCursor, x + 6 * Text.Length + 6, y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 2,
                    y + 2,
                    x + 6 * Text.Length + 2,
                    y + Height - 2);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 2,
                    y + 2,
                    x + 6 * Text.Length + 4,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 4,
                    y + Height,
                    x + 6 * Text.Length + 2,
                    y + Height - 2);

                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + 2,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + Height - 2);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + 2,
                    x + Operand.Width + 6 * Text.Length + 7,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 7,
                    y + Height,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + Height - 2);
            }
            else
            {
                Operand.Paint(graphics, expressionCursor, x + 6 * Text.Length + 4, y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 1,
                    y + 1,
                    x + 6 * Text.Length + 1,
                    y + Height - 1);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 1,
                    y + 1,
                    x + 6 * Text.Length + 2,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 2,
                    y + Height,
                    x + 6 * Text.Length + 1,
                    y + Height - 1);

                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + 1,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + Height - 1);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + 1,
                    x + Operand.Width + 6 * Text.Length + 5,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 5,
                    y + Height,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + Height - 1);
            }
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Operand.Size = Size;
            Operand.RecalculateDimensions(expressionCursor);
            Width = Operand.Width + 6 * Text.Length + (Size == DisplaySize.Large ? 12 : 9);
            Height = Operand.Height;
        }


        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing a syntax tree
        /// for this token and any children.</returns>
        public virtual ParseTreeNode Parse()
        {
            return new UnaryParseTreeNode(Evaluators[Text], Operand.Parse());
        }
    }
}
