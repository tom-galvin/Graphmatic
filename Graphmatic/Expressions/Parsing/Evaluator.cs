using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a function that is evaluated on a tree node when evaluating a parsed tree node.
    /// </summary>
    public abstract class Evaluator
    {
        /// <summary>
        /// Gets the formatting string to use when converting the parse tree into a string.
        /// </summary>
        public string FormatString
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initialize a new instance of the Evaluator class, with no formatting string.
        /// </summary>
        protected Evaluator()
        {

        }

        /// <summary>
        /// Initialize a new instance of the Evaluator class, with the given formatting string.
        /// </summary>
        /// <param name="formatString">The formatting string to use when converting the parse tree into a string.</param>
        protected Evaluator(string formatString) {
            FormatString = formatString;
        }
    }
}
