using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class Main
    {
        private Dictionary<Type, Action<Resource>> ResourceEditors = new Dictionary<Type, Action<Resource>>();

        private Resource SelectedResource
        {
            get
            {
                if (listViewResources.SelectedItems.Count < 1) return null;
                return (Resource)listViewResources.SelectedItems[0].Tag;
            }
        }

        private void InitializeEditors()
        {
            RegisterEditor<Equation>(OpenEquationEditor);
            RegisterEditor<DataSet>(OpenDataSetEditor);
            RegisterEditor<Picture>(OpenPictureViewer);
            RegisterEditor<Page>(OpenPageEditor);
            RegisterEditor<HtmlPage>(OpenHtmlViewer);

            panelPageEditor.Dock = DockStyle.Fill;
            panelImageViewer.Dock = DockStyle.Fill;
            panelHtmlViewer.Dock = DockStyle.Fill;
            CloseResourcePanels();
        }

        private void OpenEquationEditor(Equation equation)
        {
            new EquationEditor(equation).ShowDialog();
            DocumentModified = true;
        }

        private void OpenDataSetEditor(DataSet dataSet)
        {
            new DataSetEditor(dataSet).ShowDialog();
            DocumentModified = true;
        }

        private void CloseResourcePanels()
        {
            panelPageEditor.Visible = panelPageEditor.Enabled = false;
            panelImageViewer.Visible = panelImageViewer.Enabled = false;
            panelHtmlViewer.Visible = panelHtmlViewer.Enabled = false;
        }

        private void RegisterEditor<T>(Action<T> editor) where T : Resource
        {
            ResourceEditors.Add(typeof(T), r => editor(r as T));
        }

        private ListViewItem CreateListViewItem(Resource resource)
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, resource.Name) { Name = "Name" });
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, resource.Author) { Name = "Author" });
            item.Tag = resource;
            // item.SubItems.Add(new ListViewItem.ListViewSubItem(item, resource.CreationDate.ToShortDateString()) { Name = "Created on" });
            if (resource is Page)
                item.ImageIndex = 0;
            else if (resource is Equation)
                item.ImageIndex = 1;
            else if (resource is DataSet)
                item.ImageIndex = 2;
            else if (resource is Picture)
                item.ImageIndex = 4;
            else if (resource is HtmlPage)
                item.ImageIndex = 5;
            else
                item.ImageIndex = 3;

            if (CurrentDocument.CurrentResource == resource)
                item.Selected = true;

            return item;
        }

        private bool IsResourceDisplayed(Resource resource)
        {
            if (resource.Hidden)
            {
                return toolStripToggleHidden.Checked;
            }
            else if (resource is Equation)
            {
                return toolStripToggleEquations.Checked;
            }
            else if (resource is DataSet)
            {
                return toolStripToggleDataSets.Checked;
            }
            else if (resource is Picture)
            {
                return toolStripTogglePictures.Checked;
            }
            else if (resource is Page)
            {
                return toolStripTogglePages.Checked;
            }
            else if (resource is HtmlPage)
            {
                return toolStripToggleHtmlPages.Checked;
            }
            else
            {
                return false;
            }
        }

        private string GetResourceOrderComparer(Resource resource)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(resource.GetType().Name);
            sb.Append('.');
            if (resource is Page)
            {
                sb.Append(CurrentDocument.PageOrder
                    .IndexOf(resource as Page)
                    .ToString()
                    .PadLeft(10, '0'));
            }
            else
            {
                sb.Append(resource.Name);
            }
            return sb.ToString();
        }

        /// <summary>
        /// This boolean works around an internal bug in the .NET Framework's ListView layout engine.
        /// </summary>
        private bool IsRefreshingResourceListView = false;
        private void RefreshResourceListView()
        {
            IEnumerable<Resource> displayedResources = CurrentDocument
                .Where(IsResourceDisplayed)
                .OrderBy(GetResourceOrderComparer);

            var topItemIndex = listViewResources.TopItem != null ? listViewResources.TopItem.Index : 0;

            IsRefreshingResourceListView = true;
            listViewResources.SuspendLayout();
            listViewResources.Items.Clear();

            foreach (Resource resource in displayedResources)
            {
                listViewResources.Items.Add(CreateListViewItem(resource));
            }

            listViewResources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewResources.ResumeLayout(false);

            if(topItemIndex != 0 && listViewResources.Items.Count >= topItemIndex - 1)
                listViewResources.TopItem = listViewResources.Items[topItemIndex];
            IsRefreshingResourceListView = false;
        }

        private void OpenResourceEditor(Resource resource)
        {
            Type resourceType = resource.GetType();
            if (ResourceEditors.ContainsKey(resourceType))
            {
                CurrentDocument.CurrentResource = resource;
                ResourceEditors[resourceType](resource);
                RefreshResourceListView();

                NotifyResourceModified(SelectedResource);
            }
            else
            {
                MessageBox.Show("This resource cannot be edited.", "Open Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RemoveResource(Resource resource)
        {
            if (resource is Page)
            {
                RemovePage(resource as Page);
            }
            else
            {
                CurrentDocument.Remove(resource);
                DocumentModified = true;
                if (CurrentDocument.CurrentResource == resource)
                {
                    CurrentDocument.CurrentResource = null;
                    CloseResourcePanels();
                }
                RefreshResourceListView();
            }
            resource.Removed = true;
        }

        private void AddResource(Resource resource)
        {
            if (resource is Page)
            {
                AddPage(resource as Page);
            }
            else
            {
                CurrentDocument.Add(resource);
                DocumentModified = true;
                RefreshResourceListView();
            }
        }

        private void AddPage(Page page, Page after = null)
        {
            CurrentDocument.Add(page);
            DocumentModified = true;
            CurrentDocument.AddPageToPageOrder(page, after);
            RefreshResourceListView();
        }

        private void RemovePage(Page page)
        {
            CurrentDocument.RemovePageFromPageOrder(page);
            CurrentDocument.Remove(page);
            DocumentModified = true;
            if (CurrentDocument.CurrentResource == page)
            {
                CurrentDocument.CurrentResource = null;
                CloseResourcePanels();
            }
            RefreshResourceListView();
        }

        private string CreateResourceName(string rootName)
        {
            int currentNumber = 0;
            string derivedName;
            do
            {
                currentNumber += 1;
                derivedName = String.Format("{0} {1}", rootName, currentNumber);
            } while (CurrentDocument.Any(r => r.Name.ToLowerInvariant() == derivedName.ToLowerInvariant()));

            return derivedName;
        }

        private string GetResourceNameFromFileName(string fileName)
        {
            return String.Join(".", fileName
                   .Split('\\', '/')
                   .Last()
                   .Split('.')
                   .Reverse()
                   .Skip(1)
                   .Reverse()
                   .ToArray());
        }

        private void NotifyResourceModified(Resource resource)
        {
            CurrentDocument.NotifyResourceModified(resource);
            DocumentModified = true;
        }

        #region WinForms code
        #region Toggles
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }

        private void toolStripTogglePages_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }

        private void toolStripToggleEquations_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }

        private void toolStripToggleDataSets_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }

        private void toolStripToggleHtmlPages_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }

        private void toolStripToggleHidden_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }

        private void toolStripTogglePictures_Click(object sender, EventArgs e)
        {
            RefreshResourceListView();
        }
        #endregion

        private void listViewResources_Resize(object sender, EventArgs e)
        {
            if (!IsRefreshingResourceListView)
            {
                // throws .NET framework exception without if statement... see IsRefreshingResourceListView
                listViewResources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resource resource = SelectedResource;
            if (resource != null)
            {
                Image captionImage = resource.GetResourceIcon(true);

                string newName = GetUserTextInput("Enter a new name for this resource.", "Rename", resource.Name, captionImage);
                if (newName != null)
                {
                    resource.Name = newName;
                    DocumentModified = true;
                    RefreshResourceListView();
                    NotifyResourceModified(resource);
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resource resource = SelectedResource;
            if (resource != null)
            {
                ResourcePropertiesEditor propertiesEditor = new ResourcePropertiesEditor(resource);
                propertiesEditor.ShowDialog();
                DocumentModified = true;
                RefreshResourceListView();
                NotifyResourceModified(resource);
            }
        }

        private void listViewResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool somethingSelected = listViewResources.SelectedIndices.Count >= 1;
            renameToolStripMenuItem.Enabled =
                propertiesToolStripMenuItem.Enabled =
                removeToolStripMenuItem.Enabled =
                editToolStripMenuItem.Enabled = somethingSelected;
            if (somethingSelected)
            {
                int selectedIndex = listViewResources.SelectedIndices[0];
            }
            else
            {
                
            }
        }

        private void listViewResources_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Resource resource = SelectedResource;
            if(SelectedResource != null)
                OpenResourceEditor(resource);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resource resource = SelectedResource;
            if (SelectedResource != null)
                OpenResourceEditor(resource);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resource resource = SelectedResource;
            if (SelectedResource != null)
            {
                if (MessageBox.Show("Are you sure you want to remove " + resource.Name + "?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    RemoveResource(resource);
                }
            }
        }

        private void equationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string equationName = GetUserTextInput("Enter a name for the new equation.", "New Equation", CreateResourceName("Equation"), Properties.Resources.Equation32);

            if (equationName != null)
            {
                Equation equation = new Equation()
                {
                    Name = equationName
                };

                AddResource(equation);
                OpenResourceEditor(equation);
            }
        }

        private void dataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dataSetName = GetUserTextInput("Enter a name for the new data set.", "New Data Set", CreateResourceName("Data Set"), Properties.Resources.DataSet32);

            if (dataSetName != null)
            {
                DataSet dataSet = new DataSet(Properties.Settings.Default.DefaultDataSetVariables)
                {
                    Name = dataSetName
                };
                DataSetCreator creator = new DataSetCreator(dataSet);
                if (creator.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    AddResource(dataSet);
                    OpenResourceEditor(dataSet);
                }
            }
        }

        private void pageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pageName = GetUserTextInput("Enter a name for the new page.", "New Page", CreateResourceName("Page"), Properties.Resources.Page);

            if (pageName != null)
            {
                Page page = new Page()
                {
                    Name = pageName
                };
                AddResource(page);
                OpenResourceEditor(page);
            }
        }

        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Image files (*.png; *.jpg; *.jpe; *.jpeg; *.bmp; *.gif; *.tga; *.tif)|*.png;*.jpg;*.jpe;*.jpeg;*.bmp;*.gif;*.tga;*.tif|All files|*",
                Title = "Import Image"
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = GetResourceNameFromFileName(dialog.FileName);

                Picture picture = new Picture(Image.FromFile(dialog.FileName))
                {
                    Name = fileName
                };
                AddResource(picture);
                OpenResourceEditor(picture);
            }
        }

        private void pageOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resource resource = SelectedResource;
                
            PageOrderEditor editor = new PageOrderEditor(
                CurrentDocument,
                resource != null && resource is Page ?
                    resource as Page :
                    null);
            editor.ShowDialog();
            RefreshResourceListView();
            DocumentModified = true;
        }

        private void webPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "HTML files (*.html; *.htm)|*.html;*.htm|All files|*",
                FilterIndex = 0,
                Title = "Import HTML"
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HtmlPage page = new HtmlPage(File.ReadAllText(dialog.FileName))
                {
                    Name = GetResourceNameFromFileName(dialog.FileName)
                };

                AddResource(page);
                DocumentModified = true;
                OpenResourceEditor(page);
            }
        }

        private void listViewResources_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listViewResources.DoDragDrop(SelectedResource.Guid.ToString(), DragDropEffects.Link);
        }
        #endregion
    }
}
