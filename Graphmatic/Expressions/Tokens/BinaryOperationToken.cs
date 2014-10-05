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

        public BinaryOperation Operation
        {
            get;
            set;
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
            protected set;
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

        public BinaryOperationToken(Expression parent, BinaryOperation operation, DisplaySize size)
        {
            Parent = parent;
            Children = new Expression[] { };
            Operation = operation;
            Size = size;
            RecalculateDimensions();
        }

        public XElement ToXml()
        {
            throw new NotImplementedException();
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Symbols[(int)Operation].ToString(), Size == DisplaySize.Small, x, y);
        }

        public void RecalculateDimensions()
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
