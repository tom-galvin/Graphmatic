using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class AbsoluteToken : FunctionToken
    {
        public AbsoluteToken(Expression parent)
            : base(parent, "Abs")
        {
        }

        public AbsoluteToken(Expression parent, XElement xml)
            :base(parent, xml)
        {

        }

        public override XElement ToXml()
        {
            return new XElement("Absolute",
                new XElement("Operand", Operand.ToXml()));
        }

        public virtual void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Operand.Paint(g, expressionCursor, x + 1, y);
            g.DrawLine(Expression.ExpressionPen,
                x,
                y,
                x,
                y + Height);

            g.DrawLine(Expression.ExpressionPen,
                x + Operand.Width + 2,
                y,
                x + Operand.Width + 2,
                y + Height);
        }

        public virtual void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Operand.Size = Size;
            Operand.RecalculateDimensions(expressionCursor);
            Width = Operand.Width + 3;
            Height = Operand.Height;
        }
    }
}
