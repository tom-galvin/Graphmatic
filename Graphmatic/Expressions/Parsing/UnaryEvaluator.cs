using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public class UnaryEvaluator : Evaluator
    {
        public Func<double, double> Function
        {
            get;
            protected set;
        }

        public UnaryEvaluator(Func<double, double> function, string formatString)
            : base(formatString)
        {
            Function = function;
        }
    }
}
