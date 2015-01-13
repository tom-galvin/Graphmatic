namespace Graphmatic
{
    partial class SettingsEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDefaults = new System.Windows.Forms.TabPage();
            this.numericBackupInterval = new System.Windows.Forms.NumericUpDown();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.labelBackupPath = new System.Windows.Forms.Label();
            this.textBoxBackupPath = new System.Windows.Forms.TextBox();
            this.textBoxDefaultDataSetVariables = new System.Windows.Forms.TextBox();
            this.labelDefaultDataSetVariables = new System.Windows.Forms.Label();
            this.textBoxDefaultVaryingVariable = new System.Windows.Forms.TextBox();
            this.textBoxDefaultPlottedVariable = new System.Windows.Forms.TextBox();
            this.labelDefaultPlottedVariable = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.colorChooserDefaultGraphColor = new Graphmatic.ColorChooser();
            this.labelDefaultFeatureColor = new System.Windows.Forms.Label();
            this.colorChooserDefaultPageColor = new Graphmatic.ColorChooser();
            this.labelDefaultPageColor = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.colorChooserDefaultHighlightColor = new Graphmatic.ColorChooser();
            this.labelDefaultHighlightColor = new System.Windows.Forms.Label();
            this.colorChooserDefaultPencilColor = new Graphmatic.ColorChooser();
            this.labelDefaultPencilColor = new System.Windows.Forms.Label();
            this.numericDefaultPencilWidth = new System.Windows.Forms.NumericUpDown();
            this.labelDefaultPencilWidth = new System.Windows.Forms.Label();
            this.labelDefaultHighlightWidth = new System.Windows.Forms.Label();
            this.numericDefaultHighlightWidth = new System.Windows.Forms.NumericUpDown();
            this.tabControl.SuspendLayout();
            this.tabPageDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackupInterval)).BeginInit();
            this.tabPageDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDefaultPencilWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDefaultHighlightWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageDefaults);
            this.tabControl.Controls.Add(this.tabPageDisplay);
            this.tabControl.Location = new System.Drawing.Point(12, 22);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(419, 178);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageDefaults
            // 
            this.tabPageDefaults.Controls.Add(this.numericBackupInterval);
            this.tabPageDefaults.Controls.Add(this.checkBoxBackup);
            this.tabPageDefaults.Controls.Add(this.labelBackupPath);
            this.tabPageDefaults.Controls.Add(this.textBoxBackupPath);
            this.tabPageDefaults.Controls.Add(this.textBoxDefaultDataSetVariables);
            this.tabPageDefaults.Controls.Add(this.labelDefaultDataSetVariables);
            this.tabPageDefaults.Controls.Add(this.textBoxDefaultVaryingVariable);
            this.tabPageDefaults.Controls.Add(this.textBoxDefaultPlottedVariable);
            this.tabPageDefaults.Controls.Add(this.labelDefaultPlottedVariable);
            this.tabPageDefaults.Controls.Add(this.textBoxUserName);
            this.tabPageDefaults.Controls.Add(this.label3);
            this.tabPageDefaults.Location = new System.Drawing.Point(4, 24);
            this.tabPageDefaults.Name = "tabPageDefaults";
            this.tabPageDefaults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefaults.Size = new System.Drawing.Size(411, 150);
            this.tabPageDefaults.TabIndex = 0;
            this.tabPageDefaults.Text = "Defaults";
            this.tabPageDefaults.UseVisualStyleBackColor = true;
            // 
            // numericBackupInterval
            // 
            this.numericBackupInterval.Location = new System.Drawing.Point(179, 122);
            this.numericBackupInterval.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.numericBackupInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericBackupInterval.Name = "numericBackupInterval";
            this.numericBackupInterval.Size = new System.Drawing.Size(226, 23);
            this.numericBackupInterval.TabIndex = 9;
            this.numericBackupInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxBackup
            // 
            this.checkBoxBackup.AutoSize = true;
            this.checkBoxBackup.Location = new System.Drawing.Point(9, 123);
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.Size = new System.Drawing.Size(164, 19);
            this.checkBoxBackup.TabIndex = 8;
            this.checkBoxBackup.Text = "Backup interval (seconds):";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            // 
            // labelBackupPath
            // 
            this.labelBackupPath.AutoSize = true;
            this.labelBackupPath.Location = new System.Drawing.Point(56, 96);
            this.labelBackupPath.Name = "labelBackupPath";
            this.labelBackupPath.Size = new System.Drawing.Size(117, 15);
            this.labelBackupPath.TabIndex = 11;
            this.labelBackupPath.Text = "Default backup path:";
            // 
            // textBoxBackupPath
            // 
            this.textBoxBackupPath.Location = new System.Drawing.Point(179, 93);
            this.textBoxBackupPath.Name = "textBoxBackupPath";
            this.textBoxBackupPath.Size = new System.Drawing.Size(226, 23);
            this.textBoxBackupPath.TabIndex = 7;
            // 
            // textBoxDefaultDataSetVariables
            // 
            this.textBoxDefaultDataSetVariables.Location = new System.Drawing.Point(179, 64);
            this.textBoxDefaultDataSetVariables.Name = "textBoxDefaultDataSetVariables";
            this.textBoxDefaultDataSetVariables.Size = new System.Drawing.Size(226, 23);
            this.textBoxDefaultDataSetVariables.TabIndex = 6;
            // 
            // labelDefaultDataSetVariables
            // 
            this.labelDefaultDataSetVariables.AutoSize = true;
            this.labelDefaultDataSetVariables.Location = new System.Drawing.Point(32, 67);
            this.labelDefaultDataSetVariables.Name = "labelDefaultDataSetVariables";
            this.labelDefaultDataSetVariables.Size = new System.Drawing.Size(141, 15);
            this.labelDefaultDataSetVariables.TabIndex = 8;
            this.labelDefaultDataSetVariables.Text = "Default data set variables:";
            // 
            // textBoxDefaultVaryingVariable
            // 
            this.textBoxDefaultVaryingVariable.Location = new System.Drawing.Point(231, 35);
            this.textBoxDefaultVaryingVariable.Name = "textBoxDefaultVaryingVariable";
            this.textBoxDefaultVaryingVariable.Size = new System.Drawing.Size(46, 23);
            this.textBoxDefaultVaryingVariable.TabIndex = 5;
            // 
            // textBoxDefaultPlottedVariable
            // 
            this.textBoxDefaultPlottedVariable.Location = new System.Drawing.Point(179, 35);
            this.textBoxDefaultPlottedVariable.Name = "textBoxDefaultPlottedVariable";
            this.textBoxDefaultPlottedVariable.Size = new System.Drawing.Size(46, 23);
            this.textBoxDefaultPlottedVariable.TabIndex = 4;
            // 
            // labelDefaultPlottedVariable
            // 
            this.labelDefaultPlottedVariable.AutoSize = true;
            this.labelDefaultPlottedVariable.Location = new System.Drawing.Point(76, 38);
            this.labelDefaultPlottedVariable.Name = "labelDefaultPlottedVariable";
            this.labelDefaultPlottedVariable.Size = new System.Drawing.Size(97, 15);
            this.labelDefaultPlottedVariable.TabIndex = 4;
            this.labelDefaultPlottedVariable.Text = "Default variables:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(179, 6);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(226, 23);
            this.textBoxUserName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.labelDefaultHighlightWidth);
            this.tabPageDisplay.Controls.Add(this.numericDefaultHighlightWidth);
            this.tabPageDisplay.Controls.Add(this.labelDefaultPencilWidth);
            this.tabPageDisplay.Controls.Add(this.numericDefaultPencilWidth);
            this.tabPageDisplay.Controls.Add(this.colorChooserDefaultHighlightColor);
            this.tabPageDisplay.Controls.Add(this.labelDefaultHighlightColor);
            this.tabPageDisplay.Controls.Add(this.colorChooserDefaultPencilColor);
            this.tabPageDisplay.Controls.Add(this.labelDefaultPencilColor);
            this.tabPageDisplay.Controls.Add(this.colorChooserDefaultGraphColor);
            this.tabPageDisplay.Controls.Add(this.labelDefaultFeatureColor);
            this.tabPageDisplay.Controls.Add(this.colorChooserDefaultPageColor);
            this.tabPageDisplay.Controls.Add(this.labelDefaultPageColor);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 24);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(411, 150);
            this.tabPageDisplay.TabIndex = 1;
            this.tabPageDisplay.Text = "Display";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // colorChooserDefaultGraphColor
            // 
            this.colorChooserDefaultGraphColor.Color = System.Drawing.Color.Empty;
            this.colorChooserDefaultGraphColor.Location = new System.Drawing.Point(348, 8);
            this.colorChooserDefaultGraphColor.Name = "colorChooserDefaultGraphColor";
            this.colorChooserDefaultGraphColor.Size = new System.Drawing.Size(57, 17);
            this.colorChooserDefaultGraphColor.TabIndex = 11;
            // 
            // labelDefaultFeatureColor
            // 
            this.labelDefaultFeatureColor.AutoSize = true;
            this.labelDefaultFeatureColor.Location = new System.Drawing.Point(217, 8);
            this.labelDefaultFeatureColor.Name = "labelDefaultFeatureColor";
            this.labelDefaultFeatureColor.Size = new System.Drawing.Size(125, 15);
            this.labelDefaultFeatureColor.TabIndex = 19;
            this.labelDefaultFeatureColor.Text = "Default feature colour:";
            // 
            // colorChooserDefaultPageColor
            // 
            this.colorChooserDefaultPageColor.Color = System.Drawing.Color.Empty;
            this.colorChooserDefaultPageColor.Location = new System.Drawing.Point(146, 8);
            this.colorChooserDefaultPageColor.Name = "colorChooserDefaultPageColor";
            this.colorChooserDefaultPageColor.Size = new System.Drawing.Size(57, 17);
            this.colorChooserDefaultPageColor.TabIndex = 10;
            // 
            // labelDefaultPageColor
            // 
            this.labelDefaultPageColor.AutoSize = true;
            this.labelDefaultPageColor.Location = new System.Drawing.Point(26, 8);
            this.labelDefaultPageColor.Name = "labelDefaultPageColor";
            this.labelDefaultPageColor.Size = new System.Drawing.Size(114, 15);
            this.labelDefaultPageColor.TabIndex = 17;
            this.labelDefaultPageColor.Text = "Default page colour:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(352, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(271, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // colorChooserDefaultHighlightColor
            // 
            this.colorChooserDefaultHighlightColor.Color = System.Drawing.Color.Empty;
            this.colorChooserDefaultHighlightColor.Location = new System.Drawing.Point(348, 31);
            this.colorChooserDefaultHighlightColor.Name = "colorChooserDefaultHighlightColor";
            this.colorChooserDefaultHighlightColor.Size = new System.Drawing.Size(57, 17);
            this.colorChooserDefaultHighlightColor.TabIndex = 14;
            // 
            // labelDefaultHighlightColor
            // 
            this.labelDefaultHighlightColor.AutoSize = true;
            this.labelDefaultHighlightColor.Location = new System.Drawing.Point(206, 33);
            this.labelDefaultHighlightColor.Name = "labelDefaultHighlightColor";
            this.labelDefaultHighlightColor.Size = new System.Drawing.Size(136, 15);
            this.labelDefaultHighlightColor.TabIndex = 23;
            this.labelDefaultHighlightColor.Text = "Default highlight colour:";
            // 
            // colorChooserDefaultPencilColor
            // 
            this.colorChooserDefaultPencilColor.Color = System.Drawing.Color.Empty;
            this.colorChooserDefaultPencilColor.Location = new System.Drawing.Point(146, 31);
            this.colorChooserDefaultPencilColor.Name = "colorChooserDefaultPencilColor";
            this.colorChooserDefaultPencilColor.Size = new System.Drawing.Size(57, 17);
            this.colorChooserDefaultPencilColor.TabIndex = 12;
            // 
            // labelDefaultPencilColor
            // 
            this.labelDefaultPencilColor.AutoSize = true;
            this.labelDefaultPencilColor.Location = new System.Drawing.Point(20, 32);
            this.labelDefaultPencilColor.Name = "labelDefaultPencilColor";
            this.labelDefaultPencilColor.Size = new System.Drawing.Size(120, 15);
            this.labelDefaultPencilColor.TabIndex = 21;
            this.labelDefaultPencilColor.Text = "Default pencil colour:";
            // 
            // numericDefaultPencilWidth
            // 
            this.numericDefaultPencilWidth.Location = new System.Drawing.Point(146, 54);
            this.numericDefaultPencilWidth.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericDefaultPencilWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericDefaultPencilWidth.Name = "numericDefaultPencilWidth";
            this.numericDefaultPencilWidth.Size = new System.Drawing.Size(57, 23);
            this.numericDefaultPencilWidth.TabIndex = 13;
            this.numericDefaultPencilWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // labelDefaultPencilWidth
            // 
            this.labelDefaultPencilWidth.AutoSize = true;
            this.labelDefaultPencilWidth.Location = new System.Drawing.Point(24, 56);
            this.labelDefaultPencilWidth.Name = "labelDefaultPencilWidth";
            this.labelDefaultPencilWidth.Size = new System.Drawing.Size(116, 15);
            this.labelDefaultPencilWidth.TabIndex = 26;
            this.labelDefaultPencilWidth.Text = "Default pencil width:";
            // 
            // labelDefaultHighlightWidth
            // 
            this.labelDefaultHighlightWidth.AutoSize = true;
            this.labelDefaultHighlightWidth.Location = new System.Drawing.Point(210, 56);
            this.labelDefaultHighlightWidth.Name = "labelDefaultHighlightWidth";
            this.labelDefaultHighlightWidth.Size = new System.Drawing.Size(132, 15);
            this.labelDefaultHighlightWidth.TabIndex = 28;
            this.labelDefaultHighlightWidth.Text = "Default highlight width:";
            // 
            // numericDefaultHighlightWidth
            // 
            this.numericDefaultHighlightWidth.Location = new System.Drawing.Point(348, 54);
            this.numericDefaultHighlightWidth.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericDefaultHighlightWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericDefaultHighlightWidth.Name = "numericDefaultHighlightWidth";
            this.numericDefaultHighlightWidth.Size = new System.Drawing.Size(57, 23);
            this.numericDefaultHighlightWidth.TabIndex = 15;
            this.numericDefaultHighlightWidth.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // SettingsEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(443, 212);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Graphmatic.Properties.Resources.SettingsIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Options_FormClosed);
            this.Load += new System.EventHandler(this.Options_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageDefaults.ResumeLayout(false);
            this.tabPageDefaults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackupInterval)).EndInit();
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDefaultPencilWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDefaultHighlightWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDefaults;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelDefaultPlottedVariable;
        private System.Windows.Forms.TextBox textBoxDefaultPlottedVariable;
        private System.Windows.Forms.TextBox textBoxDefaultVaryingVariable;
        private System.Windows.Forms.TextBox textBoxDefaultDataSetVariables;
        private System.Windows.Forms.Label labelDefaultDataSetVariables;
        private System.Windows.Forms.Label labelBackupPath;
        private System.Windows.Forms.TextBox textBoxBackupPath;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.NumericUpDown numericBackupInterval;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private ColorChooser colorChooserDefaultGraphColor;
        private System.Windows.Forms.Label labelDefaultFeatureColor;
        private ColorChooser colorChooserDefaultPageColor;
        private System.Windows.Forms.Label labelDefaultPageColor;
        private System.Windows.Forms.Label labelDefaultPencilWidth;
        private System.Windows.Forms.NumericUpDown numericDefaultPencilWidth;
        private ColorChooser colorChooserDefaultHighlightColor;
        private System.Windows.Forms.Label labelDefaultHighlightColor;
        private ColorChooser colorChooserDefaultPencilColor;
        private System.Windows.Forms.Label labelDefaultPencilColor;
        private System.Windows.Forms.Label labelDefaultHighlightWidth;
        private System.Windows.Forms.NumericUpDown numericDefaultHighlightWidth;
    }
}