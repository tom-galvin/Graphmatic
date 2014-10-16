using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            new InputWindow("y:x;").ShowDialog();
        }

        private void lorgorgoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(EnterTextDialog.Display("Test", "ABC", "Lol", Properties.Resources.Graph24) ?? "hue");
        }
    }
}
