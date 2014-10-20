namespace Graphmatic
{
    partial class ColorChooser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPicker = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPicker
            // 
            this.buttonPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPicker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPicker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPicker.Location = new System.Drawing.Point(0, 0);
            this.buttonPicker.Name = "buttonPicker";
            this.buttonPicker.Size = new System.Drawing.Size(81, 23);
            this.buttonPicker.TabIndex = 0;
            this.buttonPicker.UseVisualStyleBackColor = true;
            this.buttonPicker.Click += new System.EventHandler(this.buttonPicker_Click);
            // 
            // ColorChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonPicker);
            this.Name = "ColorChooser";
            this.Size = new System.Drawing.Size(81, 23);
            this.Load += new System.EventHandler(this.ColorChooser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPicker;
    }
}
