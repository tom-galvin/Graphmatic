using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class SymbolicToken : SimpleToken
    {
        private SymbolicType _Type;
        public SymbolicType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public override string Text
        {
            get
            {
                switch (Type)
                {
                    case SymbolicType.Comma:
                        return ",";
                    case SymbolicType.DecimalPoint:
                        return ".";
                    case SymbolicType.Exp10:
                        return "*~";
                    case SymbolicType.Percent:
                        return "%";
                    default:
                        return "<unknown symbolic>";
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public SymbolicToken(Expression parent, SymbolicType type)
            : base(parent)
        {
            Type = type;
        }

        public SymbolicToken(Expression parent, XElement xml)
            : base(parent)
        {
            string symbolicTypeName = xml.Element("Type").Value;
            if (!Enum.TryParse<SymbolicType>(symbolicTypeName, out _Type))
                throw new NotImplementedException("The symbolic type " + symbolicTypeName + " is not implemented.");
        }

        public override XElement ToXml()
        {
            return new XElement("Symbolic",
                new XAttribute("Type", _Type.ToString()));
        }

        public override double Evaluate(Dictionary<char, double> variables)
        {
            throw new NotImplementedException();
        }

        public enum SymbolicType
        {
            Comma,
            Percent,
            Exp10,
            DecimalPoint
        }
    }
}
