namespace Graphmatic.Interaction
{
    partial class TestPlotter
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
            this.checkBoxLerp = new System.Windows.Forms.CheckBox();
            this.trackBarResolution = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxLerp
            // 
            this.checkBoxLerp.AutoSize = true;
            this.checkBoxLerp.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxLerp.Checked = true;
            this.checkBoxLerp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLerp.Location = new System.Drawing.Point(12, 12);
            this.checkBoxLerp.Name = "checkBoxLerp";
            this.checkBoxLerp.Size = new System.Drawing.Size(49, 19);
            this.checkBoxLerp.TabIndex = 0;
            this.checkBoxLerp.Text = "Lerp";
            this.checkBoxLerp.UseVisualStyleBackColor = false;
            this.checkBoxLerp.CheckedChanged += new System.EventHandler(this.checkBoxLerp_CheckedChanged);
            // 
            // trackBarResolution
            // 
            this.trackBarResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarResolution.Location = new System.Drawing.Point(277, 12);
            this.trackBarResolution.Maximum = 32;
            this.trackBarResolution.Minimum = 3;
            this.trackBarResolution.Name = "trackBarResolution";
            this.trackBarResolution.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarResolution.Size = new System.Drawing.Size(45, 287);
            this.trackBarResolution.TabIndex = 1;
            this.trackBarResolution.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarResolution.Value = 5;
            this.trackBarResolution.Scroll += new System.EventHandler(this.trackBarResolution_Scroll);
            // 
            // TestPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.trackBarResolution);
            this.Controls.Add(this.checkBoxLerp);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TestPlotter";
            this.ShowIcon = false;
            this.Text = "TestPlotter";
            this.Load += new System.EventHandler(this.TestPlotter_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestPlotter_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxLerp;
        private System.Windows.Forms.TrackBar trackBarResolution;
    }
}