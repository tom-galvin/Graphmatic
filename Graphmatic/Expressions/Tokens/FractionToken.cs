using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class FractionToken : IToken
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
                return Top.Height + 1 - (Size == DisplaySize.Large ? 4 : 3);
            }
        }

        public Expression Top
        {
            get;
            protected set;
        }

        public Expression Bottom
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

        public FractionToken(Expression parent, DisplaySize size)
        {
            Parent = parent;
            Size = size;
            Top = new Expression(this, DisplaySize.Small);
            Bottom = new Expression(this, DisplaySize.Small);
            Children = new Expression[] { Top, Bottom };
            RecalculateDimensions();
        }

        public XElement ToXml()
        {
            throw new NotImplementedException();
            // return new XE
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Top.Paint(g, expressionCursor, x + (Width - Top.Width) / 2, y);
            Bottom.Paint(g, expressionCursor, x + (Width - Bottom.Width) / 2, y + Top.Height + 3);
            g.DrawLine(Expression.ExpressionPen,
                x,
                y + Top.Height + 1,
                x + Width - 1,
                y + Top.Height + 1);
        }

        public void RecalculateDimensions()
        {
            Top.RecalculateDimensions();
            Bottom.RecalculateDimensions();
            Width = Math.Max(Top.Width, Bottom.Width);
            Height = Top.Height + Bottom.Height + 3;
        }
    }
}
