namespace Graphmatic
{
    partial class CreateVariableDialog
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
            this.textBoxVariableName = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxVariableName
            // 
            this.textBoxVariableName.AcceptsReturn = true;
            this.textBoxVariableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVariableName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxVariableName.Location = new System.Drawing.Point(57, 12);
            this.textBoxVariableName.Name = "textBoxVariableName";
            this.textBoxVariableName.ReadOnly = true;
            this.textBoxVariableName.Size = new System.Drawing.Size(55, 23);
            this.textBoxVariableName.TabIndex = 0;
            this.textBoxVariableName.Text = "?";
            this.textBoxVariableName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxVariableName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxVariableName_KeyPress);
            this.textBoxVariableName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxVariableName_KeyUp);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(9, 15);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(42, 15);
            this.label.TabIndex = 1;
            this.label.Text = "Name:";
            // 
            // CreateVariableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 49);
            this.Controls.Add(this.label);
            this.Controls.Add(this.textBoxVariableName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateVariableDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Variable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxVariableName;
        private System.Windows.Forms.Label label;
    }
}