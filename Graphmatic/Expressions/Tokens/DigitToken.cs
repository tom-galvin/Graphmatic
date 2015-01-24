using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class DigitToken : SimpleToken
    {
        private int _Value;
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

        public DigitToken(int value)
            : base()
        {
            Value = value;
        }

        public DigitToken(XElement xml)
            : base()
        {
            Value = Int32.Parse(xml.Attribute("Value").Value);
        }

        public override XElement ToXml()
        {
            return new XElement("Digit",
                new XAttribute("Value", Value.ToString()));
        }
    }
}
