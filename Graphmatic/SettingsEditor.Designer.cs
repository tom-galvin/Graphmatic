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
            this.textBoxDefaultDataSetVariables = new System.Windows.Forms.TextBox();
            this.labelDefaultDataSetVariables = new System.Windows.Forms.Label();
            this.textBoxDefaultVaryingVariable = new System.Windows.Forms.TextBox();
            this.labelDefaultVaryingVariable = new System.Windows.Forms.Label();
            this.textBoxDefaultPlottedVariable = new System.Windows.Forms.TextBox();
            this.labelDefaultPlottedVariable = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxBackupPath = new System.Windows.Forms.TextBox();
            this.labelBackupPath = new System.Windows.Forms.Label();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.numericBackupInterval = new System.Windows.Forms.NumericUpDown();
            this.colorChooserDefaultPageColor = new Graphmatic.ColorChooser();
            this.tabControl.SuspendLayout();
            this.tabPageDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBackupInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageDefaults);
            this.tabControl.Location = new System.Drawing.Point(12, 22);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(419, 170);
            this.tabControl.TabIndex = 2;
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
            this.tabPageDefaults.Controls.Add(this.labelDefaultVaryingVariable);
            this.tabPageDefaults.Controls.Add(this.textBoxDefaultPlottedVariable);
            this.tabPageDefaults.Controls.Add(this.labelDefaultPlottedVariable);
            this.tabPageDefaults.Controls.Add(this.textBoxUserName);
            this.tabPageDefaults.Controls.Add(this.label3);
            this.tabPageDefaults.Controls.Add(this.colorChooserDefaultPageColor);
            this.tabPageDefaults.Controls.Add(this.label2);
            this.tabPageDefaults.Location = new System.Drawing.Point(4, 24);
            this.tabPageDefaults.Name = "tabPageDefaults";
            this.tabPageDefaults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefaults.Size = new System.Drawing.Size(411, 142);
            this.tabPageDefaults.TabIndex = 0;
            this.tabPageDefaults.Text = "Defaults";
            this.tabPageDefaults.UseVisualStyleBackColor = true;
            // 
            // textBoxDefaultDataSetVariables
            // 
            this.textBoxDefaultDataSetVariables.Location = new System.Drawing.Point(153, 59);
            this.textBoxDefaultDataSetVariables.Name = "textBoxDefaultDataSetVariables";
            this.textBoxDefaultDataSetVariables.Size = new System.Drawing.Size(252, 23);
            this.textBoxDefaultDataSetVariables.TabIndex = 9;
            // 
            // labelDefaultDataSetVariables
            // 
            this.labelDefaultDataSetVariables.AutoSize = true;
            this.labelDefaultDataSetVariables.Location = new System.Drawing.Point(6, 62);
            this.labelDefaultDataSetVariables.Name = "labelDefaultDataSetVariables";
            this.labelDefaultDataSetVariables.Size = new System.Drawing.Size(141, 15);
            this.labelDefaultDataSetVariables.TabIndex = 8;
            this.labelDefaultDataSetVariables.Text = "Default data set variables:";
            // 
            // textBoxDefaultVaryingVariable
            // 
            this.textBoxDefaultVaryingVariable.Location = new System.Drawing.Point(359, 30);
            this.textBoxDefaultVaryingVariable.Name = "textBoxDefaultVaryingVariable";
            this.textBoxDefaultVaryingVariable.Size = new System.Drawing.Size(46, 23);
            this.textBoxDefaultVaryingVariable.TabIndex = 7;
            // 
            // labelDefaultVaryingVariable
            // 
            this.labelDefaultVaryingVariable.AutoSize = true;
            this.labelDefaultVaryingVariable.Location = new System.Drawing.Point(205, 33);
            this.labelDefaultVaryingVariable.Name = "labelDefaultVaryingVariable";
            this.labelDefaultVaryingVariable.Size = new System.Drawing.Size(148, 15);
            this.labelDefaultVaryingVariable.TabIndex = 6;
            this.labelDefaultVaryingVariable.Text = "Default horizontal variable:";
            // 
            // textBoxDefaultPlottedVariable
            // 
            this.textBoxDefaultPlottedVariable.Location = new System.Drawing.Point(359, 6);
            this.textBoxDefaultPlottedVariable.Name = "textBoxDefaultPlottedVariable";
            this.textBoxDefaultPlottedVariable.Size = new System.Drawing.Size(46, 23);
            this.textBoxDefaultPlottedVariable.TabIndex = 5;
            // 
            // labelDefaultPlottedVariable
            // 
            this.labelDefaultPlottedVariable.AutoSize = true;
            this.labelDefaultPlottedVariable.Location = new System.Drawing.Point(220, 9);
            this.labelDefaultPlottedVariable.Name = "labelDefaultPlottedVariable";
            this.labelDefaultPlottedVariable.Size = new System.Drawing.Size(133, 15);
            this.labelDefaultPlottedVariable.TabIndex = 4;
            this.labelDefaultPlottedVariable.Text = "Default vertical variable:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(74, 30);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(125, 23);
            this.textBoxUserName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Default page colour:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(352, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
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
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxBackupPath
            // 
            this.textBoxBackupPath.Location = new System.Drawing.Point(153, 88);
            this.textBoxBackupPath.Name = "textBoxBackupPath";
            this.textBoxBackupPath.Size = new System.Drawing.Size(252, 23);
            this.textBoxBackupPath.TabIndex = 10;
            // 
            // labelBackupPath
            // 
            this.labelBackupPath.AutoSize = true;
            this.labelBackupPath.Location = new System.Drawing.Point(71, 91);
            this.labelBackupPath.Name = "labelBackupPath";
            this.labelBackupPath.Size = new System.Drawing.Size(76, 15);
            this.labelBackupPath.TabIndex = 11;
            this.labelBackupPath.Text = "Backup path:";
            // 
            // checkBoxBackup
            // 
            this.checkBoxBackup.AutoSize = true;
            this.checkBoxBackup.Location = new System.Drawing.Point(9, 118);
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.Size = new System.Drawing.Size(164, 19);
            this.checkBoxBackup.TabIndex = 12;
            this.checkBoxBackup.Text = "Backup interval (seconds):";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            // 
            // numericBackupInterval
            // 
            this.numericBackupInterval.Location = new System.Drawing.Point(179, 117);
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
            this.numericBackupInterval.TabIndex = 14;
            this.numericBackupInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // colorChooserDefaultPageColor
            // 
            this.colorChooserDefaultPageColor.Color = System.Drawing.Color.Empty;
            this.colorChooserDefaultPageColor.Location = new System.Drawing.Point(126, 9);
            this.colorChooserDefaultPageColor.Name = "colorChooserDefaultPageColor";
            this.colorChooserDefaultPageColor.Size = new System.Drawing.Size(73, 15);
            this.colorChooserDefaultPageColor.TabIndex = 1;
            // 
            // SettingsEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(443, 204);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDefaults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private ColorChooser colorChooserDefaultPageColor;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelDefaultPlottedVariable;
        private System.Windows.Forms.TextBox textBoxDefaultPlottedVariable;
        private System.Windows.Forms.TextBox textBoxDefaultVaryingVariable;
        private System.Windows.Forms.Label labelDefaultVaryingVariable;
        private System.Windows.Forms.TextBox textBoxDefaultDataSetVariables;
        private System.Windows.Forms.Label labelDefaultDataSetVariables;
        private System.Windows.Forms.Label labelBackupPath;
        private System.Windows.Forms.TextBox textBoxBackupPath;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.NumericUpDown numericBackupInterval;
    }
}