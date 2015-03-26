using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Graphmatic
{
    /// <summary>
    /// Represents a dialog used to enter a single-character variable name.
    /// </summary>
    public partial class CreateVariableDialog : Form
    {
        /// <summary>
        /// Gets the character entered by the user.
        /// </summary>
        public char EnteredChar
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets whether this dialog requires a single key-press before the dialog closes.<para/>
        /// If this is true, then once the user enters a variable name, the dialog closes immediately.
        /// </summary>
        public bool OneKey
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graphmatic.CreateVariableDialog"/> with the specified default character and key behaviour.
        /// </summary>
        /// <param name="defaultChar">The default character to display in the dialog.</param>
        /// <param name="oneKey">True if the dialog should close immediately after a key is pressed, false otherwise.</param>
        private CreateVariableDialog(char defaultChar, bool oneKey)
        {
            InitializeComponent();
            textBoxVariableName.Text = defaultChar.ToString();
            OneKey = oneKey;
        }

        /// <summary>
        /// Prompts the user to enter a variable name.
        /// </summary>
        /// <param name="oneKey">Whether the dialog only requires a single key-press of input before automatically closing.</param>
        /// <param name="defaultChar">The default character displayed to the user in the dialog window.</param>
        /// <returns>Returns the character entered by the user. If the user cancels the interaction, the null character ('\0') is returned.</returns>
        public static char EnterVariable(char defaultChar = '?', bool oneKey = true)
        {
            CreateVariableDialog dialog = new CreateVariableDialog(defaultChar, oneKey);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.EnteredChar;
            }
            else
            {
                return '\0';
            }
        }

        private void textBoxVariableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Properties.Resources.AllowedVariables.IndexOf(e.KeyChar) != -1)
            {
                EnteredChar = e.KeyChar;
                textBoxVariableName.Text = e.KeyChar.ToString();

                if (OneKey)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            }
            else
            {
                SystemSounds.Beep.Play();
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
