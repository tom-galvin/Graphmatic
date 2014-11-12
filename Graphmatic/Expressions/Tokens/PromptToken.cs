﻿using System;
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

        public Expression Content
        {
            get;
            protected set;
        }

        public override Expression DefaultChild
        {
            get
            {
                return Content;
            }
        }

        public PromptToken(Expression parent, string text)
            : base(parent)
        {
            Text = text;
            Content = new Expression(this);
            Children = new Expression[] { Content };
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
