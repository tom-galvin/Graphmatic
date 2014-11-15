using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Parsing;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class EquationEditor : Form
    {
        public Equation Equation
        {
            get;
            protected set;
        }

        public EquationEditor()
        {
            InitializeComponent();
            Equation = new Equation('y', 'x');
            expressionDisplay.Expression = Equation.Expression;
            textBoxPlotted.Text = Equation.PlottedVariable.ToString();
            textBoxVarying.Text = Equation.VaryingVariable.ToString();
        }

        public EquationEditor(Equation equation)
        {
            InitializeComponent();
            Equation = equation;
            expressionDisplay.Expression = Equation.Expression;
            textBoxPlotted.Text = Equation.PlottedVariable.ToString();
            textBoxVarying.Text = Equation.VaryingVariable.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ExpressionEditor inputWindow = new ExpressionEditor(Equation);
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

            if(inputWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                expressionDisplay.Refresh();
        }

        private void textBoxPlotted_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxPlotted.Text = e.KeyChar.ToString();
            Equation.PlottedVariable = e.KeyChar;
        }

        private void textBoxVarying_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxVarying.Text = e.KeyChar.ToString();
            Equation.VaryingVariable = e.KeyChar;
        }

        private void EquationEditor_Load(object sender, EventArgs e)
        {
            Text = Equation.Name + " - Equation Editor";
            buttonEdit.Select();
        }
    }
}
