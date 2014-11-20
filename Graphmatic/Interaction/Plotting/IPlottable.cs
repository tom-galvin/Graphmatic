using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public interface IPlottable
    {
        void PlotOnto(Graph graph, Graphics graphics, Size graphSize, Color color, PlotParameters parameters);
    }
}
