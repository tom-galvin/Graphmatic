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
        /// Gets the mathematical expression to display.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Expression Expression
        {
            get;
            set;
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
            Expression = new Expressions.Expression(null, DisplaySize.Large);
            ExpressionCursor = new ExpressionCursor(Expression, 0);
            DisplayScale = 1;
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// Create a new instance of the ExpressionDisplay class with an empty expression.
        /// </summary>
        /// <param name="displayScale">The scale to display the expression at.</param>
        public ExpressionDisplay(int displayScale)
        {
            Expression = new Expressions.Expression(null, DisplaySize.Large);
            DisplayScale = displayScale;
            InitializeComponent();
        }

        private void ExpressionDisplay_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ExpressionDisplay_Paint(object sender, PaintEventArgs e)
        {
            Expression.RecalculateDimensions();
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            Bitmap expressionBitmap = new Bitmap(Expression.Width + 3, Expression.Height + 3);
            ExpressionSize = expressionBitmap.Size;
            Graphics expressionGraphics = Graphics.FromImage(expressionBitmap);
            ExpressionCursor.Hotspots.Clear();
            Expression.Paint(expressionGraphics, ExpressionCursor, 1, 1);

            e.Graphics.ScaleTransform(_DisplayScale, _DisplayScale);
            e.Graphics.DrawImage(expressionBitmap, (Width / _DisplayScale - expressionBitmap.Width) / 2, (Height / _DisplayScale - expressionBitmap.Height) / 2);
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
