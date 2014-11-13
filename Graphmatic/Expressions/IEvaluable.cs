using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    public interface IEvaluable
    {
        /// <summary>
        /// Evaluates the element with a given set of variables.
        /// </summary>
        /// <param name="variables">The value of the variables to evaluate with.</param>
        double Evaluate(Dictionary<char, double> variables);
    }
}
