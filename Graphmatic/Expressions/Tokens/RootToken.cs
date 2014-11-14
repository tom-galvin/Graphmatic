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
    public class RootToken : Token, IParsable
    {

        public override int BaselineOffset
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

        public override Expression DefaultChild
        {
            get
            {
                return Simplified ? Base : Power;
            }
        }

        public RootToken(Expression parent)
            : base(parent)
        {
            Base = new Expression(this);
            Power = new Expression(this);
            Children = new Expression[] { Power, Base };
        }

        public RootToken(Expression parent, XElement xml)
            : base(parent)
        {
            Base = new Expression(this, xml.Element("Base").Elements());
            Power = new Expression(this, xml.Element("Power").Elements());
            Children = new Expression[] { Base, Power };
        }

        public override XElement ToXml()
        {
            return new XElement("Root",
                new XElement("Power", Power.ToXml()),
                new XElement("Base", Base.ToXml()));
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            if (!Simplified)
                Power.Paint(g, expressionCursor, x, y);
            int xOffset = Simplified ? 2 : Power.Width;
            Base.Paint(g, expressionCursor, x + xOffset + 3, y + Height - Base.Height);

            // draw square-root symbol
            g.DrawLine(Expression.ExpressionPen,
                x + xOffset + 1,
                y + Height - Base.Height - 2,
                x + xOffset + 2 + Base.Width,
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

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Simplified = CanSimplifyDisplay(expressionCursor);
            Base.Size = Size;
            Base.RecalculateDimensions(expressionCursor);
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            if (!Simplified)
            {
                Width = Base.Width + Power.Width + 5;
                Height = Base.Height + Power.Height - 3;
            }
            else
            {
                Width = Base.Width + 5;
                Height = Base.Height + 2;
            }
        }

        private bool Simplified;
        private bool CanSimplifyDisplay(ExpressionCursor expressionCursor)
        {
            return
                expressionCursor.Expression != Power &&
                Power.Count == 1 &&
                Power[0] is DigitToken &&
                (Power[0] as DigitToken).Value == 2;
        }

        public const BinaryEvaluator Evaluator = (rootBase, rootPower) => Math.Pow(rootBase, 1 / rootPower);

        public ParseTreeNode Parse()
        {
            return new BinaryParseTreeNode(Evaluator, Base.Parse(), Power.Parse());
        }
    }
}
