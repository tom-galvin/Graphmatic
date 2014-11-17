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
        private void OpenHtmlViewer(Resource resource)
        {
            CloseResourcePanels();
            panelHtmlViewer.Visible = panelHtmlViewer.Enabled = true;
            webBrowserHtmlViewer.DocumentText = (resource as HtmlPage).HtmlData;
        }

        #region WinForms code
        private void webBrowserHtmlViewer_CanGoForwardChanged(object sender, EventArgs e)
        {
            toolStripButtonBack.Enabled = webBrowserHtmlViewer.CanGoBack;
        }

        private void webBrowserHtmlViewer_CanGoBackChanged(object sender, EventArgs e)
        {
            toolStripButtonForward.Enabled = webBrowserHtmlViewer.CanGoForward;
        }


        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            webBrowserHtmlViewer.GoBack();
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            webBrowserHtmlViewer.GoForward();
        }

        private void webBrowserHtmlViewer_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.Scheme == "gmd")
            {
                e.Cancel = true;
                Guid guid;
                if (Guid.TryParse(e.Url.Host, out guid))
                {
                    OpenResourceEditor(CurrentDocument.FromGuid(guid));
                }
                else
                {
                    MessageBox.Show("The URI " + e.Url.ToString() + " is not a valid in-document URI.", "Navigate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
