using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public class BinaryParseTreeNode : ParseTreeNode
    {
        public BinaryEvaluator Evaluator
        {
            get;
            protected set;
        }

        public ParseTreeNode Left
        {
            get;
            protected set;
        }

        public ParseTreeNode Right
        {
            get;
            protected set;
        }

        public BinaryParseTreeNode(BinaryEvaluator evaluator, ParseTreeNode left, ParseTreeNode right)
        {
            Evaluator = evaluator;
            Left = left;
            Right = right;
        }

        public override double Evaluate(Dictionary<char, double> variables)
        {
            double leftResult = Left.Evaluate(variables);
            double rightResult = Right.Evaluate(variables);
            return Evaluator.Function(leftResult, rightResult);
        }

        public override string ToString()
        {
            return String.Format(Evaluator.FormatString, Left.ToString(), Right.ToString());
        }
    }
}
