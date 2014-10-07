﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    public interface IOperator : ITokenCollector
    {
        OperatorAssociativity Associativity
        {
            get;
        }
    }

    public enum OperatorAssociativity
    {
        Left,
        Right
    }
}
