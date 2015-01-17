using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Defines a set of axis label types that are used to draw the graph in the Graphmatic editor.
    /// </summary>
    public enum GraphAxisType
    {
        /// <summary>
        /// Defines an axis labelled in radians.
        /// </summary>
        Radians,
        /// <summary>
        /// Defines an axis labelled in degrees.
        /// </summary>
        Degrees,
        /// <summary>
        /// Defines an axis labelled in grads (lel).
        /// </summary>
        Grads,
    }
}
