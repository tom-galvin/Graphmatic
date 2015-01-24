using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graphmatic.Interaction;
using Graphmatic.Interaction.Plotting;
using Graphmatic.Interaction.Statistics;

namespace Graphmatic
{
    /// <summary>
    /// Represents a dialog box for visually editing the data contained with <c>Graphmatic.Interaction.DataSet</c> objects.
    /// </summary>
    public partial class DataSetEditor : Form
    {
        /// <summary>
        /// Gets the data set currently being edited by this DataSetEditor.
        /// </summary>
        public DataSet DataSet
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the document containing the data set referred to by the <c>DataSet</c> property.
        /// </summary>
        public Document Document
        {
            get;
            protected set;
        }

        public DataSetEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the <c>DataSetEditor</c> form, with the specified data set and corresponding parent form.
        /// </summary>
        /// <param name="document">The parent document of the data set to edit.</param>
        /// <param name="dataSet">The data set to edit.</param>
        public DataSetEditor(Document document, DataSet dataSet)
            : this()
        {
            Document = document;
            DataSet = dataSet;
            RefreshDataList();
            Text = String.Format("{0} - Data Set Editor",
                 DataSet.Name);
        }

        /// <summary>
        /// Refresh the contents of the data list to reflect changes to the underlying <c>DataSet</c>.<para/>
        /// Any unsaved changes will be erased.
        /// </summary>
        private void RefreshDataList()
        {
            dataGridView.SuspendLayout(); // speed up

            dataGridView.ColumnCount = DataSet.Variables.Length;
            for (int i = 0; i < DataSet.Variables.Length; i++)
            {
                dataGridView.Columns[i].Name = DataSet.Variables[i].ToString();
                dataGridView.Columns[i].ValueType = typeof(double);
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Rows.Clear();
            foreach (double[] rowData in DataSet)
            {
                object[] rowDataAsObjectArray = new object[rowData.Length];
                rowData.CopyTo(rowDataAsObjectArray, 0);
                int newRowIndex = dataGridView.Rows.Add(rowDataAsObjectArray);
                /* DataGridViewRow row = dataGridView.Rows[newRowIndex];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Value = rowData[i];
                } */
            }

            dataGridView.ResumeLayout();
        }

        private void buttonEditVariables_Click(object sender, EventArgs e)
        {
            if (SaveChanges())
            {
                DataSetCreator creator = new DataSetCreator(DataSet);
                if (creator.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshDataList();
                }
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

        /// <summary>
        /// Saves changes made in the editor to the data set in memory.
        /// </summary>
        /// <returns>Returns true if validation and saving was successful; false otherwise.</returns>
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
                    if (row.Cells[i].Value == null ||
                        (row.Cells[i].Value is string && (string)row.Cells[i].Value == ""))
                    {
                        dataGridView.ClearSelection();
                        row.Cells[i].Selected = true;
                        MessageBox.Show("This cell is empty. Please put a value in it or remove the row.", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    rowData[i] = (double)row.Cells[i].Value;
                }
                rows.Add(rowData);
            }
            DataSet.Set(rows);
            RefreshDataList();
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveChanges())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void buttonExcelCopy_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                // turn Excel-format tab-separated values into an array of array of strings
                string[][] clipData = Clipboard
                    .GetText()
                    .Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries))
                    .ToArray();
                if (clipData.Length > 0) // make sure the copied data isn't nonexistent...
                {
                    int arrayLength = clipData[0].Length;
                    for (int i = 1; i < clipData.Length; i++)
                    {
                        if (clipData[i].Length != arrayLength) // validate the size of the data's records
                        {
                            MessageBox.Show("All rows in the copied data must have the same length. " +
                                "Make sure you have no merged cells, and that you are selecting a square region.",
                                "Paste from Excel",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }

                    try // try to turn the strings in the data into doubles in the DataSet
                    {
                        double[][] doubleData = clipData
                            .Select(da =>
                                da.Select(d =>
                                    Double.Parse(d)).ToArray()
                                )
                            .ToArray();

                        foreach (double[] row in doubleData)
                        {
                            DataSet.Add(row);
                        }

                        RefreshDataList(); // refresh with our new changes
                    }
                    catch (FormatException)
                    {
                        // if the user tries to copy in some text from Excel, warn them
                        MessageBox.Show("Some data is not in a numeric format. " +
                        "Make sure that your data does not contain text or any formatted data.",
                        "Paste from Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                // if we can't parse the data, assume that it's not from Excel
                MessageBox.Show("The clipboard must contain text data from Microsoft Excel.",
                    "Paste from Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // validate data entered into the DataGridView
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            double temp;
            if (e.FormattedValue is string)
            {
                string formattedValue = (string)e.FormattedValue;
                if (formattedValue.Trim().Length == 0)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (double)0; // must be (Double) to avoid InvalidCastException with weird boxing later on
                }
                else if (!Double.TryParse(formattedValue, out temp))
                {
                    MessageBox.Show("The number entered is invalid.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void buttonStats_Click(object sender, EventArgs e)
        {
            contextMenuStripStatistics.Show(MousePosition);
        }

        #region Statistical Functions
        //This region contains the handlers for all of the statistical functions presented to the user

        /// <summary>
        /// Performs a single-variable reductive statistical function on a user-selected variable from the data set.<para/>
        /// This operation takes one variable from each row and turns it into one number.
        /// </summary>
        /// <param name="name">The name of the statistical function as presented to the user.</param>
        /// <param name="reductionFunction">
        /// The reduction function to use.<para/>
        /// This turns a list of numbers into one number through some transformation.
        /// </param>
        private void StatFunction(string name, Func<IEnumerable<double>, double> reductionFunction)
        {
            char[] variables = SelectVariableDialog.SelectVariables(name, DataSet.Variables, name + " of:");
            if (variables != null && SaveChanges())
            {
                var data = DataSet.RowSelect(variables[0]);
                double value = reductionFunction(data);
                MessageBox.Show(name + ": " + value.ToString("0.#####"), "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Performs a double-variable reductive statistical function on a user-selected variable from the data set.<para/>
        /// This operation takes two variables from each row and turns it into one number.
        /// </summary>
        /// <param name="name">The name of the statistical function as presented to the user.</param>
        /// <param name="reductionFunction">
        /// The reduction function to use.<para/>
        /// This turns a list of pairs of numbers into one number through some transformation.
        /// </param>
        /// <param name="uniqueVariables">True if chosen variables must be unique; false otherwise.</param>
        private void StatFunction(string name, Func<IEnumerable<Tuple<double, double>>, double> reductionFunction, bool uniqueVariables = false)
        {
            char[] variables = SelectVariableDialog.SelectVariables(name, DataSet.Variables, name + " of:", "and:");
            if (variables[0] != variables[1])
            {
                if (variables != null && SaveChanges())
                {
                    var data = DataSet.RowSelect(variables[0], variables[1]);
                    double value = reductionFunction(data);
                    MessageBox.Show(name + ": " + value.ToString("0.#####"), "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The chosen variables must not be the same.", "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Mean", ns => ns.Mean());
        }

        private void varianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Variance", ns => ns.UnnormalizedVariance() / ns.Count());
        }

        private void covarianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Covariance", ns => ns.UnnormalizedCovariance() / ns.Count(), true);
        }

        private void standarddeviationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Standard deviation", ns => Math.Sqrt(ns.UnnormalizedVariance() / ns.Count()));
        }

        private void sumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Sum", ns => ns.Sum());
        }

        private void squaredSumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Squared sum", ns => ns.Select(d => d * d).Sum());
        }

        private void productSumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatFunction("Product sum", ns => ns.Select(t => t.Item1 * t.Item2).Sum(), true);
        }

        private void pMCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            char[] variables = SelectVariableDialog.SelectVariables("Product-moment correlation coefficient", DataSet.Variables, "PMCC of:", "with:");
            if (variables[0] != variables[1])
            {
                if (variables != null && SaveChanges())
                {
                    double value = DataSet.Pmcc(variables[0], variables[1]);
                    MessageBox.Show("Product-moment correlation coefficient: " + value.ToString("0.#####"), "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The chosen variables must not be the same.", "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createRegressionLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            char[] variables = SelectVariableDialog.SelectVariables("Regression line", DataSet.Variables, "Independent variable:", "Dependent variable:");
            if (variables[0] != variables[1])
            {
                if (variables != null && SaveChanges())
                {
                    LinearCurve line = DataSet.FitLinear(variables[0], variables[1]);
                    Document.Add(line.ToEquation());
                    MessageBox.Show(
                        "The regression line has been added to the Document.\r\n" + line.ToString(),
                        "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The chosen variables must not be the same.", "Statistics", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
