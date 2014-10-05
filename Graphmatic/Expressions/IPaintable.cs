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
        /// ets the vertical ascension of the expression from the top of the container.
        /// </summary>
        int BaselineOffset
        {
            get;
        }

        /// <summary>
        /// Paint the component to the screen.
        /// </summary>
        /// <param name="g">The <c>System.Drawing.Graphics</c> object to use to paint the component to the screen.</param>
        /// <param name="expressionCursor">Cursor information to draw the green cursor line with.</param>
        /// <param name="x">The X co-ordinate to paint at.</param>
        /// <param name="y">The Y co-ordinate to paint at.</param>
        void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y);

        /// <summary>
        /// Recalculate the dimensions of the component in a recursive manner.
        /// </summary>
        void RecalculateDimensions();
    }
}
