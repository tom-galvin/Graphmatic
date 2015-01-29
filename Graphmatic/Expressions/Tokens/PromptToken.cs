using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a token showing some prompt in the ExpressionDisplay interface. This is not a member of an expression
    /// but rather just a prompt in the user interface.
    /// </summary>
    [GraphmaticObject]
    public class PromptToken : Token
    {
        /// <summary>
        /// Gets the vertical ascension of the token from the top of the container.
        /// </summary>
        public override int BaselineOffset
        {
            get
            {
                return Content.BaselineOffset;
            }
        }

        /// <summary>
        /// Gets the text displayed by this token.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        private Expression _Content;
        /// <summary>
        /// Gets the Expression containing the content of this prompt's value.<para/>
        /// This isn't associated with the prompt text, but is just used to make sure the prompt text is displayed alongside the
        /// content of the prompt.
        /// </summary>
        public Expression Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                Children = new Expression[] { _Content };
            }
        }

        /// <summary>
        /// Gets the default child expression that the cursor is placed into when the token is inserted, or null to place the cursor after the expression.
        /// </summary>
        public override Expression DefaultChild
        {
            get
            {
                return Content;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>PromptToken</c> class, with the given text and content.
        /// </summary>
        /// <param name="text">The text displayed on the visual prompt.</param>
        /// <param name="content">The content of the prompt display.</param>
        public PromptToken(string text, Expression content)
            : base()
        {
            Text = text;
            Content = content;
            Content.Parent = this;
        }

        /// <summary>
        /// Initializes a new instance of the <c>PromptToken</c> class, with the given text.
        /// </summary>
        /// <param name="text">The text displayed on the visual prompt.</param>
        public PromptToken(string text)
            : base()
        {
            Text = text;
            Content = new Expression(this);
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.<para/>
        /// <c>PromptToken</c>s cannot be serialized, so this method is guaranteed to throw a <c>NotImplementedException</c>.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            throw new NotImplementedException("Cannot convert prompt token to XML.");
        }

        /// <summary>
        /// Paint this token onto the specified GDI+ drawing surface.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to draw this token onto.</param>
        /// <param name="expressionCursor">The expression cursor to draw inside the token.</param>
        /// <param name="x">The X co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        /// <param name="y">The y co-ordinate of where to draw on <paramref name="graphics"/>.</param>
        public override void Paint(Graphics graphics, ExpressionCursor expressionCursor, int x, int y)
        {
            graphics.DrawExpressionString(Text, Size == DisplaySize.Small, x, y + Content.BaselineOffset);

            // draw bracket lines
            if (Size == DisplaySize.Large)
            {
                Content.Paint(graphics, expressionCursor, x + 6 * Text.Length + 1, y);
            }
            else
            {
                Content.Paint(graphics, expressionCursor, x + 6 * Text.Length + 1, y);
            }
        }

        /// <summary>
        /// Recalculates the painted dimensions of this token, using the dimensions of the child tokens and expressions contained within it and the expression cursor to use.
        /// </summary>
        /// <param name="expressionCursor">The expression cursor to use, for calculating sizes depending on the location of the cursor.
        /// <para/>
        /// For example, moving the cursor into the base of a <c>log</c> token with current base <c>e</c> will expand the token from <c>ln(...)</c> to <c>log_e(...)</c>.</param>
        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Content.Size = Size;
            Content.RecalculateDimensions(expressionCursor);
            Width = Content.Width + 6 * Text.Length + 1;
            Height = Content.Height;
        }
    }
}
