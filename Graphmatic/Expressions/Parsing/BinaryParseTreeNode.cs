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
            return Evaluator(leftResult, rightResult);
        }
    }
}
