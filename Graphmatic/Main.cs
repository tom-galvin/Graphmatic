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
            new InputWindow("Plot y=").ShowDialog();
        }
    }
}
