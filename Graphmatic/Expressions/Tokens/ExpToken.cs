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
    public class ExpToken : Token, ICollectorToken, IParsable
    {
        /// <summary>
        /// Gets the token collection type for this token collector.
        /// </summary>
        public CollectorTokenType CollectorType
        {
            get
            {
                return CollectorTokenType.Weak;
            }
        }

        public override int BaselineOffset
        {
            get
            {
                return (Height - Base.Height) + (Base.Count == 0 ? 0 : Base
                    .Select(token => token.BaselineOffset)
                    .Aggregate((b1, b2) => Math.Max(b1, b2)));
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
                return Base;
            }
        }

        public ExpToken(Expression parent)
            : base(parent)
        {
            Base = new Expression(this);
            Power = new Expression(this);
            Children = new Expression[] { Base, Power };
        }

        public ExpToken(Expression parent, XElement xml)
            : base(parent)
        {
            Base = new Expression(this, xml.Element("Base").Elements());
            Power = new Expression(this, xml.Element("Power").Elements());
            Children = new Expression[] { Base, Power };
        }

        public override XElement ToXml()
        {
            return new XElement("Exp",
                new XElement("Base", Base.ToXml()),
                new XElement("Power", Power.ToXml()));
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            Base.Paint(g, expressionCursor, x, y + Height - Base.Height);
            Power.Paint(g, expressionCursor, x + Base.Width + 1, y);
        }

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Base.Size = Size;
            Base.RecalculateDimensions(expressionCursor);
            Power.Size = DisplaySize.Small;
            Power.RecalculateDimensions(expressionCursor);
            Width = Base.Width + Power.Width + 1;
            Height = Base.Height + Power.Height - (Size == DisplaySize.Small ? 0 : 3);
        }

        public const BinaryEvaluator Evaluator = (powBase, powPower) => Math.Pow(powBase, powPower);
        public ParseTreeNode Parse()
        {
            return new BinaryParseTreeNode(Evaluator, Base.Parse(), Power.Parse());
        }
    }
}
