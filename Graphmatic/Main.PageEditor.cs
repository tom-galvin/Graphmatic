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
        private ToolStripMenuItem[] PlottableEditingMenu;

        /// <summary>
        /// The default colors used for new plottable items added to a page.
        /// </summary>
        private Color[] DefaultPlottableColors = {
                                                     Color.Red,
                                                     Color.Green,
                                                     Color.Teal,
                                                     Color.Blue,
                                                     Color.Purple,
                                                     Color.DarkSlateGray
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
        /// Initialize everything relating to the page display.
        /// </summary>
        private void InitializePageDisplay()
        {
            pageDisplay.AllowDrop = true;
            ToolButtons = new List<ToolStripItem>();

            RegisterTool(toolStripButtonPan, PageTool.Pan);
            RegisterTool(toolStripButtonSquareSelect, PageTool.Select);
            RegisterTool(pencilToolStripMenuItem, PageTool.Pencil);
            RegisterTool(eraserToolStripMenuItem, PageTool.Eraser);
            RegisterTool(highlightToolStripMenuItem, PageTool.Highlighter);

            toolStripButtonSquareSelect.PerformClick();
        }

        /// <summary>
        /// Register a page editor tool for this interface.
        /// </summary>
        /// <param name="item">The ToolStripItem that will be used to activate this tool.</param>
        /// <param name="toolType">The type of tool that this ToolStripItem activates.</param>
        private void RegisterTool(ToolStripItem item, PageTool toolType)
        {
            ToolButtons.Add(item);
            if (item is ToolStripButton)
                (item as ToolStripButton).CheckOnClick = true;
            if (item is ToolStripMenuItem)
                (item as ToolStripMenuItem).CheckOnClick = true;
            item.Click += (sender, e) =>
            {
                SelectedAnnotations = null;
                ToolButtons.ForEach(button =>
                {
                    if (button is ToolStripButton)
                        (button as ToolStripButton).Checked = false;
                    if (button is ToolStripMenuItem)
                        (button as ToolStripMenuItem).Checked = false;
                });
                toolStripStatusLabelEditor.Text = toolType.ToString();
                CurrentPageTool = toolType;
                if (item is ToolStripButton)
                    (item as ToolStripButton).Checked = true;
                if (item is ToolStripMenuItem)
                    (item as ToolStripMenuItem).Checked = true;
                pageDisplay.Refresh();
            };
        }

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
            toolStripComboBoxZoom.Text = String.Format(
                "{0}%",
                0.05 / CurrentPage.Graph.Parameters.HorizontalPixelScale * 100.0);
            pageDisplay.Refresh();

            // TODO remove testing code
            CurrentPage.Annotations.Add(new Annotation()
            {
                X = 0,
                Y = 0,
                Width = 1.5,
                Height = 3,
                Color = Color.Green
            });
        }

        void Graph_Update(object sender, EventArgs e)
        {
            pageDisplay.Refresh();
            RegeneratePlottableEditingMenu();
        }

        // Page display drawing procedure...
        private void pageDisplay_Paint(object sender, PaintEventArgs e)
        {
            // Only draw if the page actually exists
            if (CurrentPage != null)
            {
                pageDisplay.SuspendLayout();

                // Get some drawing equipment
                Graphics g = e.Graphics;
                Brush backgroundBrush = new SolidBrush(CurrentPage.BackgroundColor);
                Size size = pageDisplay.ClientSize;

                // Draw background color in
                g.FillRectangle(backgroundBrush, IsFormResizing ? e.ClipRectangle : pageDisplay.ClientRectangle);

                // Get the appropriate drawing resolution
                PlotResolution resolution = PlotResolution.View;
                if (IsDragging)
                    resolution = PlotResolution.Edit;
                if (IsFormResizing)
                    resolution = PlotResolution.Resize;

                // Draw the graph
                CurrentPage.Graph.Draw(g, size, resolution);

                // Draw annotations
                foreach (Annotation annotation in CurrentPage.Annotations)
                {
                    annotation.DrawOnto(CurrentPage, g, size, CurrentPage.Graph.Parameters, resolution);
                }

                // Draw selection boxes around selected annotations, if any
                if (SelectedAnnotations != null)
                {
                    foreach (Annotation annotation in SelectedAnnotations)
                    {
                        annotation.SelectDrawOnto(CurrentPage, g, size, CurrentPage.Graph.Parameters, resolution);
                    }
                }

                // Draw the current selection (dragging) box
                if (CurrentPageTool == PageTool.Select && IsSelecting)
                {
                    Pen selectionPen = new Pen(Color.Blue, 2f);
                    selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    g.DrawRectangle(selectionPen, SelectionBox);
                }

                pageDisplay.ResumeLayout(false);
            }
        }

        /// <summary>
        /// Regenerates the menu for editing the page and any plottable resources on it.
        /// </summary>
        private void RegeneratePlottableEditingMenu()
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
                                })
                                {
                                    ShortcutKeys = Keys.Delete
                                },
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
            PlottableEditingMenu = new ToolStripMenuItem[]
            {
                // Add the edit item, or the warning if there are no items to edit
                (resources.Length > 0 ?
                    new ToolStripMenuItem(
                        String.Format("&Edit {0} items", resources.Length), Properties.Resources.AnnotateDraw16,
                        resources) :
                    new ToolStripMenuItem(
                        String.Format("No items to &edit", resources.Length), Properties.Resources.AnnotateDraw16)
                    {
                        Enabled = false
                    }
                ),
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
            contextMenuStripPageEditor.Items.AddRange(PlottableEditingMenu);
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

        #region Selection Handling
        /// <summary>
        /// The horizontal page offset at the start of selection.
        /// </summary>
        private double StartingPageHozOffset;
        /// <summary>
        /// The vertical page offset at the start of selection.
        /// </summary>
        private double  StartingPageVertOffset;
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
        private void pageDisplay_MouseMove(object sender, MouseEventArgs e)
        {
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
                        offsetX *= CurrentPage.Graph.Parameters.HorizontalPixelScale;
                        offsetY *= CurrentPage.Graph.Parameters.VerticalPixelScale;

                        CurrentPage.Graph.Parameters.CenterHorizontal = StartingPageHozOffset - offsetX;
                        CurrentPage.Graph.Parameters.CenterVertical = StartingPageVertOffset + offsetY;
                    }
                    else if (CurrentPageTool == PageTool.Select)
                    {
                        // Update the dimensions of the selection box if we're in the selection tool
                        SelectionBox = CreateDirectionalInvariantRectangle(
                            MouseStart.X,
                            MouseStart.Y,
                            e.X - MouseStart.X,
                            e.Y - MouseStart.Y);
                    }
                }
                if (IsManipulatingSelected)
                {
                    // If we're manipulating (ie. moving) items, then move all of the selected items along with the cursor
                    foreach (Annotation annotation in SelectedAnnotations)
                    {
                        annotation.X += (offsetX - LastOffsetX) * CurrentPage.Graph.Parameters.HorizontalPixelScale;
                        annotation.Y -= (offsetY - LastOffsetY) * CurrentPage.Graph.Parameters.VerticalPixelScale;
                    }
                }
                pageDisplay.Refresh();
                LastOffsetX = offsetX;
                LastOffsetY = offsetY;
            }
        }

        /// <summary>
        /// Create a Rectangle using metrics that are independent of direction.
        /// <para/>
        /// For example, creating a Rectangle with a height of -3 will return a Rectangle with
        /// a height of 3 that is shifted up 3 places.
        private Rectangle CreateDirectionalInvariantRectangle(int x, int y, int width, int height)
        {
            int 
                rx = Math.Min(x, x + width), ry = Math.Min(y, y + height),
                rw = Math.Max(x, x + width) - rx, rh = Math.Max(y, y + height) - ry;
            return new Rectangle(rx, ry, rw, rh);
        }

        private void pageDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStart = new Point(e.X, e.Y);
            if (SelectedAnnotations != null)
            {
                // If we've already selected some items, and the cursor clicks somewhere within
                // one of those items, we assume the user is trying to move those items
                var selection = SelectedAnnotations
                    .Select(a => new Tuple<Annotation, int>(a, 
                        a.ScreenDistance(CurrentPage, pageDisplay.ClientSize, CurrentPage.Graph.Parameters, MouseStart)))
                    .Where(t => t.Item2 <= 0);
                if (selection.Count() > 0)
                {
                    IsManipulatingSelected = true;
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
            }
            // Set the initial size of the selection box
            SelectionBox = new Rectangle(
                MouseStart.X,
                MouseStart.Y,
                0,
                0);
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
                        if (e.Button != System.Windows.Forms.MouseButtons.Middle || SelectedAnnotations == null)
                            SelectedAnnotations = new List<Annotation>();

                        if (IsSelecting)
                        {
                            // If we've selected a bunch of annotations, add them all to the
                            // current selection
                            SelectedAnnotations.AddRange(
                                CurrentPage.Annotations
                                .Where(a => a.InSelection(
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
                                    a.ScreenDistance(
                                        CurrentPage,
                                        pageDisplay.ClientSize,
                                        CurrentPage.Graph.Parameters,
                                        SelectionBox.Location)))
                                        .Where(t => t.Item2 <= 0)
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
                        IsSelecting = false;
                    }
                    IsManipulatingSelected = false;
                }
                // Reset the state
                IsDragging = false;
                LastOffsetX = LastOffsetY = 0;
                pageDisplay.Refresh();
                NotifyResourceModified(CurrentPage);
            }
        }
        #endregion

        #region WinForms handlers
        private void pageDisplay_Resize(object sender, EventArgs e)
        {
            pageDisplay.Refresh();
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

        private void toolStripDropDownEditGraph_MouseDown(object sender, MouseEventArgs e)
        {
            RegeneratePlottableEditingMenu();
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
            Page newPage = new Page();
            AddResource(newPage);
            OpenResourceEditor(newPage);
        }

        private void toolStripComboBoxZoom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string text = toolStripComboBoxZoom.Text;
                if (text.EndsWith("%"))
                    text = text.Substring(0, text.Length - 1);
                double zoomLevel = 0.05 / (Double.Parse(text)
                    / 100.0);
                CurrentPage.Graph.Parameters.HorizontalPixelScale =
                    CurrentPage.Graph.Parameters.VerticalPixelScale =
                    zoomLevel;
                double zoomFactor = Math.Pow(2, Math.Floor(Math.Log(zoomLevel / 0.05, 2)));
                CurrentPage.Graph.Axes.GridSize = zoomFactor;
                pageDisplay.Refresh();
                NotifyResourceModified(CurrentPage);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid zoom level.", "Zoom", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        #endregion
    }
}
