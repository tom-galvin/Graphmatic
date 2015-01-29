using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents the value of a variable in an expression.
    /// </summary>
    [GraphmaticObject]
    public class VariableToken : SimpleToken, IParsable
    {
        /// <summary>
        /// Gets or sets the symbolic name of the variable represented by this VariableToken.
        /// </summary>
        public char Symbol
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the text displayed by this token.<para/>
        /// This text will be the name of the variable represented by this VariableToken.
        /// </summary>
        public override string Text
        {
            get
            {
                return Symbol.ToString();
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>VariableToken</c> class.
        /// </summary>
        /// <param name="symbol">The single-character symbolic name of the variable represented by this VariableToken.</param>
        public VariableToken(char symbol)
            :base()
        {
            Symbol = symbol;
        }

        /// <summary>
        /// Initializes a new instance of the <c>VariableToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public VariableToken(XElement xml)
            : base()
        {
            string symbolString = xml.Attribute("Symbol").Value;
            if (symbolString.Length == 1)
                Symbol = symbolString[0];
            else
                throw new NotImplementedException("Variable symbol name must be one letter long (invalid: " + symbolString + ")");
        }

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            return new VariableParseTreeNode(Symbol);
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("VariableToken",
                new XAttribute("Symbol", Symbol.ToString()));
        }
    }
}
