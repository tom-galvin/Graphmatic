using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public class UnaryParseTreeNode : ParseTreeNode
    {
        public UnaryEvaluator Evaluator
        {
            get;
            protected set;
        }

        public ParseTreeNode Operand
        {
            get;
            protected set;
        }

        public UnaryParseTreeNode(UnaryEvaluator evaluator, ParseTreeNode operand)
        {
            Evaluator = evaluator;
            Operand = operand;
        }

        public override double Evaluate(Dictionary<char, double> variables)
        {
            double operandResult = Operand.Evaluate(variables);
            return Evaluator.Function(operandResult);
        }

        public override string ToString()
        {
            return String.Format(Evaluator.FormatString, Operand.ToString());
        }
    }
}