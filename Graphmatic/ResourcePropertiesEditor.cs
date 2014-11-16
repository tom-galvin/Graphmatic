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
    public partial class ResourcePropertiesEditor : Form
    {
        public Resource Resource
        {
            get;
            set;
        }

        public ResourcePropertiesEditor()
        {
            InitializeComponent();
        }

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

        private void ResourcePropertiesEditor_Load(object sender, EventArgs e)
        {
            richTextBoxDescription.Enter += (sender1, e1) => AcceptButton = null;
            richTextBoxDescription.Leave += (sender1, e1) => AcceptButton = buttonOK;
        }

        private void textBoxAuthor_TextChanged(object sender, EventArgs e)
        {
            Resource.Author = textBoxAuthor.Text;
        }

        private void textBoxCreationDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dateTime;
            if (DateTime.TryParse(textBoxCreationDate.Text, out dateTime))
                Resource.CreationDate = dateTime;
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Formatting Stuff
        private void XorStyle(FontStyle style)
        {
            richTextBoxDescription.SelectionFont = new Font(
                richTextBoxDescription.SelectionFont,
                richTextBoxDescription.SelectionFont.Style ^ style);
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XorStyle(FontStyle.Bold);
        }

        private void richTextBoxDescription_TextChanged(object sender, EventArgs e)
        {
            Resource.Description = richTextBoxDescription.Rtf;
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XorStyle(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XorStyle(FontStyle.Underline);
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
