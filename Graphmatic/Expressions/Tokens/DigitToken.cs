using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class DigitToken : IToken
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
                return 0;
            }
        }

        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value >= 0 && value <= 9)
                {
                    _Value = value;
                    RecalculateDimensions();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Digit must be between 0 and 9.");
                }
            }
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

        public DigitToken(Expression parent, int value, DisplaySize size)
        {
            Parent = parent;
            Children = new Expression[] { };
            Value = value;
            Size = size;
            RecalculateDimensions();
        }

        public XElement ToXml()
        {
            throw new NotImplementedException();
            // return new XE
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Value.ToString(), Size == DisplaySize.Small, x, y);
        }

        public void RecalculateDimensions()
        {
            Width = 5;
            Height = Size == DisplaySize.Large ? 9 : 6;
        }
    }
}
