using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Interaction;

namespace Graphmatic
{
    public class ExpressionVerificationEventArgs : EventArgs
    {
        public bool Failure
        {
            get;
            set;
        }

        public ExpressionCursor Cursor
        {
            get;
            set;
        }

        public Equation Equation
        {
            get;
            set;
        }
    }
}
