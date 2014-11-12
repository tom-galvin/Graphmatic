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
    public partial class Options : Form
    {
        private bool IsReloading = false;

        public Options()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            CreateSetting(
                "default-page-color",
                colorChooserDefaultPageColor,
                editor => editor.Color,
                (editor, value) => editor.Color = value,
                Color.White);
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
            string settingName,
            TSettingEditor settingEditor,
            Func<TSettingEditor, TSetting> getEditorValue,
            Action<TSettingEditor, TSetting> setEditorValue,
            TSetting defaultSettingValue = default(TSetting)) where TSettingEditor : Control
        {
            setEditorValue(settingEditor, Program.Settings.Root.GetOrDefault<TSetting>(settingName, defaultSettingValue));
            this.FormClosing += (sender, e) => Program.Settings.Root.Set<TSetting>(settingName, getEditorValue(settingEditor));
        }
    }
}
