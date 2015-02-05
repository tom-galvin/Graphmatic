using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using Graphmatic.Properties;

namespace Graphmatic
{
    /// <summary>
    /// Represents a form used for editing the application settings stored in the .NET
    /// <see cref="Properties.Settings"/> XML settings container.
    /// </summary>
    public partial class SettingsEditor : Form
    {
        /// <summary>
        /// Initializes a new SettingsEditor instance.
        /// </summary>
        public SettingsEditor()
        {
            InitializeComponent();
            InitializeSettings();
        }

        /// <summary>
        /// Turns a TextBox into an input region used for inputting a character.<para/>
        /// Ideally I'd create another UserControl for this, but it's used rarely enough that
        /// there isn't really any point.
        /// </summary>
        /// <param name="textBox">The TextBox to turn into a character input region.</param>
        /// <param name="defaultValue">The default character to set in the text box.</param>
        /// <param name="onChange">The delegate fired on the event of the character changing
        /// inside the text box.</param>
        private void MakeCharBox(TextBox textBox, char defaultValue, Action<char> onChange)
        {
            textBox.ReadOnly = true;
            textBox.Text = defaultValue.ToString();
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                textBox.Text = e.KeyChar.ToString();
                onChange(e.KeyChar);
            };
            textBox.BackColor = SystemColors.Window;
        }
        /// <summary>
        /// Initializes the settings editors on the form to show the current values of the
        /// settings as set by the user, and sets the control changed events to update the
        /// program settings to the new value.
        /// </summary>
        private void InitializeSettings()
        {
            // this repetition is unavoidable :(
            colorChooserDefaultPageColor.Color = Settings.Default.DefaultPageColor;
            colorChooserDefaultPageColor.ColorChanged += (sender, e) =>
                Settings.Default.DefaultPageColor = colorChooserDefaultPageColor.Color;

            colorChooserDefaultGraphColor.Color = Settings.Default.DefaultGraphFeatureColor;
            colorChooserDefaultGraphColor.ColorChanged += (sender, e) =>
                Settings.Default.DefaultGraphFeatureColor = colorChooserDefaultGraphColor.Color;

            colorChooserDefaultPencilColor.Color = Settings.Default.DefaultPencilColor;
            colorChooserDefaultPencilColor.ColorChanged += (sender, e) =>
                Settings.Default.DefaultPencilColor = colorChooserDefaultPencilColor.Color;

            colorChooserDefaultHighlightColor.Color = Settings.Default.DefaultHighlightColor;
            colorChooserDefaultHighlightColor.ColorChanged += (sender, e) =>
                Settings.Default.DefaultHighlightColor = colorChooserDefaultHighlightColor.Color;

            textBoxUserName.Text = Settings.Default.Username;
            textBoxUserName.TextChanged += (sender, e) =>
                Settings.Default.Username = textBoxUserName.Text;

            MakeCharBox(textBoxDefaultPlottedVariable,
                Settings.Default.DefaultVariable2,
                c => Settings.Default.DefaultVariable2 = c);

            MakeCharBox(textBoxDefaultVaryingVariable,
                Settings.Default.DefaultVariable1,
                c => Settings.Default.DefaultVariable1 = c);

            // get the text in the data set variables box,
            // split it on commas, and set each of those variables
            // as default data set variables
            textBoxDefaultDataSetVariables.Text = String.Join(", ", Settings.Default.DefaultDataSetVariables);
            textBoxDefaultDataSetVariables.TextChanged += (sender, e) => Settings.Default.DefaultDataSetVariables =
                textBoxDefaultDataSetVariables.Text
                    .Split(',')
                    .Select(s => s.Trim())
                    .Where(s => s.Length == 1)
                    .Select(s => Char.Parse(s))
                    .ToArray();

            numericBackupInterval.Value = (decimal)Settings.Default.BackupInterval;
            numericBackupInterval.ValueChanged += (sender, e) =>
                Settings.Default.BackupInterval = (int)numericBackupInterval.Value;

            numericDefaultPencilWidth.Value = (decimal)Settings.Default.DefaultPencilWidth;
            numericDefaultPencilWidth.ValueChanged += (sender, e) =>
                Settings.Default.DefaultPencilWidth = (float)numericDefaultPencilWidth.Value;

            numericDefaultHighlightWidth.Value = (decimal)Settings.Default.DefaultHighlightWidth;
            numericDefaultHighlightWidth.ValueChanged += (sender, e) =>
                Settings.Default.DefaultHighlightWidth = (float)numericDefaultHighlightWidth.Value;

            // (en|dis)able the backup-related settings controls depending on
            // whether the backup check box is checked or not
            checkBoxBackup.CheckedChanged += (sender, e) =>
            {
                Settings.Default.BackupEnabled =
                    numericBackupInterval.Enabled =
                    textBoxBackupPath.Enabled =
                    checkBoxBackup.Checked;
            };
            checkBoxBackup.Checked = Settings.Default.BackupEnabled;

            textBoxBackupPath.Text = Settings.Default.BackupPath;
            textBoxBackupPath.TextChanged += (sender, e) =>
                Settings.Default.BackupPath = textBoxBackupPath.Text;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
