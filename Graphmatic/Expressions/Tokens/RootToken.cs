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
            set;
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

        public RootToken(Expression parent)
        {
            Parent = parent;
            Base = new Expression(this);
            Power = new Expression(this);
            Children = new Expression[] { Power, Base };
        }

        public RootToken(Expression parent, XElement xml)
        {
            Parent = parent;
            Base = new Expression(this, xml.Element("Base").Elements());
            Power = new Expression(this, xml.Element("Power").Elements());
            Children = new Expression[] { Base, Power };
        }

        public XElement ToXml()
        {
            return new XElement("Root",
                new XElement("Power", Power.ToXml()),
                new XElement("Base", Base.ToXml()));
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            bool skippingRoot = CanSimplifyDisplay(expressionCursor);
            if(!skippingRoot)
                Power.Paint(g, expressionCursor, x, y);
            if (Size == DisplaySize.Small) y += 1;
            int xOffset = skippingRoot ? 1 : Power.Width;
            Base.Paint(g, expressionCursor, x + xOffset + 3, y + Height - Base.Height);

            // draw square-root symbol
            g.DrawLine(Expression.ExpressionPen,
                x + xOffset + 1,
                y + Height - Base.Height - 2,
                x + xOffset + 3 + Base.Width,
                y + Height - Base.Height - 2);

            g.DrawLine(Expression.ExpressionPen,
                x + xOffset + 1,
                y + Height - Base.Height - 2,
                x + xOffset,
                y + Height - 1);

            g.DrawLine(Expression.ExpressionPen,
                x + xOffset,
                y + Height - 1,
                x + xOffset - 2,
                y + Height - 3);
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Base.Size = Size;
            Base.RecalculateDimensions(expressionCursor);
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            if (!CanSimplifyDisplay(expressionCursor))
            {
                Width = Base.Width + Power.Width + 5;
                Height = Base.Height + Power.Height - 3 + (Size == DisplaySize.Small ? 1 : 0);
            }
            else
            {
                Width = Base.Width + 5;
                Height = Base.Height + 2;
            }
        }

        private bool CanSimplifyDisplay(ExpressionCursor expressionCursor)
        {
            return
                expressionCursor.Expression != Power &&
                Power.Count == 1 &&
                Power[0] is DigitToken &&
                (Power[0] as DigitToken).Value == 2;
        }
    }
}
