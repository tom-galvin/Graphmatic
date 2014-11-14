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
    public class AbsoluteToken : FunctionToken, IParsable
    {
        public AbsoluteToken(Expression parent)
            : base(parent, "Abs")
        {
        }

        public AbsoluteToken(Expression parent, XElement xml)
            : base(parent, xml)
        {

        }

        public override XElement ToXml()
        {
            return new XElement("Absolute",
                new XElement("Operand", Operand.ToXml()));
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Operand.Paint(g, expressionCursor, x + 2, y);
            g.DrawLine(Expression.ExpressionPen,
                x,
                y,
                x,
                y + Height - 1);

            g.DrawLine(Expression.ExpressionPen,
                x + Operand.Width + 3,
                y,
                x + Operand.Width + 3,
                y + Height - 1);
        }

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Operand.Size = Size;
            Operand.RecalculateDimensions(expressionCursor);
            Width = Operand.Width + 4;
            Height = Operand.Height;
        }

        public static readonly UnaryEvaluator Evaluator = x => Math.Abs(x);

        public override ParseTreeNode Parse()
        {
            return new UnaryParseTreeNode(Evaluator, Operand.Parse());
        }
    }
}
