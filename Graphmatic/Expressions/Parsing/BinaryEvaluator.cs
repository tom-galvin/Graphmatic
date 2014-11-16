using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a function that is evaluated on a binary (two-child) tree node when evaluating a parsed expression.
    /// </summary>
    public class BinaryEvaluator : Evaluator
    {
        /// <summary>
        /// Gets the function applied on the children upon evaluation.
        /// </summary>
        public Func<double, double, double> Function
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initialize a new instance of the BinaryEvaluator class, with the given function and formatting string.
        /// </summary>
        /// <param name="function">The function applied on the children upon evaluation.</param>
        /// <param name="formatString">The formatting string used when converting the parse tree to a string.</param>
        public BinaryEvaluator(Func<double, double, double> function, string formatString)
            : base(formatString)
        {
            Function = function;
        }
    }
}
