using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public interface IPlottable : IXmlConvertible
    {
        void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams, PlotResolution resolution);

        bool CanPlot(char variable1, char variable2);
    }
}
