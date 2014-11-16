using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class Main : Form
    {
        private void OpenPictureViewer(Picture picture)
        {
            CloseResourcePanels();
            panelImageViewer.Visible = panelImageViewer.Enabled = true;
            pictureBoxImageViewer.Image = picture.ImageData;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxImageViewer.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxImageViewer.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
