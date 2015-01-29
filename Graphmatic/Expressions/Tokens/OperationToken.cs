using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a token representing an elementary arithmetic operation.
    /// </summary>
    [GraphmaticObject]
    public class OperationToken : SimpleToken
    {
        private OperationType _Operation;
        /// <summary>
        /// Gets or sets the type of binary operation performed and represented by this token.
        /// </summary>
        public OperationType Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        /// <summary>
        /// Gets the text displayed by this token.<para/>
        /// This text will be the elementary arithmetic symbol of the operation
        /// represented by this OperationToken; for example, the plus and minus
        /// sign for addition and subtraction, the cross sign for multiplication
        /// and the obelus for division.
        /// </summary>
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
                        return "/"; // shown as an obelus in the display charset
                    default:
                        return "<unknown operation>";
                }
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <c>OperationToken</c> class.
        /// </summary>
        /// <param name="operation">The type of binary operation performed by this token.</param>
        public OperationToken(OperationType operation)
            : base()
        {
            Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <c>OperationToken</c> class from the given serialized data.
        /// </summary>
        /// <param name="xml">The serialized data with which to deserialize the token.</param>
        public OperationToken(XElement xml)
            : base()
        {
            string operationName = xml.Attribute("Operation").Value;
            if (!Enum.TryParse<OperationType>(operationName, out _Operation))
                throw new NotImplementedException("The operation " + operationName + " is not implemented.");
        }

        /// <summary>
        /// Converts this Token, and any child Expressions contained within, into a serialized XML representation.
        /// </summary>
        /// <returns>The serialized form of this Token.</returns>
        public override XElement ToXml()
        {
            return new XElement("OperationToken",
                new XAttribute("Operation", Operation.ToString()));
        }

        /// <summary>
        /// Represents a type of operation performed by a <c>Graphmatic.Expressions.Tokens.OperationToken</c>.
        /// </summary>
        public enum OperationType
        {
            /// <summary>
            /// Represents addition.
            /// </summary>
            Add,
            /// <summary>
            /// Represents subtraction.
            /// </summary>
            Subtract,
            /// <summary>
            /// Represents multiplication.
            /// </summary>
            Multiply,
            /// <summary>
            /// Represents division.
            /// </summary>
            Divide
        }
    }
}
