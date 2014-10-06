using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class BinaryOperationToken : SimpleToken
    {
        public BinaryOperationType _Operation;
        public BinaryOperationType Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        public override string Text
        {
            get
            {
                switch (_Operation)
                {
                    case BinaryOperationType.Add:
                        return "+";
                    case BinaryOperationType.Subtract:
                        return "-";
                    case BinaryOperationType.Multiply:
                        return "*";
                    case BinaryOperationType.Divide:
                        return "/";
                    default:
                        return "<unknown operation>";
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public BinaryOperationToken(Expression parent, BinaryOperationType operation)
            :base(parent)
        {
            Operation = operation;
        }

        public BinaryOperationToken(Expression parent, XElement xml)
            :base(parent)
        {
            string operationName = xml.Element("Operation").Value;
            if(!Enum.TryParse<BinaryOperationType>(operationName, out _Operation))
                throw new NotImplementedException("The operation " + operationName + " is not implemented.");
        }

        public override XElement ToXml()
        {
            return new XElement("BinaryOperation",
                new XAttribute("Operation", Operation.ToString()));
        }

        public enum BinaryOperationType
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }
    }
}
