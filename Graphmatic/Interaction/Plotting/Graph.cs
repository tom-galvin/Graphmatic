using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    public class Graph : IEnumerable<KeyValuePair<IPlottable, PlottableParameters>>
    {
        private Dictionary<IPlottable, PlottableParameters> Resources;

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

        protected GraphKey Key
        {
            get;
            set;
        }

        protected Color AxisColor
        {
            get
            {
                return Resources[Axes].PlotColor;
            }
            set
            {
                Resources[Axes].PlotColor = value;
            }
        }

        protected Color KeyTextColor
        {
            get
            {
                return Resources[Key].PlotColor;
            }
            set
            {
                Resources[Key].PlotColor = value;
            }
        }

        public Graph()
        {
            Resources = new Dictionary<IPlottable, PlottableParameters>();
            Axes = new GraphAxis(1, 5);
            Key = new GraphKey();
            Resources.Add(Axes, new PlottableParameters()
            {
                PlotColor = Color.Black
            });
            Resources.Add(Key, new PlottableParameters()
            {
                PlotColor = Color.Black
            });
            HorizontalAxis = Properties.Settings.Default.DefaultHorizontalAxis;
            VerticalAxis = Properties.Settings.Default.DefaultVerticalAxis;
        }

        public void Add(IPlottable plottable, PlottableParameters parameters)
        {
            if(Resources.ContainsKey(plottable))
                throw new InvalidOperationException("The Graph already contains this IPlottable.");
            else
                Resources.Add(plottable, parameters);
        }

        public PlottableParameters GetParameters(IPlottable plottable)
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

        public Image ToImage(Size size, GraphParameters parameters, bool renderContent = true)
        {
            Bitmap graphBitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(graphBitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            if (renderContent)
            {
                foreach (KeyValuePair<IPlottable, PlottableParameters> plottable in Resources)
                {
                    plottable.Key.PlotOnto(this, g, size, plottable.Value, parameters);
                }
            }
            else
            {
                Axes.PlotOnto(this, g, size, Resources[Axes], parameters);
            }

            return graphBitmap;
        }

        public void ToImageSpace(Size graphSize, GraphParameters parameters, double horizontal, double vertical, out int x, out int y)
        {
            x = graphSize.Width / 2 +
                (int)((horizontal - parameters.CenterHorizontal) / parameters.HorizontalPixelScale);
            y = graphSize.Height / 2 -
                (int)((vertical - parameters.CenterVertical) / parameters.VerticalPixelScale);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Resources.GetEnumerator();
        }

        IEnumerator<KeyValuePair<IPlottable, PlottableParameters>> IEnumerable<KeyValuePair<IPlottable, PlottableParameters>>.GetEnumerator()
        {
            return Resources.GetEnumerator() as IEnumerator<KeyValuePair<IPlottable, PlottableParameters>>;
        }
    }
}