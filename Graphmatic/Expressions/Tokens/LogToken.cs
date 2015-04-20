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
    /// Represents a token whose value is the arbitrary-base logarithm of some other value.
    /// </summary>
    [GraphmaticObject]
    public class LogToken : Token, IParsable
    {
        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                return Simplification == SimplificationType.None ?
                    Operand.BaselineOffset + Math.Min(0, Height - Operand.Height) :
                    Operand.BaselineOffset + Height - Operand.Height;
            }
        }

        /// <summary>
        /// Gets the Expression containing the operand of the logarithm function.
        /// </summary>
        public Expression Operand
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the Expression containing the logarithmic base of the function.
        /// </summary>
        public Expression Base
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the default child expression that the cursor is placed into when the token is inserted, or null to place the cursor after the expression.<para/>
        /// If the token's display is unsimplified, then the default child will be the log base. Otherwise, it will be the operand.
        /// </summary>
        public override Expression DefaultChild
        {
            get
            {
                return Simplification == SimplificationType.None ? Base : Operand;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>LogToken</c> class.
        /// </summary>
        public LogToken()
            : base()
        {
            Operand = new Expression(this);
            Base = new Expression(this);
            Children = new Expression[] { Base, Operand };
        }

        /// <summary>
        /// Initializes a new instance of the <c>LogToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public LogToken(XElement xml)
            : base()
        {
            Operand = new Expression(this, xml.Element("Operand").Elements());
            Base = new Expression(this, xml.Element("Base").Elements());
            Children = new Expression[] { Base, Operand };
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("LogToken",
                new XElement("Base", Base.ToXml()),
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
            string text = Simplification == SimplificationType.LogE ? "ln" : "log";
            int xOffset = 6 * text.Length;

            graphics.DrawExpressionString(text, Size == DisplaySize.Small, x, y + Operand.BaselineOffset);
            if (Simplification == SimplificationType.None)
            {
                Base.Paint(graphics, expressionCursor, x + xOffset, y + (Size == DisplaySize.Large ? 5 : 4) + Operand.BaselineOffset);
            }
            if (Simplification == SimplificationType.None) xOffset += Base.Width;

            // draw bracket lines
            if (Size == DisplaySize.Large)
            {
                Operand.Paint(graphics, expressionCursor, x + xOffset + 6, y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 2,
                    y + 2,
                    x + xOffset + 2,
                    y + Operand.Height - 2);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 2,
                    y + 2,
                    x + xOffset + 4,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 4,
                    y + Operand.Height,
                    x + xOffset + 2,
                    y + Operand.Height - 2);

                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 9,
                    y + 2,
                    x + Operand.Width + xOffset + 9,
                    y + Operand.Height - 2);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 9,
                    y + 2,
                    x + Operand.Width + xOffset + 7,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 7,
                    y + Operand.Height,
                    x + Operand.Width + xOffset + 9,
                    y + Operand.Height - 2);
            }
            else
            {
                Operand.Paint(graphics, expressionCursor, x + xOffset + 4, y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 1,
                    y + 1,
                    x + xOffset + 1,
                    y + Operand.Height - 1);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 1,
                    y + 1,
                    x + xOffset + 2,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 2,
                    y + Operand.Height,
                    x + xOffset + 1,
                    y + Operand.Height - 1);

                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 6,
                    y + 1,
                    x + Operand.Width + xOffset + 6,
                    y + Operand.Height - 1);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 6,
                    y + 1,
                    x + Operand.Width + xOffset + 5,
                    y);
                graphics.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 5,
                    y + Operand.Height,
                    x + Operand.Width + xOffset + 6,
                    y + Operand.Height - 1);
            }
        }

        private SimplificationType Simplification;
        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Simplification = GetSimplificationType(expressionCursor);
            string text = Simplification == SimplificationType.LogE ? "ln" : "log";

            Operand.Size = Size;
            Operand.RecalculateDimensions(expressionCursor);
            Base.Size = DisplaySize.Small;
            Base.RecalculateDimensions(expressionCursor);
            Width = Operand.Width + 6 * text.Length + (Size == DisplaySize.Large ? 12 : 9) + (Simplification == SimplificationType.None ? 1 + Base.Width : 0);
            int fnSize = (Size == DisplaySize.Large ? 5 : 4) + Base.Height + Operand.BaselineOffset;
            Height = Simplification != SimplificationType.None ? Operand.Height : Math.Max(Operand.Height, fnSize);
        }

        /// <summary>
        /// Gets the simplification type to use for drawing this logarithm token.<para/>
        /// If the logarithm's base is the constant 'e' (ie. a natural log), and <paramref name="expressionCursor"/> is not located
        /// inside the base of the logarithm, then the base is hidden and the text changes from 'log' to 'ln'.<para/>
        /// If the logarithm's base is 10, and <paramref name="expressionCursor"/> is not located inside the base of the logarithm,
        /// then the base is hidden.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use for determining whether to simplify the display of the token or not.</param>
        /// <returns>Returns the type of visual simplification to use for displaying this LogToken.</returns>
        private SimplificationType GetSimplificationType(ExpressionCursor expressionCursor)
        {
            if (expressionCursor.Expression != Base)
            {
                if (Base.Count == 2)
                {
                    if (Base[0] is DigitToken && Base[1] is DigitToken)
                    {
                        if ((Base[0] as DigitToken).Value == 1 && (Base[1] as DigitToken).Value == 0)
                        {
                            return SimplificationType.LogTen;
                        }
                    }
                }
                else if (Base.Count == 1)
                {
                    if (Base[0] is ConstantToken)
                    {
                        if ((Base[0] as ConstantToken).Value == ConstantToken.ConstantType.E)
                        {
                            return SimplificationType.LogE;
                        }
                    }
                }
            }
            return SimplificationType.None;
        }

        /// <summary>
        /// An evaluator for the natural log.
        /// </summary>
        public static readonly UnaryEvaluator NaturalLogEvaluator = new UnaryEvaluator(logOperand => Math.Log(logOperand), "ln({0})");
        /// <summary>
        /// An evaluator for the arbitrary-base log.
        /// </summary>
        public static readonly BinaryEvaluator Evaluator = new BinaryEvaluator((logBase, logOperand) => Math.Log(logOperand, logBase), "log[{0}]({1})");

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            if (Base.Count == 1 && Base[0] is ConstantToken && (Base[0] as ConstantToken).Value == ConstantToken.ConstantType.E)
                return new UnaryParseTreeNode(NaturalLogEvaluator, Operand.Parse());
            else
                return new BinaryParseTreeNode(Evaluator, Base.Parse(), Operand.Parse());
        }

        /// <summary>
        /// Represents the type of drawing simplification to be performed on a <c>Graphmatic.Expressions.Tokens.LogToken</c> object.<para/>
        /// For example, log-base-10 tokens omit the <c>10</c> subscript when drawing, and log-base-e changes the <c>log_e</c> text to read <c>ln</c> instead.
        /// </summary>
        private enum SimplificationType
        {
            /// <summary>
            /// No simplification.
            /// </summary>
            None,
            /// <summary>
            /// Simplify the display to omit the subscript <c>10</c>.
            /// </summary>
            LogTen,
            /// <summary>
            /// Simplify the display to read <c>ln</c> rather than <c>log_e</c>.
            /// </summary>
            LogE
        }
    }
}
