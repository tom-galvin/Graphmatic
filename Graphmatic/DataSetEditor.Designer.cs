namespace Graphmatic
{
    partial class DataSetEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSetEditor));
            this.buttonOK = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonEditVariables = new System.Windows.Forms.Button();
            this.buttonExcelCopy = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonStats = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.contextMenuStripStatistics = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.meanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squaredSumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productSumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.standarddeviationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varianceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.pMCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRegressionLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.covarianceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStripStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(234, 268);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 24);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridView.Size = new System.Drawing.Size(297, 250);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            // 
            // buttonEditVariables
            // 
            this.buttonEditVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditVariables.Location = new System.Drawing.Point(153, 268);
            this.buttonEditVariables.Name = "buttonEditVariables";
            this.buttonEditVariables.Size = new System.Drawing.Size(75, 24);
            this.buttonEditVariables.TabIndex = 3;
            this.buttonEditVariables.Text = "&Variables...";
            this.buttonEditVariables.UseVisualStyleBackColor = true;
            this.buttonEditVariables.Click += new System.EventHandler(this.buttonEditVariables_Click);
            // 
            // buttonExcelCopy
            // 
            this.buttonExcelCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExcelCopy.Image = global::Graphmatic.Properties.Resources.Excel16;
            this.buttonExcelCopy.Location = new System.Drawing.Point(12, 268);
            this.buttonExcelCopy.Name = "buttonExcelCopy";
            this.buttonExcelCopy.Size = new System.Drawing.Size(136, 24);
            this.buttonExcelCopy.TabIndex = 4;
            this.buttonExcelCopy.Text = "Paste from Excel";
            this.buttonExcelCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonExcelCopy, "Select some data on Microsoft Excel, Copy it, and then click this button.");
            this.buttonExcelCopy.UseVisualStyleBackColor = true;
            this.buttonExcelCopy.Click += new System.EventHandler(this.buttonExcelCopy_Click);
            // 
            // buttonStats
            // 
            this.buttonStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStats.Image = global::Graphmatic.Properties.Resources.Statistics24;
            this.buttonStats.Location = new System.Drawing.Point(280, 320);
            this.buttonStats.Name = "buttonStats";
            this.buttonStats.Size = new System.Drawing.Size(29, 29);
            this.buttonStats.TabIndex = 6;
            this.toolTip.SetToolTip(this.buttonStats, "Perform statistical functions on this data set");
            this.buttonStats.UseVisualStyleBackColor = true;
            this.buttonStats.Click += new System.EventHandler(this.buttonStats_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.Location = new System.Drawing.Point(12, 295);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(262, 57);
            this.labelInfo.TabIndex = 5;
            this.labelInfo.Text = "To remove a row, highlight the rows using the left side of the editor pane and pr" +
    "ess the delete (Del) key.";
            // 
            // contextMenuStripStatistics
            // 
            this.contextMenuStripStatistics.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sumToolStripMenuItem,
            this.squaredSumToolStripMenuItem,
            this.productSumToolStripMenuItem,
            this.toolStripMenuItem1,
            this.meanToolStripMenuItem,
            this.standarddeviationToolStripMenuItem,
            this.varianceToolStripMenuItem,
            this.covarianceToolStripMenuItem,
            this.toolStripMenuItem2,
            this.pMCCToolStripMenuItem,
            this.createRegressionLineToolStripMenuItem});
            this.contextMenuStripStatistics.Name = "contextMenuStripStatistics";
            this.contextMenuStripStatistics.Size = new System.Drawing.Size(200, 236);
            // 
            // meanToolStripMenuItem
            // 
            this.meanToolStripMenuItem.Name = "meanToolStripMenuItem";
            this.meanToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.meanToolStripMenuItem.Text = "&Mean...";
            this.meanToolStripMenuItem.Click += new System.EventHandler(this.meanToolStripMenuItem_Click);
            // 
            // sumToolStripMenuItem
            // 
            this.sumToolStripMenuItem.Name = "sumToolStripMenuItem";
            this.sumToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.sumToolStripMenuItem.Text = "&Sum...";
            this.sumToolStripMenuItem.Click += new System.EventHandler(this.sumToolStripMenuItem_Click);
            // 
            // squaredSumToolStripMenuItem
            // 
            this.squaredSumToolStripMenuItem.Name = "squaredSumToolStripMenuItem";
            this.squaredSumToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.squaredSumToolStripMenuItem.Text = "S&quared sum...";
            this.squaredSumToolStripMenuItem.Click += new System.EventHandler(this.squaredSumToolStripMenuItem_Click);
            // 
            // productSumToolStripMenuItem
            // 
            this.productSumToolStripMenuItem.Name = "productSumToolStripMenuItem";
            this.productSumToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.productSumToolStripMenuItem.Text = "&Product sum...";
            this.productSumToolStripMenuItem.Click += new System.EventHandler(this.productSumToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 6);
            // 
            // standarddeviationToolStripMenuItem
            // 
            this.standarddeviationToolStripMenuItem.Name = "standarddeviationToolStripMenuItem";
            this.standarddeviationToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.standarddeviationToolStripMenuItem.Text = "Standard &deviation...";
            this.standarddeviationToolStripMenuItem.Click += new System.EventHandler(this.standarddeviationToolStripMenuItem_Click);
            // 
            // varianceToolStripMenuItem
            // 
            this.varianceToolStripMenuItem.Name = "varianceToolStripMenuItem";
            this.varianceToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.varianceToolStripMenuItem.Text = "&Variance...";
            this.varianceToolStripMenuItem.Click += new System.EventHandler(this.varianceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // pMCCToolStripMenuItem
            // 
            this.pMCCToolStripMenuItem.Name = "pMCCToolStripMenuItem";
            this.pMCCToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.pMCCToolStripMenuItem.Text = "PM&CC...";
            this.pMCCToolStripMenuItem.Click += new System.EventHandler(this.pMCCToolStripMenuItem_Click);
            // 
            // createRegressionLineToolStripMenuItem
            // 
            this.createRegressionLineToolStripMenuItem.Name = "createRegressionLineToolStripMenuItem";
            this.createRegressionLineToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.createRegressionLineToolStripMenuItem.Text = "Create &Regression line...";
            this.createRegressionLineToolStripMenuItem.Click += new System.EventHandler(this.createRegressionLineToolStripMenuItem_Click);
            // 
            // covarianceToolStripMenuItem
            // 
            this.covarianceToolStripMenuItem.Name = "covarianceToolStripMenuItem";
            this.covarianceToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.covarianceToolStripMenuItem.Text = "C&ovariance...";
            this.covarianceToolStripMenuItem.Click += new System.EventHandler(this.covarianceToolStripMenuItem_Click);
            // 
            // DataSetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 361);
            this.Controls.Add(this.buttonStats);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonExcelCopy);
            this.Controls.Add(this.buttonEditVariables);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataSetEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Set";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStripStatistics.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonEditVariables;
        private System.Windows.Forms.Button buttonExcelCopy;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonStats;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStatistics;
        private System.Windows.Forms.ToolStripMenuItem meanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squaredSumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productSumToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem standarddeviationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem varianceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pMCCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRegressionLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem covarianceToolStripMenuItem;

    }
}