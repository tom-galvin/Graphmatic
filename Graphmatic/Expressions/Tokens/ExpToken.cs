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
            set;
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

        public ExpToken(Expression parent)
        {
            Parent = parent;
            Base = new Expression(this);
            Power = new Expression(this);
            Children = new Expression[] { Base, Power };
        }

        public ExpToken(Expression parent, XElement xml)
        {
            Parent = parent;
            Base = new Expression(this, xml.Element("Base").Elements());
            Power = new Expression(this, xml.Element("Power").Elements());
            Children = new Expression[] { Base, Power };
        }

        public XElement ToXml()
        {
            return new XElement("Exp",
                new XElement("Base", Base.ToXml()),
                new XElement("Power", Power.ToXml()));
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Base.Paint(g, expressionCursor, x, y + Height - Base.Height);
            Power.Paint(g, expressionCursor, x + Base.Width + 1, y);
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Base.Size = Size;
            Base.RecalculateDimensions(expressionCursor);
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            Width = Base.Width + Power.Width + 1;
            Height = Base.Height + Power.Height - (Size == DisplaySize.Small ? 0 : 3);
        }
    }
}
