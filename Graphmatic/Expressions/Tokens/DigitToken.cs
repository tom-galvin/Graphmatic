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

        public DigitToken(Expression parent, int value)
            : base(parent)
        {
            Value = value;
        }

        public DigitToken(Expression parent, XElement xml)
            : base(parent)
        {
            Value = Int32.Parse(xml.Element("Value").Value);
        }

        public override double Evaluate(Dictionary<char, double> variables)
        {
            throw new NotImplementedException();
        }

        public override XElement ToXml()
        {
            return new XElement("Digit",
                new XAttribute("Value", Value.ToString()));
        }
    }
}
