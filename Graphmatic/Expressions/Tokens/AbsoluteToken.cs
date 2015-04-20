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
    /// Represents a token whose value is the absolute (positive) value of some other expression.
    /// </summary>
    [GraphmaticObject]
    public class AbsoluteToken : FunctionToken, IParsable
    {
        /// <summary>
        /// Initializes a new instance of the <c>AbsoluteToken</c> class.
        /// </summary>
        public AbsoluteToken()
            : base("Abs")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>AbsoluteToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public AbsoluteToken(XElement xml)
            : base(xml)
        {

        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("AbsoluteToken",
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
            Operand.Paint(graphics, expressionCursor, x + 2, y);
            graphics.DrawLine(Expression.ExpressionPen,
                x,
                y,
                x,
                y + Height - 1);

            graphics.DrawLine(Expression.ExpressionPen,
                x + Operand.Width + 3,
                y,
                x + Operand.Width + 3,
                y + Height - 1);
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
            Width = Operand.Width + 4;
            Height = Operand.Height;
        }

        /// <summary>
        /// An evaluator for the absolute value operation.
        /// </summary>
        public static readonly UnaryEvaluator Evaluator = new UnaryEvaluator(x => Math.Abs(x), "|{0}|");

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing a syntax tree
        /// for this token and any children.</returns>
        public override ParseTreeNode Parse()
        {
            return new UnaryParseTreeNode(Evaluator, Operand.Parse());
        }
    }
}
