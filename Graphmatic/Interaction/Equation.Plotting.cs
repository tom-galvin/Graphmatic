using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Graphmatic.Expressions.Parsing;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction
{
    public partial class Equation : Resource, IPlottable
    {
        public const float EquationPenWidth = 2f;
        public const int EquationResolution = 3;

        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters parameters, PlotResolution resolution)
        {
            if (resolution == PlotResolution.Resize) return;
            BinaryParseTreeNode parseTreeRoot = ParseTree as BinaryParseTreeNode;
            Pen graphPen = new Pen(plotParams.PlotColor);
            if (parseTreeRoot.Left is VariableParseTreeNode &&
                !parseTreeRoot.Right.ContainsVariable((parseTreeRoot.Left as VariableParseTreeNode).Variable))
            {
                PlotExplicit(
                    graph,
                    graphics,
                    graphSize,
                    graphPen,
                    parameters,
                    (parseTreeRoot.Left as VariableParseTreeNode).Variable,
                    parseTreeRoot.Right,
                    resolution);
            }
            else if (parseTreeRoot.Right is VariableParseTreeNode &&
                !parseTreeRoot.Left.ContainsVariable((parseTreeRoot.Right as VariableParseTreeNode).Variable))
            {
                PlotExplicit(
                    graph,
                    graphics,
                    graphSize,
                    graphPen,
                    parameters,
                    (parseTreeRoot.Right as VariableParseTreeNode).Variable,
                    parseTreeRoot.Left,
                    resolution);
            }
            else
            {
                PlotImplicit(graph, graphics, graphSize, graphPen, parameters, resolution);
            }
        }

        private int IntLerp(int scale, double a, double b)
        {
            return (int)(a / (a - b) * (double)scale);
        }

        private void PlotImplicit(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, GraphParameters plotParams, PlotResolution resolution)
        {
            int gridResolution = EquationResolution;
            if (resolution == PlotResolution.Resize) gridResolution *= 10;
            if (resolution == PlotResolution.Edit) gridResolution *= 4;

            Dictionary<char, double> vars = new Dictionary<char, double>();

            int gridWidth = graphSize.Width / gridResolution,
                gridHeight = graphSize.Height / gridResolution;
            double[,] values = new double[
                gridWidth + 1,
                gridHeight + 1];

            for (int i = 0; i < gridWidth + 1; i++)
            {
                for (int j = 0; j < gridHeight + 1; j++)
                {
                    double horizontal = plotParams.HorizontalPixelScale * ((i * gridResolution) - graphSize.Width / 2) + plotParams.CenterHorizontal,
                           vertical = plotParams.VerticalPixelScale * -((j * gridResolution) - graphSize.Height / 2) + plotParams.CenterVertical;
                    vars[plotParams.HorizontalAxis] = horizontal;
                    vars[plotParams.VerticalAxis] = vertical;
                    values[i, j] = ParseTree.Evaluate(vars);
                }
            }

            // marching squares
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    double av = values[i, j],
                            bv = values[i + 1, j],
                            cv = values[i, j + 1],
                            dv = values[i + 1, j + 1];
                    if (Double.IsInfinity(av) || Double.IsNaN(av) ||
                        Double.IsInfinity(bv) || Double.IsNaN(bv) ||
                        Double.IsInfinity(cv) || Double.IsNaN(cv) ||
                        Double.IsInfinity(dv) || Double.IsNaN(dv))
                    {
                        /* graphics.DrawLine(Pens.Red,
                            i * EquationResolution,
                            j * EquationResolution,
                            (i + 1) * EquationResolution,
                            (j + 1) * EquationResolution); */
                        // we don't need the red hash anymore
                        continue;
                    }
                    bool ab = ((av > 0) ^ (bv > 0)),
                            bd = ((bv > 0) ^ (dv > 0)),
                            ac = ((av > 0) ^ (cv > 0)),
                            cd = ((cv > 0) ^ (dv > 0));
                    int x = i * gridResolution,
                        y = j * gridResolution;
                    Point abp = new Point(x + IntLerp(gridResolution, av, bv), y),
                            bdp = new Point(x + gridResolution, y + IntLerp(gridResolution, bv, dv)),
                            acp = new Point(x, y + IntLerp(gridResolution, av, cv)),
                            cdp = new Point(x + IntLerp(gridResolution, cv, dv), y + gridResolution);
                    if (ab && bd && ac && cd) { }
                    else if (!ab && !bd && !ac && !cd) { }
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
                }
            }
        }

        private void PlotExplicit(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, GraphParameters graphParams, char explicitVariable, ParseTreeNode node, PlotResolution resolution)
        {
            if (explicitVariable == graphParams.HorizontalAxis)
            {
                PlotExplicitVertical(graph, graphics, graphSize, graphPen, graphParams, graphParams.VerticalAxis, node, resolution); // x=...
            }
            else if (explicitVariable == graphParams.VerticalAxis)
            {
                PlotExplicitHorizontal(graph, graphics, graphSize, graphPen, graphParams, graphParams.HorizontalAxis, node, resolution); // y=...
            }
            else
            {
                // actually plotting implicitly in relation to a constant (eg. xy=a)
                PlotImplicit(graph, graphics, graphSize, graphPen, graphParams, resolution);
            }
        }

        private void PlotExplicitHorizontal(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, GraphParameters plotParams, char explicitVariable, ParseTreeNode node, PlotResolution resolution)
        {
            Dictionary<char, double> vars = new Dictionary<char, double>();
            int previousX = -1, previousY = -1;
            float interval = 0.1f;
            if (resolution == PlotResolution.Resize) interval *= 100f;
            if (resolution == PlotResolution.Edit) interval *= 40f;
            for (float i = 0; i < graphSize.Width; i += interval)
            {
                double horizontal = plotParams.HorizontalPixelScale * (i - graphSize.Width / 2) + plotParams.CenterHorizontal;
                vars[explicitVariable] = horizontal;
                double vertical = node.Evaluate(vars);

                int x, y;
                graph.ToImageSpace(graphSize, plotParams, horizontal, vertical, out x, out y);

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

        private void PlotExplicitVertical(Graph graph, Graphics graphics, Size graphSize, Pen graphPen, GraphParameters plotParams, char explicitVariable, ParseTreeNode node, PlotResolution resolution)
        {
            Dictionary<char, double> vars = new Dictionary<char, double>();
            int previousX = -1, previousY = -1;
            float interval = 0.1f;
            if (resolution == PlotResolution.Resize) interval *= 100f;
            if (resolution == PlotResolution.Edit) interval *= 40f;
            for (float i = 0; i < graphSize.Width; i += interval)
            {
                double vertical = plotParams.VerticalPixelScale * -(i - graphSize.Height / 2) + plotParams.CenterVertical;
                vars[explicitVariable] = vertical;
                double horizontal = node.Evaluate(vars);

                int x, y;
                graph.ToImageSpace(graphSize, plotParams, horizontal, vertical, out x, out y);

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

        public bool CanPlot(char variable1, char variable2)
        {
            if (ParseTree == null)
            {
                Parse();
            }
            return ParseTree.ContainsVariable(variable1) &&
                ParseTree.ContainsVariable(variable2);
        }
    }
}
