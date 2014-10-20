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

        private void colorChooserDefaultPageColor_Load(object sender, EventArgs e)
        {
            colorChooserDefaultPageColor.Color =
                Program.Settings.Root.GetOrDefault<Color>("default-page-color", Color.White);
        }

        private void colorChooserDefaultPageColor_ColorChanged(object sender, EventArgs e)
        {
            Program.Settings.Root.Set<Color>("default-page-color", colorChooserDefaultPageColor.Color);
        }
    }
}
