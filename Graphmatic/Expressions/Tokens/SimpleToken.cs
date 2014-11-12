using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public abstract class SimpleToken : Token
    {

        public override int BaselineOffset
        {
            get
            {
                return 0;
            }
        }

        public abstract string Text
        {
            get;
            protected set;
        }

        public SimpleToken(Expression parent)
            : base(parent)
        {
        }

        public override void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Text, Size == DisplaySize.Small, x, y);
        }

        public override void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Width = 5 * Text.Length + 1 * (Text.Length - 1);
            Height = Size == DisplaySize.Large ? 9 : 6;
        }
    }
}
