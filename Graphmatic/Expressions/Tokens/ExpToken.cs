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
    /// Represents a token denoting an expression raised to the power of another expression.
    /// </summary>
    public class ExpToken : Token, ICollectorToken, IParsable
    {
        /// <summary>
        /// Gets the token collection type for this token collector.
        /// </summary>
        public CollectorTokenType CollectorType
        {
            get
            {
                return CollectorTokenType.Weak;
            }
        }

        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                return (Height - Base.Height) + (Base.Count == 0 ? 0 : Base
                    .Select(token => token.BaselineOffset)
                    .Aggregate((b1, b2) => Math.Max(b1, b2)));
            }
        }

        /// <summary>
        /// Gets the expression containing the base of the exponent operator.
        /// </summary>
        public Expression Base
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the expression containing the power of the exponent operator.
        /// </summary>
        public Expression Power
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
                return Base;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>ExpToken</c> class.
        /// </summary>
        public ExpToken()
            : base()
        {
            Base = new Expression(this);
            Power = new Expression(this);
            Children = new Expression[] { Base, Power };
        }

        /// <summary>
        /// Initializes a new instance of the <c>ExpToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public ExpToken(XElement xml)
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
            return new XElement("Exp",
                new XElement("Base", Base.ToXml()),
                new XElement("Power", Power.ToXml()));
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
            Base.Paint(graphics, expressionCursor, x, y + Height - Base.Height);
            Power.Paint(graphics, expressionCursor, x + Base.Width + 1, y);
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Base.Size = Size;
            Base.RecalculateDimensions(expressionCursor);
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            Width = Base.Width + Power.Width + 1;
            Height = Base.Height + Power.Height - (Size == DisplaySize.Small ? 0 : 3);
        }

        /// <summary>
        /// An evaluator for the exponent (power) operator.
        /// </summary>
        public static readonly BinaryEvaluator Evaluator = new BinaryEvaluator((powBase, powPower) => Math.Pow(powBase, powPower), "pow[{1}]({0})");

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            return new BinaryParseTreeNode(Evaluator, Base.Parse(), Power.Parse());
        }
    }
}
