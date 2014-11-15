using Graphmatic.Interaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic
{
    public partial class Main : Form
    {
        Equation equation = new Equation('y', 'x');

        public Main()
        {
            InitializeComponent();
        }

        private void dispToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputWindow inputWindow = new InputWindow(equation);
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var tree = equation.Expression.Parse(); 
                    MessageBox.Show(tree.Evaluate(new Dictionary<char, double>()).ToString(), "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (ParseException ex)
                {
                    MessageBox.Show(ex.Message, "Parse error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lorgorgoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(EnterTextDialog.Display("Test", "ABC", "Lol", Properties.Resources.Graph24) ?? "hue");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.SaveSettings();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().ShowDialog(this);
        }
    }
}
