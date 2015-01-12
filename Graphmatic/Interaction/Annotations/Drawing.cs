using Graphmatic.Interaction.Plotting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction.Annotations
{
    public class Drawing : Annotation
    {
        public Tuple<double, double>[] Points
        {
            get;
            set;
        }

        public float Thickness
        {
            get;
            set;
        }

        public DrawingType Type
        {
            get;
            set;
        }

        public Drawing(Point[] screenPoints, Size graphSize, GraphParameters parameters, Color color, float thickness, DrawingType type)
        {
            Points = screenPoints
                .Select(p => ToGraphSpace(p, graphSize, parameters))
                .ToArray();
            var firstPoint = Points.First();

            double x1 = firstPoint.Item1, y1 = firstPoint.Item2,
                x2 = firstPoint.Item1, y2 = firstPoint.Item2;

            foreach(var point in Points)
            {
                if(point.Item1 < x1) x1 = point.Item1;
                if(point.Item1 < y1) y1 = point.Item2;
                if(point.Item1 > x2) x2 = point.Item1;
                if(point.Item1 > y2) y2 = point.Item2;
            }

            X = x1;
            Y = y1;
            Width = x2 - x1;
            Height = y2 - y1;

            Points = Points
                .Select(p =>
                    new Tuple<double, double>((p.Item1 - X) / Width, (p.Item2 - Y) / Height))
                .ToArray();

            Color = color;
            Thickness = thickness;
            Type = type;
        }

        public Drawing(XElement xml)
            : base(xml)
        {
            Points = xml
                .Element("Points")
                .Elements("Point")
                .Select(x =>
                    new Tuple<double, double>(
                        Double.Parse(x.Attribute("X").Value),
                        Double.Parse(x.Attribute("Y").Value)))
                .ToArray();
            Thickness = Single.Parse(xml.Attribute("Thickness").Value);
            Type = (DrawingType)Enum.Parse(typeof(DrawingType), xml.Attribute("Type").Value);
        }

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Drawing";
            baseElement.Add(new XElement("Points",
                Points.Select(p => new XElement("Point",
                    new XAttribute("X", p.Item1),
                    new XAttribute("Y", p.Item2)))),
                new XAttribute("Thickness", Thickness),
                new XAttribute("Type", Type.ToString()));
            return baseElement;
        }

        public override void SelectDrawOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            Point[] screenPoints = Points
                .Select(p =>
                {
                    int x, y;
                    page.Graph.ToImageSpace(
                        graphSize, graphParams,
                        p.Item1 * Width + X,
                        p.Item2 * Height + Y,
                        out x, out y);
                    return new Point(x, y);
                })
                .ToArray();
            Pen annotationPen = new Pen(Color.ColorWarp(0.6), Thickness + 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            };
            graphics.DrawLines(annotationPen, screenPoints);
        }

        public override void DrawOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
        {
            Point[] screenPoints = Points
                .Select(p =>
                {
                    int x, y;
                    page.Graph.ToImageSpace(
                        graphSize, graphParams,
                        p.Item1 * Width + X,
                        p.Item2 * Height + Y,
                        out x, out y);
                    return new Point(x, y);
                })
                .ToArray();
            Pen annotationPen = new Pen(Color, Thickness);
            graphics.DrawLines(annotationPen, screenPoints);
        }

        private Tuple<double, double> ToGraphSpace(Point p, Size graphSize, GraphParameters parameters)
        {
            double x = ((double)(p.X - graphSize.Width / 2) * parameters.HorizontalPixelScale) + parameters.CenterHorizontal;
            double y = ((double)(p.Y - graphSize.Height / 2) * parameters.VerticalPixelScale) + parameters.CenterVertical;

            return new Tuple<double,double>(x, y);
        }
    }

    /// <summary>
    /// Represents the type of user-drawn annotation.
    /// This influences the rendering method.
    /// </summary>
    public enum DrawingType
    {
        /// <summary>
        /// A pencil-style annotation, drawn with normal blending.
        /// </summary>
        Pencil,
        /// <summary>
        /// A highlighter-style annotation, drawn with multiplicative blending.
        /// </summary>
        Highlight
    }
}
