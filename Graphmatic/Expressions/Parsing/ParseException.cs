using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents errors that occur during generation of a parse tree (parsing.)
    /// </summary>
    [Serializable]
    public class ParseException : Exception
    {
        public Token Cause
        {
            get;
            protected set;
        }

        private ParseException() { }
        public ParseException(string message, Token cause) : base(message) { Cause = cause; }
        protected ParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
