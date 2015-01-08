using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Interaction;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic
{
    public partial class DataSetEditor : Form
    {
        private bool _DataChanged;
        public bool DataChanged
        {
            get
            {
                return _DataChanged;
            }
            protected set
            {
                _DataChanged = value;
                RefreshTitle();
            }
        }

        public DataSet DataSet
        {
            get;
            protected set;
        }

        public DataSetEditor()
        {
            InitializeComponent();
        }

        public DataSetEditor(DataSet dataSet)
            : this()
        {
            DataSet = dataSet;
            RefreshDataList();
            DataChanged = false;
        }

        private void RefreshDataList()
        {
            dataGridView.SuspendLayout(); // speed up

            dataGridView.ColumnCount = DataSet.Variables.Length;
            for (int i = 0; i < DataSet.Variables.Length; i++)
            {
                dataGridView.Columns[i].Name = DataSet.Variables[i].ToString();
                dataGridView.Columns[i].ValueType = typeof(double);
            }

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Rows.Clear();
            foreach (double[] rowData in DataSet)
            {
                int newRowIndex = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[newRowIndex];
                for (int i = 0; i < row.Cells.Count; i++)
                    row.Cells[i].Value = rowData[i];
            }

            dataGridView.ResumeLayout();
        }

        private void RefreshTitle()
        {
            Text = String.Format("{0}{1} - Data Set Editor",
                DataSet.Name,
                DataChanged ? "*" : "");
        }

        private void buttonEditVariables_Click(object sender, EventArgs e)
        {
            if (DataChanged)
            {
                if (MessageBox.Show("Editing the variables will save any changed data beforehand.\r\n" +
                    "Are you sure you want to do this?", "Edit Variables", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return;
                SaveChanges();
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            DataSetCreator creator = new DataSetCreator(DataSet);
            if (creator.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                RefreshDataList();
                DataChanged = false;
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException)
            {
                MessageBox.Show("The number entered is invalid.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                throw e.Exception;
            }
        }

        private bool SaveChanges()
        {
            List<double[]> rows = new List<double[]>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                double[] rowData = new double[DataSet.Variables.Length];
                bool allNull = true;
                for (int i = 0; i < DataSet.Variables.Length; i++)
                {
                    if (row.Cells[i].Value != null)
                    {
                        allNull = false;
                        break;
                    }
                }
                if (allNull) continue;
                for (int i = 0; i < DataSet.Variables.Length; i++)
                {
                    if (row.Cells[i].Value == null)
                    {
                        MessageBox.Show("This cell is empty. Please put a value in it or remove the row.", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView.ClearSelection();
                        row.Cells[i].Selected = true;
                        return false;
                    }
                    rowData[i] = (double)row.Cells[i].Value;
                }
                rows.Add(rowData);
            }
            DataSet.Set(rows);
            RefreshDataList();
            DataChanged = false;
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (DataChanged)
            {
                if (MessageBox.Show(
                    "You have unsaved changes to your data. Are you sure you want to cancel?",
                    "Cancel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataChanged = true;
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataChanged = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveChanges())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataChanged = true;
        }
    }
}
