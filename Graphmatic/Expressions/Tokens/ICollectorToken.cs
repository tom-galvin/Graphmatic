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
        /// <summary>
        /// Gets the token collection type for this token collector.
        /// </summary>
        CollectorTokenType CollectorType
        {
            get;
        }
    }

    /// <summary>
    /// Specifies the token collection type.
    /// </summary>
    public enum CollectorTokenType
    {
        /// <summary>
        /// Defines strong token collection - that is, collects as many items as possible.
        /// </summary>
        Strong,

        /// <summary>
        /// Defines weak token collection - that is, collects as few items as possible (in most cases, one.)
        /// </summary>
        Weak
    }
}
