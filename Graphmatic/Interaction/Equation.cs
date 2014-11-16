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

        public char PlottedVariable
        {
            get;
            set;
        }

        public char VaryingVariable
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

        public override bool Hidden
        {
            get
            {
                return false;
            }
        }

        public Equation(char plotted, char varying)
            : base()
        {
            PlottedVariable = plotted;
            VaryingVariable = varying;
            Expression = new Expression(null);
            ParseTree = new ConstantParseTreeNode(0);
        }

        public Equation(char plotted, char varying, Expression expression)
            : base()
        {
            PlottedVariable = plotted;
            VaryingVariable = varying;
            Expression = expression;
            ParseTree = Expression.Parse();
        }

        public Equation(XElement xml)
            : base(xml)
        {
            PlottedVariable = Char.Parse(xml.Element("Plotted").Value);
            VaryingVariable = Char.Parse(xml.Element("Varying").Value);
            Expression = new Expression(null, xml.Element("Expression").Elements());
            ParseTree = Expression.Parse();
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
                new XElement("Plotted", PlottedVariable),
                new XElement("Varying", VaryingVariable));
            return baseElement;
        }
    }
}
