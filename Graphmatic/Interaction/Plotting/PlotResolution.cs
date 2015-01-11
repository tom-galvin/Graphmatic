using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Defines a set of resolutions at which graphs can be rendered.
    /// </summary>
    public enum PlotResolution
    {
        /// <summary>
        /// Standard viewing resolution (slowest).
        /// </summary>
        View,
        /// <summary>
        /// Editing resolution (medium).
        /// </summary>
        Edit,
        /// <summary>
        /// Resizing resolution (fastest). Some graph elements may not be drawn.
        /// </summary>
        Resize
    }
}
