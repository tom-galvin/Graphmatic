using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class BinaryOperationToken : IToken
    {
        private static char[] Symbols = { '+', '-', '*', '/' };

        public BinaryOperation _Operation;
        public BinaryOperation Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

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

        public BinaryOperationToken(Expression parent, BinaryOperation operation)
        {
            Parent = parent;
            Children = new Expression[] { };
            Operation = operation;
        }

        public BinaryOperationToken(Expression parent, XElement xml)
        {
            Parent = parent;
            string operationName = xml.Element("Operation").Value;
            if(!Enum.TryParse<BinaryOperation>(operationName, out _Operation))
                throw new NotImplementedException("The operation " + operationName + " is not implemented.");
            Children = new Expression[] { };
        }

        public XElement ToXml()
        {
            return new XElement("BinaryOperation",
                new XAttribute("Operation", Operation.ToString()));
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Symbols[(int)Operation].ToString(), Size == DisplaySize.Small, x, y);
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Width = 5;
            Height = Size == DisplaySize.Large ? 9 : 6;
        }
    }

    public enum BinaryOperation
    {
        Add = 0,
        Subtract = 1,
        Multiply = 2,
        Divide = 3
    }
}
