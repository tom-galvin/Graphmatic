using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions.Parsing;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a token whose value is a constant, such as pi or e.
    /// </summary>
    [GraphmaticObject]
    public class ConstantToken : SimpleToken, IParsable
    {
        private ConstantType _Value;
        /// <summary>
        /// Gets or sets the value of the constant represented by this token.
        /// </summary>
        public ConstantType Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// Gets the text displayed by this token.<para/>
        /// This will be the mathematical symbol for this constant; eg. the
        /// italic letter 'e' for Euler's number e, or the greek letter pi for
        /// the ratio of a circle's circumference to its diameter.
        /// </summary>
        public override string Text
        {
            get
            {
                switch (_Value)
                {
                    case ConstantType.E:
                        return "£";
                    case ConstantType.Pi:
                        return "^";
                    default:
                        return "<unknown constant>";
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>ConstantToken</c> class.
        /// </summary>
        /// <param name="value">The value of the constant represented by this token.</param>
        public ConstantToken(ConstantType value)
            : base()
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <c>ConstantToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public ConstantToken(XElement xml)
            : base()
        {
            string constantName = xml.Attribute("Value").Value;
            if (!Enum.TryParse<ConstantType>(constantName, out _Value))
                throw new NotImplementedException("The constant value " + constantName + " is not implemented.");
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("ConstantToken",
                new XAttribute("Value", Value.ToString()));
        }

        /// <summary>
        /// Represents the constant value held by a <c>Graphmatic.Expressions.Tokens.ConstantToken</c> token.
        /// </summary>
        public enum ConstantType
        {
            /// <summary>
            /// Represents the constant E (2.718281828).
            /// </summary>
            E,
            /// <summary>
            /// Represents the constant Pi (3.141592654).
            /// </summary>
            Pi
        }

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeNode</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            switch (Value)
            {
                case ConstantType.E:
                    return new ConstantParseTreeNode(Math.E);
                case ConstantType.Pi:
                    return new ConstantParseTreeNode(Math.PI);
                default:
                    throw new NotImplementedException("The constant value " + Value.ToString() + " is not implemented, and thus has no value.");
            }
        }
    }
}
