using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class PromptToken : Token
    {
        public override int BaselineOffset
        {
            get
            {
                return Content.BaselineOffset;
            }
        }

        public string Text
        {
            get;
            set;
        }

        private Expression _Content;
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

        public override Expression DefaultChild
        {
            get
            {
                return Content;
            }
        }

        public PromptToken(string text, Expression child)
            : base()
        {
            Text = text;
            Content = child;
            Content.Parent = this;
        }

        public PromptToken(string text)
            : base()
        {
            Text = text;
            Content = new Expression(this);
        }

        public override XElement ToXml()
        {
            throw new NotImplementedException("Cannot convert prompt token to XML.");
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Text, Size == DisplaySize.Small, x, y + Content.BaselineOffset);

            // draw bracket lines
            if (Size == DisplaySize.Large)
            {
                Content.Paint(g, expressionCursor, x + 6 * Text.Length + 1, y);
            }
            else
            {
                Content.Paint(g, expressionCursor, x + 6 * Text.Length + 1, y);
            }
        }

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Content.Size = Size;
            Content.RecalculateDimensions(expressionCursor);
            Width = Content.Width + 6 * Text.Length + 1;
            Height = Content.Height;
        }
    }
}
