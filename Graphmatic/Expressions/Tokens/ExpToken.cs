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
    /// Represents a token denoting a value raised to the power of another expression.<para/>
    /// The <c>ExpToken</c> is unique in that its size can potentially be determined by the size of tokens
    /// not directly contained within it. The height (and baseline offset) of an ExpToken is determined by
    /// the nearest token to the left of this ExpToken that is not in itself an ExpToken.
    /// </summary>
    [GraphmaticObject]
    public class ExpToken : Token
    {
        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                Token operand = Operand;
                if (operand != null)
                    return Height - operand.Height + operand.BaselineOffset;
                else
                    return Height - (Size == DisplaySize.Large ? 9 : 6);
            }
        }

        /// <summary>
        /// The Token that this ExpToken will use for determining the display height. This is
        /// not used for parsing, but rather for displaying the ExpToken visually, and as such
        /// this Operand might not be the actual operand of the ExpToken, but rather just the
        /// token used for determining the height of the ExpToken above the baseline.
        /// </summary>
        public Token Operand
        {
            get
            {
                if (Parent != null)
                {
                    // finds the first token before this ExpToken that is not, itself, an ExpToken
                    int index = this.IndexInParent();
                    while (--index >= 0)
                    {
                        if (!(Parent[index] is ExpToken))
                        {
                            return Parent[index];
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
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
                // only move the cursor inside the power of the ExpToken when the power is empty...
                // if you place a ^2 (squared) you want to skip right over it rather than editing
                // the two
                if (Power.Count == 0)
                {
                    return Power;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>ExpToken</c> class.
        /// </summary>
        public ExpToken()
            : base()
        {
            Power = new Expression(this);
            Children = new Expression[] { Power };
        }

        /// <summary>
        /// Initializes a new instance of the <c>ExpToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public ExpToken(XElement xml)
            : base()
        {
            Power = new Expression(this, xml.Element("Power").Elements());
            Children = new Expression[] { Power };
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("ExpToken",
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
            Power.Paint(graphics, expressionCursor, x, y);
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            Width = Power.Width;
            Height = (Operand == null ? 9 : Operand.Height) + Power.Height - (Size == DisplaySize.Small ? 0 : 3);
        }
    }
}
