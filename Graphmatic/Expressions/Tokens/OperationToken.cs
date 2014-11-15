using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    public class OperationToken : SimpleToken
    {
        private OperationType _Operation;
        public OperationType Operation
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
                    case OperationType.Add:
                        return "+";
                    case OperationType.Subtract:
                        return "-";
                    case OperationType.Multiply:
                        return "*";
                    case OperationType.Divide:
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

        public OperationToken(Expression parent, OperationType operation)
            : base(parent)
        {
            Operation = operation;
        }

        public OperationToken(Expression parent, XElement xml)
            : base(parent)
        {
            string operationName = xml.Attribute("Operation").Value;
            if (!Enum.TryParse<OperationType>(operationName, out _Operation))
                throw new NotImplementedException("The operation " + operationName + " is not implemented.");
        }

        public override XElement ToXml()
        {
            return new XElement("Operation",
                new XAttribute("Operation", Operation.ToString()));
        }

        public enum OperationType
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }
    }
}
