namespace Graphmatic
{
    partial class EquationEditor
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
            this.components = new System.ComponentModel.Container();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxPlotted = new System.Windows.Forms.TextBox();
            this.labelPlotted = new System.Windows.Forms.Label();
            this.labelVarying = new System.Windows.Forms.Label();
            this.textBoxVarying = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.expressionDisplay = new Graphmatic.ExpressionDisplay();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(350, 164);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 1;
            this.buttonEdit.Text = "&Edit...";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(350, 193);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "&Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxPlotted
            // 
            this.textBoxPlotted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPlotted.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxPlotted.Location = new System.Drawing.Point(112, 164);
            this.textBoxPlotted.Name = "textBoxPlotted";
            this.textBoxPlotted.ReadOnly = true;
            this.textBoxPlotted.Size = new System.Drawing.Size(27, 23);
            this.textBoxPlotted.TabIndex = 3;
            this.textBoxPlotted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPlotted.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPlotted_KeyPress);
            // 
            // labelPlotted
            // 
            this.labelPlotted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPlotted.AutoSize = true;
            this.labelPlotted.Location = new System.Drawing.Point(14, 167);
            this.labelPlotted.Name = "labelPlotted";
            this.labelPlotted.Size = new System.Drawing.Size(92, 15);
            this.labelPlotted.TabIndex = 4;
            this.labelPlotted.Text = "Plotted variable:";
            // 
            // labelVarying
            // 
            this.labelVarying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVarying.AutoSize = true;
            this.labelVarying.Location = new System.Drawing.Point(12, 196);
            this.labelVarying.Name = "labelVarying";
            this.labelVarying.Size = new System.Drawing.Size(94, 15);
            this.labelVarying.TabIndex = 5;
            this.labelVarying.Text = "Varying variable:";
            // 
            // textBoxVarying
            // 
            this.textBoxVarying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxVarying.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxVarying.Location = new System.Drawing.Point(112, 193);
            this.textBoxVarying.Name = "textBoxVarying";
            this.textBoxVarying.ReadOnly = true;
            this.textBoxVarying.Size = new System.Drawing.Size(27, 23);
            this.textBoxVarying.TabIndex = 6;
            this.textBoxVarying.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxVarying.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxVarying_KeyPress);
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.Location = new System.Drawing.Point(145, 167);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(199, 64);
            this.labelDescription.TabIndex = 7;
            this.labelDescription.Text = "The plotted variable (on the vertical axis) is the variable which is defined in t" +
    "erns of the varying variable (on the horizontal axis.)";
            // 
            // expressionDisplay
            // 
            this.expressionDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expressionDisplay.BackColor = System.Drawing.Color.White;
            this.expressionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expressionDisplay.DisplayScale = 2;
            this.expressionDisplay.Edit = false;
            this.expressionDisplay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expressionDisplay.Location = new System.Drawing.Point(3, 12);
            this.expressionDisplay.MoeinMode = false;
            this.expressionDisplay.Name = "expressionDisplay";
            this.expressionDisplay.Size = new System.Drawing.Size(422, 146);
            this.expressionDisplay.TabIndex = 0;
            // 
            // EquationEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(437, 233);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxVarying);
            this.Controls.Add(this.labelVarying);
            this.Controls.Add(this.labelPlotted);
            this.Controls.Add(this.textBoxPlotted);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.expressionDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Graphmatic.Properties.Resources.Equation;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(453, 271);
            this.Name = "EquationEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Equation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EquationEditor_FormClosing);
            this.Load += new System.EventHandler(this.EquationEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExpressionDisplay expressionDisplay;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxPlotted;
        private System.Windows.Forms.Label labelPlotted;
        private System.Windows.Forms.Label labelVarying;
        private System.Windows.Forms.TextBox textBoxVarying;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.ToolTip toolTip;
    }
}