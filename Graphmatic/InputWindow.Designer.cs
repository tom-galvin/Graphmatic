namespace Graphmatic
{
    partial class InputWindow
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
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRoot = new System.Windows.Forms.Button();
            this.buttonSqrt = new System.Windows.Forms.Button();
            this.expressionDisplay = new Graphmatic.ExpressionDisplay();
            this.buttonLn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRight
            // 
            this.buttonRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRight.Image = global::Graphmatic.Properties.Resources.RightArrow;
            this.buttonRight.Location = new System.Drawing.Point(606, 209);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(39, 39);
            this.buttonRight.TabIndex = 1;
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLeft.Image = global::Graphmatic.Properties.Resources.LeftArrow;
            this.buttonLeft.Location = new System.Drawing.Point(561, 209);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(39, 39);
            this.buttonLeft.TabIndex = 2;
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Image = global::Graphmatic.Properties.Resources.Delete;
            this.buttonDelete.Location = new System.Drawing.Point(516, 209);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(39, 39);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRoot
            // 
            this.buttonRoot.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRoot.Location = new System.Drawing.Point(12, 209);
            this.buttonRoot.Name = "buttonRoot";
            this.buttonRoot.Size = new System.Drawing.Size(84, 39);
            this.buttonRoot.TabIndex = 4;
            this.buttonRoot.Text = "ROOT";
            this.buttonRoot.UseVisualStyleBackColor = true;
            // 
            // buttonSqrt
            // 
            this.buttonSqrt.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSqrt.Location = new System.Drawing.Point(102, 209);
            this.buttonSqrt.Name = "buttonSqrt";
            this.buttonSqrt.Size = new System.Drawing.Size(84, 39);
            this.buttonSqrt.TabIndex = 5;
            this.buttonSqrt.Text = "SQRT";
            this.buttonSqrt.UseVisualStyleBackColor = true;
            // 
            // expressionDisplay
            // 
            this.expressionDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expressionDisplay.BackColor = System.Drawing.Color.White;
            this.expressionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expressionDisplay.DisplayScale = 4;
            this.expressionDisplay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expressionDisplay.Location = new System.Drawing.Point(12, 12);
            this.expressionDisplay.Name = "expressionDisplay";
            this.expressionDisplay.Size = new System.Drawing.Size(633, 191);
            this.expressionDisplay.TabIndex = 0;
            // 
            // buttonLn
            // 
            this.buttonLn.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLn.Location = new System.Drawing.Point(192, 209);
            this.buttonLn.Name = "buttonLn";
            this.buttonLn.Size = new System.Drawing.Size(84, 39);
            this.buttonLn.TabIndex = 6;
            this.buttonLn.Text = "LN";
            this.buttonLn.UseVisualStyleBackColor = true;
            // 
            // InputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 378);
            this.Controls.Add(this.buttonLn);
            this.Controls.Add(this.buttonSqrt);
            this.Controls.Add(this.buttonRoot);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.expressionDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Graphmatic.Properties.Resources.Equation;
            this.MinimumSize = new System.Drawing.Size(673, 254);
            this.Name = "InputWindow";
            this.Text = "Enter Input";
            this.Load += new System.EventHandler(this.InputWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ExpressionDisplay expressionDisplay;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRoot;
        private System.Windows.Forms.Button buttonSqrt;
        private System.Windows.Forms.Button buttonLn;
    }
}