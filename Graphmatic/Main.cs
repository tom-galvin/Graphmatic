using Graphmatic.Interaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic
{
    public partial class Main : Form
    {
        private Document _CurrentDocument;
        public Document CurrentDocument
        {
            get
            {
                return _CurrentDocument;
            }
            set
            {
                _CurrentDocument = value;
            }
        }

        private string _DocumentPath;
        public string DocumentPath
        {
            get
            {
                return _DocumentPath;
            }
            set
            {
                _DocumentPath = value;
                UpdateTitle();
            }
        }

        private bool _DocumentModified;
        public bool DocumentModified
        {
            get
            {
                return _DocumentModified;
            }
            set
            {
                _DocumentModified = value;
                UpdateTitle();
            }
        }

        public bool SaveCompressed
        {
            get
            {
                return true;
            }
        }

        public Main()
        {
            InitializeComponent();
        }

        private void UpdateTitle()
        {
            string documentPath =
                DocumentPath == null ?
                "unsaved" :
                DocumentPath.Split('/', '\\').Last();

            this.Text = String.Format("{0}{1} - {2}",
                documentPath,
                DocumentModified ? "*" : "",
                String.Format(Properties.Resources.TitleBarName, Properties.Resources.VersionString));
        }

        private void dispToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Equation equation = new Equation('y', 'x');
            InputWindow inputWindow = new InputWindow(equation);
            inputWindow.Verify += delegate(object verificationSender, ExpressionVerificationEventArgs verificationEventArgs)
            {
                try
                {
                    verificationEventArgs.Equation.ParseTree = verificationEventArgs.Equation.Expression.Parse();
                }
                catch (ParseException ex)
                {
                    MessageBox.Show(ex.Message, "Equation Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    verificationEventArgs.Cursor.Expression = ex.Cause.Parent;
                    verificationEventArgs.Cursor.Index = ex.Cause.IndexInParent();
                    verificationEventArgs.Failure = true;
                }
            };
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(equation.ParseTree.ToString());
                MessageBox.Show(equation.ParseTree.Evaluate(new Dictionary<char, double>()).ToString(), "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CurrentDocument.Add(equation);
            }
        }

        private void lorgorgoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(EnterTextDialog.Display("Test", "ABC", "Lol", Properties.Resources.Graph24) ?? "hue");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.SaveSettings();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsWindow().ShowDialog(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CurrentDocument = new Document();
            DocumentPath = null;
            DocumentModified = false;
        }

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckDocument()) return;
            Application.Exit();
        }
    }
}
