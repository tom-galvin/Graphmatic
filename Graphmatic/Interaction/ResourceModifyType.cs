using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction
{
    /// <summary>
    /// When a Resource is added to or removed from a document, this enum is used to notify
    /// any other Resources in the document of the occurrence.
    /// </summary>
    public enum ResourceModifyType
    {
        /// <summary>
        /// A resource has been added to the document.
        /// </summary>
        Add,

        /// <summary>
        /// A resource has been modified in the document.
        /// </summary>
        Modify,

        /// <summary>
        /// A resource has been removed in the document.
        /// </summary>
        Remove
    }
}
