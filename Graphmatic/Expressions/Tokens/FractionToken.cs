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
    /// Represents a token whose value is the division of one value by another, represented as a fraction rather than a simple division with the
    /// classic obelus symbol.
    /// </summary>
    [GraphmaticObject]
    public class FractionToken : Token, ICollectorToken, IParsable
    {
        /// <summary>
        /// Gets the token collection type for this token collector.
        /// </summary>
        public CollectorTokenType CollectorType
        {
            get
            {
                return CollectorTokenType.Strong;
            }
        }

        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                return Top.Height + 1 - (Size == DisplaySize.Large ? 4 : 3);
            }
        }

        /// <summary>
        /// Gets the Expression containing the top of the fraction.
        /// </summary>
        public Expression Top
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the Expression containing the bottom of the fraction.
        /// </summary>
        public Expression Bottom
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
                return Top;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>FractionToken</c> class.
        /// </summary>
        public FractionToken()
            : base()
        {
            Top = new Expression(this);
            Bottom = new Expression(this);
            Children = new Expression[] { Top, Bottom };
        }

        /// <summary>
        /// Initializes a new instance of the <c>FractionToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public FractionToken(XElement xml)
            : base()
        {
            Top = new Expression(this, xml.Element("Top").Elements());
            Bottom = new Expression(this, xml.Element("Bottom").Elements());
            Children = new Expression[] { Top, Bottom };
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("FractionToken",
                new XElement("Top", Top.ToXml()),
                new XElement("Bottom", Bottom.ToXml()));
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
            Top.Paint(graphics, expressionCursor, x + (Width - Top.Width) / 2, y);
            Bottom.Paint(graphics, expressionCursor, x + (Width - Bottom.Width) / 2, y + Top.Height + 3);

            // draw fraction line
            graphics.DrawLine(Expression.ExpressionPen,
                x,
                y + Top.Height + 1,
                x + Width - 1,
                y + Top.Height + 1);
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Top.Size = Bottom.Size = DisplaySize.Small;
            Top.RecalculateDimensions(expressionCursor);
            Bottom.RecalculateDimensions(expressionCursor);
            Width = Math.Max(Top.Width, Bottom.Width);
            Height = Top.Height + Bottom.Height + 3;
        }

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            return new BinaryParseTreeNode(Expression.DivideEvaluator, Top.Parse(), Bottom.Parse());
        }
    }
}
