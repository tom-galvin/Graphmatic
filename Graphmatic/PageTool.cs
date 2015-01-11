using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic
{
    /// <summary>
    /// Defines different tools which can be used on a Graphmatic graph page.
    /// </summary>
    public enum PageTool
    {
        /// <summary>
        /// Select annotations by drawing a box around them, or move selections around.
        /// </summary>
        Select,
        /// <summary>
        /// Move the viewport around.
        /// </summary>
        Pan,
        /// <summary>
        /// Erase annotations by clicking on them.
        /// </summary>
        Eraser,
        /// <summary>
        /// Draw annotations in pencil style.
        /// </summary>
        Pencil,
        /// <summary>
        /// Draw annotations in highlighter style.
        /// </summary>
        Highlighter
    }
}
