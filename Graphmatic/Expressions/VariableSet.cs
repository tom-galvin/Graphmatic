using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Contains the data needed to evaluate an expression, such as any defined variables.
    /// </summary>
    public class VariableSet
    {
        /// <summary>
        /// The variables contained in this VariableSet.
        /// </summary>
        public Dictionary<char, double> Variables;

        /// <summary>
        /// Gets or sets a variable in the variable set.
        /// </summary>
        /// <param name="variable">The name of the variable for which to modify the value.</param>
        /// <returns>The value of the variable with the name <paramref name="variable"/>.</returns>
        public double this[char variable]
        {
            get { return Variables[variable]; }
            set { Variables[variable] = value; }
        }

        /// <summary>
        /// Initialize a new instance of <c>VariableSet</c>.
        /// </summary>
        public VariableSet()
        {
            Variables = new Dictionary<char, double>();
        }
    }
}
