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
        /// <summary>
        /// Gets or sets whether the most recent attempt to recover a backup failed or not.
        /// </summary>
        bool FailedBackup
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a backup of the current document, as long as one is actually open.
        /// </summary>
        public void Backup()
        {
            if (CurrentDocument != null)
            {
                SaveBackupDocument();
            }
        }

        /// <summary>
        /// Tries to remove the backup document for the currently-open document.
        /// </summary>
        public void RemoveBackupDocument()
        {
            string backupPath = GetBackupPath();

            try
            {
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Backup document cannot be removed.\r\n" +
                    ex.Message, "Backup - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tries to save a backup of the current document to the backup location.
        /// </summary>
        public void SaveBackupDocument()
        {
            string backupPath = GetBackupPath();

            try
            {
                CurrentDocument.Save(backupPath, true);
            }
            catch (IOException ex)
            {
                if (!FailedBackup)
                {
                    MessageBox.Show("A backup document cannot be created for this document. This message will only be shown once while this document is open. " +
                        "Consider changing the backup path in the program Settings.\r\n" +
                        ex.Message, "Backup and Restore - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FailedBackup = true;
                }
            }
        }

        /// <summary>
        /// Attempts to recover a backup of the current document and move it to a user-specified location.
        /// </summary>
        /// <param name="backupPath">The path from which to recover the backup document.</param>
        public void RecoverBackupDocument(string backupPath)
        {
            while (true) // this document gon get recovered, yo
            {
                SaveFileDialog saveFileDialog = CreateSaveFileDialog();
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) // only try to recover if the user doesn't cancel
                {
                    try
                    {
                        // moves the backup from the backup path to the user-selected location
                        File.Move(backupPath, saveFileDialog.FileName);

                        if (MessageBox.Show("A backup has been recovered to the specified location. Do you want to open the backup now?\r\n" +
                            "",
                            "Backup and Restore",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // give the user the choice to open the backup instead of the possibly-corrupted document
                            OpenDocument(backupPath);
                        }
                        break;
                    }
                    catch (IOException ex)
                    {
                        // tell the user that the document can't be recovered
                        MessageBox.Show("The document cannot be recovered to this location.\r\n" +
                            ex.Message + "\r\nSave the recovered document to another location and try again.", "Backup and Restore - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Checks if a backup document exists for the current document.
        /// If it does, give the user the choice to recover it, ignore it, or remove it.
        /// </summary>
        public void CheckBackupDocument()
        {
            string backupPath = GetBackupPath();
            if (File.Exists(backupPath))
            {
                DialogResult result = MessageBox.Show("It appears that the program ended unexpectedly before the document was closed. Do you want to recover the backup document, and save it to another location?", "Backup and Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    RecoverBackupDocument(backupPath);
                }
                else if (result == DialogResult.No)
                {
                    RemoveBackupDocument();
                }
            }
        }

        /// <summary>
        /// Gets the path to save backups of the document that is currently open.
        /// This includes the directory name (from <see cref="GetBackupFolder"/>)
        /// and the file name (from <see cref="Graphmatic.Interaction.Document.BackupFileName"/>).
        /// </summary>
        public string GetBackupPath()
        {
            string path = String.Format("{0}/{1}", GetBackupFolder(), CurrentDocument.BackupFileName);
            return path;
        }

        /// <summary>
        /// Gets the backup folder to which to save document backups.
        /// </summary>
        public string GetBackupFolder()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.BackupPath);

            // if the directory already exists, this does nothing; this just ensures that the backup folder actually
            // exists before we try to save backups
            try
            {
                Directory.CreateDirectory(folderPath);
            }
            catch (IOException ex)
            {
                // if for whatever reason we can't get the folder to which to create backups, tell the user
                MessageBox.Show("The backup folder cannot be created. " +
                    "Consider changing the backup path in the program Settings.\r\n" +
                ex.Message, "Backup and Restore - " + ex.GetType().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return folderPath;
        }

        /// <summary>
        /// Creates an appropriate dialog for saving Graphmatic documents.
        /// </summary>
        /// <returns>Returns a <see cref="System.Windows.Forms.SaveFileDialog"/> with the correct filter and
        /// parameters for saving Graphmatic documents.</returns>
        private SaveFileDialog CreateSaveFileDialog()
        {
            return new SaveFileDialog()
            {
                Filter = Properties.Resources.DialogFilter,
                FilterIndex = 0
            };
        }

        /// <summary>
        /// Creates an appropriate dialog for opening Graphmatic documents.
        /// </summary>
        /// <returns>Returns a <see cref="System.Windows.Forms.OpenFileDialog"/> with the correct filter and
        /// parameters for opening Graphmatic documents.</returns>
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

        /// <summary>
        /// Saves the current document to the given path.
        /// Uses the extension of <paramref name="path"/> to determine whether to
        /// save uncompressed (<c>.xml</c>) or not.
        /// </summary>
        /// <param name="path">The file system path to which to save the document.</param>
        public void SaveDocument(string path)
        {
            try
            {
                CurrentDocument.Save(path, path.ToLowerInvariant().EndsWith(".xml") ? false : true);
                DocumentPath = path;
                DocumentModified = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("The document cannot be saved.\r\n" + ex.Message, "Save - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the current document to a path specified by the user in a save file dialog.
        /// </summary>
        /// <returns>Returns false if the user cancelled the interaction; true otherwise.</returns>
        public bool SaveDocumentAs()
        {
            SaveFileDialog saveFileDialog = CreateSaveFileDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveDocument(saveFileDialog.FileName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the current document. If the document has already been saved (or was opened from somewhere),
        /// then save to the path at which the document already exists. Otherwise, prompt the user to save the
        /// document to a location specified by a save file dialog using <see cref="SaveDocumentAs"/>.
        /// </summary>
        /// <returns>Returns false if the user cancelled the interaction; true otherwise.</returns>
        public bool SaveDocument()
        {
            if (DocumentPath == null)
            {
                return SaveDocumentAs();
            }
            else
            {
                SaveDocument(DocumentPath);
                return true;
            }
        }

        /// <summary>
        /// Attempts to open the file at location <paramref name="path"/> on the file system as a Graphmatic document.
        /// This also checks for the existence of a backup document for the document that has just been opened.
        /// </summary>
        /// <param name="path"></param>
        public void OpenDocument(string path)
        {
            try
            {
                CurrentDocument = Document.Open(path, path.ToLowerInvariant().EndsWith(".xml") ? false : true);
                FailedBackup = false;
                CheckBackupDocument(); // check for a backup document

                DocumentPath = path; // set the current document path to the path from which we just loaded a document
                OpenResourceEditor(CurrentDocument.PageOrder.Last()); // opens last page in document

                DocumentModified = false; // unsets the modified flag

                Backup(); // make a backup immediately
            }
            catch (IOException ex)
            {
                MessageBox.Show("The document cannot be opened.\r\n" + ex.Message, "Open - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Attempts to open a document from a path specified by the user in an open file dialog.
        /// </summary>
        public void OpenDocument()
        {
            if (!CheckDocument()) return;
            RemoveBackupDocument();
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

        /// <summary>
        /// Checks the current document for unsaved changes, and if there are any, prompt the user to save the document.
        /// </summary>
        /// <returns>Returns false if the user cancelled the interaction; true otherwise. Hence, if false is returned,
        /// the program should not be closed (or whatever this function was called for).</returns>
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
                    if (SaveDocument()) return true;
            }
            return true;
        }

        /// <summary>
        /// Creates a new document and displays it, prompting the user to save the current document if it has not already
        /// been saved.
        /// </summary>
        public void NewDocument()
        {
            if (!CheckDocument()) return;
            CurrentDocument = new Document();
            AddPage();
            DocumentPath = null;
            DocumentModified = false;
        }

        #region WinForms code
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
        #endregion
    }
}
