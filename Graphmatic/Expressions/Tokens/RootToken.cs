using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class RootToken : IToken
    {

        public int Width
        {
            get;
            protected set;
        }

        public int Height
        {
            get;
            protected set;
        }

        public DisplaySize Size
        {
            get;
            protected set;
        }

        public int BaselineOffset
        {
            get
            {
                return Base.BaselineOffset + Height - Base.Height;
            }
        }

        public Expression Base
        {
            get;
            protected set;
        }

        public Expression Power
        {
            get;
            protected set;
        }

        public Expression Parent
        {
            get;
            protected set;
        }

        public Expression[] Children
        {
            get;
            protected set;
        }

        public RootToken(Expression parent, DisplaySize size)
        {
            Parent = parent;
            Size = size;
            Base = new Expression(this, size);
            Power = new Expression(this, DisplaySize.Small);
            Children = new Expression[] { Power, Base };
            RecalculateDimensions();
        }

        public XElement ToXml()
        {
            throw new NotImplementedException();
            // return new XE
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Power.Paint(g, expressionCursor, x, y);
            if (Size == DisplaySize.Small) y += 1;
            Base.Paint(g, expressionCursor, x + Power.Width + 3, y + Height - Base.Height);

            // draw square-root symbol
            g.DrawLine(Expression.ExpressionPen,
                x + Power.Width + 1,
                y + Height - Base.Height - 2,
                x + Power.Width + 3 + Base.Width,
                y + Height - Base.Height - 2);

            g.DrawLine(Expression.ExpressionPen,
                x + Power.Width + 1,
                y + Height - Base.Height - 2,
                x + Power.Width,
                y + Height - 1);

            g.DrawLine(Expression.ExpressionPen,
                x + Power.Width,
                y + Height - 1,
                x + Power.Width - 2,
                y + Height - 3);
        }

        public void RecalculateDimensions()
        {
            Base.RecalculateDimensions();
            Power.RecalculateDimensions();
            Width = Base.Width + Power.Width + 5;
            Height = Base.Height + Power.Height - 3 + (Size == DisplaySize.Small ? 1 : 0);
        }
    }
}
