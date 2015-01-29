using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Parsing;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction
{
    public partial class Equation : Resource, IPlottable
    {
        /// <summary>
        /// The GDI+ pen width to use for plotting equations.
        /// </summary>
        public const float EquationPenWidth = 2f;

        /// <summary>
        /// The marching-squares grid width to use for plotting equations.
        /// </summary>
        public const int EquationResolution = 3;

        /// <summary>
        /// Plots this Equation onto the specified <paramref name="graph"/> with the given <paramref name="plotParams"/>.
        /// This method will determine which method of plotting is most appropriate for this equation. This could be the
        /// implicit plotting method for implicit equations, or explicit plotting with respect to one of the two plotted
        /// variables in the graph. Explicit plotting will be attempted wherever possible.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="plotParams">The parameters used to plot this IPlottable.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, PlotResolution resolution)
        {
            if (resolution == PlotResolution.Resize) return;
            if (ParseTree == null)
                Parse();
            BinaryParseTreeNode parseTreeRoot = ParseTree as BinaryParseTreeNode;

            var originalSmoothingMode = graphics.SmoothingMode;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // smoother pen for the graph

            using (Pen graphPen = new Pen(plotParams.PlotColor, EquationPenWidth))
            {
                if (parseTreeRoot.Left is VariableParseTreeNode && // if the only instance of a variable is on the left eg. y=x+1
                    !parseTreeRoot.Right.ContainsVariable((parseTreeRoot.Left as VariableParseTreeNode).Variable))
                {
                    PlotExplicit(
                        graph,
                        graphics,
                        graphSize,
                        graphPen,
                        (parseTreeRoot.Left as VariableParseTreeNode).Variable,
                        parseTreeRoot.Right,
                        resolution);
                }
                else if (parseTreeRoot.Right is VariableParseTreeNode && // if the only instance of a variable is on the right eg. y-1=x
                    !parseTreeRoot.Left.ContainsVariable((parseTreeRoot.Right as VariableParseTreeNode).Variable))
                {
                    PlotExplicit(
                        graph,
                        graphics,
                        graphSize,
                        graphPen,
                        (parseTreeRoot.Right as VariableParseTreeNode).Variable,
                        parseTreeRoot.Left,
                        resolution);
                }
                else // if variables are equated implicitly eg. xy=x+y-x^2
                {
                    PlotImplicit(graph, graphics, graphSize, graphPen, resolution);
                }
            }

            graphics.SmoothingMode = originalSmoothingMode;
        }

        /// <summary>
        /// Reverse-interpolates linearly between two values <paramref name="a"/> and <paramref name="b"/>
        /// to find the fractional distance from a to b at which zero lies, multiplied by the given
        /// <paramref name="scale"/>. For example, if a=-2 and b=3, then the fractional distance will be
        /// 0.4, as zero is 40% of the way between -2 and 3. This value is multiplied by an integer value
        /// and this product is returned as an integer.<para/>
        /// This is used during the marching-squares algorithm to estimate the point at which the value
        /// of a function changes sign in a square on the screen; see the Graphmatic coursework specification
        /// for a lengthier explanation of this.
        /// </summary>
        /// <param name="scale">The scale to multiply the fractional distance by.</param>
        /// <param name="a">A value to reverse interpolate between.</param>
        /// <param name="b">A value to reverse interpolate between.</param>
        private int ZeroReverseLerp(int scale, double a, double b)
        {
            return (int)(a / (a - b) * (double)scale);
        }

        /// <summary>
        /// Plots this equation implicitly using the marching-squares algorithm and linear interpolation within
        /// the squares; see the coursework specification of Graphmatic for a better explanation of this algorithm in
        /// pseudo code.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="graphPen">The pen to use for drawing the curve of the equation.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        private void PlotImplicit(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, PlotResolution resolution)
        {
            // use a bigger grid at coarser resolutions
            int gridResolution = EquationResolution;
            if (resolution == PlotResolution.Resize) gridResolution *= 10;
            if (resolution == PlotResolution.Edit) gridResolution *= 4;

            VariableSet vars = new VariableSet();

            int gridWidth = graphSize.Width / gridResolution,
                gridHeight = graphSize.Height / gridResolution;

            // evaluates the Equation parse tree at the corners of each square on a grid on the screen
            double[,] values = new double[
                gridWidth + 1,
                gridHeight + 1];
            double horizontal, vertical;
            for (int i = 0; i < gridWidth + 1; i++)
            {
                for (int j = 0; j < gridHeight + 1; j++)
                {
                    graph.ToScreenSpace(graphSize, i * gridResolution, j * gridResolution, out horizontal, out vertical);
                    vars[graph.Parameters.HorizontalAxis] = horizontal;
                    vars[graph.Parameters.VerticalAxis] = vertical;
                    values[i, j] = ParseTree.Evaluate(vars);
                }
            }

            // marching squares
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    // the four values at the corners of this grid cell
                    // the corners are in this layout:
                    //
                    // A---B
                    // |   |
                    // |   |
                    // C---D

                    double av = values[i, j],
                           bv = values[i + 1, j],
                           cv = values[i, j + 1],
                           dv = values[i + 1, j + 1];

                    if (Double.IsInfinity(av) || Double.IsNaN(av) ||
                        Double.IsInfinity(bv) || Double.IsNaN(bv) ||
                        Double.IsInfinity(cv) || Double.IsNaN(cv) ||
                        Double.IsInfinity(dv) || Double.IsNaN(dv))
                    {
                        // don't try to plot these values
                        continue;
                    }

                    // see which edges of the aforementioned grid cell have a change of
                    // sign of the value. For example, ab stores whether the value at A
                    // has a different sign to that at B (using the xor operator).
                    bool ab = ((av > 0) ^ (bv > 0)),
                         bd = ((bv > 0) ^ (dv > 0)),
                         ac = ((av > 0) ^ (cv > 0)),
                         cd = ((cv > 0) ^ (dv > 0));

                    int x = i * gridResolution,
                        y = j * gridResolution;

                    // guess the points on the 4 edges at which the sign change occurs
                    // this is a linear interpolation and thus isn't perfect but it's good enough
                    // and is almost perfect at low enough grid resolutions. If a sign change does
                    // not occur, this point will not actually be within the boundaries of the edge
                    // but that point won't be used anyway so it doesn't matter
                    Point abp = new Point(x + ZeroReverseLerp(gridResolution, av, bv), y),
                          bdp = new Point(x + gridResolution, y + ZeroReverseLerp(gridResolution, bv, dv)),
                          acp = new Point(x, y + ZeroReverseLerp(gridResolution, av, cv)),
                          cdp = new Point(x + ZeroReverseLerp(gridResolution, cv, dv), y + gridResolution);

                    // plot lines between the appropriate points where the sign changes
                    if (ab && bd && ac && cd)
                        ; // if the sign changes on all 4 edges, don't even bother plotting lines as it's an ambiguous case
                    else if (!ab && !bd && !ac && !cd)
                        ; // the sign doesn't change at all, so still do nothing
                    else if (ac && cd)
                        graphics.DrawLine(graphPen, acp, cdp);
                    else if (bd && cd)
                        graphics.DrawLine(graphPen, bdp, cdp);
                    else if (bd && ac)
                        graphics.DrawLine(graphPen, bdp, acp);
                    else if (ab && bd)
                        graphics.DrawLine(graphPen, abp, bdp);
                    else if (ab && cd)
                        graphics.DrawLine(graphPen, abp, cdp);
                    else if (ab && ac)
                        graphics.DrawLine(graphPen, abp, acp);

                    // the sign will always change 0, 2 or 4 times in a cell
                }
            }
        }

        /// <summary>
        /// Plots this equation explicitly on the graph. This is faster than implicit plotting but plotting explicitly with respect
        /// to a variable (eg. <c>'y'</c>) requires that variable to occur once and once only in an equation, alone on one side of
        /// the equation. For example, the equation <c>y=x^2-3x+2</c> explicitly defines the value of y, but <c>y^2+x^2=4</c> does not,
        /// instead using it generally in the terms of the equation. This method will determine if the equation is to be plotted on the
        /// horizontal or vertical axis of <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="graphPen">The pen to use for drawing the curve of the equation.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        /// <param name="explicitVariable">The variable that is explicitly defined.</param>
        /// <param name="node">The parse tree node representing what <paramref name="explicitVariable"/> is explicitly equal to.</param>
        private void PlotExplicit(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, char explicitVariable, ParseTreeNode node, PlotResolution resolution)
        {
            if (explicitVariable == graph.Parameters.HorizontalAxis)
            {
                PlotExplicitVertical(graph, graphics, graphSize, graphPen, graph.Parameters.VerticalAxis, node, resolution); // x=...
            }
            else if (explicitVariable == graph.Parameters.VerticalAxis)
            {
                PlotExplicitHorizontal(graph, graphics, graphSize, graphPen, graph.Parameters.HorizontalAxis, node, resolution); // y=...
            }
            else
            {
                // actually plotting implicitly in relation to a constant (eg. xy=a)
                // this functionality isn't yet supported but we're accounting for the
                // possibility of its support in the future here anyway
                PlotImplicit(graph, graphics, graphSize, graphPen, resolution);
            }
        }

        /// <summary>
        /// Plots this equation explicitly on the graph. This is faster than implicit plotting but plotting explicitly with respect
        /// to a variable (eg. <c>'y'</c>) requires that variable to occur once and once only in an equation, alone on one side of
        /// the equation. For example, the equation <c>y=x^2-3x+2</c> explicitly defines the value of y, but <c>y^2+x^2=4</c> does not,
        /// instead using it generally in the terms of the equation. This method plots <paramref name="explicitVariable"/> on the
        /// horizontal axis of <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="graphPen">The pen to use for drawing the curve of the equation.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        /// <param name="explicitVariable">The variable that is explicitly defined.</param>
        /// <param name="node">The parse tree node representing what <paramref name="explicitVariable"/> is explicitly equal to.</param>
        private void PlotExplicitHorizontal(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, char explicitVariable, ParseTreeNode node, PlotResolution resolution)
        {
            VariableSet vars = new VariableSet();

            int previousX = -1, previousY = -1;

            // use wider intervals for coarser plot resolutions
            float interval = 0.1f;
            if (resolution == PlotResolution.Resize) interval *= 100f;
            if (resolution == PlotResolution.Edit) interval *= 40f;

            // evaluate the equation at every point along the axis and plot accordingly
            for (float i = 0; i < graphSize.Width; i += interval)
            {
                double horizontal = graph.Parameters.HorizontalPixelScale * (i - graphSize.Width / 2) + graph.Parameters.CenterHorizontal;
                vars[explicitVariable] = horizontal;
                double vertical = node.Evaluate(vars);

                int x, y;
                graph.ToImageSpace(graphSize, horizontal, vertical, out x, out y);

                if (previousX != -1)
                {
                    if (y < -100 || y >= graphSize.Height + 100)
                    {
                        x = -1;
                        y = -1;
                    }
                    else
                    {
                        try
                        {
                            graphics.DrawLine(graphPen, previousX, previousY, x, y);
                        }
                        catch (OverflowException) // can happen if the graph line shoots up tending to infinity
                        {
                            x = -1;
                            y = -1;
                        }
                    }
                }

                previousX = x;
                previousY = y;
            }
        }

        /// <summary>
        /// Plots this equation explicitly on the graph. This is faster than implicit plotting but plotting explicitly with respect
        /// to a variable (eg. <c>'y'</c>) requires that variable to occur once and once only in an equation, alone on one side of
        /// the equation. For example, the equation <c>y=x^2-3x+2</c> explicitly defines the value of y, but <c>y^2+x^2=4</c> does not,
        /// instead using it generally in the terms of the equation. This method plots <paramref name="explicitVariable"/> on the
        /// vertical axis of <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="graphPen">The pen to use for drawing the curve of the equation.</param>
        /// <param name="resolution">The plotting resolution to use. Using a coarser resolution may make the plotting
        /// process faster, and is thus more suitable when the display is being resized or moved.</param>
        /// <param name="explicitVariable">The variable that is explicitly defined.</param>
        /// <param name="node">The parse tree node representing what <paramref name="explicitVariable"/> is explicitly equal to.</param>
        private void PlotExplicitVertical(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, char explicitVariable, ParseTreeNode node, PlotResolution resolution)
        {
            VariableSet vars = new VariableSet();

            int previousX = -1, previousY = -1;

            // use wider intervals for coarser plot resolutions
            float interval = 0.1f;
            if (resolution == PlotResolution.Resize) interval *= 100f;
            if (resolution == PlotResolution.Edit) interval *= 40f;

            // evaluate the equation at every point along the axis and plot accordingly
            for (float i = 0; i < graphSize.Width; i += interval)
            {
                double vertical = graph.Parameters.VerticalPixelScale * -(i - graphSize.Height / 2) + graph.Parameters.CenterVertical;
                vars[explicitVariable] = vertical;
                double horizontal = node.Evaluate(vars);

                int x, y;
                graph.ToImageSpace(graphSize, horizontal, vertical, out x, out y);

                if (previousX != -1)
                {
                    if (x < -100 || x >= graphSize.Width + 100)
                    {
                        x = -1;
                        y = -1;
                    }
                    else
                    {
                        try
                        {
                            graphics.DrawLine(graphPen, previousX, previousY, x, y);
                        }
                        catch (OverflowException) // can happen if the graph line shoots up tending to infinity
                        {
                            x = -1;
                            y = -1;
                        }
                    }
                }

                previousX = x;
                previousY = y;
            }
        }

        /// <summary>
        /// Determines whether this equation can be plotted using the specific given variables. If calling this
        /// method with parameters <c>'x'</c> and <c>'y'</c> on an Equation such as <c>y=x+1</c> will return true,
        /// however calling it with the same parameters on an equation such as <c>a=3b-7</c> will return false,
        /// as the equation does not equate those two variables.
        /// </summary>
        /// <param name="variable1">A variable plotted on an axis of a Graph.</param>
        /// <param name="variable2">A variable plotted on an axis of a Graph.</param>
        /// <returns>Returns whether this plottable resource can be plotted over the given variables.</returns>
        public bool CanPlot(char variable1, char variable2)
        {
            if (ParseTree == null)
            {
                Parse();
            }
            return ParseTree.CanPlotOverVariables(variable1, variable2);
        }
    }
}
