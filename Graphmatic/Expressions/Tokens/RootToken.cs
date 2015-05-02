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
    /// Represents the nth root of some other value.
    /// </summary>
    [GraphmaticObject]
    public class RootToken : Token, IParsable
    {
        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                return Base.BaselineOffset + Height - Base.Height;
            }
        }

        /// <summary>
        /// Gets the Expression containing the base of this nth-root operation.
        /// </summary>
        public Expression Base
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the Expression containing the (inverse) power of this nth-root operation.<para/>
        /// For example, a square-root would have a power of 2. This power is the reciprocal of the
        /// equivalent exponent power.
        /// </summary>
        public Expression Power
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the default child expression that the cursor is placed into when the token is inserted, or null to place the cursor after the expression.<para/>
        /// For a square-root, this jumps to the base of the expression. Otherwise, this jumps to the power.
        /// </summary>
        public override Expression DefaultChild
        {
            get
            {
                return Simplified ? Base : Power;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>RootToken</c> class.
        /// </summary>
        public RootToken()
            : base()
        {
            Base = new Expression(this);
            Power = new Expression(this);
            Children = new Expression[] { Power, Base };
        }

        /// <summary>
        /// Initializes a new instance of the <c>RootToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public RootToken(XElement xml)
            : base()
        {
            Base = new Expression(this, xml.Element("Base").Elements());
            Power = new Expression(this, xml.Element("Power").Elements());
            Children = new Expression[] { Base, Power };
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("RootToken",
                new XElement("Power", Power.ToXml()),
                new XElement("Base", Base.ToXml()));
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
            if (!Simplified)
                Power.Paint(graphics, expressionCursor, x, y);
            int xOffset = Simplified ? 2 : Power.Width;
            Base.Paint(graphics, expressionCursor, x + xOffset + 3, y + Height - Base.Height);

            // draw square-root symbol
            graphics.DrawLine(Expression.ExpressionPen,
                x + xOffset + 1,
                y + Height - Base.Height - 2,
                x + xOffset + 2 + Base.Width,
                y + Height - Base.Height - 2);

            graphics.DrawLine(Expression.ExpressionPen,
                x + xOffset + 1,
                y + Height - Base.Height - 2,
                x + xOffset,
                y + Height - 1);

            graphics.DrawLine(Expression.ExpressionPen,
                x + xOffset,
                y + Height - 1,
                x + xOffset - 2,
                y + Height - 3);
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Simplified = CanSimplifyDisplay(expressionCursor);
            Base.Size = Size;
            Base.RecalculateDimensions(expressionCursor);
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            if (!Simplified)
            {
                Width = Base.Width + Power.Width + 5;
                Height = Base.Height + Power.Height - 3;
            }
            else
            {
                Width = Base.Width + 5;
                Height = Base.Height + 2;
            }
        }

        private bool Simplified;
        /// <summary>
        /// Determines whether the display is to be simplified or not.<para/>
        /// If the power of the token is 2, and the cursor is not in the power expression, then this function
        /// returns true, as the power of the square-root is not shown (a radical symbol with no index is
        /// by convention a square-root).
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use for determining whether to simplify the display of the token or not.</param>
        /// <returns>Returns true if the power index of the root can be hidden; false otherwise.</returns>
        private bool CanSimplifyDisplay(ExpressionCursor expressionCursor)
        {
            return
                expressionCursor.Expression != Power &&
                Power.Count == 1 &&
                Power[0] is DigitToken &&
                (Power[0] as DigitToken).Value == 2;
        }

        /// <summary>
        /// An evaluator for the nth-root of the operand.<para/>
        /// This is the equivalent of the evaluator for the ExpToken except with the power being reciprocated.
        /// </summary>
        public static readonly BinaryEvaluator Evaluator = new BinaryEvaluator((rootBase, rootPower) => Math.Pow(rootBase, 1 / rootPower), "√[{1}]({0})");
        /// <summary>
        /// An evaluator for the square-root of the operand.
        /// </summary>
        public static readonly UnaryEvaluator SquareRootEvaluator = new UnaryEvaluator(x => Math.Sqrt(x), "√({0})");

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            if(Power.Count == 1 && Power[0] is DigitToken && (Power[0] as DigitToken).Value == 2)
                return new UnaryParseTreeNode(SquareRootEvaluator, Base.Parse());
            else
                return new BinaryParseTreeNode(Evaluator, Base.Parse(), Power.Parse());
        }
    }
}
