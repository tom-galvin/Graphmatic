using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public struct PlotParameters
    {
        public double CenterHorizontal
        {
            get;
            set;
        }

        public double CenterVertical
        {
            get;
            set;
        }

        public double HorizontalPixelScale
        {
            get;
            set;
        }

        public double VerticalPixelScale
        {
            get;
            set;
        }

        public char HorizontalAxis
        {
            get;
            set;
        }

        public char VerticalAxis
        {
            get;
            set;
        }
    }
}
