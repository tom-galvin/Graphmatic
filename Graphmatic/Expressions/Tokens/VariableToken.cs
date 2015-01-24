using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions.Tokens
{
    public class VariableToken : SimpleToken, IParsable
    {
        private char _Symbol;
        public char Symbol
        {
            get
            {
                return _Symbol;
            }
        }

        public override string Text
        {
            get
            {
                return _Symbol.ToString();
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public VariableToken(char symbol)
            :base()
        {
            _Symbol = symbol;
        }

        public VariableToken(XElement xml)
            :base()
        {
            string symbolString = xml.Attribute("Symbol").Value;
            if (symbolString.Length == 1)
                _Symbol = symbolString[0];
            else
                throw new NotImplementedException("Variable symbol name must be one letter long (invalid: " + symbolString + ")");
        }

        public ParseTreeNode Parse()
        {
            return new VariableParseTreeNode(Symbol);
        }

        public override XElement ToXml()
        {
            return new XElement("Variable",
                new XAttribute("Symbol", _Symbol.ToString()));
        }
    }
}
