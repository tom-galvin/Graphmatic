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
        bool FailedBackup
        {
            get;
            set;
        }

        public void Backup()
        {
            if (CurrentDocument != null)
            {
                SaveBackupDocument();
            }
        }

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
                    MessageBox.Show("A backup document cannot be created for this document. This message will only be shown once while this document is open.\r\n" +
                        ex.Message, "Backup and Restore - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FailedBackup = true;
                }
            }
        }

        public void RecoverBackupDocument(string backupPath)
        {
            SaveFileDialog saveFileDialog = CreateSaveFileDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.Move(backupPath, saveFileDialog.FileName);
                }
                catch (IOException ex)
                {
                    if (!FailedBackup)
                    {
                        MessageBox.Show("The backup document cannot be recovered.\r\n" +
                            ex.Message, "Backup and Restore - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FailedBackup = true;
                    }
                }
            }
        }

        public void CheckBackupDocument()
        {
            string backupPath = GetBackupPath();
            if (File.Exists(backupPath))
            {
                DialogResult result = MessageBox.Show("It appears that the program ended unexpectedly before the document was closed. Do you want to recover the backup document?", "Backup and Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    RecoverBackupDocument(backupPath);
                }
                else if (result == DialogResult.No)
                {
                    RemoveBackupDocument();
                }
            }
            else
            {
            }
        }

        public string GetBackupPath()
        {
            string path = Environment.ExpandEnvironmentVariables(
                String.Format("{0}/{1}", GetBackupFolder(), CurrentDocument.BackupFileName));
            return path;
        }

        public string GetBackupFolder()
        {
            string folderPath = Properties.Settings.Default.BackupPath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
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

        public void SaveDocumentAs()
        {
            SaveFileDialog saveFileDialog = CreateSaveFileDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
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
            try
            {
                CurrentDocument = Document.Open(path, path.ToLowerInvariant().EndsWith(".xml") ? false : true);
                FailedBackup = false;
                CheckBackupDocument();

                DocumentPath = path;
                DocumentModified = false;
                if (CurrentDocument.CurrentResource != null &&
                    Properties.Settings.Default.RestoreLastResource)
                    OpenResourceEditor(CurrentDocument.CurrentResource);

                Backup();
            }
            catch (IOException ex)
            {
                MessageBox.Show("The document cannot be opened.\r\n" + ex.Message, "Open - " + ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
