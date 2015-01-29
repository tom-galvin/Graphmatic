using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Represents a visual graph onto which <c>Graphmatic.Interaction.Plotting.IPlottable</c> implementations
    /// can be plotted.
    /// </summary>
    [GraphmaticObject]
    public class Graph : IEnumerable<IPlottable>, IXmlConvertible
    {
        /// <summary>
        /// The resources plotted on this graph.
        /// </summary>
        private Dictionary<IPlottable, PlottableParameters> Resources;

        /// <summary>
        /// Resources to be plotted on this graph that haven't yet been resolved
        /// after deserialization.
        /// </summary>
        private Dictionary<Guid, PlottableParameters> UnresolvedResources;

        /// <summary>
        /// Gets the parameter set used to plot this graph onto the screen.
        /// </summary>
        public GraphParameters Parameters
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the set of axes on the graph.
        /// This is plotted as a resource like all other <c>IPlottable</c>s.
        /// </summary>
        public GraphAxis Axes
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the key on the graph.
        /// This is plotted as a resource like all other <c>IPlottable</c>s.
        /// </summary>
        public GraphKey Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the <c>PlottableParameters</c> used to plot a resource contained within this graph.<para/>
        /// This returns the value of <c>GetParameters(</c><paramref name="plottable"/><c>)</c>.
        /// </summary>
        /// <param name="plottable">The <c>IPlottable</c> for which to get the drawing parameters.</param>
        public PlottableParameters this[IPlottable plottable]
        {
            get
            {
                return GetParameters(plottable);
            }
        }

        /// <summary>
        /// Fired whenever a resource is added, changed or removed from the Graph. Whether this event is fired
        /// when a resource is changed depends on the specific implementation of that resource.
        /// </summary>
        public event EventHandler Update;

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.Graph</c> class with a new <c>GraphParameters</c> parameter set.
        /// </summary>
        public Graph()
            : this(new GraphParameters())
        {

        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.Graph</c> class with the given parameter set.
        /// </summary>
        /// <param name="graphParameters">The parameters to use for plotting the graph.</param>
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

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.Graph</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
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
                if (plottable.Name == "Reference")
                {
                    UnresolvedResources.Add(Guid.Parse(plottable.Attribute("ID").Value), parameters);
                }
                else
                {
                    IPlottable resource = plottable.Deserialize<IPlottable>();
                    Resources.Add(resource, parameters);
                    if (resource is GraphKey)
                        Key = resource as GraphKey;
                    else if (resource is GraphAxis)
                        Axes = resource as GraphAxis;
                }
            }
        }

        /// <summary>
        /// Adds a resource to this Graph with the given drawing parameters.
        /// </summary>
        /// <param name="plottable">The <c>IPlottable</c> to add to the graph.</param>
        /// <param name="parameters">The parameters to use for plotting the object.</param>
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
        
        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Plotting.Graph</c> class with the given parameter set.
        /// </summary>
        /// <param name="graphParameters">The parameters to use for plotting the graph.</param>
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

        /// <summary>
        /// Removes <paramref name="plottable"/> from this graph.
        /// </summary>
        /// <param name="plottable">The thing to remove from this graph.</param>
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

        /// <summary>
        /// Removes all resources from the graph.
        /// </summary>
        public void Clear()
        {
            Resources.Clear();
        }

        /// <summary>
        /// Draws this Graph onto the specified GDI+ drawing surface.
        /// </summary>
        /// <param name="g">The GDI+ drawing surface to draw onto.</param>
        /// <param name="size">The size of the GDI+ drawing surface.</param>
        /// <param name="resolution">The resolution to plot with. Coarser resolutions may allow
        /// the graph to be plotted faster at the expense of visual accuracy, though this is
        /// dependent on the implementations of the resources on the Graph.</param>
        public void Draw(Graphics g, Size size, PlotResolution resolution)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

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
                            plottable.Key.PlotOnto(this, g, size, plottable.Value, resolution);
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

        /// <summary>
        /// Converts a point in graph space to a location in screen (display) space.
        /// </summary>
        /// <param name="graphSize">The size of this graph on the screen.</param>
        /// <param name="horizontal">The horizontal co-ordinate in screen space.</param>
        /// <param name="vertical">The vertical co-ordinate in screen space.</param>
        /// <param name="x">The variable in which to store the horizontal co-ordinate in graph space.</param>
        /// <param name="y">The variable in which to store the vertical co-ordinate in graph space.</param>
        public void ToImageSpace(Size graphSize, double horizontal, double vertical, out int x, out int y)
        {
            x = graphSize.Width / 2 +
                (int)((horizontal - Parameters.CenterHorizontal) / Parameters.HorizontalPixelScale);
            y = graphSize.Height / 2 -
                (int)((vertical - Parameters.CenterVertical) / Parameters.VerticalPixelScale);
        }

        /// <summary>
        /// Converts a point in screen (display) space to a location in graph space.
        /// </summary>
        /// <param name="graphSize">The size of this graph on the screen.</param>
        /// <param name="x">The horizontal co-ordinate in graph space.</param>
        /// <param name="y">The vertical co-ordinate in graph space.</param>
        /// <param name="horizontal">The variable in which to store the horizontal co-ordinate in screen space.</param>
        /// <param name="vertical">The variable in which to store the vertical co-ordinate in screen space.</param>
        public void ToScreenSpace(Size graphSize, int x, int y, out double horizontal, out double vertical)
        {
            horizontal = Parameters.HorizontalPixelScale * (x - graphSize.Width / 2) + Parameters.CenterHorizontal;
            vertical = Parameters.VerticalPixelScale * -(y - graphSize.Height / 2) + Parameters.CenterVertical;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Resources.Keys.GetEnumerator();
        }

        IEnumerator<IPlottable> IEnumerable<IPlottable>.GetEnumerator()
        {
            return Resources.Keys.GetEnumerator() as IEnumerator<IPlottable>;
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
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

        /// <summary>
        /// Updates any references to other resources in the document to point to the correct resource.<para/>
        /// This method (and related method <c>Graphmatic.Interaction.Resource.ToResourceReference()</c>) are
        /// needed because certain resources, such as the Page resource, can refer to other resources from within
        /// them (for example if a Page contains a plotted DataSet). However, if the Page is deserialized before
        /// the DataSet, then it will not have an object to refer to. Thus, the resource reference system is used,
        /// whereby certain objects (such as the Page) keep track of which resources they need to refer to later on,
        /// and only actually dereference the Resource references (via the resource's GUID) after all resources in
        /// the document have been deserialized.
        /// </summary>
        /// <param name="document">The parent document containing this resource, and any other resources that this
        /// resource may point to.</param>
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

        /// <summary>
        /// Called when another resource in the Document containing this resource is modified.
        /// This allows removed resource to also be removed from the Graph.
        /// </summary>
        /// <param name="resource">The other resource which was modified.</param>
        /// <param name="type">The type of resource modification which took place.</param>
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