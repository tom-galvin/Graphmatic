using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Interaction
{
    public class Equation : Resource
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

        public char VerticalVariable
        {
            get;
            set;
        }

        public char HorizontalVariable
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

        public Equation(char vertical, char horizontal)
            : this(vertical, horizontal, new Expression(null))
        {
        }

        public Equation(char vertical, char horizontal, Expression expression)
            : base()
        {
            VerticalVariable = vertical;
            HorizontalVariable = horizontal;
            Expression = expression;
            if(Expression.Count == 0)
                ParseTree = new ConstantParseTreeNode(0);
            else
                ParseTree = Expression.Parse();
        }

        public Equation(XElement xml)
            : base(xml)
        {
            VerticalVariable = Char.Parse(xml.Element("Vertical").Value);
            HorizontalVariable = Char.Parse(xml.Element("Horizontal").Value);
            Expression = new Expression(null, xml.Element("Expression").Elements());
            ParseTree = Expression.Parse();
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
                new XElement("Expression", Expression.ToXml()),
                new XElement("Vertical", VerticalVariable),
                new XElement("Horizontal", HorizontalVariable));
            return baseElement;
        }
    }
}
