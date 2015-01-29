using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a symbol (for example, punctuation) in a Graphmatic expression.
    /// </summary>
    [GraphmaticObject]
    public class SymbolicToken : SimpleToken
    {
        private SymbolicType _Type;
        /// <summary>
        /// Gets or sets the type of symbol that this SymbolicToken represents.
        /// </summary>
        public SymbolicType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        /// <summary>
        /// Gets the text displayed by this token.<para/>
        /// This text will be the symbol denoted by this SymbolicToken.
        /// </summary>
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
                    case SymbolicType.Equals:
                        return "=";
                    default:
                        return "<unknown symbolic>";
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>SymbolicToken</c> class.
        /// </summary>
        /// <param name="type">The type of symbol that this SymbolicToken represents.</param>
        public SymbolicToken(SymbolicType type)
            : base()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <c>SymbolicToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public SymbolicToken(XElement xml)
            : base()
        {
            string symbolicTypeName = xml.Attribute("Type").Value;
            if (!Enum.TryParse<SymbolicType>(symbolicTypeName, out _Type))
                throw new NotImplementedException("The symbolic type " + symbolicTypeName + " is not implemented.");
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("SymbolicToken",
                new XAttribute("Type", _Type.ToString()));
        }

        /// <summary>
        /// Represents some symbolic designation within a Graphmatic expression.
        /// </summary>
        public enum SymbolicType
        {
            /// <summary>
            /// Represents a comma, for example in a list of parameters in a function.
            /// </summary>
            Comma,
            /// <summary>
            /// Represents the percentage symbol.
            /// </summary>
            Percent,
            /// <summary>
            /// Represents the '*10^' symbol denoting the exponent of a small or large literal value.
            /// </summary>
            Exp10,
            /// <summary>
            /// Represents a decimal point in a non-integer literal value.
            /// </summary>
            DecimalPoint,
            /// <summary>
            /// Represents the equals symbol in an equation.
            /// </summary>
            Equals
        }
    }
}
