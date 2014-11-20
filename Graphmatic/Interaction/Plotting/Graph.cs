using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public class Graph
    {
        private Dictionary<IPlottable, Color> Resources;

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

        protected GraphAxis Axes
        {
            get;
            set;
        }

        protected Color AxisColor
        {
            get
            {
                return GetColor(Axes);
            }
            set
            {
                SetColor(Axes, value);
            }
        }

        public Graph()
        {
            Resources = new Dictionary<IPlottable, Color>();
            Axes = new GraphAxis();
            Resources.Add(Axes, Color.Black);
            HorizontalAxis = Properties.Settings.Default.DefaultHorizontalAxis;
            VerticalAxis = Properties.Settings.Default.DefaultVerticalAxis;
        }

        public IEnumerable<IPlottable> GetContent()
        {
            foreach(IPlottable plottable in Resources.Keys)
            {
                if (plottable != Axes)
                {
                    yield return plottable;
                }
            }
        }

        public void Add(IPlottable plottable, Color color)
        {
            if(Resources.ContainsKey(plottable))
                throw new InvalidOperationException("The Graph already contains this IPlottable.");
            else
                Resources.Add(plottable, color);
        }

        public void SetColor(IPlottable plottable, Color color)
        {
            if (Resources.ContainsKey(plottable))
                Resources[plottable] = color;
            else
                throw new InvalidOperationException("The Graph does not contain this IPlottable.");
        }

        public Color GetColor(IPlottable plottable)
        {
            if (Resources.ContainsKey(plottable))
                return Resources[plottable];
            else
                throw new InvalidOperationException("The Graph does not contain this IPlottable.");
        }

        public void Remove(IPlottable plottable)
        {
            if (Resources.ContainsKey(plottable))
                Resources.Remove(plottable);
            else
                throw new InvalidOperationException("The Graph does not contain this IPlottable.");
        }

        public void Clear()
        {
            Resources.Clear();
        }

        public Image ToImage(Size size, PlotParameters parameters)
        {
            Bitmap graphBitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(graphBitmap);

            foreach (KeyValuePair<IPlottable, Color> plottable in Resources)
            {
                plottable.Key.PlotOnto(this, g, size, plottable.Value, parameters);
            }

            return graphBitmap;
        }

        public void ToImageSpace(Size graphSize, PlotParameters parameters, double horizontal, double vertical, out int x, out int y)
        {
            x = graphSize.Width / 2 +
                (int)((horizontal - parameters.CenterHorizontal) / parameters.HorizontalPixelScale);
            y = graphSize.Height / 2 -
                (int)((vertical - parameters.CenterVertical) / parameters.VerticalPixelScale);
        }
    }
}