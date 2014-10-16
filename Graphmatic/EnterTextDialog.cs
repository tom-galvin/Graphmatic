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

        public EnterTextDialog()
        {
            InitializeComponent();
        }

        public EnterTextDialog(string title, string prompt, string defaultValue = "", Image captionImage = null)
            : this()
        {
            if (captionImage == null) captionImage = Properties.Resources.GraphmaticDocument.ToBitmap();

            pictureBoxIcon.Image = captionImage;
            Value = defaultValue;
            labelPrompt.Text = prompt;
            Text = title;

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

        public static string Display(string title, string prompt, string defaultValue = "", Image captionImage = null)
        {
            var dialog = new EnterTextDialog(title, prompt, defaultValue, captionImage);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
