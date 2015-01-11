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
    public partial class SettingsEditor : Form
    {
        public SettingsEditor()
        {
            InitializeComponent();
            InitializeSettings();
        }

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

        private void InitializeSettings()
        {
            colorChooserDefaultPageColor.Color = Settings.Default.DefaultPageColor;
            colorChooserDefaultPageColor.ColorChanged += (sender, e) =>
                Settings.Default.DefaultPageColor = colorChooserDefaultPageColor.Color;

            colorChooserDefaultGraphColor.Color = Settings.Default.DefaultGraphFeatureColor;
            colorChooserDefaultGraphColor.ColorChanged += (sender, e) =>
                Settings.Default.DefaultGraphFeatureColor = colorChooserDefaultGraphColor.Color;

            textBoxUserName.Text = Settings.Default.Username;
            textBoxUserName.TextChanged += (sender, e) =>
                Settings.Default.Username = textBoxUserName.Text;

            MakeCharBox(textBoxDefaultPlottedVariable,
                Settings.Default.DefaultVariable2,
                c => Settings.Default.DefaultVariable2 = c);

            MakeCharBox(textBoxDefaultVaryingVariable,
                Settings.Default.DefaultVariable1,
                c => Settings.Default.DefaultVariable1 = c);

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
