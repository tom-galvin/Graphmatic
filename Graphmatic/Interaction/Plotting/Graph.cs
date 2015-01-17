using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    public class Graph : IEnumerable<IPlottable>, IXmlConvertible
    {
        private Dictionary<IPlottable, PlottableParameters> Resources;
        private Dictionary<Guid, PlottableParameters> UnresolvedResources;

        public GraphParameters Parameters
        {
            get;
            protected set;
        }

        public GraphAxis Axes
        {
            get;
            protected set;
        }

        public GraphKey Key
        {
            get;
            protected set;
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

        public PlottableParameters this[IPlottable plottable]
        {
            get
            {
                return Resources[plottable];
            }
        }

        public event EventHandler Update;

        public Graph()
            : this(new GraphParameters())
        {

        }

        public Graph(GraphParameters graphParameters)
        {
            Parameters = graphParameters;
            Resources = new Dictionary<IPlottable, PlottableParameters>();
            Axes = new GraphAxis(1, 5);
            Key = new GraphKey();
            Resources.Add(Axes, new PlottableParameters()
            {
                PlotColor = Properties.Settings.Default.DefaultGraphFeatureColor
            });
            Resources.Add(Key, new PlottableParameters()
            {
                PlotColor = Properties.Settings.Default.DefaultGraphFeatureColor
            });
        }

        internal void OnUpdate()
        {
            var handler = Update;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public Graph(XElement xml)
        {
            UnresolvedResources = new Dictionary<Guid, PlottableParameters>();
            Resources = new Dictionary<IPlottable, PlottableParameters>();
            Parameters = new GraphParameters(xml.Element("GraphParameters"));
            XElement contents = xml.Element("Content");
            foreach (XElement content in contents.Elements())
            {
                XElement plottable = content.Elements()
                    .Where(x => x.Name != "PlottableParameters")
                    .First();
                PlottableParameters parameters = new PlottableParameters(content.Element("PlottableParameters"));
                if (plottable.Name == "GraphKey")
                {
                    GraphKey key = new GraphKey(plottable);
                    Key = key;
                    Resources.Add(key, parameters);
                }
                else if (plottable.Name == "GraphAxis")
                {
                    GraphAxis axes = new GraphAxis(plottable);
                    Axes = axes;
                    Resources.Add(axes, parameters);
                }
                else if (plottable.Name == "Reference")
                {
                    UnresolvedResources.Add(Guid.Parse(plottable.Attribute("ID").Value), parameters);
                }
                else
                {
                    throw new Exception("Unknown Graph content item " + plottable.Name + ".");
                }
            }
        }

        public void Add(IPlottable plottable, PlottableParameters parameters)
        {
            if (Resources.ContainsKey(plottable))
            {
                throw new InvalidOperationException("The Graph already contains this IPlottable.");
            }
            else
            {
                Resources.Add(plottable, parameters);
                OnUpdate();
            }
        }

        public PlottableParameters GetParameters(IPlottable plottable)
        {
            if (Resources.ContainsKey(plottable))
            {
                return Resources[plottable];
            }
            else
            {
                throw new InvalidOperationException("The Graph does not contain this IPlottable.");
            }
        }

        public void Remove(IPlottable plottable)
        {
            if (Resources.ContainsKey(plottable))
            {
                Resources.Remove(plottable);
                OnUpdate();
            }
            else
            {
                throw new InvalidOperationException("The Graph does not contain this IPlottable.");
            }
        }

        public void Clear()
        {
            Resources.Clear();
        }

        public void Draw(Graphics g, Size size, PlotResolution resolution)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            if (resolution == PlotResolution.View && false)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            }
            else
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            }

            float errorX = 5, errorY = 0;
            using (Font errorFont = new Font(SystemFonts.MessageBoxFont.FontFamily, 20f, FontStyle.Bold))
            {
                foreach (KeyValuePair<IPlottable, PlottableParameters> plottable in Resources)
                {
                    string plottableName =
                        plottable.Key is Resource ?
                        (plottable.Key as Resource).Name :
                        plottable.GetType().Name;
                    string error = null;

                    if (plottable.Key.CanPlot(Parameters.HorizontalAxis, Parameters.VerticalAxis))
                    {
                        try
                        {
                            plottable.Key.PlotOnto(this, g, size, plottable.Value, Parameters, resolution);
                        }
                        catch (Exception ex)
                        {

                            error = String.Format(
                                "Could not plot \"{0}\": {1} ({2})",
                                plottableName,
                                ex.Message,
                                ex.GetType().Name);
                        }
                    }
                    else
                    {
                        error = String.Format(
                            "Could not plot \"{0}\": it does not contain variables {1} and {2}.",
                            plottableName,
                            Parameters.HorizontalAxis,
                            Parameters.VerticalAxis);
                    }

                    if (error != null)
                    {
                        SizeF errorSize = g.MeasureString(error, errorFont, size.Width);
                        g.DrawString(error, errorFont, Brushes.Red, new RectangleF(
                            errorX,
                            errorY,
                            size.Width - errorY,
                            size.Height - errorX));
                        errorY += 5 + errorSize.Height;
                    }
                }
            }
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
            return Resources.Keys.GetEnumerator();
        }

        IEnumerator<IPlottable> IEnumerable<IPlottable>.GetEnumerator()
        {
            return Resources.Keys.GetEnumerator() as IEnumerator<IPlottable>;
        }

        public XElement ToXml()
        {
            return new XElement("Graph",
                Parameters.ToXml(),
                new XElement("Content",
                    Resources.Select(kvp =>
                        new XElement("Item",
                            kvp.Value.ToXml(),
                            (kvp.Key is Resource ?
                            (kvp.Key as Resource).ToResourceReference() :
                            kvp.Key.ToXml())))));
        }


        public void UpdateReferences(Document document)
        {
            if (UnresolvedResources != null)
            {
                foreach (var reference in UnresolvedResources)
                {
                    Resources.Add(document[reference.Key] as IPlottable, reference.Value);
                }
                UnresolvedResources = null;
            }
        }

        public void ResourceModified(Resource resource, ResourceModifyType type)
        {
            if (resource is IPlottable)
            {
                IPlottable plottable = resource as IPlottable;
                if (Resources.ContainsKey(plottable))
                {
                    if (type == ResourceModifyType.Remove)
                    {
                        Remove(plottable);
                        OnUpdate();
                    }
                    else if (type == ResourceModifyType.Modify)
                    {
                        OnUpdate();
                    }
                }
            }
        }
    }
}