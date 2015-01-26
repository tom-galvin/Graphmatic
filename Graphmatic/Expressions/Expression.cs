using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Represents a mathematical expression, which can be composed of individual mathematical tokens and other sub-expressions, and be evaluated to form a graph.
    /// </summary>
    public partial class Expression : List<Token>, IPaintable
    {
        /// <summary>
        /// Represents the pixel spacing between tokens in an expression.
        /// </summary>
        private const int TokenSpacing = 1;

        /// <summary>
        /// The brush with which to fill expression components (including tokens).
        /// </summary>
        public static readonly Brush ExpressionBrush = Brushes.Black;

        /// <summary>
        /// The pen with which to draw expression components (including tokens).
        /// </summary>
        public static readonly Pen ExpressionPen = new Pen(ExpressionBrush, 1f);

        /// <summary>
        /// The width of the expression, in pixels.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// The height of the expression, in pixels.
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display size style (large or small) of the drawable element.
        /// </summary>
        public DisplaySize Size
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the vertical ascension of the expression from the top of the container.
        /// </summary>
        public int BaselineOffset
        {
            get
            {
                if (Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.Select(token => token.BaselineOffset).Aggregate((b1, b2) => Math.Max(b1, b2));
                }
            }
        }

        /// <summary>
        /// Gets the parent token within which this expression is contained as a child expression.
        /// </summary>
        public Token Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the Graphmatic.Expressions.Expression class, with the given parent token and using the given XML elements as children.
        /// </summary>
        /// <param name="parent">The parent token of this expression.</param>
        /// <param name="xml">XML elements to deserialise and use as children.</param>
        public Expression(Token parent, IEnumerable<XElement> xml)
            : this(parent)
        {
            AddRange(xml
                .Select(x => x.Deserialize<Token>(SerializationExtensionMethods.TokenName)));
        }

        /// <summary>
        /// Adds a token to the given position in the Expression.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="token"/> should be inserted.</param>
        /// <param name="token">The token to add to the Expression.</param>
        public new void Insert(int index, Token token)
        {
            token.Parent = this;
            base.Insert(index, token);
        }

        /// <summary>
        /// Adds a token to the given position in the Expression.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="token"/> should be inserted.</param>
        /// <param name="tokens">The tokens to add to the Expression.</param>
        public new void InsertRange(int index, IEnumerable<Token> tokens)
        {
            foreach (Token token in tokens.Reverse()) // in reverse order, so they are correct after all are inserted at the same index
            {
                Insert(index, token);
            }
        }

        /// <summary>
        /// Adds a token to the end of the Expression.
        /// </summary>
        /// <param name="token">The token to add to the end of the Expression.</param>
        public new void Add(Token token)
        {
            token.Parent = this;
            base.Add(token);
        }

        /// <summary>
        /// Adds an enumerable collection of tokens to the end of the Expression.
        /// </summary>
        /// <param name="tokens">The tokens to add to the end of the Expression.</param>
        public new void AddRange(IEnumerable<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                Add(token);
            }
        }

        /// <summary>
        /// Initializes a new instance of the Graphmatic.Expressions.Expression class, with the given parent token.
        /// </summary>
        /// <param name="parent">The parent token of this expression.</param>
        public Expression(Token parent)
            : base()
        {
            Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the Graphmatic.Expressions.Expression class, with the given parent token and using the given child tokens.
        /// </summary>
        /// <param name="parent">The parent token of this expression.</param>
        /// <param name="children">The child tokens contained within this expression.</param>
        public Expression(Token parent, params Token[] children)
            : this(parent)
        {
            AddRange(children);
        }

        /// <summary>
        /// Recalculates the painted dimensions of this expression, using the dimensions of the child tokens contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            if (Count == 0)
            {
                Width = 5;
                Height = Size == DisplaySize.Large ? 9 : 6;
            }
            else
            {
                foreach (Token token in this)
                {
                    token.Size = Size;
                    token.RecalculateDimensions(expressionCursor);
                }

                Width =
                    this.Select(token => token.Width).Aggregate((w1, w2) => w1 + w2) + TokenSpacing * (Count - 1);
                int tokenBaselineOffset = this.Select(token => token.BaselineOffset).Aggregate((b1, b2) => Math.Max(b1, b2));
                Height =
                    this.Select(token => token.Height + (tokenBaselineOffset - token.BaselineOffset)).Aggregate((h1, h2) => Math.Max(h1, h2));
            }
        }

        /// <summary>
        /// Paints the current expression onto the given GDI+ drawing surface at the specified lodation.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to draw onto.</param>
        /// <param name="expressionCursor">The expression cursor to draw inside the expression.</param>
        /// <param name="x">The X co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        /// <param name="y">The y co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        public void Paint(Graphics graphics, ExpressionCursor expressionCursor, int x, int y)
        {
            if (Count == 0) // draw box if expression is empty
            {
                if (expressionCursor.Expression == this && expressionCursor.Visible)
                    graphics.FillRectangle(Brushes.Blue, x, y, Width, Height);
                else
                    graphics.DrawRectangle(Pens.Gray, x, y, Width - 1, Height - 1);

                expressionCursor.CreateHotspot(new Rectangle(x, y, Width, Height),
                    (point, cursor) =>
                    {
                        cursor.Expression = this;
                        cursor.Index = 0;
                    });
            }
            else
            {
                if (expressionCursor.Visible && expressionCursor.Expression == this) // draw yellow background if selected by cursor
                {
                    graphics.FillRectangle(Brushes.Yellow, x, y, Width + 1, Height + 1);
                }
                int tokenX = x;
                int tokenBaselineOffset = this.Select(token => token.BaselineOffset).Aggregate((b1, b2) => Math.Max(b1, b2));

                for (int i = 0; i <= Count; i++)
                {
                    int cursorX = tokenX - 1;

                    if (i < Count) // as we're also iterating over the end of the array we need to do the check again
                    {
                        Token token = this[i];
                        int tokenY = y + tokenBaselineOffset - token.BaselineOffset;
                        token.Paint(graphics, expressionCursor, tokenX, tokenY);

                        int hotspotTokenIndex = i, hotspotTokenWidth = token.Width; // we're making a closure, so create this variable so i doesn't change in the meantime
                        if (!(token is PromptToken)) // don't let the user select prompts
                        {
                            expressionCursor.CreateHotspot(new Rectangle(tokenX, tokenY, token.Width, token.Height),
                                (point, cursor) => // the closure responsible for the creation of the necessary temporary variable
                                {
                                    cursor.Expression = this;
                                    cursor.Index = hotspotTokenIndex + (point.X >= hotspotTokenWidth / 2 ? 1 : 0);
                                });
                        }
                        tokenX += token.Width + TokenSpacing;
                    }

                    if (expressionCursor.Visible && expressionCursor.Expression == this && expressionCursor.Index == i) // draw red line at cursor
                    {
                        graphics.DrawLine(Pens.Blue,
                            cursorX,
                            y + tokenBaselineOffset,
                            cursorX,
                            y + tokenBaselineOffset + (Size == DisplaySize.Small ? 6 : 9) - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Converts the content of this Expression to an enumerable sequence of XML elements.
        /// </summary>
        /// <returns>An enumerable sequence of XML elements containing this Expression's child tokens.</returns>
        public IEnumerable<XElement> ToXml()
        {
            foreach (Token token in this)
                yield return token.ToXml();
        }
    }
}
