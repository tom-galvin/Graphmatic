using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphmatic
{
    /// <summary>
    /// Represents a dialog used to enter a short string of text by the user.
    /// </summary>
    public partial class EnterTextDialog : Form
    {
        /// <summary>
        /// Gets or sets the entered text of this dialog.
        /// </summary>
        public string Value
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        /// <summary>
        /// Prompts the user to enter some text.<para/>
        /// This essentially does InputBox's job but much prettier.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <param name="title">The title of the prompt to show.</param>
        /// <param name="defaultValue">The default value in the prompt box. This will be selected initially.</param>
        /// <param name="captionImage">An image to show in the dialog, or null to show no message.</param>
        /// <returns>Returns the text entered by the user, or null if the user cancelled the interaction.</returns>
        public static string EnterText(string prompt, string title, string defaultValue = "", Image captionImage = null)
        {
            EnterTextDialog dialog = new EnterTextDialog(title, prompt, defaultValue, captionImage);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.Value;
            }
            else
            {
                return null;
            }
        }

        public EnterTextDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <c>EnterTextDialog</c> form, with the specified default values.
        /// </summary>
        /// <param name="prompt">The prompt text to display to the user.</param>
        /// <param name="title">The title of the prompt to show.</param>
        /// <param name="defaultValue">The default value in the prompt box. This will be selected initially.</param>
        /// <param name="captionImage">An image to show in the dialog, or null to show no message.</param>
        public EnterTextDialog(string title, string prompt, string defaultValue = "", Image captionImage = null)
            : this()
        {
            if (captionImage == null) captionImage = Properties.Resources.GraphmaticDocument.ToBitmap();

            pictureBoxIcon.Image = captionImage;
            Value = defaultValue;
            labelPrompt.Text = prompt;
            Text = title;
            textBoxName.SelectAll();

            DialogResult = DialogResult.Cancel;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
