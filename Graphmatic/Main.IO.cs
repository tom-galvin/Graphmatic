using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class Main
    {
        public void SaveDocument(string path)
        {
            CurrentDocument.Save(path, SaveCompressed);
            DocumentPath = path;
            DocumentModified = false;
        }

        private SaveFileDialog CreateSaveFileDialog()
        {
            return new SaveFileDialog()
            {
                Filter = Properties.Resources.DialogFilter,
                FilterIndex = 0
            };
        }

        private OpenFileDialog CreateOpenFileDialog()
        {
            return new OpenFileDialog()
            {
                Filter = Properties.Resources.DialogFilter,
                FilterIndex = 0,
                CheckPathExists = true,
                CheckFileExists = true
            };
        }

        public void SaveDocumentAs()
        {
            SaveFileDialog saveFileDialog = CreateSaveFileDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName) && false)
                {
                    if (MessageBox.Show(
                        "This file already exists. Do you want to overwrite it?",
                        "Save",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                SaveDocument(saveFileDialog.FileName);
            }
        }

        public void SaveDocument()
        {
            if (DocumentPath == null)
            {
                SaveDocumentAs();
            }
            else
            {
                SaveDocument(DocumentPath);
            }
        }

        public void OpenDocument(string path)
        {
            CurrentDocument = Document.Open(path, SaveCompressed);
            DocumentPath = path;
            DocumentModified = false;
        }

        public void OpenDocument()
        {
            if (!CheckDocument()) return;
            OpenFileDialog openFileDialog = CreateOpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    OpenDocument(openFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("The file does not exist.", "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool CheckDocument()
        {
            if (DocumentModified)
            {
                DialogResult result = MessageBox.Show(
                    "You have unsaved changes to your current document. Do you wish to save them?",
                    "New Document",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return false;
                else if (result == System.Windows.Forms.DialogResult.Yes)
                    SaveDocument();
            }
            return true;
        }

        public void NewDocument()
        {
            if (!CheckDocument()) return;
            CurrentDocument = new Document();
            DocumentPath = null;
            DocumentModified = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocumentAs();
        }
    }
}
