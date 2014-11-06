using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    public interface CollectibleToken
    {
        int Precedence
        {
            get;
        }
    }
}
