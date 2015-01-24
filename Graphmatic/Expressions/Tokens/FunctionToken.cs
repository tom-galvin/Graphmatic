using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions.Tokens
{
    public class FunctionToken : Token, IParsable
    {
        public static readonly Dictionary<string, UnaryEvaluator> Evaluators = new Dictionary<string, UnaryEvaluator>()
        {
            { "sin", new UnaryEvaluator(x => Math.Sin(x), "sin({0})") },
            { "cos", new UnaryEvaluator(x => Math.Cos(x), "cos({0})") },
            { "tan", new UnaryEvaluator(x => Math.Tan(x), "tan({0})") },
            { "sin`", new UnaryEvaluator(x => Math.Asin(x), "arcsin({0})") },
            { "cos`", new UnaryEvaluator(x => Math.Acos(x), "arccos({0})") },
            { "tan`", new UnaryEvaluator(x => Math.Atan(x), "arctan({0})") },
            { "", new UnaryEvaluator(x => x, "({0})") },
        };

        public override int BaselineOffset
        {
            get
            {
                return Operand.BaselineOffset;
            }
        }

        public string Text
        {
            get;
            set;
        }

        public Expression Operand
        {
            get;
            protected set;
        }

        public override Expression DefaultChild
        {
            get
            {
                return Operand;
            }
        }

        public FunctionToken(string text)
            : base()
        {
            Text = text;
            Operand = new Expression(this);
            Children = new Expression[] { Operand };
        }

        public FunctionToken(XElement xml)
            : base()
        {
            Text = xml.Attribute("Name").Value;
            Operand = new Expression(this, xml.Element("Operand").Elements());
            Children = new Expression[] { Operand };
        }

        public override XElement ToXml()
        {
            return new XElement("Function",
                new XAttribute("Name", Text),
                new XElement("Operand", Operand.ToXml()));
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Text, Size == DisplaySize.Small, x, y + Operand.BaselineOffset);

            // draw bracket lines
            if (Size == DisplaySize.Large)
            {
                Operand.Paint(g, expressionCursor, x + 6 * Text.Length + 6, y);
                g.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 2,
                    y + 2,
                    x + 6 * Text.Length + 2,
                    y + Height - 2);
                g.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 2,
                    y + 2,
                    x + 6 * Text.Length + 4,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 4,
                    y + Height,
                    x + 6 * Text.Length + 2,
                    y + Height - 2);

                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + 2,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + Height - 2);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + 2,
                    x + Operand.Width + 6 * Text.Length + 7,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 7,
                    y + Height,
                    x + Operand.Width + 6 * Text.Length + 9,
                    y + Height - 2);
            }
            else
            {
                Operand.Paint(g, expressionCursor, x + 6 * Text.Length + 4, y);
                g.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 1,
                    y + 1,
                    x + 6 * Text.Length + 1,
                    y + Height - 1);
                g.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 1,
                    y + 1,
                    x + 6 * Text.Length + 2,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + 6 * Text.Length + 2,
                    y + Height,
                    x + 6 * Text.Length + 1,
                    y + Height - 1);

                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + 1,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + Height - 1);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + 1,
                    x + Operand.Width + 6 * Text.Length + 5,
                    y);
                g.DrawLine(Expression.ExpressionPen,
                    x + Operand.Width + 6 * Text.Length + 5,
                    y + Height,
                    x + Operand.Width + 6 * Text.Length + 6,
                    y + Height - 1);
            }
        }

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Operand.Size = Size;
            Operand.RecalculateDimensions(expressionCursor);
            Width = Operand.Width + 6 * Text.Length + (Size == DisplaySize.Large ? 12 : 9);
            Height = Operand.Height;
        }

        public virtual ParseTreeNode Parse()
        {
            return new UnaryParseTreeNode(Evaluators[Text], Operand.Parse());
        }
    }
}
