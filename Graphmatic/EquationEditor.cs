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
using Graphmatic.Interaction.Plotting;

namespace Graphmatic
{
    /// <summary>
    /// Represents a form that is used for editing an Equation object.
    /// </summary>
    public partial class EquationEditor : Form
    {
        /// <summary>
        /// Gets the equation that is to be edited.
        /// </summary>
        public Equation Equation
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the <c>EquationEditor</c> form with a new equation.
        /// </summary>
        public EquationEditor()
            : this(new Equation())
        {
        }

        /// <summary>
        /// Refreshes the expression preview display, parsing the Equation if necessary.
        /// </summary>
        private void RefreshDisplay()
        {
            if (Equation.ParseTree != null)
            {
                toolTip.SetToolTip(expressionDisplay, Equation.ParseTree.ToString());
            }
            expressionDisplay.Refresh();
        }

        /// <summary>
        /// Initializes a new instance of the <c>EquationEditor</c> form with a new equation.
        /// </summary>
        /// <param name="equation">The equation that is to be edited.</param>
        public EquationEditor(Equation equation)
        {
            InitializeComponent();
            Equation = equation;
            expressionDisplay.Expression = Equation.Expression;
            RefreshDisplay();
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
            inputWindow.Verify += inputWindow_Verify;

            if (inputWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                RefreshDisplay();
        }

        /// <summary>
        /// Verification is handled by this form rather than the expression editor, as the expression editor is just for the
        /// expression rather than the equation object as a whole.
        /// </summary>
        private void inputWindow_Verify(object sender, ExpressionVerificationEventArgs e)
        {
            try
            {
                e.Equation.Parse();
            }
            catch (ParseException ex)
            {
                MessageBox.Show(ex.Message, "Equation Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Cause != null)
                {
                    e.Cursor.Expression = ex.Cause.Parent;
                    e.Cursor.Index = ex.Cause.IndexInParent();
                }
                e.Failure = true;
            }
        }

        private void EquationEditor_Load(object sender, EventArgs e)
        {
            Text = Equation.Name + " - Equation Editor";
            buttonEdit.Select();
        }

        private void EquationEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Equation.Expression == null ||
                Equation.ParseTree == null)
            {
                MessageBox.Show("Please enter an expression in the editor window.",
                    "Equation Editor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                e.Cancel = true;
            }
        }

        private void expressionDisplay_Load(object sender, EventArgs e)
        {

        }
    }
}
