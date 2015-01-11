using System.Windows.Forms;
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.pageOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBugReport = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelEditor = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewResources = new System.Windows.Forms.ListView();
            this.columnHeaderIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripResources = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListResources = new System.Windows.Forms.ImageList(this.components);
            this.toolStripResources = new System.Windows.Forms.ToolStrip();
            this.toolStripTogglePages = new System.Windows.Forms.ToolStripButton();
            this.toolStripToggleEquations = new System.Windows.Forms.ToolStripButton();
            this.toolStripToggleDataSets = new System.Windows.Forms.ToolStripButton();
            this.toolStripTogglePictures = new System.Windows.Forms.ToolStripButton();
            this.toolStripToggleHtmlPages = new System.Windows.Forms.ToolStripButton();
            this.toolStripToggleHidden = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonAdd = new System.Windows.Forms.ToolStripDropDownButton();
            this.equationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonPageOrder = new System.Windows.Forms.ToolStripButton();
            this.panelHtmlViewer = new System.Windows.Forms.Panel();
            this.toolStripContainerHtmlViewer = new System.Windows.Forms.ToolStripContainer();
            this.webBrowserHtmlViewer = new System.Windows.Forms.WebBrowser();
            this.toolStripHtmlViewer = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonForward = new System.Windows.Forms.ToolStripButton();
            this.panelImageViewer = new System.Windows.Forms.Panel();
            this.toolStripContainerImageViewer = new System.Windows.Forms.ToolStripContainer();
            this.pictureBoxImageViewer = new System.Windows.Forms.PictureBox();
            this.toolStripImageViewer = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonView = new System.Windows.Forms.ToolStripDropDownButton();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPageEditor = new System.Windows.Forms.Panel();
            this.toolStripContainerPageEditor = new System.Windows.Forms.ToolStripContainer();
            this.toolStripPageEditor = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonIncreasePenSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDecreasePenSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelPenSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonErase = new System.Windows.Forms.ToolStripDropDownButton();
            this.pencilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonDraw = new System.Windows.Forms.ToolStripDropDownButton();
            this.eraserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eraseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonTextAnnotation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButtonColorAnnotation = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownEditGraph = new System.Windows.Forms.ToolStripDropDownButton();
            this.contextMenuStripPageEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripButtonPlotDataSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlotEquation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSquareSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxZoom = new System.Windows.Forms.ToolStripComboBox();
            this.pageDisplay = new System.Windows.Forms.PictureBox();
            this.timerBackup = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStripResources.SuspendLayout();
            this.toolStripResources.SuspendLayout();
            this.panelHtmlViewer.SuspendLayout();
            this.toolStripContainerHtmlViewer.ContentPanel.SuspendLayout();
            this.toolStripContainerHtmlViewer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerHtmlViewer.SuspendLayout();
            this.toolStripHtmlViewer.SuspendLayout();
            this.panelImageViewer.SuspendLayout();
            this.toolStripContainerImageViewer.ContentPanel.SuspendLayout();
            this.toolStripContainerImageViewer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerImageViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageViewer)).BeginInit();
            this.toolStripImageViewer.SuspendLayout();
            this.panelPageEditor.SuspendLayout();
            this.toolStripContainerPageEditor.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainerPageEditor.ContentPanel.SuspendLayout();
            this.toolStripContainerPageEditor.SuspendLayout();
            this.toolStripPageEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1070, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveasToolStripMenuItem,
            this.toolStripMenuItem5,
            this.pageOrderToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.ToolTipText = "Create a new document";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.FolderOpen16;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.ToolTipText = "Open an existing document";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.DiskReturn16;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.ToolTipText = "Save the current document";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveasToolStripMenuItem
            // 
            this.saveasToolStripMenuItem.Name = "saveasToolStripMenuItem";
            this.saveasToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveasToolStripMenuItem.Text = "Save &As...";
            this.saveasToolStripMenuItem.ToolTipText = "Save the current document to a specified location";
            this.saveasToolStripMenuItem.Click += new System.EventHandler(this.saveasToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 6);
            // 
            // pageOrderToolStripMenuItem
            // 
            this.pageOrderToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Documents16;
            this.pageOrderToolStripMenuItem.Name = "pageOrderToolStripMenuItem";
            this.pageOrderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pageOrderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.pageOrderToolStripMenuItem.Text = "&Page Order...";
            this.pageOrderToolStripMenuItem.Click += new System.EventHandler(this.pageOrderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Exit16;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.ToolTipText = "Exit the program";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resourcesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // resourcesToolStripMenuItem
            // 
            this.resourcesToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Resources16;
            this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
            this.resourcesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.resourcesToolStripMenuItem.Text = "&Resources";
            this.resourcesToolStripMenuItem.Click += new System.EventHandler(this.resourcesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Settings16;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "&Settings...";
            this.optionsToolStripMenuItem.ToolTipText = "Edit settings relating to the functioning of the program";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabelBugReport,
            this.toolStripStatusLabelEditor});
            this.statusStrip.Location = new System.Drawing.Point(0, 585);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1070, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(69, 17);
            this.toolStripStatusLabel.Text = "Graphmatic";
            // 
            // toolStripStatusLabelBugReport
            // 
            this.toolStripStatusLabelBugReport.IsLink = true;
            this.toolStripStatusLabelBugReport.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.toolStripStatusLabelBugReport.Name = "toolStripStatusLabelBugReport";
            this.toolStripStatusLabelBugReport.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabelBugReport.Text = "Report bugs here";
            this.toolStripStatusLabelBugReport.Click += new System.EventHandler(this.toolStripStatusLabelBugReport_Click);
            // 
            // toolStripStatusLabelEditor
            // 
            this.toolStripStatusLabelEditor.Name = "toolStripStatusLabelEditor";
            this.toolStripStatusLabelEditor.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabelEditor.Text = "Default";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listViewResources);
            this.splitContainer.Panel1.Controls.Add(this.toolStripResources);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelHtmlViewer);
            this.splitContainer.Panel2.Controls.Add(this.panelImageViewer);
            this.splitContainer.Panel2.Controls.Add(this.panelPageEditor);
            this.splitContainer.Size = new System.Drawing.Size(1070, 561);
            this.splitContainer.SplitterDistance = 356;
            this.splitContainer.TabIndex = 2;
            // 
            // listViewResources
            // 
            this.listViewResources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIcon,
            this.columnHeaderName,
            this.columnHeaderAuthor});
            this.listViewResources.ContextMenuStrip = this.contextMenuStripResources;
            this.listViewResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewResources.FullRowSelect = true;
            this.listViewResources.GridLines = true;
            this.listViewResources.Location = new System.Drawing.Point(0, 25);
            this.listViewResources.MultiSelect = false;
            this.listViewResources.Name = "listViewResources";
            this.listViewResources.Size = new System.Drawing.Size(356, 536);
            this.listViewResources.SmallImageList = this.imageListResources;
            this.listViewResources.TabIndex = 1;
            this.listViewResources.UseCompatibleStateImageBehavior = false;
            this.listViewResources.View = System.Windows.Forms.View.Details;
            this.listViewResources.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewResources_ItemDrag);
            this.listViewResources.SelectedIndexChanged += new System.EventHandler(this.listViewResources_SelectedIndexChanged);
            this.listViewResources.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewResources_MouseDoubleClick);
            this.listViewResources.Resize += new System.EventHandler(this.listViewResources_Resize);
            // 
            // columnHeaderIcon
            // 
            this.columnHeaderIcon.Text = "";
            this.columnHeaderIcon.Width = 36;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderAuthor
            // 
            this.columnHeaderAuthor.Text = "Author";
            this.columnHeaderAuthor.Width = 100;
            // 
            // contextMenuStripResources
            // 
            this.contextMenuStripResources.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.toolStripMenuItem4,
            this.removeToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripMenuItem3,
            this.propertiesToolStripMenuItem});
            this.contextMenuStripResources.Name = "contextMenuStripResources";
            this.contextMenuStripResources.Size = new System.Drawing.Size(142, 104);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateDraw16;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editToolStripMenuItem.Text = "&Edit...";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(138, 6);
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.Document16Remove;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.ToolTipText = "Remove the resource from the document";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.renameToolStripMenuItem.Text = "Re&name";
            this.renameToolStripMenuItem.ToolTipText = "Rename the resource";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(138, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("propertiesToolStripMenuItem.Image")));
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties...";
            this.propertiesToolStripMenuItem.ToolTipText = "See properties of the resource";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // imageListResources
            // 
            this.imageListResources.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListResources.ImageStream")));
            this.imageListResources.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListResources.Images.SetKeyName(0, "Page");
            this.imageListResources.Images.SetKeyName(1, "Equation");
            this.imageListResources.Images.SetKeyName(2, "Table");
            this.imageListResources.Images.SetKeyName(3, "Data");
            this.imageListResources.Images.SetKeyName(4, "Picture");
            this.imageListResources.Images.SetKeyName(5, "HtmlPage");
            // 
            // toolStripResources
            // 
            this.toolStripResources.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripResources.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTogglePages,
            this.toolStripToggleEquations,
            this.toolStripToggleDataSets,
            this.toolStripTogglePictures,
            this.toolStripToggleHtmlPages,
            this.toolStripToggleHidden,
            this.toolStripSeparator6,
            this.toolStripDropDownButtonAdd,
            this.toolStripButtonPageOrder});
            this.toolStripResources.Location = new System.Drawing.Point(0, 0);
            this.toolStripResources.Name = "toolStripResources";
            this.toolStripResources.Size = new System.Drawing.Size(356, 25);
            this.toolStripResources.TabIndex = 0;
            this.toolStripResources.Text = "toolStrip1";
            // 
            // toolStripTogglePages
            // 
            this.toolStripTogglePages.Checked = true;
            this.toolStripTogglePages.CheckOnClick = true;
            this.toolStripTogglePages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripTogglePages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTogglePages.Image = global::Graphmatic.Properties.Resources.Documents16;
            this.toolStripTogglePages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTogglePages.Name = "toolStripTogglePages";
            this.toolStripTogglePages.Size = new System.Drawing.Size(23, 22);
            this.toolStripTogglePages.ToolTipText = "Toggle Pages";
            this.toolStripTogglePages.Click += new System.EventHandler(this.toolStripTogglePages_Click);
            // 
            // toolStripToggleEquations
            // 
            this.toolStripToggleEquations.Checked = true;
            this.toolStripToggleEquations.CheckOnClick = true;
            this.toolStripToggleEquations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripToggleEquations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripToggleEquations.Image = global::Graphmatic.Properties.Resources.Equation16;
            this.toolStripToggleEquations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripToggleEquations.Name = "toolStripToggleEquations";
            this.toolStripToggleEquations.Size = new System.Drawing.Size(23, 22);
            this.toolStripToggleEquations.ToolTipText = "Toggle Equations";
            this.toolStripToggleEquations.Click += new System.EventHandler(this.toolStripToggleEquations_Click);
            // 
            // toolStripToggleDataSets
            // 
            this.toolStripToggleDataSets.Checked = true;
            this.toolStripToggleDataSets.CheckOnClick = true;
            this.toolStripToggleDataSets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripToggleDataSets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripToggleDataSets.Image = global::Graphmatic.Properties.Resources.DataSet16;
            this.toolStripToggleDataSets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripToggleDataSets.Name = "toolStripToggleDataSets";
            this.toolStripToggleDataSets.Size = new System.Drawing.Size(23, 22);
            this.toolStripToggleDataSets.ToolTipText = "Toggle Data Sets";
            this.toolStripToggleDataSets.Click += new System.EventHandler(this.toolStripToggleDataSets_Click);
            // 
            // toolStripTogglePictures
            // 
            this.toolStripTogglePictures.Checked = true;
            this.toolStripTogglePictures.CheckOnClick = true;
            this.toolStripTogglePictures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripTogglePictures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTogglePictures.Image = global::Graphmatic.Properties.Resources.Images16;
            this.toolStripTogglePictures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTogglePictures.Name = "toolStripTogglePictures";
            this.toolStripTogglePictures.Size = new System.Drawing.Size(23, 22);
            this.toolStripTogglePictures.ToolTipText = "Toggle Pictures";
            this.toolStripTogglePictures.Click += new System.EventHandler(this.toolStripTogglePictures_Click);
            // 
            // toolStripToggleHtmlPages
            // 
            this.toolStripToggleHtmlPages.Checked = true;
            this.toolStripToggleHtmlPages.CheckOnClick = true;
            this.toolStripToggleHtmlPages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripToggleHtmlPages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripToggleHtmlPages.Image = global::Graphmatic.Properties.Resources.HtmlPages;
            this.toolStripToggleHtmlPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripToggleHtmlPages.Name = "toolStripToggleHtmlPages";
            this.toolStripToggleHtmlPages.Size = new System.Drawing.Size(23, 22);
            this.toolStripToggleHtmlPages.ToolTipText = "Toggle HTML Pages";
            this.toolStripToggleHtmlPages.Click += new System.EventHandler(this.toolStripToggleHtmlPages_Click);
            // 
            // toolStripToggleHidden
            // 
            this.toolStripToggleHidden.CheckOnClick = true;
            this.toolStripToggleHidden.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripToggleHidden.Image = global::Graphmatic.Properties.Resources.Resources16;
            this.toolStripToggleHidden.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripToggleHidden.Name = "toolStripToggleHidden";
            this.toolStripToggleHidden.Size = new System.Drawing.Size(23, 22);
            this.toolStripToggleHidden.ToolTipText = "Toggle Hidden Resources";
            this.toolStripToggleHidden.Click += new System.EventHandler(this.toolStripToggleHidden_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButtonAdd
            // 
            this.toolStripDropDownButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equationToolStripMenuItem,
            this.dataSetToolStripMenuItem,
            this.pictureToolStripMenuItem,
            this.pageToolStripMenuItem,
            this.webPageToolStripMenuItem});
            this.toolStripDropDownButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonAdd.Image")));
            this.toolStripDropDownButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonAdd.Name = "toolStripDropDownButtonAdd";
            this.toolStripDropDownButtonAdd.Size = new System.Drawing.Size(42, 22);
            this.toolStripDropDownButtonAdd.Text = "Add";
            this.toolStripDropDownButtonAdd.ToolTipText = "Add resource";
            // 
            // equationToolStripMenuItem
            // 
            this.equationToolStripMenuItem.Name = "equationToolStripMenuItem";
            this.equationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.equationToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.equationToolStripMenuItem.Text = "&Equation...";
            this.equationToolStripMenuItem.ToolTipText = "Add an equation to the document";
            this.equationToolStripMenuItem.Click += new System.EventHandler(this.equationToolStripMenuItem_Click);
            // 
            // dataSetToolStripMenuItem
            // 
            this.dataSetToolStripMenuItem.Name = "dataSetToolStripMenuItem";
            this.dataSetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.dataSetToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.dataSetToolStripMenuItem.Text = "&Data Set...";
            this.dataSetToolStripMenuItem.ToolTipText = "Add a data set to the document";
            this.dataSetToolStripMenuItem.Click += new System.EventHandler(this.dataSetToolStripMenuItem_Click);
            // 
            // pictureToolStripMenuItem
            // 
            this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
            this.pictureToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.pictureToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pictureToolStripMenuItem.Text = "P&icture...";
            this.pictureToolStripMenuItem.ToolTipText = "Add a Picture to the document";
            this.pictureToolStripMenuItem.Click += new System.EventHandler(this.pictureToolStripMenuItem_Click);
            // 
            // pageToolStripMenuItem
            // 
            this.pageToolStripMenuItem.Name = "pageToolStripMenuItem";
            this.pageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.pageToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pageToolStripMenuItem.Text = "&Page...";
            this.pageToolStripMenuItem.Click += new System.EventHandler(this.pageToolStripMenuItem_Click);
            // 
            // webPageToolStripMenuItem
            // 
            this.webPageToolStripMenuItem.Name = "webPageToolStripMenuItem";
            this.webPageToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.webPageToolStripMenuItem.Text = "&HTML Page...";
            this.webPageToolStripMenuItem.Click += new System.EventHandler(this.webPageToolStripMenuItem_Click);
            // 
            // toolStripButtonPageOrder
            // 
            this.toolStripButtonPageOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPageOrder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPageOrder.Image")));
            this.toolStripButtonPageOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPageOrder.Name = "toolStripButtonPageOrder";
            this.toolStripButtonPageOrder.Size = new System.Drawing.Size(70, 22);
            this.toolStripButtonPageOrder.Text = "Page Order";
            this.toolStripButtonPageOrder.ToolTipText = "Edit the order of the pages in the document";
            this.toolStripButtonPageOrder.Click += new System.EventHandler(this.pageOrderToolStripMenuItem_Click);
            // 
            // panelHtmlViewer
            // 
            this.panelHtmlViewer.Controls.Add(this.toolStripContainerHtmlViewer);
            this.panelHtmlViewer.Location = new System.Drawing.Point(414, 87);
            this.panelHtmlViewer.Name = "panelHtmlViewer";
            this.panelHtmlViewer.Size = new System.Drawing.Size(200, 100);
            this.panelHtmlViewer.TabIndex = 2;
            // 
            // toolStripContainerHtmlViewer
            // 
            // 
            // toolStripContainerHtmlViewer.ContentPanel
            // 
            this.toolStripContainerHtmlViewer.ContentPanel.Controls.Add(this.webBrowserHtmlViewer);
            this.toolStripContainerHtmlViewer.ContentPanel.Size = new System.Drawing.Size(200, 100);
            this.toolStripContainerHtmlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerHtmlViewer.LeftToolStripPanelVisible = false;
            this.toolStripContainerHtmlViewer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerHtmlViewer.Name = "toolStripContainerHtmlViewer";
            this.toolStripContainerHtmlViewer.RightToolStripPanelVisible = false;
            this.toolStripContainerHtmlViewer.Size = new System.Drawing.Size(200, 100);
            this.toolStripContainerHtmlViewer.TabIndex = 3;
            this.toolStripContainerHtmlViewer.Text = "toolStripContainer1";
            // 
            // toolStripContainerHtmlViewer.TopToolStripPanel
            // 
            this.toolStripContainerHtmlViewer.TopToolStripPanel.Controls.Add(this.toolStripHtmlViewer);
            // 
            // webBrowserHtmlViewer
            // 
            this.webBrowserHtmlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserHtmlViewer.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserHtmlViewer.Location = new System.Drawing.Point(0, 0);
            this.webBrowserHtmlViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHtmlViewer.Name = "webBrowserHtmlViewer";
            this.webBrowserHtmlViewer.ScriptErrorsSuppressed = true;
            this.webBrowserHtmlViewer.Size = new System.Drawing.Size(200, 100);
            this.webBrowserHtmlViewer.TabIndex = 0;
            this.webBrowserHtmlViewer.WebBrowserShortcutsEnabled = false;
            this.webBrowserHtmlViewer.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserHtmlViewer_Navigating);
            // 
            // toolStripHtmlViewer
            // 
            this.toolStripHtmlViewer.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripHtmlViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBack,
            this.toolStripButtonForward});
            this.toolStripHtmlViewer.Location = new System.Drawing.Point(83, 0);
            this.toolStripHtmlViewer.Name = "toolStripHtmlViewer";
            this.toolStripHtmlViewer.Size = new System.Drawing.Size(58, 25);
            this.toolStripHtmlViewer.TabIndex = 0;
            this.toolStripHtmlViewer.Visible = false;
            // 
            // toolStripButtonBack
            // 
            this.toolStripButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBack.Image = global::Graphmatic.Properties.Resources.NavBackward16;
            this.toolStripButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBack.Name = "toolStripButtonBack";
            this.toolStripButtonBack.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBack.Text = "Back";
            this.toolStripButtonBack.ToolTipText = "Go back";
            this.toolStripButtonBack.Click += new System.EventHandler(this.toolStripButtonBack_Click);
            // 
            // toolStripButtonForward
            // 
            this.toolStripButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonForward.Image = global::Graphmatic.Properties.Resources.NavForward16;
            this.toolStripButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonForward.Name = "toolStripButtonForward";
            this.toolStripButtonForward.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonForward.Text = "Forward";
            this.toolStripButtonForward.ToolTipText = "Go forward";
            this.toolStripButtonForward.Click += new System.EventHandler(this.toolStripButtonForward_Click);
            // 
            // panelImageViewer
            // 
            this.panelImageViewer.Controls.Add(this.toolStripContainerImageViewer);
            this.panelImageViewer.Location = new System.Drawing.Point(62, 62);
            this.panelImageViewer.Name = "panelImageViewer";
            this.panelImageViewer.Size = new System.Drawing.Size(299, 276);
            this.panelImageViewer.TabIndex = 1;
            // 
            // toolStripContainerImageViewer
            // 
            // 
            // toolStripContainerImageViewer.ContentPanel
            // 
            this.toolStripContainerImageViewer.ContentPanel.Controls.Add(this.pictureBoxImageViewer);
            this.toolStripContainerImageViewer.ContentPanel.Size = new System.Drawing.Size(299, 251);
            this.toolStripContainerImageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerImageViewer.LeftToolStripPanelVisible = false;
            this.toolStripContainerImageViewer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerImageViewer.Name = "toolStripContainerImageViewer";
            this.toolStripContainerImageViewer.RightToolStripPanelVisible = false;
            this.toolStripContainerImageViewer.Size = new System.Drawing.Size(299, 276);
            this.toolStripContainerImageViewer.TabIndex = 3;
            this.toolStripContainerImageViewer.Text = "toolStripContainer1";
            // 
            // toolStripContainerImageViewer.TopToolStripPanel
            // 
            this.toolStripContainerImageViewer.TopToolStripPanel.Controls.Add(this.toolStripImageViewer);
            // 
            // pictureBoxImageViewer
            // 
            this.pictureBoxImageViewer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBoxImageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxImageViewer.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImageViewer.Name = "pictureBoxImageViewer";
            this.pictureBoxImageViewer.Size = new System.Drawing.Size(299, 251);
            this.pictureBoxImageViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImageViewer.TabIndex = 0;
            this.pictureBoxImageViewer.TabStop = false;
            // 
            // toolStripImageViewer
            // 
            this.toolStripImageViewer.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripImageViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonView});
            this.toolStripImageViewer.Location = new System.Drawing.Point(3, 0);
            this.toolStripImageViewer.Name = "toolStripImageViewer";
            this.toolStripImageViewer.Size = new System.Drawing.Size(41, 25);
            this.toolStripImageViewer.TabIndex = 0;
            this.toolStripImageViewer.Text = "toolStrip1";
            // 
            // toolStripDropDownButtonView
            // 
            this.toolStripDropDownButtonView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem,
            this.centerToolStripMenuItem,
            this.stretchToolStripMenuItem});
            this.toolStripDropDownButtonView.Image = global::Graphmatic.Properties.Resources.Image16;
            this.toolStripDropDownButtonView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonView.Name = "toolStripDropDownButtonView";
            this.toolStripDropDownButtonView.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButtonView.Text = "View";
            this.toolStripDropDownButtonView.ToolTipText = "Image view settings";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.zoomToolStripMenuItem.Text = "&Zoom";
            this.zoomToolStripMenuItem.ToolTipText = "Stretch the image as much as possible to fill the screen without changing the asp" +
    "ect ratio";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.centerToolStripMenuItem.Text = "&Center";
            this.centerToolStripMenuItem.ToolTipText = "Show the image unzoomed in the center of the page";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // stretchToolStripMenuItem
            // 
            this.stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
            this.stretchToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.stretchToolStripMenuItem.Text = "&Stretch";
            this.stretchToolStripMenuItem.ToolTipText = "Stretch the image to cover the whole page";
            this.stretchToolStripMenuItem.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
            // 
            // panelPageEditor
            // 
            this.panelPageEditor.Controls.Add(this.toolStripContainerPageEditor);
            this.panelPageEditor.Location = new System.Drawing.Point(62, 160);
            this.panelPageEditor.Name = "panelPageEditor";
            this.panelPageEditor.Size = new System.Drawing.Size(579, 280);
            this.panelPageEditor.TabIndex = 0;
            // 
            // toolStripContainerPageEditor
            // 
            // 
            // toolStripContainerPageEditor.BottomToolStripPanel
            // 
            this.toolStripContainerPageEditor.BottomToolStripPanel.Controls.Add(this.toolStripPageEditor);
            // 
            // toolStripContainerPageEditor.ContentPanel
            // 
            this.toolStripContainerPageEditor.ContentPanel.Controls.Add(this.pageDisplay);
            this.toolStripContainerPageEditor.ContentPanel.Size = new System.Drawing.Size(579, 224);
            this.toolStripContainerPageEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerPageEditor.LeftToolStripPanelVisible = false;
            this.toolStripContainerPageEditor.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerPageEditor.Name = "toolStripContainerPageEditor";
            this.toolStripContainerPageEditor.RightToolStripPanelVisible = false;
            this.toolStripContainerPageEditor.Size = new System.Drawing.Size(579, 280);
            this.toolStripContainerPageEditor.TabIndex = 4;
            this.toolStripContainerPageEditor.Text = "toolStripContainer1";
            // 
            // toolStripPageEditor
            // 
            this.toolStripPageEditor.AllowItemReorder = true;
            this.toolStripPageEditor.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripPageEditor.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripPageEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonIncreasePenSize,
            this.toolStripButtonDecreasePenSize,
            this.toolStripLabelPenSize,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonErase,
            this.toolStripDropDownButtonDraw,
            this.toolStripButtonTextAnnotation,
            this.toolStripSplitButtonColorAnnotation,
            this.toolStripSeparator5,
            this.toolStripDropDownEditGraph,
            this.toolStripButtonPlotDataSet,
            this.toolStripButtonPlotEquation,
            this.toolStripSeparator2,
            this.toolStripButtonPreviousPage,
            this.toolStripButtonNextPage,
            this.toolStripButtonAddPage,
            this.toolStripSeparator3,
            this.toolStripButtonSquareSelect,
            this.toolStripButtonPan,
            this.toolStripComboBoxZoom});
            this.toolStripPageEditor.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripPageEditor.Location = new System.Drawing.Point(3, 0);
            this.toolStripPageEditor.Name = "toolStripPageEditor";
            this.toolStripPageEditor.Size = new System.Drawing.Size(575, 31);
            this.toolStripPageEditor.TabIndex = 0;
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
            // toolStripDropDownButtonErase
            // 
            this.toolStripDropDownButtonErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonErase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pencilToolStripMenuItem,
            this.highlightToolStripMenuItem});
            this.toolStripDropDownButtonErase.Image = global::Graphmatic.Properties.Resources.AnnotateDraw24;
            this.toolStripDropDownButtonErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonErase.Name = "toolStripDropDownButtonErase";
            this.toolStripDropDownButtonErase.Size = new System.Drawing.Size(37, 28);
            this.toolStripDropDownButtonErase.Text = "&Draw";
            this.toolStripDropDownButtonErase.ToolTipText = "Tools for drawing annotations";
            // 
            // pencilToolStripMenuItem
            // 
            this.pencilToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateDraw16;
            this.pencilToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pencilToolStripMenuItem.Name = "pencilToolStripMenuItem";
            this.pencilToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.pencilToolStripMenuItem.Text = "&Pencil";
            this.pencilToolStripMenuItem.ToolTipText = "Draw in a pencil line annotation";
            // 
            // highlightToolStripMenuItem
            // 
            this.highlightToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateHighlight16;
            this.highlightToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.highlightToolStripMenuItem.Name = "highlightToolStripMenuItem";
            this.highlightToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.highlightToolStripMenuItem.Text = "&Highlight";
            this.highlightToolStripMenuItem.ToolTipText = "Draw in highlight annotations";
            // 
            // toolStripDropDownButtonDraw
            // 
            this.toolStripDropDownButtonDraw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonDraw.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eraserToolStripMenuItem,
            this.eraseAllToolStripMenuItem});
            this.toolStripDropDownButtonDraw.Image = global::Graphmatic.Properties.Resources.AnnotateErase24;
            this.toolStripDropDownButtonDraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonDraw.Name = "toolStripDropDownButtonDraw";
            this.toolStripDropDownButtonDraw.Size = new System.Drawing.Size(37, 28);
            this.toolStripDropDownButtonDraw.Text = "&Erase";
            this.toolStripDropDownButtonDraw.ToolTipText = "Tools for erasing annotations";
            // 
            // eraserToolStripMenuItem
            // 
            this.eraserToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateErase16;
            this.eraserToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eraserToolStripMenuItem.Name = "eraserToolStripMenuItem";
            this.eraserToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.eraserToolStripMenuItem.Text = "&Eraser";
            // 
            // eraseAllToolStripMenuItem
            // 
            this.eraseAllToolStripMenuItem.Image = global::Graphmatic.Properties.Resources.AnnotateEraseAll16;
            this.eraseAllToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eraseAllToolStripMenuItem.Name = "eraseAllToolStripMenuItem";
            this.eraseAllToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.eraseAllToolStripMenuItem.Text = "Erase &all annotations";
            this.eraseAllToolStripMenuItem.Click += new System.EventHandler(this.eraseAllToolStripMenuItem_Click);
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
            // toolStripDropDownEditGraph
            // 
            this.toolStripDropDownEditGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownEditGraph.DropDown = this.contextMenuStripPageEditor;
            this.toolStripDropDownEditGraph.Image = global::Graphmatic.Properties.Resources.Chart24;
            this.toolStripDropDownEditGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownEditGraph.Name = "toolStripDropDownEditGraph";
            this.toolStripDropDownEditGraph.Size = new System.Drawing.Size(37, 28);
            this.toolStripDropDownEditGraph.Text = "Edit page and plotted resources";
            this.toolStripDropDownEditGraph.ToolTipText = "Edit page and plotted resources";
            this.toolStripDropDownEditGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripDropDownEditGraph_MouseDown);
            // 
            // contextMenuStripPageEditor
            // 
            this.contextMenuStripPageEditor.Name = "contextMenuStripPageEditor";
            this.contextMenuStripPageEditor.OwnerItem = this.toolStripDropDownEditGraph;
            this.contextMenuStripPageEditor.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripButtonPlotDataSet
            // 
            this.toolStripButtonPlotDataSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlotDataSet.Image = global::Graphmatic.Properties.Resources.Data24;
            this.toolStripButtonPlotDataSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlotDataSet.Name = "toolStripButtonPlotDataSet";
            this.toolStripButtonPlotDataSet.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPlotDataSet.ToolTipText = "Plot a data set on this graph";
            this.toolStripButtonPlotDataSet.Click += new System.EventHandler(this.toolStripButtonPlotDataSet_Click);
            // 
            // toolStripButtonPlotEquation
            // 
            this.toolStripButtonPlotEquation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlotEquation.Image = global::Graphmatic.Properties.Resources.Graph24;
            this.toolStripButtonPlotEquation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPlotEquation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlotEquation.Name = "toolStripButtonPlotEquation";
            this.toolStripButtonPlotEquation.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPlotEquation.ToolTipText = "Plot an equation on this graph";
            this.toolStripButtonPlotEquation.Click += new System.EventHandler(this.toolStripButtonPlotEquation_Click);
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
            this.toolStripButtonPreviousPage.Click += new System.EventHandler(this.toolStripButtonPreviousPage_Click);
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
            this.toolStripButtonNextPage.Click += new System.EventHandler(this.toolStripButtonNextPage_Click);
            // 
            // toolStripButtonAddPage
            // 
            this.toolStripButtonAddPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddPage.Image = global::Graphmatic.Properties.Resources.TokenAdd;
            this.toolStripButtonAddPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPage.Name = "toolStripButtonAddPage";
            this.toolStripButtonAddPage.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAddPage.Text = "toolStripButton1";
            this.toolStripButtonAddPage.ToolTipText = "Add a page after this one";
            this.toolStripButtonAddPage.Click += new System.EventHandler(this.toolStripButtonAddPage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonSquareSelect
            // 
            this.toolStripButtonSquareSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSquareSelect.Image = global::Graphmatic.Properties.Resources.SquareSelect24;
            this.toolStripButtonSquareSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSquareSelect.Name = "toolStripButtonSquareSelect";
            this.toolStripButtonSquareSelect.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSquareSelect.ToolTipText = "Select objects with a rectangle";
            // 
            // toolStripButtonPan
            // 
            this.toolStripButtonPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPan.Image = global::Graphmatic.Properties.Resources.ArrowMove16;
            this.toolStripButtonPan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPan.Name = "toolStripButtonPan";
            this.toolStripButtonPan.Size = new System.Drawing.Size(23, 28);
            this.toolStripButtonPan.ToolTipText = "Move the viewport around";
            // 
            // toolStripComboBoxZoom
            // 
            this.toolStripComboBoxZoom.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxZoom.Items.AddRange(new object[] {
            "10%",
            "25%",
            "50%",
            "100%",
            "150%",
            "200%",
            "400%",
            "800%"});
            this.toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            this.toolStripComboBoxZoom.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxZoom.Text = "100%";
            this.toolStripComboBoxZoom.ToolTipText = "Change the zoom level for this page";
            this.toolStripComboBoxZoom.TextChanged += new System.EventHandler(this.toolStripComboBoxZoom_TextChanged);
            // 
            // pageDisplay
            // 
            this.pageDisplay.ContextMenuStrip = this.contextMenuStripPageEditor;
            this.pageDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageDisplay.Location = new System.Drawing.Point(0, 0);
            this.pageDisplay.Name = "pageDisplay";
            this.pageDisplay.Size = new System.Drawing.Size(579, 224);
            this.pageDisplay.TabIndex = 0;
            this.pageDisplay.TabStop = false;
            this.pageDisplay.DragDrop += new System.Windows.Forms.DragEventHandler(this.pageDisplay_DragDrop);
            this.pageDisplay.DragOver += new System.Windows.Forms.DragEventHandler(this.pageDisplay_DragOver);
            this.pageDisplay.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.pageDisplay_GiveFeedback);
            this.pageDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.pageDisplay_Paint);
            this.pageDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pageDisplay_MouseDown);
            this.pageDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pageDisplay_MouseMove);
            this.pageDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pageDisplay_MouseUp);
            this.pageDisplay.Resize += new System.EventHandler(this.pageDisplay_Resize);
            // 
            // timerBackup
            // 
            this.timerBackup.Enabled = true;
            this.timerBackup.Interval = 60000;
            this.timerBackup.Tick += new System.EventHandler(this.timerBackup_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 607);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Graphmatic.Properties.Resources.Graphmatic;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Graphmatic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResizeBegin += new System.EventHandler(this.Main_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStripResources.ResumeLayout(false);
            this.toolStripResources.ResumeLayout(false);
            this.toolStripResources.PerformLayout();
            this.panelHtmlViewer.ResumeLayout(false);
            this.toolStripContainerHtmlViewer.ContentPanel.ResumeLayout(false);
            this.toolStripContainerHtmlViewer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerHtmlViewer.TopToolStripPanel.PerformLayout();
            this.toolStripContainerHtmlViewer.ResumeLayout(false);
            this.toolStripContainerHtmlViewer.PerformLayout();
            this.toolStripHtmlViewer.ResumeLayout(false);
            this.toolStripHtmlViewer.PerformLayout();
            this.panelImageViewer.ResumeLayout(false);
            this.toolStripContainerImageViewer.ContentPanel.ResumeLayout(false);
            this.toolStripContainerImageViewer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerImageViewer.TopToolStripPanel.PerformLayout();
            this.toolStripContainerImageViewer.ResumeLayout(false);
            this.toolStripContainerImageViewer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageViewer)).EndInit();
            this.toolStripImageViewer.ResumeLayout(false);
            this.toolStripImageViewer.PerformLayout();
            this.panelPageEditor.ResumeLayout(false);
            this.toolStripContainerPageEditor.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainerPageEditor.BottomToolStripPanel.PerformLayout();
            this.toolStripContainerPageEditor.ContentPanel.ResumeLayout(false);
            this.toolStripContainerPageEditor.ResumeLayout(false);
            this.toolStripContainerPageEditor.PerformLayout();
            this.toolStripPageEditor.ResumeLayout(false);
            this.toolStripPageEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListResources;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripResources;
        private System.Windows.Forms.ListView listViewResources;
        private System.Windows.Forms.ToolStripButton toolStripTogglePages;
        private System.Windows.Forms.ToolStripButton toolStripToggleEquations;
        private System.Windows.Forms.ToolStripButton toolStripToggleDataSets;
        private System.Windows.Forms.Panel panelPageEditor;
        private System.Windows.Forms.ToolStripContainer toolStripContainerPageEditor;
        private System.Windows.Forms.ToolStrip toolStripPageEditor;
        private System.Windows.Forms.ToolStripButton toolStripButtonIncreasePenSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonDecreasePenSize;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPenSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTextAnnotation;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonColorAnnotation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlotDataSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSquareSelect;
        private System.Windows.Forms.ToolStripButton toolStripButtonPan;
        private System.Windows.Forms.ColumnHeader columnHeaderIcon;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderAuthor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripResources;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButtonAdd;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem equationToolStripMenuItem;
        private ToolStripButton toolStripButtonPlotEquation;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem dataSetToolStripMenuItem;
        private ToolStripButton toolStripToggleHidden;
        private ToolStripButton toolStripTogglePictures;
        private ToolStripMenuItem pictureToolStripMenuItem;
        private Panel panelImageViewer;
        private ToolStripContainer toolStripContainerImageViewer;
        private ToolStrip toolStripImageViewer;
        private PictureBox pictureBoxImageViewer;
        private ToolStripDropDownButton toolStripDropDownButtonView;
        private ToolStripMenuItem zoomToolStripMenuItem;
        private ToolStripMenuItem centerToolStripMenuItem;
        private ToolStripMenuItem stretchToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem resourcesToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem pageToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem pageOrderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton toolStripButtonPageOrder;
        private Panel panelHtmlViewer;
        private ToolStripContainer toolStripContainerHtmlViewer;
        private WebBrowser webBrowserHtmlViewer;
        private ToolStrip toolStripHtmlViewer;
        private ToolStripButton toolStripButtonBack;
        private ToolStripButton toolStripButtonForward;
        private ToolStripButton toolStripToggleHtmlPages;
        private ToolStripMenuItem webPageToolStripMenuItem;
        private Timer timerBackup;
        private ToolStripDropDownButton toolStripDropDownEditGraph;
        private ContextMenuStrip contextMenuStripPageEditor;
        private PictureBox pageDisplay;
        private ToolStripDropDownButton toolStripDropDownButtonErase;
        private ToolStripMenuItem pencilToolStripMenuItem;
        private ToolStripMenuItem highlightToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButtonDraw;
        private ToolStripMenuItem eraserToolStripMenuItem;
        private ToolStripMenuItem eraseAllToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripStatusLabel toolStripStatusLabelBugReport;
        private ToolStripStatusLabel toolStripStatusLabelEditor;
        private ToolStripComboBox toolStripComboBoxZoom;
    }
}

