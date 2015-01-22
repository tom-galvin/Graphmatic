using Graphmatic.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Graphmatic.Interaction.Plotting;
using Graphmatic.Interaction.Annotations;

namespace Graphmatic
{
    public partial class Main : Form
    {
        /// <summary>
        /// Whether the form is currently being resized or not;
        /// </summary>
        private bool IsFormResizing = false;

        /// <summary>
        /// The page currently open in the page editor.
        /// </summary>
        private Page CurrentPage;

        /// <summary>
        /// The menu items used to edit IPlottable resources on a graph page.
        /// </summary>
        private ToolStripMenuItem[] GraphEditMenu;

        /// <summary>
        /// The default colors used for new plottable items added to a page.
        /// </summary>
        private Color[] DefaultPlottableColors = {
                                                     Color.Red,
                                                     Color.Green,
                                                     Color.Teal,
                                                     Color.Blue,
                                                     Color.Purple,
                                                 };

        /// <summary>
        /// The currently-active tool being used to edit the page.
        /// </summary>
        private PageTool CurrentPageTool;

        /// <summary>
        /// All tool strip items used to activate tools in the page editor.
        /// </summary>
        private List<ToolStripItem> ToolButtons;

        /// <summary>
        /// A list of selected annotations;
        /// </summary>
        private List<Annotation> SelectedAnnotations;

        /// <summary>
        /// A rectangle representing the selected area on the screen.
        /// </summary>
        private Rectangle SelectionBox;

        /// <summary>
        /// Whether the user is currently clicking and dragging in the page display or not.
        /// </summary>
        private bool IsDragging = false;

        /// <summary>
        /// The starting point of the dragging action in the page display.
        /// </summary>
        private Point MouseStart;

        /// <summary>
        /// The current point of the dragging action in the page display.
        /// </summary>
        private Point MouseLocation;

        /// <summary>
        /// Whether the user is currently resizing an element on the page or not.
        /// </summary>
        private bool IsResizing = false;

        /// <summary>
        /// The color of the pen to draw with.
        /// </summary>
        private Color PenColor = Color.Black;

        /// <summary>
        /// The width of the pen to draw with.
        /// </summary>
        private float PenWidth = 3f;

        /// <summary>
        /// Initialize everything relating to the page display.
        /// </summary>
        private void InitializePageDisplay()
        {
            pageDisplay.AllowDrop = true;
            ToolButtons = new List<ToolStripItem>();

            RegisterTool(PageTool.Pan, toolStripButtonPan, panToolStripMenuItem);
            RegisterTool(PageTool.Select, toolStripButtonSquareSelect, selectToolStripMenuItem);
            RegisterTool(PageTool.Pencil, pencilToolStripMenuItem, pencilToolStripMenuItem1, toolStripSplitButtonPencil);
            RegisterTool(PageTool.Eraser, eraserToolStripMenuItem, eraserToolStripMenuItem1, toolStripSplitButtonErase);
            RegisterTool(PageTool.Highlighter, highlightToolStripMenuItem, highlightToolStripMenuItem);

            toolStripComboBoxZoom.ComboBox.MouseWheel += toolStripComboBoxZoom_MouseWheel;
            pageDisplay.MouseWheel += pageDisplay_MouseWheel;

            RegeneratePenPreviewImage();

            toolStripButtonSquareSelect.PerformClick();
        }

