using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    public delegate double UnaryEvaluator(double operand);
    public delegate double BinaryEvaluator(double left, double right);
}
