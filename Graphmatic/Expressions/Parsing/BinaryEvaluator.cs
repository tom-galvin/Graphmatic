using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public class BinaryEvaluator : Evaluator
    {
        public Func<double, double, double> Function
        {
            get;
            protected set;
        }

        public BinaryEvaluator(Func<double, double, double> function, string formatString)
            : base(formatString)
        {
            Function = function;
        }
    }
}
