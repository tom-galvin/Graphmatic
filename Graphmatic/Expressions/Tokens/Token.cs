using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Interaction;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a single atomic token (or component) in a mathematical expression.
    /// </summary>
    public abstract class Token : IPaintable, IXmlConvertible
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

        /// <summary>
        /// Initializes a new instance of the <c>Token</c> class.
        /// </summary>
        public Token()
        {
            Children = new Expression[] { };
        }

        /// <summary>
        /// Paint this token onto the specified GDI+ drawing surface.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to draw this token onto.</param>
        /// <param name="expressionCursor">The expression cursor to draw inside the token.</param>
        /// <param name="x">The X co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        /// <param name="y">The y co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        public abstract void Paint(Graphics graphics, ExpressionCursor expressionCursor, int x, int y);

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public abstract void RecalculateDimensions(ExpressionCursor expressionCursor);

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public abstract XElement ToXml();

        /// <summary>
        /// Updates any resource references contained in this Token.<para/>
        /// There shouldn't be any, so this method does nothing.
        /// </summary>
        /// <param name="document">The Document in which this element resides.</param>
        public sealed void UpdateReferences(Document document)
        {
        }
    }
}
