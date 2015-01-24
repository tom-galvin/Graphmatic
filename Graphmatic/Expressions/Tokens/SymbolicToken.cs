﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class SymbolicToken : SimpleToken
    {
        private SymbolicType _Type;
        public SymbolicType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public override string Text
        {
            get
            {
                switch (Type)
                {
                    case SymbolicType.Comma:
                        return ",";
                    case SymbolicType.DecimalPoint:
                        return ".";
                    case SymbolicType.Exp10:
                        return "*~";
                    case SymbolicType.Percent:
                        return "%";
                    case SymbolicType.Equals:
                        return "=";
                    default:
                        return "<unknown symbolic>";
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public SymbolicToken(SymbolicType type)
            : base()
        {
            Type = type;
        }

        public SymbolicToken(XElement xml)
            : base()
        {
            string symbolicTypeName = xml.Attribute("Type").Value;
            if (!Enum.TryParse<SymbolicType>(symbolicTypeName, out _Type))
                throw new NotImplementedException("The symbolic type " + symbolicTypeName + " is not implemented.");
        }

        public override XElement ToXml()
        {
            return new XElement("Symbolic",
                new XAttribute("Type", _Type.ToString()));
        }

        public enum SymbolicType
        {
            Comma,
            Percent,
            Exp10,
            DecimalPoint,
            Equals
        }
    }
}
