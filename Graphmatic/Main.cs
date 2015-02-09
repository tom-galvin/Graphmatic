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
using Graphmatic.Interaction.Plotting;
using Graphmatic.Interaction.Annotations;

namespace Graphmatic
{
    /// <summary>
    /// Represents the main form window of the Graphmatic program.
    /// </summary>
    public partial class Main : Form
    {
        private Document _CurrentDocument;
        /// <summary>
        /// Gets or sets the document that is currently open and being edited.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the displayed path of the document in the editor.
        /// </summary>
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
        /// <summary>
        /// Gets or sets whether the document has been modified by the user or not.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the Main form, and initializes everything ready to
        /// be displayed.
        /// </summary>
        public Main()
        {
            InitializeComponent();
            InitializeEditors();
            InitializeBackupTimer();
            InitializePageDisplay();
        }

        /// <summary>
        /// Initializes the timer used for creating regular backups of the currently-open document.
        /// </summary>
        public void InitializeBackupTimer()
        {
            timerBackup.Interval = Properties.Settings.Default.BackupInterval * 1000;
            timerBackup.Enabled = Properties.Settings.Default.BackupEnabled;
        }

        /// <summary>
        /// Updates the title of the Main editor window to reflect the currently-open document and
        /// whether it has been modified by the user or not.
        /// </summary>
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

        /// <summary>
        /// Checks if the user's current username is "Anonymous". If it is, then the user has not
        /// used the program yet, so default to the current environment's user name and save the
        /// settings.
        /// </summary>
        private void NewUserUsernameCheck()
        {
            if (Properties.Settings.Default.Username == "Anonymous")
            {
                Properties.Settings.Default.Username = Environment.UserName;
                Properties.Settings.Default.Save();
            }
        }

        #region WinForms code
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        // open the options editor and then re-initialize the backup timer
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsEditor().ShowDialog(this);
            InitializeBackupTimer();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            NewDocument();

            listViewResources_SelectedIndexChanged(sender, e);
            NewUserUsernameCheck();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // unless Application.Exit was called, prompt the user to save an unsave document
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if (!CheckDocument())
                {
                    e.Cancel = true;
                }
                else
                {
                    RemoveBackupDocument();
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void resourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewResources.Focus();
        }

        // show the about dialog
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                Properties.Resources.AboutBoxMessage,
                "About " + String.Format(Properties.Resources.TitleBarName, Properties.Resources.VersionString),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion

        private void timerBackup_Tick(object sender, EventArgs e)
        {
            Backup();
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            IsFormResizing = false;
            pageDisplay.Refresh();
        }

        private void Main_ResizeBegin(object sender, EventArgs e)
        {
            IsFormResizing = true;
        }

        // show the bug reporting prompt with an obfuscated version of my e-mail
        private void toolStripStatusLabelBugReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "For now, send me an email to report a bug or crash. " +
                "Please provide information on the bug itself, what causes the bug, " +
                "steps to reproduce the bug and any information that you can, including " +
                "screenshots of relevant dialogs. Thanks!\r\n" +
                String.Format("{0}97@{1}.com",
                new string("nivlagt".Reverse().ToArray()),
                new string("liamg".Reverse().ToArray())), // OTT email antispam
                "Report a Bug",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
