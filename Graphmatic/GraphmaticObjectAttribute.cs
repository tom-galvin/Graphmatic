using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic
{
    /// <summary>
    /// Specifies the serialized XML element name of a Graphmatic object.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GraphmaticObjectAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <c>Graphmatic.GraphmaticObjectAttribute</c> for a Graphmatic
        /// object.
        /// </summary>
        public GraphmaticObjectAttribute()
        {
        }
    }
}
