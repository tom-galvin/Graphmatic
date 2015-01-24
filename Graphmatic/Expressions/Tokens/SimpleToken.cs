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
    /// Represents a token whose value can be drawn as simple characters from the expression engine's font's character set.<para/>
    /// Tokens of this type will have no child Expressions.
    /// </summary>
    public abstract class SimpleToken : Token
    {
        /// <summary>
        /// Gets the text displayed by this token.
        /// </summary>
        public abstract string Text
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the <c>SimpleToken</c> class.
        /// </summary>
        public SimpleToken()
            : base()
        {
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
            graphics.DrawExpressionString(Text, Size == DisplaySize.Small, x, y);
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Width = 5 * Text.Length + 1 * (Text.Length - 1);
            Height = Size == DisplaySize.Large ? 9 : 6;
        }
    }
}
