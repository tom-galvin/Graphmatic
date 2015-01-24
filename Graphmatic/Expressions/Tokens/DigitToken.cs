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
    /// Represents a token whose value is a single digit.
    /// </summary>
    public class DigitToken : SimpleToken
    {
        private int _Value;
        /// <summary>
        /// Gets or sets the digital value of this DigitToken.<para/>
        /// This must be between 0 or 9 inclusive. If not, an ArgumentOutOfRangeException is thrown.
        /// </summary>
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value >= 0 && value <= 9)
                {
                    _Value = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Digit must be between 0 and 9.");
                }
            }
        }

        /// <summary>
        /// Gets the text displayed by this token.<para/>
        /// This will be the digit value of this DigitToken.
        /// </summary>
        public override string Text
        {
            get
            {
                return _Value.ToString();
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>DigitToken</c> class.
        /// </summary>
        /// <param name="value">The digit held by this token.</param>
        public DigitToken(int value)
            : base()
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <c>DigitToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public DigitToken(XElement xml)
            : base()
        {
            Value = Int32.Parse(xml.Attribute("Value").Value);
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("Digit",
                new XAttribute("Value", Value.ToString()));
        }
    }
}
