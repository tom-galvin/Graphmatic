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

        private void InitializeResourceDragDrop()
        {
            pageDisplay.AllowDrop = true;
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
                CurrentPage.Graph.Draw(g, pageDisplay.ClientSize, !IsFormResizing);
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
                                Properties.Resources.Delete,
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
                        String.Format("&Edit {0} items", resources.Length), Properties.Resources.pencil,
                        resources) :
                    new ToolStripMenuItem(
                        String.Format("No items to &edit", resources.Length), Properties.Resources.pencil)
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
    }
}
