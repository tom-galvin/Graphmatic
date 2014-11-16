using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public partial class DataSetCreator : Form
    {
        public DataSet DataSet
        {
            get;
            protected set;
        }

        public DataSetCreator()
        {
            InitializeComponent();
        }

        public DataSetCreator(DataSet dataSet)
            : this()
        {
            DataSet = dataSet;
            Text = DataSet.Name + " - Data Set Creator";

            foreach (char variable in DataSet.Variables)
                listBoxVariables.Items.Add(variable);
        }

        private void listBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool somethingSelected = listBoxVariables.SelectedIndex != -1;
            removeToolStripMenuItem.Enabled =
                moveUpToolStripMenuItem.Enabled =
                moveDownToolStripMenuItem.Enabled = somethingSelected;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateVariableDialog dialog = new CreateVariableDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                char newVariable = dialog.EnteredChar;
                int index = listBoxVariables.SelectedIndex != -1 ? listBoxVariables.SelectedIndex : DataSet.Variables.Length;
                foreach (char existingVariable in listBoxVariables.Items)
                {
                    if (existingVariable == newVariable)
                    {
                        MessageBox.Show("The variable " + newVariable + " already exists.", "Add Variable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                DataSet.AddVariable(
                    index,
                    newVariable,
                    0.0);
                listBoxVariables.Items.Insert(index, newVariable);
                listBoxVariables.SelectedIndex = index;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxVariables.SelectedIndex != -1)
            {
                DataSet.RemoveVariable(listBoxVariables.SelectedIndex);
                listBoxVariables.Items.RemoveAt(listBoxVariables.SelectedIndex);
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxVariables.SelectedIndex;
            if (selectedIndex != 0 && selectedIndex != -1)
            {
                char movedVariable = (char)listBoxVariables.SelectedItem;
                DataSet.SwapVariables(selectedIndex - 1, selectedIndex);
                listBoxVariables.Items.RemoveAt(selectedIndex);
                listBoxVariables.Items.Insert(selectedIndex - 1, movedVariable);
                listBoxVariables.SelectedIndex = selectedIndex - 1;
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxVariables.SelectedIndex;
            if (selectedIndex != listBoxVariables.Items.Count - 1 && selectedIndex != -1)
            {
                char movedVariable = (char)listBoxVariables.SelectedItem;
                DataSet.SwapVariables(selectedIndex, selectedIndex + 1);
                listBoxVariables.Items.RemoveAt(selectedIndex);
                listBoxVariables.Items.Insert(selectedIndex + 1, movedVariable);
                listBoxVariables.SelectedIndex = selectedIndex + 1;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void DataSetCreator_Load(object sender, EventArgs e)
        {
            listBoxVariables_SelectedIndexChanged(sender, e); // fixes nasty IndexOutOfRangeException
        }
    }
}