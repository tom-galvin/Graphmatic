using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class Main
    {
        private void listViewResources_Resize(object sender, EventArgs e)
        {
            listViewResources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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
            else
                item.ImageIndex = 3;
            return item;
        }

        private void RefreshResourceListView()
        {
            IEnumerable<Resource> displayedResources = CurrentDocument
                .Where(r =>
                    (r is Equation && toolStripToggleEquations.Checked) ||
                    (r is Page && toolStripTogglePages.Checked) ||
                    (r is DataSet && toolStripToggleDataSets.Checked) ||
                    (r.Hidden && toolStripToggleHidden.Checked));
            listViewResources.Items.Clear();

            foreach (Resource resource in displayedResources)
            {
                listViewResources.Items.Add(CreateListViewItem(resource));
            }

            listViewResources.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent); 
        }

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

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewResources.SelectedItems.Count < 1) return;
            Resource resource = (Resource)listViewResources.SelectedItems[0].Tag;
            Image captionImage = resource.GetResourceIcon(true);

            string newName = EnterText("Enter a new name for this resource.", "Rename", resource.Name, captionImage);
            if (newName != null)
            {
                resource.Name = newName;
                DocumentModified = true;
            }
            RefreshResourceListView();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewResources.SelectedItems.Count < 1) return;
            Resource resource = (Resource)listViewResources.SelectedItems[0].Tag;
            ResourcePropertiesEditor propertiesEditor = new ResourcePropertiesEditor(resource);
            propertiesEditor.ShowDialog();
            DocumentModified = true;
            RefreshResourceListView();
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

        private void OpenResourceEditor(Resource resource)
        {
            if (resource is Equation)
            {
                new EquationEditor(resource as Equation).ShowDialog();
                DocumentModified = true;
                RefreshResourceListView();
            }
            else if (resource is DataSet)
            {
                new DataSetEditor(resource as DataSet).ShowDialog();
                DocumentModified = true;
                RefreshResourceListView();
            }
            else
            {
                MessageBox.Show("This resource cannot be edited.", "Open Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void listViewResources_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewResources.SelectedItems.Count < 1) return;
            Resource resource = (Resource)listViewResources.SelectedItems[0].Tag;
            OpenResourceEditor(resource);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewResources.SelectedItems.Count < 1) return;
            Resource resource = (Resource)listViewResources.SelectedItems[0].Tag;
            OpenResourceEditor(resource);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewResources.SelectedItems.Count < 1) return;
            Resource resource = (Resource)listViewResources.SelectedItems[0].Tag;

            if (MessageBox.Show("Are you sure you want to remove " + resource.Name + "?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                RemoveResource(resource);
            }
        }

        private void RemoveResource(Resource resource)
        {
            CurrentDocument.Remove(resource);
            DocumentModified = true;
            RefreshResourceListView();
        }

        private void AddResource(Resource resource)
        {
            CurrentDocument.Add(resource);
            DocumentModified = true;
            RefreshResourceListView();
        }

        private string GetNextName(string root)
        {
            int number = 0;
            string derived;
            do
            {
                number += 1;
                derived = String.Format("{0} {1}", root, number);
            } while (CurrentDocument.Any(r => r.Name.ToLowerInvariant() == derived.ToLowerInvariant()));

            return derived;
        }

        private void equationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string equationName = EnterText("Enter a name for the new equation.", "New Equation", GetNextName("Equation"), Properties.Resources.Equation32);

            if (equationName != null)
            {
                Equation equation = new Equation(
                    Properties.Settings.Default.DefaultPlottedVariable,
                    Properties.Settings.Default.DefaultVaryingVariable)
                {
                    Name = equationName
                };

                AddResource(equation);
                OpenResourceEditor(equation);
            }
        }

        private void dataSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dataSetName = EnterText("Enter a name for the new data set.", "New Data Set", GetNextName("Data Set"), Properties.Resources.DataSet32);

            if (dataSetName != null)
            {
                DataSet dataSet = new DataSet(Properties.Settings.Default.DefaultDataSetVariables)
                {
                    Name = dataSetName
                };
                DataSetCreator creator = new DataSetCreator(dataSet);
                if (creator.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AddResource(dataSet);
                    OpenResourceEditor(dataSet);
                }
            }
        }
    }
}
