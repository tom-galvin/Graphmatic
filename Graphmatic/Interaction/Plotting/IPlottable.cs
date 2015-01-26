using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Exposes methods that a plottable graph resource needs to be plotted onto a graph.<para/>
    /// This does not cover annotations - those inherit from the <c>Graphmatic.Interaction.Annotations.Annotation</c>
    /// object and work considerably differently.
    /// </summary>
    public interface IPlottable : IXmlConvertible
    {
        void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams, PlotResolution resolution);

        bool CanPlot(char variable1, char variable2);
    }
}
