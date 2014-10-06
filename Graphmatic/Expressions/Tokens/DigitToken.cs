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
            set;
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

        public Expression DefaultChild
        {
            get
            {
                return null;
            }
        }

        public DigitToken(Expression parent, int value)
        {
            Parent = parent;
            Children = new Expression[] { };
            Value = value;
        }

        public DigitToken(Expression parent, XElement xml)
        {
            Parent = parent;
            Value = Int32.Parse(xml.Element("Value").Value);
            Children = new Expression[] { };
        }

        public XElement ToXml()
        {
            return new XElement("Digit",
                new XAttribute("Value", Value.ToString()));
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Value.ToString(), Size == DisplaySize.Small, x, y);
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Width = 5;
            Height = Size == DisplaySize.Large ? 9 : 6;
        }
    }
}
