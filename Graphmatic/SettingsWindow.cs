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

namespace Graphmatic
{
    public partial class SettingsWindow : Form
    {
        private bool IsReloading = false;

        public SettingsWindow()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            CreateSetting<Color, ColorChooser>(
                colorChooserDefaultPageColor,
                (settings, editor) => editor.Color = settings.DefaultPageColor,
                (settings, editor) => settings.DefaultPageColor = editor.Color);
            CreateSetting<string, TextBox>(
                textBoxUserName,
                (settings, editor) => editor.Text = settings.UserName,
                (settings, editor) => settings.UserName = editor.Text);
        }

        private void Options_Load(object sender, EventArgs e)
        {
            buttonReload.Visible = Program.SettingsError;
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            IsReloading = true;
            Close();
            Program.LoadSettings();
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!IsReloading)
                Program.SaveSettings();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            IsReloading = true;
            Close();
        }

        private void CreateSetting<TSetting, TSettingEditor>(
            TSettingEditor settingEditor,
            Action<Settings, TSettingEditor> loadValue,
            Action<Settings, TSettingEditor> saveValue) where TSettingEditor : Control
        {
            loadValue(Program.Settings, settingEditor);
            this.FormClosing += (sender, e) => saveValue(Program.Settings, settingEditor);
        }
    }
}
