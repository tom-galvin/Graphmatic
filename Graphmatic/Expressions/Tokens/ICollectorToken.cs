using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Defines a contract for tokens that can 'absorb' or 'collect' neighbouring tokens when placed,
    /// to enable the user to enter expressions in a faster and more natural manner.
    /// </summary>
    public interface ICollectorToken
    {
        int MaxPrecedence
        {
            get;
        }
    }
}
