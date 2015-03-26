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
    /// <summary>
    /// Represents a form used for modifying the layout and variables of a data
    /// set (but not modifying the values inside it.)
    /// </summary>
    public partial class DataSetCreator : Form
    {
        /// <summary>
        /// Gets the data set that this form is to edit.
        /// </summary>
        public DataSet DataSet
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the DataSetCreator form.
        /// </summary>
        public DataSetCreator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the DataSetCreator form, with the
        /// given data set to edit.
        /// </summary>
        /// <param name="dataSet">The data set that this form is to edit.</param>
        public DataSetCreator(DataSet dataSet)
            : this()
        {
            DataSet = dataSet;
            Text = DataSet.Name + " - Data Set Creator";

            foreach (char variable in DataSet.Variables)
                listBoxVariables.Items.Add(variable);
        }

        /// <summary>
        /// Whenever the selected index in the list box is changed, the menu item values
        /// for removing and moving a variable need to be changed - for example, if an
        /// item is deselected, the menu item for removing a variable must be disabled as
        /// you cannot remove what isn't selected.
        /// </summary>
        private void listBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool somethingSelected = listBoxVariables.SelectedIndex != -1;
            removeToolStripMenuItem.Enabled =
                moveUpToolStripMenuItem.Enabled =
                moveDownToolStripMenuItem.Enabled = somethingSelected;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            char newVariable = CreateVariableDialog.EnterVariable();
            if (newVariable != '\0')
            {
                // find the index in the variable list at which to add the variable
                int index = listBoxVariables.SelectedIndex != -1 ? listBoxVariables.SelectedIndex : DataSet.Variables.Length;

                // check the data set does not already contain this variable
                foreach (char existingVariable in listBoxVariables.Items)
                {
                    if (existingVariable == newVariable)
                    {
                        MessageBox.Show("The variable " + newVariable + " already exists.", "Add Variable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // adds the variable to the data set
                DataSet.AddVariable(
                    index,
                    newVariable,
                    0.0);

                // adds the variable to the list box
                listBoxVariables.Items.Insert(index, newVariable);
                listBoxVariables.SelectedIndex = index;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxVariables.SelectedIndex != -1)
            {
                // remove a variable from the data set and from the list box
                DataSet.RemoveVariable(listBoxVariables.SelectedIndex);
                listBoxVariables.Items.RemoveAt(listBoxVariables.SelectedIndex);
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxVariables.SelectedIndex;
            if (selectedIndex != 0 && selectedIndex != -1)
            {
                // swaps two variables around to move a variable up (or to the left) in the data set
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
                // swaps two variables around to move a variable down (or to the right) in the data set
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

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxVariables.SelectedIndex;
            if (selectedIndex != -1)
            {
                // asks the user for the new name for the variable
                char newName = CreateVariableDialog.EnterVariable(DataSet.Variables[selectedIndex]);
                if (newName != '\0')
                {
                    // renames the variable as long as the user did not close the dialog
                    // (ie. when null character is returned)
                    DataSet.Variables[selectedIndex] = newName;
                    listBoxVariables.Items.RemoveAt(selectedIndex);
                    listBoxVariables.Items.Insert(selectedIndex, newName.ToString());
                }
            }
        }
    }
}