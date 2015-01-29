using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Exposes methods that a plottable graph resource needs to be plotted onto a <c>Graphmatic.Interaction.Plotting.Graph</c>.<para/>
    /// This does not cover annotations - those inherit from the <c>Graphmatic.Interaction.Annotations.Annotation</c>
    /// object and work considerably differently.
    /// </summary>
    public interface IPlottable : IXmlConvertible
    {
        /// <summary>
        /// Plots this IPlottable onto a given graph. The specific method of plotting will depend on the implementation of the
        /// IPlottable.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="plotParams">The parameters used to plot this IPlottable.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, PlotResolution resolution);

        /// <summary>
        /// Determines whether this plottable object can be plotted using the specific given variables.
        /// For example, if calling this method with parameters <c>'x'</c> and <c>'y'</c> on an Equation
        /// such as <c>y=x+1</c> will return true, however calling it on an equation such as <c>a=3b-7</c>
        /// will return false, as the equation does not equate those two variables.
        /// </summary>
        /// <param name="variable1">A variable plotted on an axis of a Graph.</param>
        /// <param name="variable2">A variable plotted on an axis of a Graph.</param>
        /// <returns>Returns whether this plottable resource can be plotted over the given variables.</returns>
        bool CanPlot(char variable1, char variable2);
    }
}
