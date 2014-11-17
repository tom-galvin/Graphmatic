using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class PageOrderEditor : Form
    {
        public Document Document
        {
            get;
            protected set;
        }

        public PageOrderEditor()
        {
            InitializeComponent();
        }

        public PageOrderEditor(Document document, Page selectedPage = null)
            : this()
        {
            Document = document;

            foreach (Page page in Document.PageOrder)
            {
                listBoxPages.Items.Add(page);
            }

            listBoxPages.SelectedIndex = selectedPage == null ?
                listBoxPages.Items.Count - 1 :
                listBoxPages.Items.IndexOf(selectedPage);
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxPages.SelectedIndex;
            if (selectedIndex != 0 && selectedIndex != -1)
            {
                Page page = listBoxPages.Items[selectedIndex] as Page,
                     previousPage = listBoxPages.Items[selectedIndex - 1] as Page;
                int pageIndex = listBoxPages.Items.IndexOf(page),
                    previousPageIndex = listBoxPages.Items.IndexOf(previousPage);
                Document.SwapPages(page, previousPage);
                listBoxPages.Items[pageIndex] = previousPage;
                listBoxPages.Items[previousPageIndex] = page;

                listBoxPages.SelectedIndex = selectedIndex - 1;
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxPages.SelectedIndex;
            if (selectedIndex != listBoxPages.Items.Count - 1 && selectedIndex != -1)
            {
                Page page = listBoxPages.Items[selectedIndex] as Page,
                     nextPage = listBoxPages.Items[selectedIndex + 1] as Page;
                int pageIndex = listBoxPages.Items.IndexOf(page),
                    nextPageIndex = listBoxPages.Items.IndexOf(nextPage);
                Document.SwapPages(page, nextPage);
                listBoxPages.Items[pageIndex] = nextPage;
                listBoxPages.Items[nextPageIndex] = page;

                listBoxPages.SelectedIndex = selectedIndex + 1;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBoxPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            moveUpToolStripMenuItem.Enabled =
                moveDownToolStripMenuItem.Enabled =
                listBoxPages.SelectedIndex != -1;
        }

        private void PageOrderEditor_Load(object sender, EventArgs e)
        {
            listBoxPages_SelectedIndexChanged(sender, e);
        }
    }
}
