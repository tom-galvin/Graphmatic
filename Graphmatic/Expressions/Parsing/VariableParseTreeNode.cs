using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public class VariableParseTreeNode : ParseTreeNode
    {
        public char Variable
        {
            get;
            protected set;
        }

        public VariableParseTreeNode(char variableIdentifier)
        {
            Variable = variableIdentifier;
        }

        public override double Evaluate(Dictionary<char, double> variables)
        {
            return variables[Variable];
        }
    }
}
