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
        /// <summary>
        /// The resource editors to use for each type of resource.
        /// </summary>
        private Dictionary<Type, Action<Resource>> ResourceEditors = new Dictionary<Type, Action<Resource>>();

        /// <summary>
        /// Gets the resource that is currently selected in the resource view.
        /// </summary>
        private Resource SelectedResource
        {
            get
            {
                if (listViewResources.SelectedItems.Count < 1) return null;
                return (Resource)listViewResources.SelectedItems[0].Tag;
            }
        }

        /// <summary>
        /// Initializes the editors for the different types of Graphmatic resources.
        /// </summary>
        private void InitializeEditors()
        {
            RegisterEditor<Equation>(OpenEquationEditor);
            RegisterEditor<DataSet>(OpenDataSetEditor);
            RegisterEditor<Page>(OpenPageEditor);

            panelPageEditor.Dock = DockStyle.Fill;
            CloseResourcePanels();
        }

        /// <summary>
        /// Opens the equation editor for the specified equation.
        /// </summary>
        /// <param name="equation">The equation for which to open the editor.</param>
        private void OpenEquationEditor(Equation equation)
        {
            new EquationEditor(equation).ShowDialog();
            DocumentModified = true;
        }

        /// <summary>
        /// Opens the data set editor for the specified equation.
        /// </summary>
        /// <param name="dataSet">The data set for which to open the editor.</param>
        private void OpenDataSetEditor(DataSet dataSet)
        {
            new DataSetEditor(CurrentDocument, dataSet).ShowDialog();
            DocumentModified = true;
        }

        /// <summary>
        /// Closes any open resource panels in the document.
        /// </summary>
        private void CloseResourcePanels()
        {
            panelPageEditor.Visible = panelPageEditor.Enabled = false;
        }

        /// <summary>
        /// Registers the specified resource editor method for the specified resource type.
        /// Thus, whenever a resource of type <typeparamref name="T"/> is opened in the editor,
        /// call the <paramref name="editor"/> method.
        /// </summary>
        /// <typeparam name="T">The type of resource to register to this editor.</typeparam>
        /// <param name="editor">The editor to open.</param>
        private void RegisterEditor<T>(Action<T> editor) where T : Resource
        {
            ResourceEditors.Add(typeof(T), r => editor(r as T));
        }

        /// <summary>
        /// Creates an item in the list view for the given resource.
        /// </summary>
        /// <param name="resource">The resource for which to create a list view item.</param>
        /// <returns>Returns a <see cref="System.Windows.Forms.ListViewItem"/> with the correct
        /// text, author and icon for <paramref name="resource"/>.</returns>
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
            else
                item.ImageIndex = 3;

            if (CurrentDocument.CurrentResource == resource)
                item.Selected = true;

            return item;
        }

        /// <summary>
        /// Determines if <paramref name="resource"/> is to be displayed in the resource list.
        /// This method exists as you can filter the list on the resource type, and thus we
        /// need to check if the resource is to be displayed in the specific circumstances.
        /// </summary>
        /// <param name="resource">The resource to check.</param>
        /// <returns>Returns <c>true</c> if <paramref name="resource"/> is to be displayed.</returns>
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
            else if (resource is Page)
            {
                return toolStripTogglePages.Checked;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the string used to order resources in the resource editor.
        /// This is a bit hacky as it relies on the behaviour of the default
        /// string comparer to order resources, so <see cref="Graphmatic.Interaction.Page"/>s
        /// are ordered slightly different to everything else; they are ordered based on the
        /// stored page order for the current document, whereas everything else is ordered on
        /// the resource name alphabetically, so depending on the type of resource, this returns
        /// a string dependent on <paramref name="resource"/> which is sorted in the correct
        /// order.
        /// </summary>
        /// <param name="resource">The resource for which to return a comparing string.</param>
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

                toolStripStatusLabelEditor.Text = resourceType.GetType().Name;
                NotifyResourceModified(SelectedResource);
            }
            else
            {
                MessageBox.Show("This resource cannot be edited.", "Open Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private Resource DuplicateResource(Resource resource, string newName)
        {
            Resource duplicate = resource.Duplicate(CurrentDocument);
            duplicate.Name = newName;
            AddResource(duplicate);
            return duplicate;
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

                string newName = EnterTextDialog.EnterText("Enter a new name for this resource.", "Rename", resource.Name, captionImage);
                if (newName != null)
                {
                    resource.Name = newName;
                    DocumentModified = true;
                    RefreshResourceListView();
                    NotifyResourceModified(resource);
                }
            }
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resource resource = SelectedResource;
            if (resource != null)
            {
                string newResourceName = EnterTextDialog.EnterText(
                String.Format("Enter a name for the duplicate of {0}.", resource.Name),
                "Duplicate",
                String.Format("Copy of {0}", resource.Name),
                resource.GetResourceIcon(true));
                if (newResourceName != null)
                {
                    OpenResourceEditor(DuplicateResource(resource, newResourceName));
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
                duplicateToolStripMenuItem.Enabled = 
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
            string equationName = EnterTextDialog.EnterText("Enter a name for the new equation.", "New Equation", CreateResourceName("Equation"), Properties.Resources.Equation32);

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
            string dataSetName = EnterTextDialog.EnterText("Enter a name for the new data set.", "New Data Set", CreateResourceName("Data Set"), Properties.Resources.DataSet32);

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
            string pageName = EnterTextDialog.EnterText("Enter a name for the new page.", "New Page", CreateResourceName("Page"), Properties.Resources.Page32);

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

        private void listViewResources_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listViewResources.DoDragDrop(SelectedResource.Guid.ToString(), DragDropEffects.Link);
        }
        #endregion
    }
}
