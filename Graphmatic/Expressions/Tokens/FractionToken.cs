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
    public class FractionToken : Token, ICollectorToken, IParsable
    {
        /// <summary>
        /// Gets the token collection type for this token collector.
        /// </summary>
        public CollectorTokenType CollectorType
        {
            get
            {
                return CollectorTokenType.Strong;
            }
        }

        public override int BaselineOffset
        {
            get
            {
                return Top.Height + 1 - (Size == DisplaySize.Large ? 4 : 3);
            }
        }

        public Expression Top
        {
            get;
            protected set;
        }

        public Expression Bottom
        {
            get;
            protected set;
        }

        public override Expression DefaultChild
        {
            get
            {
                return Top;
            }
        }

        public FractionToken()
            : base()
        {
            Top = new Expression(this);
            Bottom = new Expression(this);
            Children = new Expression[] { Top, Bottom };
        }

        public FractionToken(XElement xml)
            : base()
        {
            Top = new Expression(this, xml.Element("Top").Elements());
            Bottom = new Expression(this, xml.Element("Bottom").Elements());
            Children = new Expression[] { Top, Bottom };
        }

        public override XElement ToXml()
        {
            return new XElement("Fraction",
                new XElement("Top", Top.ToXml()),
                new XElement("Bottom", Bottom.ToXml()));
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Top.Paint(g, expressionCursor, x + (Width - Top.Width) / 2, y);
            Bottom.Paint(g, expressionCursor, x + (Width - Bottom.Width) / 2, y + Top.Height + 3);
            g.DrawLine(Expression.ExpressionPen,
                x,
                y + Top.Height + 1,
                x + Width - 1,
                y + Top.Height + 1);
        }

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Top.Size = Bottom.Size = DisplaySize.Small;
            Top.RecalculateDimensions(expressionCursor);
            Bottom.RecalculateDimensions(expressionCursor);
            Width = Math.Max(Top.Width, Bottom.Width);
            Height = Top.Height + Bottom.Height + 3;
        }

        public static readonly BinaryEvaluator Evaluator = new BinaryEvaluator((fracTop, fracBottom) => fracTop / fracBottom, "({0})/({1})");
        public ParseTreeNode Parse()
        {
            return new BinaryParseTreeNode(Evaluator, Top.Parse(), Bottom.Parse());
        }
    }
}
