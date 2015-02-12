using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Graphmatic
{
    /// <summary>
    /// Represents a dialog used to report errors occurring in Graphmatic in the user's environment.
    /// </summary>
    public partial class ErrorReportingDialog : Form
    {
        /// <summary>
        /// Gets the exception that was thrown.
        /// </summary>
        public Exception Exception
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets whether an error report has been created.
        /// </summary>
        public bool ErrorReportCreated
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of an crash reporting dialog.
        /// </summary>
        private ErrorReportingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of an crash reporting dialog with the specified <see cref="System.Exception"/> that was thrown.
        /// </summary>
        /// <param name="exception">The exception that was thrown.</param>
        public ErrorReportingDialog(Exception exception)
            : this()
        {
            Exception = exception;
            InitializeDialogContent();
        }

        /// <summary>
        /// Initializes the content of the dialog displaying information about the exception to the user.
        /// </summary>
        private void InitializeDialogContent()
        {
            labelHeader.Text = Exception.GetType().Name;
            Text = String.Format(
                "{0} - Crash Reporting",
                Exception.GetType().Name);
        }

        /// <summary>
        /// Creates a save file dialog used to save the error report to an appropriate location.
        /// </summary>
        /// <returns>A save file dialog with appropriate properties for saving XML error reports.</returns>
        private SaveFileDialog CreateSaveFileDialog()
        {
            return new SaveFileDialog()
            {
                DefaultExt = "xml",
                Filter = "Graphmatic error report (*.xml)|*.xml|All files|*",
                FilterIndex = 0
            };
        }

        /// <summary>
        /// Creates an error report for this crash event.
        /// </summary>
        /// <returns>An XML document containing the crash data for the error report.</returns>
        private XDocument CreateErrorReport()
        {
            return new XDocument(
                new XElement("ErrorReport",
                    new XElement("Name", Exception.GetType().Name),
                    new XElement("Details", Exception.ToString()),
                    new XElement("UserDescription", textBoxDescription.Text),
                    new XElement("ReportTime", DateTime.Now)));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = CreateSaveFileDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CreateErrorReport().Save(dialog.FileName);
                    MessageBox.Show(
                        "Thank you for creating an error report. Send the file to me and I will do my best to fix the problem.",
                        "Crash Reporting",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    ErrorReportCreated = true;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("The error report could not be saved.\r\n" +
                    ex.Message, "Crash Reporting - Save Report",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ErrorReportingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ErrorReportCreated)
            {
                if (MessageBox.Show(
                    "You have not yet created an error report. " +
                    "Are you sure you want to close this dialog?", "Crash Reporting",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
