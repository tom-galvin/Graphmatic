using Graphmatic.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Graphmatic.Interaction.Plotting;

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
        /// Whether the user is currently clicking and dragging in the page display or not.
        /// </summary>
        private bool IsDragging = false;

        /// <summary>
        /// The starting point of the dragging action in the page display.
        /// </summary>
        private Point MouseStart;

        private void InitializePageDisplay()
        {
            pageDisplay.AllowDrop = true;
            ToolButtons = new List<ToolStripItem>();

            RegisterTool(toolStripButtonPan, PageTool.Pan);
            RegisterTool(toolStripButtonSquareSelect, PageTool.Select);
            RegisterTool(pencilToolStripMenuItem, PageTool.Pencil);
            RegisterTool(eraserToolStripMenuItem, PageTool.Eraser);
            RegisterTool(highlightToolStripMenuItem, PageTool.Highlighter);
        }

        private void RegisterTool(ToolStripItem item, PageTool toolType)
        {
            ToolButtons.Add(item);
            if (item is ToolStripButton)
                (item as ToolStripButton).CheckOnClick = true;
            if (item is ToolStripMenuItem)
                (item as ToolStripMenuItem).CheckOnClick = true;
            item.Click += (sender, e) =>
            {
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
            };
        }

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
            toolStripComboBoxZoom.Text = String.Format(
                "{0}%",
                0.05 / CurrentPage.Graph.Parameters.HorizontalPixelScale * 100.0);
            RenderPage();
        }

        void Graph_Update(object sender, EventArgs e)
        {
            pageDisplay.Refresh();
            RegeneratePlottableEditingMenu();
        }

        private void RenderPage()
        {
            if (!IsFormResizing)
            {
                pageDisplay.Refresh();
            }
        }

        private void pageDisplay_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentPage != null)
            {
                pageDisplay.SuspendLayout();
                Graphics g = e.Graphics;
                Brush backgroundBrush = new SolidBrush(CurrentPage.BackgroundColor);
                g.FillRectangle(backgroundBrush, IsFormResizing ? e.ClipRectangle : pageDisplay.ClientRectangle);
                PlotResolution resolution = PlotResolution.View;
                if (IsDragging)
                    resolution = PlotResolution.Edit;
                if (IsFormResizing)
                    resolution = PlotResolution.Resize;
                CurrentPage.Graph.Draw(g, pageDisplay.ClientSize, resolution);
                pageDisplay.ResumeLayout(false);
            }
        }

        private void pageDisplay_Resize(object sender, EventArgs e)
        {
            pageDisplay.Refresh();
        }

        private void RegeneratePlottableEditingMenu()
        {
            var resources =
                CurrentPage.Graph
                    .OfType<Resource>()
                    .Select(r => new ToolStripMenuItem(r.Name,
                        null,
                        new ToolStripMenuItem[] {
                            new ToolStripMenuItem("&Remove",
                                Properties.Resources.TokenDelete,
                                delegate(object sender, EventArgs e)
                                {
                                    CurrentPage.Graph.Remove(r as IPlottable);
                                    RenderPage();
                                })
                                {
                                    ShortcutKeys = Keys.Delete
                                },
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
                new ToolStripMenuItem("&Horizontal variable...", null, delegate(object sender, EventArgs e)
                    {
                        CreateVariableDialog dialog = new CreateVariableDialog(CurrentPage.Graph.Parameters.HorizontalAxis);
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CurrentPage.Graph.Parameters.HorizontalAxis = dialog.EnteredChar;
                            NotifyResourceModified(CurrentPage);
                        }
                    }),
                new ToolStripMenuItem("&Vertical variable...", null, delegate(object sender, EventArgs e)
                    {
                        CreateVariableDialog dialog = new CreateVariableDialog(CurrentPage.Graph.Parameters.VerticalAxis);
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            CurrentPage.Graph.Parameters.VerticalAxis = dialog.EnteredChar;
                            NotifyResourceModified(CurrentPage);
                        }
                    }),
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

        private PlottableParameters CreateNewPlottableParameters()
        {
            PlottableParameters parameters = new PlottableParameters();
            parameters.PlotColor = DefaultPlottableColors[Program.Random.Next(DefaultPlottableColors.Length)];
            return parameters;
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

        private double startPageH, startPageV;
        private void pageDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                if (CurrentPageTool == PageTool.Pan)
                {
                    double offsetX = e.X - MouseStart.X;
                    double offsetY = e.Y - MouseStart.Y;

                    offsetX *= CurrentPage.Graph.Parameters.HorizontalPixelScale;
                    offsetY *= CurrentPage.Graph.Parameters.VerticalPixelScale;

                    CurrentPage.Graph.Parameters.CenterHorizontal = startPageH - offsetX;
                    CurrentPage.Graph.Parameters.CenterVertical = startPageV + offsetY;
                }
                pageDisplay.Refresh();
            }
        }

        private void pageDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentPageTool == PageTool.Pan)
            {
                startPageH = CurrentPage.Graph.Parameters.CenterHorizontal;
                startPageV = CurrentPage.Graph.Parameters.CenterVertical;
            }
            MouseStart = new Point(e.X, e.Y);
            IsDragging = true;
        }

        private void pageDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
            pageDisplay.Refresh();
            NotifyResourceModified(CurrentPage);
        }
    }
}
