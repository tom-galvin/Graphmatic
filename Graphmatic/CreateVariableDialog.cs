using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphmatic
{
    public partial class CreateVariableDialog : Form
    {
        public char EnteredChar
        {
            get;
            protected set;
        }

        public CreateVariableDialog()
            : this('?')
        {
        }

        public CreateVariableDialog(char defaultChar)
        {
            InitializeComponent();
            textBoxVariableName.Text = defaultChar.ToString();
        }

        private void textBoxVariableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Properties.Resources.AllowedVariables.IndexOf(e.KeyChar) != -1)
            {
                EnteredChar = e.KeyChar;
                textBoxVariableName.Text = e.KeyChar.ToString();
            }
        }

        private void textBoxVariableName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && textBoxVariableName.Text != "?")
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
        }
    }
}
