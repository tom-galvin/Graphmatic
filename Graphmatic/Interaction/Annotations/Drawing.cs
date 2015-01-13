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

            double
                x1 = Double.PositiveInfinity, y1 = Double.PositiveInfinity,
                x2 = Double.NegativeInfinity, y2 = Double.NegativeInfinity;

            foreach(var point in Points)
            {
                if(point.Item1 < x1) x1 = point.Item1;
                if(point.Item2 < y1) y1 = point.Item2;
                if(point.Item1 > x2) x2 = point.Item1;
                if(point.Item2 > y2) y2 = point.Item2;
            }

            X = x1;
            Y = y1;
            Width = x2 - x1;
            Height = y2 - y1;
            if (Width == 0) Width = 0.1;
            if (Height == 0) Height = 0.1;

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
                    new XAttribute("X", p.Item1.ToString("0.####")),
                    new XAttribute("Y", p.Item2.ToString("0.####"))))),
                new XAttribute("Thickness", Thickness),
                new XAttribute("Type", Type.ToString()));
            return baseElement;
        }

        public override void DrawSelectionIndicatorOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
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
            Brush annotationBrush = new SolidBrush(Color.Red);
            foreach (var screenPoint in screenPoints)
            {
                graphics.FillEllipse(annotationBrush,
                    screenPoint.X - (int)(Thickness * 0.65),
                    screenPoint.Y - (int)(Thickness * 0.65),
                    (int)(Thickness * 1.3),
                    (int)(Thickness * 1.3));
            }
            base.DrawSelectionIndicatorOnto(page, graphics, graphSize, graphParams, resolution);
        }

        public override void DrawAnnotationOnto(Page page, Graphics graphics, Size graphSize, GraphParameters graphParams, PlotResolution resolution)
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
            annotationPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            graphics.DrawLines(annotationPen, screenPoints);
        }

        public override int DistanceToPointOnScreen(Page page, Size graphSize, GraphParameters graphParams, Point screenSelection)
        {
            // if selection is in resize node, it's ALWAYS in the boundaries of the selection
            if (IsPointInResizeNode(page, graphSize, graphParams, screenSelection)) return -1;

            var graphSpaceSelection = ToGraphSpace(screenSelection, graphSize, graphParams);
            double minimumSquareDistance = Double.PositiveInfinity;
            for (int i = 0; i < Points.Length; i++)
            {
                var point = Points[i];
                double 
                    xDist = (X + Width * point.Item1) - graphSpaceSelection.Item1,
                    yDist = (Y + Height * point.Item2) - graphSpaceSelection.Item2;
                double squareDistance = xDist * xDist + yDist * yDist;
                if (squareDistance < minimumSquareDistance) minimumSquareDistance = squareDistance;
            }
            int distanceAsInteger = (int)(Math.Sqrt(minimumSquareDistance) / page.Graph.Parameters.HorizontalPixelScale);
            return distanceAsInteger;
        }

        public override bool IsAnnotationInSelection(Page page, Size graphSize, GraphParameters graphParams, Rectangle screenSelection)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                var point = Points[i];
                int x, y;
                page.Graph.ToImageSpace(graphSize, graphParams, X + point.Item1 * Width, Y + point.Item2 * Height, out x, out y);
                if (screenSelection.Contains(x, y)) return true;
            }
            return false;
        }

        private Tuple<double, double> ToGraphSpace(Point p, Size graphSize, GraphParameters parameters)
        {
            double x = ((double)(p.X - graphSize.Width / 2) * parameters.HorizontalPixelScale) + parameters.CenterHorizontal;
            double y = -((double)(p.Y - graphSize.Height / 2) * parameters.VerticalPixelScale) + parameters.CenterVertical;

            return new Tuple<double,double>(x, y);
        }

        public void FlipHorizontal()
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new Tuple<double, double>(1 - Points[i].Item1, Points[i].Item2);
            }
        }

        public void FlipVertical()
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new Tuple<double, double>(Points[i].Item1, 1 - Points[i].Item2);
            }
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
