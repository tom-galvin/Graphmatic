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

namespace Graphmatic
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void dispToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputWindow inputWindow = new InputWindow('y', 'x');
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText("test.xml", inputWindow.Result.ToXmlElement().ToString());
                Process.Start("test.xml");
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
            new Options().ShowDialog(this);
        }
    }
}
