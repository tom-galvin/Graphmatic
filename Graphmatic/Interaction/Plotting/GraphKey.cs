using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public class GraphKey : IPlottable
    {
        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams)
        {
            Font font = SystemFonts.DefaultFont;
            Brush brush = new SolidBrush(plotParams.PlotColor);

            float startY = graphSize.Height;
            int offset = 5;
            foreach (KeyValuePair<IPlottable, PlottableParameters> plot in graph)
            {
                if (plot.Key is Resource)
                {
                    var resource = plot.Key as Resource;
                    string name = resource.Name;
                    var size = graphics.MeasureString(name, font);
                    startY -= size.Height + offset;

                    graphics.DrawString(name, font, brush, graphSize.Width - offset - size.Width, startY);
                }
            }
        }
    }
}