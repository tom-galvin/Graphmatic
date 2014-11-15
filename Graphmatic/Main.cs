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
                RefreshResourceListView();
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
            ExpressionEditor inputWindow = new ExpressionEditor(equation);
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
            new SettingsEditor().ShowDialog(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CurrentDocument = new Document();
            DocumentPath = null;
            DocumentModified = false;

            listViewResources_SelectedIndexChanged(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckDocument()) return;
            Application.Exit();
        }

        public string EnterText(string prompt, string title, string defaultValue = "", Image captionImage = null)
        {
            EnterTextDialog dialog = new EnterTextDialog(title, prompt, defaultValue, captionImage);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.Value;
            }
            else
            {
                return null;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckDocument()) e.Cancel = true;
        }
    }
}
