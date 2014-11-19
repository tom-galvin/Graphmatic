using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphmatic.Interaction
{
    public partial class TestPlotter : Form
    {
        public Equation Equation
        {
            get;
            protected set;
        }

        public TestPlotter()
        {
            InitializeComponent();
        }

        public TestPlotter(Equation equation)
            : this()
        {
            Equation = equation;
        }

        private void ScreenToGraphSpace(int i, int j, out double x, out double y)
        {
            x = (double)(i - ClientSize.Width / 2) / (double)(ClientSize.Width) * 20.0;
            y = -(double)(j - ClientSize.Height / 2) / (double)(ClientSize.Height) * 20.0;
        }

        private double EvaluateAt(int i, int j, Dictionary<char, double> vars)
        {
            double x, y;
            ScreenToGraphSpace(i, j, out x, out y);
            vars[Equation.HorizontalVariable] = x;
            vars[Equation.VerticalVariable] = y;
            return Equation.ParseTree.Evaluate(vars);
        }

        private int ILerp(int scale, double a, double b)
        {
            if(checkBoxLerp.Checked)
                return (int)(a / (a - b) * (double)scale);
            else
                return scale / 2;
        }

        private void TestPlotter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush less = Brushes.AliceBlue,
                    more = Brushes.LightYellow;
            Dictionary<char, double> vars = new Dictionary<char, double>();
            int gridSize = trackBarResolution.Value,
                gridWidth = ClientSize.Width / gridSize,
                gridHeight = ClientSize.Height / gridSize;
            double[,] values = new double[
                gridWidth + 1,
                gridHeight + 1];
            for (int i = 0; i < gridWidth + 1; i++)
            {
                for (int j = 0; j < gridHeight + 1; j++)
                {
                    g.FillRectangle(
                        unchecked((values[i, j] = EvaluateAt(i * gridSize, j * gridSize, vars))) > 0 ?
                        more : less,
                        i * gridSize - gridSize / 2, j * gridSize - gridSize / 2, gridSize, gridSize);
                }
            }
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
                        g.DrawLine(Pens.Red,
                            i * gridSize,
                            j * gridSize,
                            (i + 1) * gridSize,
                            (j + 1) * gridSize);
                        continue;
                    }
                    bool ab = ((av > 0) ^ (bv > 0)),
                            bd = ((bv > 0) ^ (dv > 0)),
                            ac = ((av > 0) ^ (cv > 0)),
                            cd = ((cv > 0) ^ (dv > 0));
                    int x = i * gridSize,
                        y = j * gridSize;
                    Point abp = new Point(x + ILerp(gridSize, av, bv), y),
                            bdp = new Point(x + gridSize, y + ILerp(gridSize, bv, dv)),
                            acp = new Point(x, y + ILerp(gridSize, av, cv)),
                            cdp = new Point(x + ILerp(gridSize, cv, dv), y + gridSize);
                    if (ab && bd && ac && cd) { }
                    else if (!ab && !bd && !ac && !cd) { }
                    else if (ac && cd)
                        g.DrawLine(Pens.Black, acp, cdp);
                    else if (bd && cd)
                        g.DrawLine(Pens.Black, bdp, cdp);
                    else if (bd && ac)
                        g.DrawLine(Pens.Black, bdp, acp);
                    else if (ab && bd)
                        g.DrawLine(Pens.Black, abp, bdp);
                    else if (ab && cd)
                        g.DrawLine(Pens.Black, abp, cdp);
                    else if (ab && ac)
                        g.DrawLine(Pens.Black, abp, acp);
                }
            }
        }

        private void TestPlotter_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(350, 350);
        }

        private void checkBoxLerp_CheckedChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void trackBarResolution_Scroll(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
