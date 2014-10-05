using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Represents a mathematical expression, which can be composed of individual mathematical tokens and other sub-expressions, and be evaluated to form a graph.
    /// </summary>
    public class Expression : List<IToken>, IPaintable
    {
        private const int TokenSpacing = 1;
        public static readonly Brush ExpressionBrush = Brushes.Black;
        public static readonly Pen ExpressionPen = new Pen(ExpressionBrush, 1f);

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
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
                if (Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.Select(token => token.BaselineOffset).Aggregate((b1, b2) => Math.Max(b1, b2));
                }
            }
        }

        public IToken Parent
        {
            get;
            protected set;
        }

        public Expression(IToken parent, IEnumerable<XElement> xml)
        {
            Parent = parent;
            AddRange(xml
                .Select(x => TokenDeserializer.FromXml(this, x)));
        }

        public Expression(IToken parent, DisplaySize size)
            : base()
        {
            Parent = parent;
            Size = size;
            RecalculateDimensions();
        }

        public Expression(IToken parent, DisplaySize size, params IToken[] children)
            : this(parent, size)
        {
            AddRange(children);
        }

        public void RecalculateDimensions()
        {
            if (Count == 0)
            {
                Width = 5;
                Height = Size == DisplaySize.Large ? 9 : 6;
            }
            else
            {
                foreach (IToken token in this)
                    token.RecalculateDimensions();

                Width =
                    this.Select(token => token.Width).Aggregate((w1, w2) => w1 + w2) + TokenSpacing * (Count - 1);
                int tokenBaselineOffset = this.Select(token => token.BaselineOffset).Aggregate((b1, b2) => Math.Max(b1, b2));
                Height =
                    this.Select(token => token.Height + (tokenBaselineOffset - token.BaselineOffset)).Aggregate((h1, h2) => Math.Max(h1, h2));
            }
        }

        public void Paint(Graphics g, ExpressionCursor expressionCursor, int x, int y)
        {
            if (Count == 0) // draw box if expression is empty
            {
                if (expressionCursor.Expression == this)
                    g.FillRectangle(Brushes.Blue, x, y, Width - 1, Height - 1);
                else
                    g.DrawRectangle(Pens.Gray, x, y, Width - 1, Height - 1);

                expressionCursor.CreateHotspot(new Rectangle(x, y, Width, Height),
                    (point, cursor) =>
                    {
                        cursor.Expression = this;
                        cursor.Index = 0;
                    });
            }
            else
            {
                if (expressionCursor.Expression == this) // draw yellow background if selected by cursor
                {
                    g.FillRectangle(Brushes.Yellow, x, y, Width + 1, Height + 1);
                }
                int tokenX = x;
                int tokenBaselineOffset = this.Select(token => token.BaselineOffset).Aggregate((b1, b2) => Math.Max(b1, b2));

                for (int i = 0; i <= Count; i++)
                {
                    int cursorX = tokenX - 1;

                    if (i < Count) // as we're also iterating over the end of the array we need to do the check again
                    {
                        IToken token = this[i];
                        int tokenY = y + tokenBaselineOffset - token.BaselineOffset;
                        token.Paint(g, expressionCursor, tokenX, tokenY);

                        int hotspotTokenIndex = i, hotspotTokenWidth = token.Width; // we're making a closure, so create this variable so i doesn't change in the meantime
                        if (!(token is PromptToken)) // don't let the user select prompts
                        {
                            expressionCursor.CreateHotspot(new Rectangle(tokenX, tokenY, token.Width, token.Height),
                                (point, cursor) =>
                                {
                                    cursor.Expression = this;
                                    cursor.Index = hotspotTokenIndex + (point.X >= hotspotTokenWidth / 2 ? 1 : 0);
                                });
                        }
                        tokenX += token.Width + TokenSpacing;
                    }

                    if (expressionCursor.Expression == this && expressionCursor.Index == i) // draw red line at cursor
                    {
                        g.DrawLine(Pens.Blue,
                            cursorX,
                            y + tokenBaselineOffset,
                            cursorX,
                            y + tokenBaselineOffset + (Size == DisplaySize.Small ? 6 : 9) - 1);
                    }
                }
            }
        }

        public IEnumerable<XElement> ToXml()
        {
            foreach (IToken token in this)
                yield return token.ToXml();
        }

        public XElement ToXmlElement()
        {
            return new XElement("Expression",
                ToXml());
        }
    }

    public enum DisplaySize
    {
        Small,
        Large
    }
}
