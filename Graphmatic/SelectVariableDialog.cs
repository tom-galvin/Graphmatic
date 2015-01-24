using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphmatic
{
    /// <summary>
    /// Represents a form that allows users to choose from a selection of variables.
    /// </summary>
    public class SelectVariableDialog : Form
    {
        private const int ComboBoxWidth = 50;
        private const int LabelWidth = 125;
        private const int ButtonWidth = 75;

        /// <summary>
        /// Gets or sets the combo boxes on the form, in the order they are required.
        /// </summary>
        private List<ComboBox> ComboBoxes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the values entered by the user.
        /// If no values are returned by the user, this property will return null.
        /// </summary>
        public char[] Value
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets an array of the possible values that each user choice can take.
        /// </summary>
        private char[] PossibleValues
        {
            get;
            set;
        }

        /// <summary>
        /// Prompts the user to select a number of variables from a predefined selection of choices.
        /// </summary>
        /// <param name="title">The title of the selection dialog.</param>
        /// <param name="possibleValues">An array of possible character values that each choice can take.</param>
        /// <param name="labels">
        /// The labels for each different required variable choice.<para/>
        /// The size of this parameter array determines the size of the array returned by the method.
        /// </param>
        /// <returns>Returns an array of user-selected variables from the <c>possibleValues</c> parameter.<para/>
        /// If the user cancels the dialog interaction then this method returns null.</returns>
        public static char[] SelectVariables(string title, char[] possibleValues, params string[] labels)
        {
            SelectVariableDialog dialog = new SelectVariableDialog(title, possibleValues, labels);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>SelectVariableDialog</c> form, with the given parameters.
        /// </summary>
        /// <param name="title">The title of the selection dialog.</param>
        /// <param name="possibleValues">An array of possible character values that each choice can take.</param>
        /// <param name="labels">
        /// The labels for each different required variable choice.<para/>
        /// The size of this parameter array determines the size of the array returned by the <c>Value</c> property.
        /// </param>
        public SelectVariableDialog(string title, char[] possibleValues, params string[] labels)
        {
            Text = title;
            Value = null;
            PossibleValues = possibleValues;

            int height = 12;

            ComboBoxes = new List<ComboBox>();

            SuspendLayout();
            Font = SystemFonts.MessageBoxFont;

            int index = 0;
            foreach (string label in labels) // for each required variable to be chosen...
            {
                AddSelection(ref height, ref index, label);
            }

            AddUIButtons(ref height, ref index);

            height += 12;
            ClientSize = new Size(12 + LabelWidth + 8 + ComboBoxWidth + 12, height);
            ShowInTaskbar = ShowIcon = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = MinimizeBox = false;

            ResumeLayout(false);
        }

        /// <summary>
        /// Adds a selection region to the form, consisting of a ComboBox and a Label (for indicating the purpose of the former).
        /// <para/>
        /// The value of the ComboBox in this region will be put into the <c>Value</c> property in the order the ComboBoxes
        /// are added onto the page.
        /// </summary>
        /// <param name="height">The current height of the control, used for adding controls down the form.</param>
        /// <param name="index">The current TabIndex of the controls added to the form.</param>
        /// <param name="label">The label for this particular ComboBox.</param>
        private void AddSelection(ref int height, ref int index, string label)
        {
            // add combo box
            ComboBox comboBoxControl = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Height = 23,
                Width = ComboBoxWidth,
                Top = height,
                Left = 12 + LabelWidth + 8,
                TabIndex = index++,
                Font = this.Font
            };
            foreach (char possibleValue in PossibleValues)
            {
                // lack of value-type array covariance means I have to iterate this manually
                comboBoxControl.Items.Add(possibleValue);
            }
            comboBoxControl.SelectedIndex = 0;

            // add label
            Label labelControl = new Label()
            {
                Text = label,
                TextAlign = System.Drawing.ContentAlignment.TopRight,
                AutoSize = false,
                Height = 15,
                Width = LabelWidth,
                Top = height + 3,
                Font = this.Font,
                Left = 12
            };

            ComboBoxes.Add(comboBoxControl);
            Controls.Add(comboBoxControl);
            Controls.Add(labelControl);
            height += 23 + 6;
        }

        /// <summary>
        /// Adds to the form any buttons responsible for determining the DialogResult of the form.
        /// </summary>
        /// <param name="height">The current height of the control, used for adding controls down the form.</param>
        /// <param name="index">The current TabIndex of the controls added to the form.</param>
        private void AddUIButtons(ref int height, ref int index)
        {
            Button okButton = new Button()
            {
                Text = "&OK",
                Width = ButtonWidth,
                Height = 23,
                Top = height,
                Left = 12,
                Font = this.Font,
                TabIndex = index++
            };
            Button cancelButton = new Button()
            {
                Text = "&Cancel",
                Width = ButtonWidth,
                Height = 23,
                Top = height,
                Left = 12 + ButtonWidth + 6,
                Font = this.Font,
                TabIndex = index++
            };
            AcceptButton = okButton;
            CancelButton = cancelButton;
            okButton.Click += okButton_Click;
            cancelButton.Click += cancelButton_Click;
            height += 23;
            Controls.Add(okButton);
            Controls.Add(cancelButton);
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            Value = null;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            Value = ComboBoxes
                .Select(c => PossibleValues[c.SelectedIndex])
                .ToArray();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
