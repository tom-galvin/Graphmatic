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
    public class Equation : IXmlConvertible
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

        public Equation(char plotted, char varying)
        {
            PlottedVariable = plotted;
            VaryingVariable = varying;
            Expression = new Expression(null);
            ParseTree = new ConstantParseTreeNode(0);
        }

        public Equation(char plotted, char varying, Expression expression)
        {
            PlottedVariable = plotted;
            VaryingVariable = varying;
            Expression = expression;
            ParseTree = Expression.Parse();
        }

        public Equation(XElement xml)
            : this(Char.Parse(xml.Element("Plotted").Value), Char.Parse(xml.Element("Varying").Value), new Expression(null, xml.Element("Expression").Elements()))
        {
            ParseTree = Expression.Parse();
        }

        public XElement ToXml()
        {
            return new XElement("Equation",
                new XElement("Expression", Expression.ToXml()),
                new XElement("Plotted", PlottedVariable),
                new XElement("Varying", VaryingVariable));
        }
    }
}
