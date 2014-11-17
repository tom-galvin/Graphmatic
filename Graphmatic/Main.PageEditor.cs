using Graphmatic.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphmatic
{
    public partial class Main : Form
    {
        private void OpenPageEditor(Page page)
        {
            CloseResourcePanels();
            panelPageEditor.Visible = panelPageEditor.Enabled = true;
        }
    }
}
