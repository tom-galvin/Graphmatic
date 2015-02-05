using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    /// <summary>
    /// Represents a dialog used to edit some of the generic properties of resources.
    /// </summary>
    public partial class ResourcePropertiesEditor : Form
    {
        /// <summary>
        /// Gets or sets the <see cref="Graphmatic.Interaction.Resource"/> that this dialog is editing the properties of.
        /// </summary>
        public Resource Resource
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new ResourcePropertiesEditor form.
        /// </summary>
        public ResourcePropertiesEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new ResourcePropertiesEditor form, with the specified resource to edit the properties of.
        /// </summary>
        /// <param name="resource">The <see cref="Graphmatic.Interaction.Resource"/> that this dialog is to edit the properties of.</param>
        public ResourcePropertiesEditor(Resource resource)
            : this()
        {
            Resource = resource;
            textBoxName.Text = resource.Name;
            labelResourceType.Text = resource.Type;
            textBoxAuthor.Text = resource.Author;
            textBoxCreationDate.Text =
                resource.CreationDate.ToLongDateString() +
                " at " +
                resource.CreationDate.ToLongTimeString();
            textBoxID.Text = resource.Guid.ToString();
            richTextBoxDescription.Rtf = resource.Description;
            checkBoxHidden.Checked = resource.Hidden;
            pictureBoxIcon.Image = resource.GetResourceIcon(true);
        }

        // we don't want the accept button to be pressed when you hit Enter in the description box,
        // so change the form's AcceptButton while the user is editing description
        private void ResourcePropertiesEditor_Load(object sender, EventArgs e)
        {
            richTextBoxDescription.Enter += (sender1, e1) => AcceptButton = null;
            richTextBoxDescription.Leave += (sender1, e1) => AcceptButton = buttonOK;
        }

        // save the author name when the text box is edited
        private void textBoxAuthor_TextChanged(object sender, EventArgs e)
        {
            Resource.Author = textBoxAuthor.Text;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Formatting Stuff
        /// <summary>
        /// Toggles <paramref name="style"/> in the current font style.
        /// </summary>
        /// <param name="style">The style to toggle in the current form.</param>
        private void ToggleStyle(FontStyle style)
        {
            richTextBoxDescription.SelectionFont = new Font(
                richTextBoxDescription.SelectionFont,
                richTextBoxDescription.SelectionFont.Style ^ style); // xor -> toggle
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Bold);
        }

        // change the resource description when the rich text box's RTF changes
        private void richTextBoxDescription_TextChanged(object sender, EventArgs e)
        {
            Resource.Description = richTextBoxDescription.Rtf;
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Underline);
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxDescription.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxDescription.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxDescription.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AnyColor = true;
            dialog.Color = richTextBoxDescription.SelectionColor;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBoxDescription.SelectionColor = dialog.Color;
        }
        #endregion

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            Resource.Name = textBoxName.Text;
            Text = Resource.Name + " - Properties";
        }

        private void checkBoxHidden_CheckedChanged(object sender, EventArgs e)
        {
            Resource.Hidden = checkBoxHidden.Checked;
        }
    }
}
