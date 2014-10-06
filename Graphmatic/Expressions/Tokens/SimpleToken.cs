using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public abstract class SimpleToken : IToken
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
                return 0;
            }
        }

        public abstract string Text
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

        public Expression DefaultChild
        {
            get
            {
                return null;
            }
        }

        public SimpleToken(Expression parent)
        {
            Parent = parent;
            Children = new Expression[] { };
        }

        public abstract XElement ToXml();

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            g.DrawPixelString(Text, Size == DisplaySize.Small, x, y);
        }

        public void RecalculateDimensions(ExpressionCursor expressionCursor)
        {
            Width = 5 * Text.Length + 1 * (Text.Length - 1);
            Height = Size == DisplaySize.Large ? 9 : 6;
        }
    }
}
