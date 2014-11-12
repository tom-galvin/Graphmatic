using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Specifies the display size of a paintable element of an expression tree; large or small.
    /// </summary>
    public enum DisplaySize
    {
        /// <summary>
        /// Small display size; for example, a superscripted power, or subscripted log-base.
        /// </summary>
        Small,

        /// <summary>
        /// Large display size.
        /// </summary>
        Large
    }
}
