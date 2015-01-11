using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Parsing;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction
{
    public partial class Equation : Resource, IPlottable
    {
        public Expression Expression
        {
            get;
            protected set;
        }

        public ParseTreeNode ParseTree
        {
            get;
            set;
        }

        public override string Type
        {
            get
            {
                return "Equation";
            }
        }

        public Equation()
            : this(new Expression(null))
        {
        }

        public Equation(Expression expression)
            : base()
        {
            Expression = expression;
            if(Expression.Count == 0)
                ParseTree = new ConstantParseTreeNode(0);
            else
                ParseTree = Expression.Parse();
        }

        public Equation(XElement xml)
            : base(xml)
        {
            Expression = new Expression(null, xml.Element("Expression").Elements());
            ParseTree = Expression.Parse(true);
        }

        public void Parse()
        {
            ParseTree = Expression.Parse(true);
        }

        public override System.Drawing.Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.Equation32 :
                Properties.Resources.Equation16;
        }

        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Equation";
            baseElement.Add(
                new XElement("Expression", Expression.ToXml()));
            return baseElement;
        }
    }
}
