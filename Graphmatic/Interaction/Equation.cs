using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions;

namespace Graphmatic.Interaction
{
    public class Equation : IXmlConvertible
    {
        public Expression Expression
        {
            get;
            protected set;
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
            : this(plotted, varying, new Expression(null))
        {

        }

        public Equation(char plotted, char varying, Expression expression)
        {
            PlottedVariable = plotted;
            VaryingVariable = varying;
            Expression = expression;
        }

        public Equation(XElement xml)
            : this(Char.Parse(xml.Element("Plotted").Value), Char.Parse(xml.Element("Varying").Value), new Expression(null, xml.Element("Expression").Elements()))
        {

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
