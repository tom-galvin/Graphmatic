namespace Graphmatic
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.dispToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControlLeftPanel = new System.Windows.Forms.TabControl();
            this.tabPagePages = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonIncreasePenSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDecreasePenSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelPenSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButtonDrawAnnotation = new System.Windows.Forms.ToolStripSplitButton();
            this.drawAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButtonEraseAnnotation = new System.Windows.Forms.ToolStripSplitButton();
            this.eraseAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAnnotationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonTextAnnotation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButtonColorAnnotation = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButtonGraph = new System.Windows.Forms.ToolStripSplitButton();
            this.plotEquationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonDataSets = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSquareSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCustomSelect = new System.Windows.Forms.ToolStripButton();
            this.display = new System.Windows.Forms.PictureBox();
            this.lorgorgoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControlLeftPanel.SuspendLayout();
            this.tabPagePages.SuspendLayout();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dispToolStripMenuItem,
            this.lorgorgoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1012, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // dispToolStripMenuItem
            // 
            this.dispToolStripMenuItem.Name = "dispToolStripMenuItem";
            this.dispToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.dispToolStripMenuItem.Text = "Disp";
            this.dispToolStripMenuItem.Click += new System.EventHandler(this.dispToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 514);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1012, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tabControlLeftPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.toolStripContainer);
            this.splitContainer.Size = new System.Drawing.Size(1012, 490);
            this.splitContainer.SplitterDistance = 337;
            this.splitContainer.TabIndex = 2;
            // 
            // tabControlLeftPanel
            // 
            this.tabControlLeftPanel.Controls.Add(this.tabPagePages);
            this.tabControlLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLeftPanel.ImageList = this.imageListTabs;
            this.tabControlLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.tabControlLeftPanel.Name = "tabControlLeftPanel";
            this.tabControlLeftPanel.SelectedIndex = 0;
            this.tabControlLeftPanel.ShowToolTips = true;
            this.tabControlLeftPanel.Size = new System.Drawing.Size(337, 490);
            this.tabControlLeftPanel.TabIndex = 0;
            // 
            // tabPagePages
            // 
            this.tabPagePages.Controls.Add(this.listView1);
            this.tabPagePages.ImageIndex = 0;
            this.tabPagePages.Location = new System.Drawing.Point(4, 24);
            this.tabPagePages.Name = "tabPagePages";
            this.tabPagePages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePages.Size = new System.Drawing.Size(329, 462);
            this.tabPagePages.TabIndex = 0;
            this.tabPagePages.Text = "Pages";
            this.tabPagePages.ToolTipText = "See the pages in the Graphmatic document.";
            this.tabPagePages.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(323, 456);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // imageListTabs
            // 
            this.imageListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabs.ImageStream")));
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabs.Images.SetKeyName(0, "Document16");
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.display);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(671, 434);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(671, 490);
            this.toolStripContainer.TabIndex = 3;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStrip
            // 
            this.toolStrip.AllowItemReorder = true;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonIncreasePenSize,
            this.toolStripButtonDecreasePenSize,
            this.toolStripLabelPenSize,
            this.toolStripSeparator1,
            this.toolStripSplitButtonDrawAnnotation,
            this.toolStripSplitButtonEraseAnnotation,
            this.toolStripButtonTextAnnotation,
            this.toolStripSplitButtonColorAnnotation,
            this.toolStripSeparator5,
            this.toolStripSplitButtonGraph,
            this.toolStripButtonDataSets,
            this.toolStripSeparator2,
            this.toolStripButtonPreviousPage,
            this.toolStripButtonNextPage,
            this.toolStripButtonAddPage,
            this.toolStripSeparator3,
            this.toolStripButtonUndo,
            this.toolStripButtonRedo,
            this.toolStripSeparator4,
            this.toolStripButtonSquareSelect,
            this.toolStripButtonCustomSelect});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(3, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(490, 31);
            this.toolStrip.TabIndex = 0;
            // 
            // toolStripButtonIncreasePenSize
            // 
            this.toolStripButtonIncreasePenSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonIncreasePenSize.Image = global::Graphmatic.Properties.Resources.IncreaseSize16;
            this.toolStripButtonIncreasePenSize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonIncreasePenSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonIncreasePenSize.Name = "toolStripButtonIncreasePenSize";
            this.toolStripButtonIncreasePenSize.Size = new System.Drawing.Size(23, 28);
            this.toolStripButtonIncreasePenSize.ToolTipText = "Increase Size";
            // 
            // toolStripButtonDecreasePenSize
            // 
            this.toolStripButtonDecreasePenSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDecreasePenSize.Image = global::Graphmatic.Properties.Resources.DecreaseSize16;
            this.toolStripButtonDecreasePenSize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDecreasePenSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDecreasePenSize.Name = "toolStripButtonDecreasePenSize";
            this.toolStripButtonDecreasePenSize.Size = new System.Drawing.Size(23, 28);
            this.toolStripButtonDecreasePenSize.ToolTipText = "Decrease Size";
            // 
            // toolStripLabelPenSize
            // 
            this.toolStripLabelPenSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabelPenSize.Name = "toolStripLabelPenSize";
            this.toolStripLabelPenSize.Size = new System.Drawing.Size(0, 28);
            this.toolStripLabelPenSize.Text = "Pen Size:";
            this.toolStripLabelPenSize.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSplitButtonDrawAnnotation
            // 
            this.toolStripSplitButtonDrawAnnotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonDrawAnnotation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawAnnotationToolStripMenuItem,
            this.highlightAnnotationToolStripMenuItem});
            this.toolStripSplitButtonDrawAnnotation.Image = global::Graphmatic.Properties.Resources.AnnotateDraw24;
            this.toolStripSplitButtonDrawAnnotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonDrawAnnotation.Name = "toolStripSplitButtonDrawAnnotation";
            this.toolStripSplitButtonDrawAnnotation.Size = new System.Drawing.Size(40, 28);
            this.toolStripSplitButtonDrawAnnotation.Text = "toolStripSplitButton1";
            this.toolStripSplitButtonDrawAnnotation.ToolTipText = "Draw annotation";
            // 
            // drawAnnotationToolStripMenuItem
            // 
            this.drawAnnotationToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateDraw16;
            this.drawAnnotationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.drawAnnotationToolStripMenuItem.Name = "drawAnnotationToolStripMenuItem";
            this.drawAnnotationToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.drawAnnotationToolStripMenuItem.Text = "Draw";
            this.drawAnnotationToolStripMenuItem.ToolTipText = "Draw annotation";
            // 
            // highlightAnnotationToolStripMenuItem
            // 
            this.highlightAnnotationToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotationHighlight16;
            this.highlightAnnotationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.highlightAnnotationToolStripMenuItem.Name = "highlightAnnotationToolStripMenuItem";
            this.highlightAnnotationToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.highlightAnnotationToolStripMenuItem.Text = "Highlight";
            this.highlightAnnotationToolStripMenuItem.ToolTipText = "Highlight annotation";
            // 
            // toolStripSplitButtonEraseAnnotation
            // 
            this.toolStripSplitButtonEraseAnnotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonEraseAnnotation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eraseAnnotationToolStripMenuItem,
            this.clearAnnotationsToolStripMenuItem});
            this.toolStripSplitButtonEraseAnnotation.Image = global::Graphmatic.Properties.Resources.AnnotateErase24;
            this.toolStripSplitButtonEraseAnnotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonEraseAnnotation.Name = "toolStripSplitButtonEraseAnnotation";
            this.toolStripSplitButtonEraseAnnotation.Size = new System.Drawing.Size(40, 28);
            this.toolStripSplitButtonEraseAnnotation.Text = "toolStripSplitButton2";
            this.toolStripSplitButtonEraseAnnotation.ToolTipText = "Erase annotations";
            // 
            // eraseAnnotationToolStripMenuItem
            // 
            this.eraseAnnotationToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateErase16;
            this.eraseAnnotationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eraseAnnotationToolStripMenuItem.Name = "eraseAnnotationToolStripMenuItem";
            this.eraseAnnotationToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.eraseAnnotationToolStripMenuItem.Text = "Erase";
            this.eraseAnnotationToolStripMenuItem.ToolTipText = "Erase annotations";
            // 
            // clearAnnotationsToolStripMenuItem
            // 
            this.clearAnnotationsToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateEraseAll16;
            this.clearAnnotationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearAnnotationsToolStripMenuItem.Name = "clearAnnotationsToolStripMenuItem";
            this.clearAnnotationsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.clearAnnotationsToolStripMenuItem.Text = "Clear Annotations";
            this.clearAnnotationsToolStripMenuItem.ToolTipText = "Clear all annotations on this page";
            // 
            // toolStripButtonTextAnnotation
            // 
            this.toolStripButtonTextAnnotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTextAnnotation.Image = global::Graphmatic.Properties.Resources.AnnotateText24;
            this.toolStripButtonTextAnnotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTextAnnotation.Name = "toolStripButtonTextAnnotation";
            this.toolStripButtonTextAnnotation.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonTextAnnotation.ToolTipText = "Add text";
            // 
            // toolStripSplitButtonColorAnnotation
            // 
            this.toolStripSplitButtonColorAnnotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonColorAnnotation.Image = global::Graphmatic.Properties.Resources.Color24;
            this.toolStripSplitButtonColorAnnotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonColorAnnotation.Name = "toolStripSplitButtonColorAnnotation";
            this.toolStripSplitButtonColorAnnotation.Size = new System.Drawing.Size(40, 28);
            this.toolStripSplitButtonColorAnnotation.Text = "toolStripSplitButton1";
            this.toolStripSplitButtonColorAnnotation.ToolTipText = "Change annotation color";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSplitButtonGraph
            // 
            this.toolStripSplitButtonGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonGraph.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotEquationToolStripMenuItem,
            this.plotDataToolStripMenuItem});
            this.toolStripSplitButtonGraph.Image = global::Graphmatic.Properties.Resources.Chart24;
            this.toolStripSplitButtonGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonGraph.Name = "toolStripSplitButtonGraph";
            this.toolStripSplitButtonGraph.Size = new System.Drawing.Size(40, 28);
            this.toolStripSplitButtonGraph.Text = "toolStripSplitButton1";
            this.toolStripSplitButtonGraph.ToolTipText = "Add an empty graph";
            // 
            // plotEquationToolStripMenuItem
            // 
            this.plotEquationToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Graph24;
            this.plotEquationToolStripMenuItem.Name = "plotEquationToolStripMenuItem";
            this.plotEquationToolStripMenuItem.Size = new System.Drawing.Size(153, 30);
            this.plotEquationToolStripMenuItem.Text = "Plot Equation";
            this.plotEquationToolStripMenuItem.ToolTipText = "Plot an equation on a graph";
            // 
            // plotDataToolStripMenuItem
            // 
            this.plotDataToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Data24;
            this.plotDataToolStripMenuItem.Name = "plotDataToolStripMenuItem";
            this.plotDataToolStripMenuItem.Size = new System.Drawing.Size(153, 30);
            this.plotDataToolStripMenuItem.Text = "Plot Data";
            this.plotDataToolStripMenuItem.ToolTipText = "Plot a data set on a graph";
            // 
            // toolStripButtonDataSets
            // 
            this.toolStripButtonDataSets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDataSets.Image = global::Graphmatic.Properties.Resources.Statistics24;
            this.toolStripButtonDataSets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDataSets.Name = "toolStripButtonDataSets";
            this.toolStripButtonDataSets.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonDataSets.Text = "toolStripButton1";
            this.toolStripButtonDataSets.ToolTipText = "Edit data sets";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonPreviousPage
            // 
            this.toolStripButtonPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreviousPage.Image = global::Graphmatic.Properties.Resources.ArrowLeft24;
            this.toolStripButtonPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviousPage.Name = "toolStripButtonPreviousPage";
            this.toolStripButtonPreviousPage.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPreviousPage.Text = "toolStripButton1";
            this.toolStripButtonPreviousPage.ToolTipText = "Go to the previous page";
            // 
            // toolStripButtonNextPage
            // 
            this.toolStripButtonNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextPage.Image = global::Graphmatic.Properties.Resources.ArrowRight24;
            this.toolStripButtonNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
            this.toolStripButtonNextPage.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonNextPage.Text = "toolStripButton1";
            this.toolStripButtonNextPage.ToolTipText = "Go to the next page";
            // 
            // toolStripButtonAddPage
            // 
            this.toolStripButtonAddPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddPage.Image = global::Graphmatic.Properties.Resources.Add24;
            this.toolStripButtonAddPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPage.Name = "toolStripButtonAddPage";
            this.toolStripButtonAddPage.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAddPage.Text = "toolStripButton1";
            this.toolStripButtonAddPage.ToolTipText = "Add a page after this one";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Image = global::Graphmatic.Properties.Resources.Undo16;
            this.toolStripButtonUndo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(23, 28);
            this.toolStripButtonUndo.Text = "toolStripButton1";
            this.toolStripButtonUndo.ToolTipText = "Undo the last change made";
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image = global::Graphmatic.Properties.Resources.Redo16;
            this.toolStripButtonRedo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new System.Drawing.Size(23, 28);
            this.toolStripButtonRedo.Text = "toolStripButton1";
            this.toolStripButtonRedo.ToolTipText = "Redo the last undone change";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonSquareSelect
            // 
            this.toolStripButtonSquareSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSquareSelect.Image = global::Graphmatic.Properties.Resources.SquareSelect24;
            this.toolStripButtonSquareSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSquareSelect.Name = "toolStripButtonSquareSelect";
            this.toolStripButtonSquareSelect.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSquareSelect.Text = "toolStripButton1";
            this.toolStripButtonSquareSelect.ToolTipText = "Select objects with a rectangle";
            // 
            // toolStripButtonCustomSelect
            // 
            this.toolStripButtonCustomSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCustomSelect.Image = global::Graphmatic.Properties.Resources.CustomSelect24;
            this.toolStripButtonCustomSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCustomSelect.Name = "toolStripButtonCustomSelect";
            this.toolStripButtonCustomSelect.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonCustomSelect.Text = "toolStripButton1";
            this.toolStripButtonCustomSelect.ToolTipText = "Select objects with a free-form shape";
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(671, 434);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // lorgorgoToolStripMenuItem
            // 
            this.lorgorgoToolStripMenuItem.Name = "lorgorgoToolStripMenuItem";
            this.lorgorgoToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.lorgorgoToolStripMenuItem.Text = "TestDlg";
            this.lorgorgoToolStripMenuItem.Click += new System.EventHandler(this.lorgorgoToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 536);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Graphmatic.Properties.Resources.Graphmatic;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Graphmatic";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControlLeftPanel.ResumeLayout(false);
            this.tabPagePages.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem dispToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonIncreasePenSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonDecreasePenSize;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPenSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTextAnnotation;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonDrawAnnotation;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonEraseAnnotation;
        private System.Windows.Forms.ToolStripMenuItem drawAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eraseAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAnnotationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonColorAnnotation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonGraph;
        private System.Windows.Forms.ToolStripMenuItem plotEquationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonDataSets;
        private System.Windows.Forms.ToolStripButton toolStripButtonSquareSelect;
        private System.Windows.Forms.ToolStripButton toolStripButtonCustomSelect;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.PictureBox display;
        private System.Windows.Forms.TabControl tabControlLeftPanel;
        private System.Windows.Forms.TabPage tabPagePages;
        private System.Windows.Forms.ImageList imageListTabs;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem lorgorgoToolStripMenuItem;
    }
}

