using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public class ConstantParseTreeNode : ParseTreeNode
    {
        public double Value
        {
            get;
            protected set;
        }

        public ConstantParseTreeNode(double value)
        {
            Value = value;
        }

        public override double Evaluate(Dictionary<char, double> variables)
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
