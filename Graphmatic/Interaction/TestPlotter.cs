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

        private void ScreenToGraphSpace(Point p, out double x, out double y)
        {
            x = (double)(p.X - ClientSize.Width / 2) / (double)(ClientSize.Width) * 20.0;
            y = -(double)(p.Y - ClientSize.Height / 2) / (double)(ClientSize.Height) * 20.0;
        }

        private double EvaluateAt(Point p, Dictionary<char, double> vars)
        {
            double x, y;
            ScreenToGraphSpace(p, out x, out y);
            vars[Equation.HorizontalVariable] = x;
            vars[Equation.VerticalVariable] = y;
            return Equation.ParseTree.Evaluate(vars);
        }

        private int ILerp(int scale, double a, double b)
        {
            return (int)(a / (a - b) * (double)scale);
        }

        private void Line(Graphics g, Point m, Point n)
        {
            g.DrawLine(Pens.Black, m, n);
        }

        private void March(Graphics g, Point a, Point b, Point c, Point d, int s, Dictionary<char, double> vars)
        {
            double av = EvaluateAt(a, vars),
                   bv = EvaluateAt(b, vars),
                   cv = EvaluateAt(c, vars),
                   dv = EvaluateAt(d, vars);
            bool ab = ((av > 0) ^ (bv > 0)),
                 bd = ((bv > 0) ^ (dv > 0)),
                 ac = ((av > 0) ^ (cv > 0)),
                 cd = ((cv > 0) ^ (dv > 0));
            Point abp = new Point(a.X + ILerp(s, av, bv), a.Y),
                  bdp = new Point(b.X, b.Y + ILerp(s, bv, dv)),
                  acp = new Point(a.X, a.Y + ILerp(s, av, cv)),
                  cdp = new Point(c.X + ILerp(s, cv, dv), c.Y);
            if (ab && bd && ac && cd) { }
            else if (!ab && !bd && !ac && !cd) { }
            else if (ac && cd)
                Line(g, acp, cdp);
            else if (bd && cd)
                Line(g, bdp, cdp);
            else if (bd && ac)
                Line(g, bdp, acp);
            else if (ab && bd)
                Line(g, abp, bdp);
            else if (ab && cd)
                Line(g, abp, cdp);
            else if (ab && ac)
                Line(g, abp, acp);
        }

        private void TestPlotter_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Bitmap bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
                Graphics g = Graphics.FromImage(bitmap);
                Dictionary<char, double> vars = new Dictionary<char, double>();
                Color less = Color.Red,
                    more = Color.Blue;
                int cellSize = 3;
                for (int i = 0; i < bitmap.Width; i += cellSize)
                {
                    for (int j = 0; j < bitmap.Height; j += cellSize)
                    {
                        Point a = new Point(i, j),
                              b = new Point(i + cellSize, j),
                              c = new Point(i, j + cellSize),
                              d = new Point(i + cellSize, j + cellSize);
                        March(g, a, b, c, d, cellSize, vars);
                    }
                }
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            catch (Exception ex)
            {

            }
        }

        private void TestPlotter_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(350, 350);
        }
    }
}
