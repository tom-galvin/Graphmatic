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
using Graphmatic.Expressions;
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
            inputWindow.Verify += delegate(object verificationSender, ExpressionVerificationEventArgs verificationEventArgs)
            {
                try
                {
                    verificationEventArgs.Equation.ParseTree = verificationEventArgs.Equation.Expression.Parse();
                }
                catch (ParseException ex)
                {
                    MessageBox.Show(ex.Message, "Equation Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    verificationEventArgs.Cursor.Expression = ex.Cause.Parent;
                    verificationEventArgs.Cursor.Index = ex.Cause.IndexInParent();
                    verificationEventArgs.Failure = true;
                }
            };
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(equation.ParseTree.ToString());
                MessageBox.Show(equation.ParseTree.Evaluate(new Dictionary<char, double>()).ToString(), "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