        #region Tool stuff
        /// <summary>
        /// Regenerates the preview image of the currently selected pen.
        /// </summary>
        private void RegeneratePenPreviewImage()
        {
            Bitmap bitmap = new Bitmap((int)PenWidth + 1, (int)PenWidth + 1);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Brush brush = new SolidBrush(PenColor))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.FillEllipse(brush, 0, 0, (int)PenWidth, (int)PenWidth);
                toolStripLabelPenSize.Image = bitmap;
            }
        }

        /// <summary>
        /// Register a page editor tool for this interface.
        /// </summary>
        /// <param name="toolType">The type of tool that this ToolStripItem activates.</param>
        /// <param name="items">The ToolStripItem(s) that will be used to activate this tool.</param>
        private void RegisterTool(PageTool toolType, params ToolStripItem[] items)
        {
            // this function must accept 3 different types of button
            // MenuItem: in a menu
            // Button: on a toolbar, the normal type
            // SplitButton: on a toolbar, the drop-down type
            // the ugly-looking logic is to account for all 3
            foreach (ToolStripItem item in items)
            {
                ToolButtons.Add(item);
                if (item is ToolStripButton)
                    (item as ToolStripButton).CheckOnClick = true;
                if (item is ToolStripMenuItem)
                    (item as ToolStripMenuItem).CheckOnClick = true;
                EventHandler evt = (sender, e) =>
                {
                    SelectedAnnotations = null;
                    CheckSelectedTool(items);
                    SelectionBox = new Rectangle();
                    toolStripStatusLabelEditor.Text = toolType.ToString();
                    CurrentPageTool = toolType;
                    SetPenPresetAttributes(toolType);
                    pageDisplay.Refresh();
                };
                if (item is ToolStripSplitButton)
                    (item as ToolStripSplitButton).ButtonClick += evt;
                else
                    item.Click += evt;
            }
        }

        /// <summary>
        /// Checks the tool buttons for the currently-selected tool, and deselects the rest.
        /// </summary>
        /// <param name="currentToolItems">The ToolStripItems corresponding to the current tool.</param>
        private void CheckSelectedTool(ToolStripItem[] currentToolItems)
        {
            ToolButtons.ForEach(button =>
            {
                if (button is ToolStripButton)
                    (button as ToolStripButton).Checked = false;
                if (button is ToolStripMenuItem)
                    (button as ToolStripMenuItem).Checked = false;
            });
            foreach (ToolStripItem subItem in currentToolItems)
            {
                if (subItem is ToolStripButton)
                    (subItem as ToolStripButton).Checked = true;
                if (subItem is ToolStripMenuItem)
                    (subItem as ToolStripMenuItem).Checked = true;
            }
        }

        /// <summary>
        /// Sets the default color and width (from user settings) for the given tool.
        /// </summary>
        /// <param name="toolType">The tool to set the attributes according to.</param>
        private void SetPenPresetAttributes(PageTool toolType)
        {
            if (toolType == PageTool.Pencil)
            {
                PenColor = Properties.Settings.Default.DefaultPencilColor;
                PenWidth = Properties.Settings.Default.DefaultPencilWidth;
                RegeneratePenPreviewImage();
            }
            else if (toolType == PageTool.Highlighter)
            {
                PenColor = Properties.Settings.Default.DefaultHighlightColor;
                PenWidth = Properties.Settings.Default.DefaultHighlightWidth;
                RegeneratePenPreviewImage();
            }
        }
        #endregion

        /// <summary>
        /// Open the resource editor for Pages, and set it to edit the given page.
        /// </summary>
        /// <param name="page">The page to edit.</param>
        private void OpenPageEditor(Page page)
        {
            CloseResourcePanels();
            panelPageEditor.Visible = panelPageEditor.Enabled = true;
            if (CurrentPage != null)
            {
                CurrentPage.Graph.Update -= Graph_Update;
            }
            CurrentPage = page;
            CurrentPage.Graph.Update += Graph_Update;
            // update the zoom level
            UpdateZoomComboBox();
            pageDisplay.Refresh();
        }

        /// <summary>
        /// Gets or sets the zoom level (ie. inverse pixel scale) of the current page.
        /// <para/>
        /// This is relative to 1, where 1 is equivalent to 100%.
        /// </summary>
        public double ZoomLevel
        {
            get
            {
                return 0.05 / CurrentPage.Graph.Parameters.HorizontalPixelScale;
            }
            set
            {
                double zoomLevel = 0.05 / value;
                CurrentPage.Graph.Parameters.HorizontalPixelScale =
                     CurrentPage.Graph.Parameters.VerticalPixelScale =
                     zoomLevel;
                double zoomFactor = Math.Pow(10, Math.Floor(Math.Log(zoomLevel / 0.05, 10)));
                while (zoomLevel / zoomFactor > 0.15)
                    zoomFactor *= 2;
                CurrentPage.Graph.Axes.GridSize = zoomFactor;
                UpdateZoomComboBox();
                pageDisplay.Refresh();
                NotifyResourceModified(CurrentPage);
            }
        }

        /// <summary>
        /// Updates the content of the zoom combo box (on the page editor toolbar) to reflect the
        /// current page's zoom level.
        /// </summary>
        private void UpdateZoomComboBox()
        {
            toolStripComboBoxZoom.Text = String.Format(
                "{0:0.##}%",
                ZoomLevel * 100.0);
        }

        /// <summary>
        /// Clamps a double to a given range (inclusive).
        /// </summary>
        /// <param name="val">The value to clamp.</param>
        /// <param name="min">The lower bound of the allowable range.</param>
        /// <param name="max">The upper bound of the allowable range.</param>
        /// <returns>The clamped value.</returns>
        double DoubleClamp(double val, double min, double max)
        {
            if (max < min)
                throw new InvalidOperationException("The maximum value of a range must be greater than " +
                    "the minimum value!");
            if (val < min) val = min;
            if (val > max) val = max;
            return val;
        }

        void Graph_Update(object sender, EventArgs e)
        {
            pageDisplay.Refresh();
            RegenerateGraphEditMenu();
        }

        /// <summary>
        /// Create a new PlottableParameters object with default randomized values.
        /// </summary>
        /// <returns>A new PlottableParameters object.</returns>
        private PlottableParameters CreateNewPlottableParameters()
        {
            PlottableParameters parameters = new PlottableParameters();
            parameters.PlotColor = DefaultPlottableColors[Program.Random.Next(DefaultPlottableColors.Length)];
            return parameters;
        }

        /// <summary>
        /// Creates a page, adds it to the end of the document's pagination list, and opens it.
        /// </summary>
        private void AddPage()
        {
            Page newPage = new Page()
            {
                Name = CreateResourceName("Page")
            };
            AddResource(newPage);
            OpenResourceEditor(newPage);
        }

        #region Drawing stuff
        // Page display drawing procedure...
        private void pageDisplay_Paint(object sender, PaintEventArgs e)
        {
            // Only draw if the page actually exists
            if (CurrentPage != null)
            {
                pageDisplay.SuspendLayout();

                DrawPage(e.Graphics, pageDisplay.ClientSize);

                pageDisplay.ResumeLayout(false);
            }
        }

        /// <summary>
        /// Draws the entire content of the page, with the given drawing surface and size.
        /// <para/>
        /// The page will be drawn starting at (0, 0).
        /// </summary>
        /// <param name="graphics">The drawing surface to use to draw the page.</param>
        /// <param name="size">The size with which the page is to be drawn.</param>
        private void DrawPage(Graphics graphics, Size size)
        {
            using (Brush backgroundBrush = new SolidBrush(CurrentPage.BackgroundColor))
            {

                // Draw background color in
                graphics.FillRectangle(backgroundBrush, pageDisplay.ClientRectangle);

                // Get the appropriate drawing resolution
                PlotResolution resolution = PlotResolution.View;
                if (IsDragging)
                    resolution = PlotResolution.Edit;
                if (IsFormResizing)
                    resolution = PlotResolution.Resize;

                // Draw the graph
                CurrentPage.Graph.Draw(graphics, size, resolution);

                if (CurrentPageTool == PageTool.Highlighter)
                // highlighter goes under other annotations, so draw the preview before the rest
                {
                    DrawCurrentPencilPath(graphics);
                    DrawAnnotations(graphics, size, resolution);
                }
                else
                // otherwise, draw normally
                {
                    DrawAnnotations(graphics, size, resolution);
                    DrawCurrentPencilPath(graphics);
                }

                DrawIndicators(graphics);
            }
        }

        /// <summary>
        /// Creates a preview image of the page, with the specified size.
        /// <para/>
        /// This just renders the page to an image rather than the page display.
        /// </summary>
        /// <param name="size">The size of the preview image to create.</param>
        /// <returns>A preview image of the current page.</returns>
        private Image CreatePagePreview(Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                DrawPage(graphics, size);
            }
            return bitmap;
        }

        /// <summary>
        /// Draws annotations and associated indicators.
        /// </summary>
        /// <param name="g">The graphics object to draw the page onto.</param>
        /// <param name="resolution">The drawing resolution to use.</param>
        private void DrawAnnotations(Graphics g, Size size, PlotResolution resolution)
        {
            // Draw annotations
            foreach (Annotation annotation in CurrentPage.Annotations)
            {
                annotation.DrawAnnotationOnto(CurrentPage, g, size, CurrentPage.Graph.Parameters, resolution);
            }

            // Draw selection boxes around selected annotations, if any
            if (SelectedAnnotations != null)
            {
                foreach (Annotation annotation in SelectedAnnotations)
                {
                    annotation.DrawSelectionIndicatorOnto(CurrentPage, g, size, CurrentPage.Graph.Parameters, resolution);
                }
            }
        }

        /// <summary>
        /// Draws indicators around any annotations that are selected (ie. the dots).
        /// <para/>
        /// Also draws the circle indicating the boundaries of the eraser.
        /// </summary>
        /// <param name="g">The graphics object to draw the stuff onto.</param>
        private void DrawIndicators(Graphics g)
        {
            // Draw the current selection (dragging) box
            if (CurrentPageTool == PageTool.Select && IsSelecting)
            {
                using (Pen selectionPen = new Pen(Color.Blue, 2f))
                {
                    selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    g.DrawRectangle(selectionPen, SelectionBox);
                }
            }
            if (CurrentPageTool == PageTool.Eraser && IsDragging)
            {
                // draw a circle showing the boundaries of the eraser tool's radius
                using (Pen eraserPen = new Pen(CurrentPage.Graph[CurrentPage.Graph.Axes].PlotColor))
                {
                    g.DrawEllipse(eraserPen,
                        MouseLocation.X - (int)(PenWidth),
                        MouseLocation.Y - (int)(PenWidth),
                        (int)(PenWidth * 2),
                        (int)(PenWidth * 2));
                }
            }
        }
        /// <summary>
        /// Draws a preview of the currently-drawn pencil path.
        /// </summary>
        /// <param name="g">The graphics object to draw the path onto.</param>
        private void DrawCurrentPencilPath(Graphics g)
        {
            if (CurrentlyDrawnPath != null && CurrentlyDrawnPath.Count > 1)
            {
                // show a preview of the currently-drawn pen line
                using (Pen drawPen = new Pen(PenColor, PenWidth))
                {
                    drawPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    g.DrawLines(drawPen, CurrentlyDrawnPath.ToArray());
                }
            }
        }
        #endregion
        #region Menu stuff
        /// <summary>
        /// Regenerates the menu for editing the page and any plottable resources on it.
        /// </summary>
        private void RegenerateGraphEditMenu()
        {
            GraphEditMenu = new ToolStripMenuItem[]
            {
                // Add the edit item, or the warning if there are no items to edit
                RegenerateAnnotationMenu(),
                RegeneratePlottableEditMenu(),
                // Add the horizontal variable chooser
                new ToolStripMenuItem("&Horizontal variable...", null, delegate(object sender, EventArgs e)
                    {
                        CreateVariableDialog dialog = new CreateVariableDialog(CurrentPage.Graph.Parameters.HorizontalAxis);
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CurrentPage.Graph.Parameters.HorizontalAxis = dialog.EnteredChar;
                            NotifyResourceModified(CurrentPage);
                        }
                    }),
                new ToolStripMenuItem("Horizontal Axis in Degrees", null, delegate(object sender, EventArgs e)
                    {
                        CurrentPage.Graph.Axes.HorizontalType =
                            (sender as ToolStripMenuItem).Checked ?
                            GraphAxisType.Degrees :
                            GraphAxisType.Radians;
                            pageDisplay.Refresh();
                    })
                {
                    CheckOnClick = true,
                    Checked = CurrentPage.Graph.Axes.HorizontalType == GraphAxisType.Degrees
                },
                // Add the vertical variable chooser
                new ToolStripMenuItem("&Vertical variable...", null, delegate(object sender, EventArgs e)
                    {
                        CreateVariableDialog dialog = new CreateVariableDialog(CurrentPage.Graph.Parameters.VerticalAxis);
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CurrentPage.Graph.Parameters.VerticalAxis = dialog.EnteredChar;
                            NotifyResourceModified(CurrentPage);
                        }
                    }),
                new ToolStripMenuItem("Vertical Axis in Degrees", null, delegate(object sender, EventArgs e)
                    {
                        CurrentPage.Graph.Axes.VerticalType =
                            (sender as ToolStripMenuItem).Checked ?
                            GraphAxisType.Degrees :
                            GraphAxisType.Radians;
                            pageDisplay.Refresh();
                    })
                {
                    CheckOnClick = true,
                    Checked = CurrentPage.Graph.Axes.VerticalType == GraphAxisType.Degrees
                },
                // Add the background color chooser
                new ToolStripMenuItem("Background &color...", null, delegate(object sender, EventArgs e)
                    {
                        ColorDialog dialog = new ColorDialog()
                        {
                            Color = CurrentPage.BackgroundColor
                        };
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CurrentPage.BackgroundColor = dialog.Color;
                            pageDisplay.Refresh();
                        }
                    }),
                // Add the feature (axes and key) color chooser
                new ToolStripMenuItem("Feature color...", null, delegate(object sender, EventArgs e)
                    {
                        ColorDialog dialog = new ColorDialog()
                        {
                            Color = CurrentPage.Graph[CurrentPage.Graph.Axes].PlotColor
                        };
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CurrentPage.Graph[CurrentPage.Graph.Axes].PlotColor =
                                CurrentPage.Graph[CurrentPage.Graph.Key].PlotColor = dialog.Color;
                            pageDisplay.Refresh();
                        }
                    }),
            };
            contextMenuStripPageEditor.Items.Clear();
            contextMenuStripPageEditor.Items.AddRange(GraphEditMenu);
        }

        /// <summary>
        /// Generate the menu for editing plottable menus.
        /// </summary>
        /// <returns>Returns a menu for editing plottable menus.</returns>
        private ToolStripMenuItem RegeneratePlottableEditMenu()
        {
            var resources =
                CurrentPage.Graph
                // the user should only edit Resources
                    .OfType<Resource>()
                    .Select(r => new ToolStripMenuItem(r.Name,
                        // For each resource...
                        null,
                        new ToolStripMenuItem[] {
                            // Add the Remove option
                            new ToolStripMenuItem("&Remove",
                                Properties.Resources.TokenDelete,
                                delegate(object sender, EventArgs e)
                                {
                                    CurrentPage.Graph.Remove(r as IPlottable);
                                    pageDisplay.Refresh();
                                }),
                            // Add the Color option
                            new ToolStripMenuItem("&Color...", null, delegate(object sender, EventArgs e)
                                {
                                    var parameters = CurrentPage.Graph[r as IPlottable];
                                    ColorDialog dialog = new ColorDialog()
                                    {
                                        Color = parameters.PlotColor
                                    };
                                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    {
                                        parameters.PlotColor = dialog.Color;
                                        NotifyResourceModified(CurrentPage);
                                    }
                                })
                        })).ToArray();

            // show contextual text if there are no editable items on the page
            if (resources.Length > 0)
            {
                return new ToolStripMenuItem(
                    String.Format("&Edit {0} items", resources.Length),
                    Properties.Resources.Resources16,
                    resources);
            }
            else
            {
                return new ToolStripMenuItem(
                      String.Format("No items to &edit", resources.Length),
                      Properties.Resources.Resources16)
                  {
                      Enabled = false
                  };
            }
        }

        /// <summary>
        /// Regenerates the annotation editing menu.
        /// </summary>
        private ToolStripMenuItem RegenerateAnnotationMenu()
        {
            if (SelectedAnnotations == null || SelectedAnnotations.Count == 0)
            {
                return new ToolStripMenuItem(
                    "No annotation to edit",
                    Properties.Resources.Annotations16)
                {
                    Enabled = false
                };
            }
            else
            {
                return new ToolStripMenuItem(
                    "Annotation",
                    Properties.Resources.Annotations16,
                    RegenerateAnnotationEditMenu());
            }
        }

        /// <summary>
        /// Regenerates the content of the annotation editing menu.
        /// </summary>
        private ToolStripMenuItem[] RegenerateAnnotationEditMenu()
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            items.Add(new ToolStripMenuItem(
                "&Unselect",
                null,
                delegate(object sender, EventArgs e)
                {
                    SelectedAnnotations = null;
                    pageDisplay.Refresh();
                    RegenerateGraphEditMenu();
                }));
            items.Add(new ToolStripMenuItem(
                "&Delete",
                Properties.Resources.AnnotationRemove16,
                delegate(object sender, EventArgs e)
                {
                    foreach (Annotation annotation in SelectedAnnotations)
                    {
                        CurrentPage.Annotations.Remove(annotation);
                    }
                    SelectedAnnotations = null;
                    pageDisplay.Refresh();
                    RegenerateGraphEditMenu();
                },
                Keys.Delete));
            return items.ToArray();
        }
        #endregion
        #region Selection Handling
        /// <summary>
        /// The horizontal page offset at the start of selection.
        /// </summary>
        private double StartingPageHozOffset;
        /// <summary>
        /// The vertical page offset at the start of selection.
        /// </summary>
        private double StartingPageVertOffset;
        /// <summary>
        /// The last mouse offset, at the previous firing of the pageDisplay_mouseMove event.
        /// </summary>
        private double LastOffsetX;
        /// <summary>
        /// The last mouse offset, at the previous firing of the pageDisplay_mouseMove event.
        /// </summary>
        private double LastOffsetY;
        /// <summary>
        /// Whether the user is selecting a region or not.
        /// </summary>
        private bool IsSelecting;
        /// <summary>
        /// Whether the user is manipulating (ie. moving) the selection or not.
        /// </summary>
        private bool IsManipulatingSelected;
        /// <summary>
        /// A list of all of the points the user has drawn with the pencil.
        /// </summary>
        private List<Point> CurrentlyDrawnPath;
        private void pageDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocation = new Point(e.X, e.Y);
            if (IsDragging)
            {
                double offsetX = e.X - MouseStart.X;
                double offsetY = e.Y - MouseStart.Y;
                if (!IsSelecting && !IsManipulatingSelected)
                {
                    // The selection box will appear as soon as the user has moved the mouse
                    // at least 5 pixels away. This is to prevent over-eager selection.
                    if (offsetX * offsetX + offsetY * offsetY > 5 * 5) IsSelecting = true;
                }
                if (IsSelecting)
                {
                    if (CurrentPageTool == PageTool.Pan)
                    {
                        // Pan the page if we're in the panning tool
                        HandlePan(offsetX, offsetY);
                    }
                    else if (CurrentPageTool == PageTool.Select)
                    {
                        // Update the dimensions of the selection box if we're in the selection tool
                        HandleSelect(e);
                    }
                }
                if (IsManipulatingSelected)
                {
                    // If we're manipulating (ie. moving or resizing) items, then move all of the selected items along with the cursor
                    HandleSelectMove(e, offsetX, offsetY);
                }
                if (CurrentlyDrawnPath != null)
                {
                    HandleDraw(e);
                }
                if (CurrentPageTool == PageTool.Eraser)
                {
                    HandleErase();
                }
                pageDisplay.Refresh();

                LastOffsetX = offsetX;
                LastOffsetY = offsetY;
            }
        }

        /// <summary>
        /// Handles the drawing of annotations when the mouse cursor is dragged.
        /// </summary>
        /// <param name="e">The event arguments describing the movement of the mouse.</param>
        private void HandleDraw(MouseEventArgs e)
        {
            CurrentlyDrawnPath.Add(new Point(e.X, e.Y));
        }

        /// <summary>
        /// Handles the erasure of Drawing annotations when the mouse cursor is dragged.
        /// </summary>
        private void HandleErase()
        {
            var toErase = CurrentPage.Annotations
                .OfType<Drawing>()
                .Where(a => a.DistanceToPointOnScreen(CurrentPage, pageDisplay.ClientSize, CurrentPage.Graph.Parameters, MouseLocation) <= (int)(PenWidth))
                .ToArray();
            foreach (var annotation in toErase)
            {
                CurrentPage.Annotations.Remove(annotation);
            }
        }

        /// <summary>
        /// Handles the selection of items when the mouse cursor is dragged.
        /// </summary>
        /// <param name="e">The event arguments describing the movement of the mouse.</param>
        private void HandleSelect(MouseEventArgs e)
        {
            SelectionBox = CreateDirectionalInvariantRectangle(
                MouseStart.X,
                MouseStart.Y,
                e.X - MouseStart.X,
                e.Y - MouseStart.Y);
        }

        /// <summary>
        /// Handles the movement of selected items when the mouse cursor is dragged.
        /// </summary>
        /// <param name="e">The event arguments describing the movement of the mouse.</param>
        /// <param name="offsetX">The offset of the mouse's X co-ordinate.</param>
        /// <param name="offsetY">The offset of the mouse's Y co-ordinate.</param>
        private void HandleSelectMove(MouseEventArgs e, double offsetX, double offsetY)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (IsResizing)
                {
                    if (SelectedAnnotations.Count == 1)
                    {
                        Annotation annotation = SelectedAnnotations[0];
                        annotation.Width += CurrentPage.Graph.Parameters.HorizontalPixelScale * (offsetX - LastOffsetX);
                        annotation.Height += CurrentPage.Graph.Parameters.VerticalPixelScale * -(offsetY - LastOffsetY);
                    }
                }
                else
                {
                    foreach (Annotation annotation in SelectedAnnotations)
                    {
                        annotation.X += (offsetX - LastOffsetX) * CurrentPage.Graph.Parameters.HorizontalPixelScale;
                        annotation.Y -= (offsetY - LastOffsetY) * CurrentPage.Graph.Parameters.VerticalPixelScale;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the panning of the view port when the mouse cursor is dragged.
        /// </summary>
        /// <param name="offsetX">The offset of the mouse's X co-ordinate.</param>
        /// <param name="offsetY">The offset of the mouse's Y co-ordinate.</param>
        private void HandlePan(double offsetX, double offsetY)
        {
            offsetX *= CurrentPage.Graph.Parameters.HorizontalPixelScale;
            offsetY *= CurrentPage.Graph.Parameters.VerticalPixelScale;

            CurrentPage.Graph.Parameters.CenterHorizontal = StartingPageHozOffset - offsetX;
            CurrentPage.Graph.Parameters.CenterVertical = StartingPageVertOffset + offsetY;
        }

        /// <summary>
        /// Create a Rectangle using metrics that are independent of direction.
        /// <para/>
        /// For example, creating a Rectangle with a height of -3 will return a Rectangle with
        /// a height of 3 that is shifted up 3 places.
        /// </summary>
        private Rectangle CreateDirectionalInvariantRectangle(int x, int y, int width, int height)
        {
            int
                rx = Math.Min(x, x + width), ry = Math.Min(y, y + height),
                rw = Math.Max(x, x + width) - rx, rh = Math.Max(y, y + height) - ry;
            return new Rectangle(rx, ry, rw, rh);
        }

        /// <summary>
        /// Create a Rectangle using metrics that are independent of direction from an existing direction-dependent rectangle.
        /// <para/>
        /// For example, creating a Rectangle with a height of -3 will return a Rectangle with
        /// a height of 3 that is shifted up 3 places.
        /// </summary>
        private Rectangle CreateDirectionalInvariantRectangle(Rectangle r)
        {
            return CreateDirectionalInvariantRectangle(r.X, r.Y, r.Width, r.Height);
        }

        private void pageDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStart = new Point(e.X, e.Y);
            MouseLocation = MouseStart;
            if (SelectedAnnotations != null)
            {
                // If we've already selected some items, and the cursor clicks somewhere within
                // one of those items, we assume the user is trying to move those items
                var selection = SelectedAnnotations
                    .Where(a => a.DistanceToPointOnScreen(CurrentPage, pageDisplay.ClientSize, CurrentPage.Graph.Parameters, MouseStart) <= 4);
                int count = selection.Count();
                if (count > 0)
                {
                    IsManipulatingSelected = true;

                    if (count == 1)
                    {
                        var resourceToResize = selection.First();
                        if (resourceToResize.IsPointInResizeNode(CurrentPage, pageDisplay.ClientSize, CurrentPage.Graph.Parameters, MouseLocation))
                        {
                            IsResizing = true;
                        }
                    }
                }
            }
            if (!IsManipulatingSelected)
            {
                if (CurrentPageTool == PageTool.Pan)
                {
                    // Set the starting offsets, for reference later on
                    StartingPageHozOffset = CurrentPage.Graph.Parameters.CenterHorizontal;
                    StartingPageVertOffset = CurrentPage.Graph.Parameters.CenterVertical;
                }
                else if (CurrentPageTool == PageTool.Pencil || CurrentPageTool == PageTool.Highlighter)
                {
                    // initialize the variable containing the path drawn
                    CurrentlyDrawnPath = new List<Point>();
                    // and add the starting location to it
                    CurrentlyDrawnPath.Add(MouseLocation);
                }
                else if (CurrentPageTool == PageTool.Select)
                {
                    // Set the initial size of the selection box
                    SelectionBox = new Rectangle(
                        MouseStart.X,
                        MouseStart.Y,
                        0,
                        0);
                }
                else if (CurrentPageTool == PageTool.Eraser)
                {
                    HandleErase();
                }
            }
            IsDragging = true;
        }

        private void pageDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                if (CurrentPageTool == PageTool.Select)
                {
                    if (!IsManipulatingSelected)
                    {
                        HandleSelectionZone(e);
                        IsSelecting = false;
                    }
                    else if (IsResizing)
                    {
                        var resizedResource = SelectedAnnotations[0];

                        // if the resource now has negative width or height after resizing,
                        // correct it by restoring the correct signs
                        if (resizedResource.Width < 0)
                        {
                            resizedResource.X -= resizedResource.Width = -resizedResource.Width;
                            if (resizedResource is Drawing)
                                (resizedResource as Drawing).FlipHorizontal();
                        }
                        if (resizedResource.Height < 0)
                        {
                            resizedResource.Y -= resizedResource.Height = -resizedResource.Height;
                            if (resizedResource is Drawing)
                                (resizedResource as Drawing).FlipVertical();
                        }
                    }
                    IsManipulatingSelected = false;
                }
                else if (CurrentlyDrawnPath != null)
                {
                    CreateDrawnAnnotation();
                }
                // Reset the state
                IsDragging = false;
                IsResizing = false;
                LastOffsetX = LastOffsetY = 0;
                pageDisplay.Refresh();
                NotifyResourceModified(CurrentPage);
            }
        }

        /// <summary>
        /// Creates a drawn annotation using the currently-stored path.
        /// </summary>
        private void CreateDrawnAnnotation()
        {
            if (CurrentlyDrawnPath.Count == 1)
            {
                var artificialPoint = CurrentlyDrawnPath[0];
                CurrentlyDrawnPath.Clear();

                // offset the 1st point a bit so the 'dot' is more
                // evenly spaced under the cursor
                artificialPoint.Y -= (int)(PenWidth / 2);
                CurrentlyDrawnPath.Add(artificialPoint);

                artificialPoint.Y += (int)PenWidth; // value type so this doesn't change CurrentlyDrawnPath[0]
                CurrentlyDrawnPath.Add(artificialPoint);
            }

            Drawing drawing = new Drawing(
                CurrentlyDrawnPath.ToArray(),
                pageDisplay.ClientSize,
                CurrentPage.Graph.Parameters,
                PenColor,
                PenWidth,
                CurrentPageTool == PageTool.Highlighter ?
                    DrawingType.Highlight :
                    DrawingType.Pencil);

            // if we're using the highlighter tool, put the pen line behind other annotations
            if (CurrentPageTool == PageTool.Pencil)
                CurrentPage.Annotations.Add(drawing);
            else if (CurrentPageTool == PageTool.Highlighter)
                CurrentPage.Annotations.Insert(0, drawing);

            else
                MessageBox.Show("This shouldn't happen!", "Draw", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CurrentlyDrawnPath = null;
        }

        /// <summary>
        /// Handles the user's input of a selection zone, and selects all chosen items accordingly.
        /// </summary>
        /// <param name="e">The MouseEventArgs containing the event arguments of the MouseUp event causing the selection to take place.</param>
        private void HandleSelectionZone(MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Middle || SelectedAnnotations == null)
                SelectedAnnotations = new List<Annotation>();

            if (IsSelecting)
            {
                // If we've selected a bunch of annotations, add them all to the
                // current selection
                SelectedAnnotations.AddRange(
                    CurrentPage.Annotations
                    .Where(a => a.IsAnnotationInSelection(
                        CurrentPage,
                        pageDisplay.ClientSize,
                        CurrentPage.Graph.Parameters,
                        SelectionBox)));
            }
            else
            {
                // If we've not made a selection box, the user has selected either 0 or 1 items
                // attempt to find the one the user has selected - if it doesn't exist, then the
                // user is trying to deselect stuff (ie. clicking away from the selection), so
                // clear the selection
                var possible = CurrentPage.Annotations
                    .Select(a => new Tuple<Annotation, int>(a,
                        a.DistanceToPointOnScreen(
                            CurrentPage,
                            pageDisplay.ClientSize,
                            CurrentPage.Graph.Parameters,
                            SelectionBox.Location)))
                    .Where(t => t.Item2 <= 4)
                    .OrderBy(t => t.Item2);
                if (possible.Count() > 0)
                {
                    SelectedAnnotations.Add(possible.First().Item1);
                }
                else
                {
                    SelectedAnnotations = null;
                }
            }

            if (SelectedAnnotations != null && SelectedAnnotations.Count == 0)
                SelectedAnnotations = null;
        }
        #endregion
        #region WinForms handlers
        private void pageDisplay_Resize(object sender, EventArgs e)
        {
            pageDisplay.Refresh();
        }

        private void pageDisplay_MouseEnter(object sender, EventArgs e)
        {
            pageDisplay.Focus();
        }

        void pageDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            double power = 1.0 + 0.1 * (double)Math.Sign(e.Delta);
            ZoomLevel = DoubleClamp(Math.Pow(ZoomLevel * 100.0, power) / 100.0, 0.025, 20.0);
        }

        private void pageDisplay_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)) &&
                ((e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link))
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void pageDisplay_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void pageDisplay_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                string guidData = (string)e.Data.GetData(typeof(string));
                Guid guid;
                if (Guid.TryParse(guidData, out guid))
                {
                    if (CurrentDocument.Contains(guid))
                    {
                        Resource resource = CurrentDocument[guid];
                        if (resource is IPlottable)
                        {
                            try
                            {
                                CurrentPage.Graph.Add(resource as IPlottable, CreateNewPlottableParameters());
                            }
                            catch (InvalidOperationException)
                            {
                                MessageBox.Show("The Graph already contains this resource.", "Graph", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("This resource is not able to be added to the graph.", "Graph", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This resource does not exist.", "Graph", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("This data cannot be added to the graph.", "Graph", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("This data cannot be added to the graph.", "Graph", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // used for adding an image from a file
        private void fromfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Image files (*.png; *.jpg; *.jpe; *.jpeg; *.bmp; *.gif; *.tga; *.tif)|*.png;*.jpg;*.jpe;*.jpeg;*.bmp;*.gif;*.tga;*.tif|All files|*",
                Title = "Import Image"
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                Picture picture = new Picture(image)
                {
                    Width = CurrentPage.Graph.Parameters.HorizontalPixelScale * image.Width,
                    Height = CurrentPage.Graph.Parameters.VerticalPixelScale * image.Height
                };
                CurrentPage.Annotations.Add(picture);
                NotifyResourceModified(CurrentPage);
            }
        }

        // used for adding an image from the clipboard
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                Image image = Clipboard.GetImage();
                Picture picture = new Picture(image)
                {
                    Width = CurrentPage.Graph.Parameters.HorizontalPixelScale * image.Width,
                    Height = CurrentPage.Graph.Parameters.VerticalPixelScale * image.Height
                };
                CurrentPage.Annotations.Add(picture);
                NotifyResourceModified(CurrentPage);
            }
            else
            {
                MessageBox.Show(
                    "The current data on the clipboard is not an image.",
                    "Paste Image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void toolStripDropDownEditGraph_MouseDown(object sender, MouseEventArgs e)
        {
            RegenerateGraphEditMenu();
        }

        private void toolStripButtonPlotDataSet_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet(Properties.Settings.Default.DefaultDataSetVariables)
            {
                Name = CreateResourceName(String.Format("{0}/Data Set", CurrentPage.Name))
            };
            if (new DataSetCreator(dataSet).ShowDialog() != System.Windows.Forms.DialogResult.Cancel &&
                new DataSetEditor(dataSet).ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                AddResource(dataSet);
                CurrentPage.Graph.Add(dataSet, CreateNewPlottableParameters());
            }
        }

        private void toolStripButtonPlotEquation_Click(object sender, EventArgs e)
        {
            Equation equation = new Equation()
            {
                Name = CreateResourceName(String.Format("{0}/Equation", CurrentPage.Name))
            };
            if (new EquationEditor(equation).ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                AddResource(equation);
                CurrentPage.Graph.Add(equation, CreateNewPlottableParameters());
            }
        }

        private void toolStripButtonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageIndex = CurrentDocument.PageOrder.IndexOf(CurrentPage);
            if (pageIndex != 0)
            {
                OpenResourceEditor(CurrentDocument.PageOrder[pageIndex - 1]);
            }
        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            int pageIndex = CurrentDocument.PageOrder.IndexOf(CurrentPage);
            if (pageIndex != CurrentDocument.PageOrder.Count - 1)
            {
                OpenResourceEditor(CurrentDocument.PageOrder[pageIndex + 1]);
            }
        }

        private void toolStripButtonAddPage_Click(object sender, EventArgs e)
        {
            AddPage();
        }

        private void toolStripComboBoxZoom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string text = toolStripComboBoxZoom.Text;
                if (text.EndsWith("%"))
                    text = text.Substring(0, text.Length - 1);
                ZoomLevel = Double.Parse(text) / 100;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid zoom level.", "Zoom", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void toolStripComboBoxZoom_MouseWheel(object sender, MouseEventArgs e)
        {
            (e as HandledMouseEventArgs).Handled = true;
        }

        private void eraseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentPage.Annotations.Count > 0)
            {
                if (MessageBox.Show("This will remove all annotations from the page. This cannot be undone! Do you want to continue?",
                    "Clear Annotations",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    CurrentPage.Annotations.Clear();
                    if (SelectedAnnotations != null)
                    {
                        SelectedAnnotations = null;
                    }
                    pageDisplay.Refresh();
                    NotifyResourceModified(CurrentPage);
                }
            }
        }

        private void toolStripButtonPrintScreen_Click(object sender, EventArgs e)
        {
            using (Image previewImage = CreatePagePreview(pageDisplay.ClientSize))
            {
                Clipboard.SetImage(previewImage);
            }
        }

        #region Pen Color & Width
        // These functions all do similar things: they change the color or dimensions
        // of the annotation pen. There isn't much to them, so I've not documented each one.
        private void toolStripButtonIncreasePenSize_Click(object sender, EventArgs e)
        {
            if (PenWidth < 23f) PenWidth += 1f;
            RegeneratePenPreviewImage();
        }

        private void toolStripButtonDecreasePenSize_Click(object sender, EventArgs e)
        {
            if (PenWidth > 2f) PenWidth -= 1f;
            RegeneratePenPreviewImage();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Black;
            RegeneratePenPreviewImage();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Red;
            RegeneratePenPreviewImage();
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Orange;
            RegeneratePenPreviewImage();
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Yellow;
            RegeneratePenPreviewImage();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Green;
            RegeneratePenPreviewImage();
        }

        private void tealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Teal;
            RegeneratePenPreviewImage();
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Blue;
            RegeneratePenPreviewImage();
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Purple;
            RegeneratePenPreviewImage();
        }

        private void pickcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PickPenColor();
        }

        private void toolStripSplitButtonColorAnnotation_ButtonClick(object sender, EventArgs e)
        {
            PickPenColor();
        }

        /// <summary>
        /// Lets the user pick a custom pen color apart from the default ones.
        /// <para/>
        /// It does this with the default WinForms ColorDialog tool.
        /// </summary>
        private void PickPenColor()
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = PenColor;

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PenColor = colorDialog.Color;
                RegeneratePenPreviewImage();
            }
        }
        #endregion
        #endregion
    }
}
