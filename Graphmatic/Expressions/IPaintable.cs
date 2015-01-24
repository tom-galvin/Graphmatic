using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Exposes methods for painting a component of a mathematical expression to the screen.
    /// </summary>
    public interface IPaintable
    {
        /// <summary>
        /// Gets the width of the expression.
        /// </summary>
        int Width
        {
            get;
        }

        /// <summary>
        /// Gets the height of the expression.
        /// </summary>
        int Height
        {
            get;
        }

        /// <summary>
        /// Gets the vertical ascension of the element from the top of the container.
        /// </summary>
        int BaselineOffset
        {
            get;
        }

        /// <summary>
        /// Gets or sets the display size style (large or small) of the drawable element.
        /// </summary>
        DisplaySize Size
        {
            get;
            set;
        }

        /// <summary>
        /// Paint this <c>IPaintable</c> onto the specified GDI+ drawing surface.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to draw this object onto.</param>
        /// <param name="expressionCursor">The expression cursor to draw inside the object.</param>
        /// <param name="x">The X co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        /// <param name="y">The y co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        void Paint(Graphics graphics, ExpressionCursor expressionCursor, int x, int y);

        /// <summary>
        /// Recalculate the dimensions of the component in a recursive manner.
        /// </summary>
        /// <param name="expressionCursor">Cursor information to draw the cursor I-beam with.</param>
        void RecalculateDimensions(ExpressionCursor expressionCursor);
    }
}
