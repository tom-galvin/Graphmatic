using Graphmatic.Expressions.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    public partial class Expression : List<Token>, IPaintable, IEvaluable
    {
        public double Evaluate(Dictionary<char, double> variables)
        {
            throw new NotImplementedException();
        }
    }
}
