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

        public Main()
        {
            InitializeComponent();
            InitializeEditors();
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

        private void NewUserUsernameCheck()
        {
            if (Properties.Settings.Default.Username == "Anonymous")
            {
                Properties.Settings.Default.Username = Environment.UserName;
                Properties.Settings.Default.Save();
            }
        }

        private string GetUserTextInput(string prompt, string title, string defaultValue = "", Image captionImage = null)
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

        #region WinForms code
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
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
            NewUserUsernameCheck();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckDocument()) e.Cancel = true;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void resourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewResources.Focus();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                Properties.Resources.AboutBoxMessage,
                "About " + String.Format(Properties.Resources.TitleBarName, Properties.Resources.VersionString),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion
    }
}
