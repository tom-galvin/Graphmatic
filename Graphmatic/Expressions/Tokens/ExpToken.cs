using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class ExpToken : IToken
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
                return Height - Base.Height;
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

        public ExpToken(Expression parent, DisplaySize size)
        {
            Parent = parent;
            Size = size;
            Base = new Expression(this, size);
            Power = new Expression(this, DisplaySize.Small);
            Children = new Expression[] { Base, Power };
            RecalculateDimensions();
        }

        public XElement ToXml()
        {
            throw new NotImplementedException();
            // return new XE
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Base.Paint(g, expressionCursor, x, y + Height - Base.Height);
            Power.Paint(g, expressionCursor, x + Base.Width + 1, y);
        }

        public void RecalculateDimensions()
        {
            Base.RecalculateDimensions();
            Power.RecalculateDimensions();
            Width = Base.Width + Power.Width + 1;
            Height = Base.Height + Power.Height - (Size == DisplaySize.Small ? 0 : 3);
        }
    }
}
