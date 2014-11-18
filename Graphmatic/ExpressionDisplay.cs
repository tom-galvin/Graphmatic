using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Expressions;

namespace Graphmatic
{
    /// <summary>
    /// Provides a display for a Graphmatic mathematical expression.
    /// </summary>
    public partial class ExpressionDisplay : UserControl
    {
        /// <summary>
        /// Gets or sets if Moein mode is enabled or not.
        /// </summary>
        [Description("Changes whether Moein mode is enabled or not.")]
        public bool MoeinMode
        {
            get;
            set;
        }

        private bool _Edit;
        /// <summary>
        /// Gets or sets if the expression display is in editing mode or not.
        /// </summary>
        [Description("Changes whether the expression display is in editing mode or not.")]
        [DefaultValue(true)]
        public bool Edit
        {
            get { return _Edit; }
            set { _Edit = value; ExpressionCursor.Visible = value; }
        }

        private Expression _Expression;
        /// <summary>
        /// Gets the mathematical expression to display.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Expression Expression
        {
            get
            {
                return _Expression;
            }
            set
            {
                _Expression = value;
                RecalculateExpressionDimensions();
            }
        }

        private int _DisplayScale;
        /// <summary>
        /// Gets the scale at which to display the expression.
        /// </summary>
        [DefaultValue(1)]
        [Description("Changes the pixel-level scale at which to display the contained expression.")]
        public int DisplayScale
        {
            get
            {
                return _DisplayScale;
            }
            set
            {
                if (value > 0)
                {
                    _DisplayScale = value;
                    Invalidate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Display scale must be greater than zero.");
                }
            }
        }

        private bool _SmallExpression;
        /// <summary>
        /// Gets or sets whether to force the expression to display in small mode.
        /// </summary>
        [DefaultValue(false)]
        [Description("Determines whether to force the expression to display in small mode or not.")]
        public bool SmallExpression
        {
            get
            {
                return _SmallExpression;
            }
            set
            {
                _SmallExpression = value;
                Expression.RecalculateDimensions(ExpressionCursor);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets the cursor used to navigate and modify the entered expression.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ExpressionCursor ExpressionCursor
        {
            get;
            protected set;
        }

        /// <summary>
        /// The size of the expression, in pixels, that was most recently drawn.
        /// </summary>
        private Size ExpressionSize;

        /// <summary>
        /// Create a new instance of the ExpressionDisplay class with an empty expression and a default DisplayScale of 1.
        /// </summary>
        public ExpressionDisplay()
        {
            _Expression = new Expressions.Expression(null);
            ExpressionCursor = new ExpressionCursor(Expression, 0);
            RecalculateExpressionDimensions();
            Edit = true;
            DisplayScale = 1;
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void RecalculateExpressionDimensions()
        {
            _Expression.RecalculateDimensions(ExpressionCursor);
        }

        private void ExpressionDisplay_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ExpressionDisplay_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!MoeinMode)
                {
                    Expression.Size = SmallExpression ? DisplaySize.Small : DisplaySize.Large;
                    Expression.RecalculateDimensions(ExpressionCursor);
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    Bitmap expressionBitmap = new Bitmap(Expression.Width + 3, Expression.Height + 3);
                    ExpressionSize = expressionBitmap.Size;
                    Graphics expressionGraphics = Graphics.FromImage(expressionBitmap);
                    ExpressionCursor.Hotspots.Clear();
                    Expression.Paint(expressionGraphics, ExpressionCursor, 1, 1);

                    e.Graphics.ScaleTransform(_DisplayScale, _DisplayScale);
                    e.Graphics.DrawImage(expressionBitmap, (Width / _DisplayScale - expressionBitmap.Width) / 2, (Height / _DisplayScale - expressionBitmap.Height) / 2);
                }
                else
                {
                    e.Graphics.DrawImageUnscaled(Properties.Resources.Moein, 0, 0, Width, Height);
                }
            }
            catch (Exception)
            {
                throw; // for debugging, so the debugger catches the exception without WinForms swallowing it
            }
        }

        /// <summary>
        /// Moves the cursor left.
        /// </summary>
        /// <returns>Returns true if the cursor movement was successful, and false otherwise.</returns>
        public bool MoveLeft()
        {
            return ExpressionCursor.Left();
        }

        /// <summary>
        /// Moves the cursor right.
        /// </summary>
        /// <returns>Returns true if the cursor movement was successful, and false otherwise.</returns>
        public bool MoveRight()
        {
            return ExpressionCursor.Right();
        }

        /// <summary>
        /// Deletes whatever is to the left of the cursor.
        /// </summary>
        /// <returns>Returns true if the deletion is successful; otherwise false.</returns>
        public bool Delete()
        {
            return ExpressionCursor.Delete();
        }

        private void ExpressionDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (Edit)
            {
                Point position = e.Location;
                position.X = position.X / _DisplayScale - (Width / _DisplayScale - ExpressionSize.Width) / 2;
                position.Y = position.Y / _DisplayScale - (Height / _DisplayScale - ExpressionSize.Height) / 2;
                foreach (var keyValuePair in ExpressionCursor.Hotspots)
                {
                    if (keyValuePair.Key.Contains(position))
                    {
                        Point pointInHotspotClicked = new Point(
                            position.X - keyValuePair.Key.X,
                            position.Y - keyValuePair.Key.Y);
                        keyValuePair.Value(pointInHotspotClicked, ExpressionCursor);
                        break;
                    }
                }
            }
        }
    }
}
