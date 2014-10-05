using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    class LogToken : IToken
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
                return Operand.BaselineOffset + Height - Operand.Height;
            }
        }

        public Expression Operand
        {
            get;
            protected set;
        }

        public Expression Base
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

        public LogToken(Expression parent)
        {
            Parent = parent;
            Operand = new Expression(this);
            Base = new Expression(this);
            Children = new Expression[] { Base, Operand };
        }

        public LogToken(Expression parent, XElement xml)
        {
            Parent = parent;
            Operand = new Expression(this, xml.Element("Operand").Elements());
            Base = new Expression(this, xml.Element("Base").Elements());
            Children = new Expression[] { Base, Operand };
        }

        public XElement ToXml()
        {
            return new XElement("Log",
                new XElement("Base", Base.ToXml()),
                new XElement("Operand", Operand.ToXml()));
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            SimplificationType simplification = GetSimplificationType(expressionCursor);
            string text = simplification == SimplificationType.LogE ? "ln" : "log";
            int xOffset = 6 * text.Length;

            g.DrawPixelString(text, Size == DisplaySize.Small, x, y + Operand.BaselineOffset);
            if (simplification == SimplificationType.None)
            {
                Base.Paint(g, expressionCursor, x + xOffset, y + Base.Height - 6);
            }
            if (simplification == SimplificationType.None) xOffset += 1 + Base.Width;

            // draw bracket lines
            if (Size == DisplaySize.Large)
            {
                Operand.Paint(g, expressionCursor, x + xOffset + 6, y);
                g.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 2,
                    y + 2,
                    x + xOffset + 2,
                    y + Height - 2);
                g.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 2,
                    y + 2,
                    x + xOffset + 4,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 4,
                    y + Height,
                    x + xOffset + 2,
                    y + Height - 2);

                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 9,
                    y + 2,
                    x + Operand.Width + xOffset + 9,
                    y + Height - 2);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 9,
                    y + 2,
                    x + Operand.Width + xOffset + 7,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 7,
                    y + Height,
                    x + Operand.Width + xOffset + 9,
                    y + Height - 2);
            }
            else
            {
                Operand.Paint(g, expressionCursor, x + xOffset + 4, y);
                g.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 1,
                    y + 1,
                    x + xOffset + 1,
                    y + Height - 1);
                g.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 1,
                    y + 1,
                    x + xOffset + 2,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + xOffset + 2,
                    y + Height,
                    x + xOffset + 1,
                    y + Height - 1);

                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 6,
                    y + 1,
                    x + Operand.Width + xOffset + 6,
                    y + Height - 1);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 6,
                    y + 1,
                    x + Operand.Width + xOffset + 5,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + xOffset + 5,
                    y + Height,
                    x + Operand.Width + xOffset + 6,
                    y + Height - 1);
            }
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            SimplificationType simplification = GetSimplificationType(expressionCursor);
            string text = simplification == SimplificationType.LogE ? "ln" : "log";

            Operand.Size = Size;
            Operand.RecalculateDimensions(expressionCursor);
            Base.Size = DisplaySize.Small;
            Base.RecalculateDimensions(expressionCursor);
            Width = Operand.Width + 6 * text.Length + (Size == DisplaySize.Large ? 12 : 9) + (simplification == SimplificationType.None ? 1 + Base.Width : 0);
            Height = Operand.Height;
        }

        private SimplificationType GetSimplificationType(ExpressionCursor expressionCursor)
        {
            if(expressionCursor.Expression != Base)
            {
                if (Base.Count == 2)
                {
                    if (Base[0] is DigitToken && Base[1] is DigitToken)
                    {
                        if ((Base[0] as DigitToken).Value == 1 && (Base[1] as DigitToken).Value == 0)
                        {
                            return SimplificationType.LogTen;
                        }
                    }
                }
            }
            return SimplificationType.None;
        }

        public enum SimplificationType
        {
            None,
            LogTen,
            LogE
        }
    }
}
