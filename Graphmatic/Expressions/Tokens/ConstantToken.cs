using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class ConstantToken : IToken
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

        private ConstantType _Value;
        public ConstantType Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public string Text
        {
            get
            {
                switch (_Value)
                {
                    case ConstantType.E:
                        return "£";
                    case ConstantType.Pi:
                        return "^";
                    default:
                        return "0";
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

        public ConstantToken(Expression parent, ConstantType value)
        {
            Parent = parent;
            Children = new Expression[] { };
            Value = value;
        }

        public ConstantToken(Expression parent, XElement xml)
        {
            Parent = parent;
            string constantName = xml.Element("Value").Value;
            if(Enum.TryParse<ConstantType>(constantName, out _Value))
                throw new NotImplementedException("The operation " + constantName + " is not implemented.");
            Children = new Expression[] { };
        }

        public XElement ToXml()
        {
            return new XElement("Constant",
                new XAttribute("Value", Value.ToString()));
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Text, Size == DisplaySize.Small, x, y);
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Width = 5 * Text.Length + 1 * (Text.Length - 1);
            Height = Size == DisplaySize.Large ? 9 : 6;
        }

        public enum ConstantType
        {
            E,
            Pi
        }
    }
}
