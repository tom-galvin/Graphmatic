using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Expressions.Parsing;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic.Expressions
{
    public partial class Expression : IParsable
    {
        protected ConstantParseTreeNode ParseLiteral(IEnumerator<Token> enumerator)
        {
            StringBuilder builder = new StringBuilder();
            double scaleFactor = 1;

            if (enumerator.Current is DigitToken) // integer component
            {
                while (enumerator.Current != null && enumerator.Current is DigitToken)
                {
                    builder.Append((enumerator.Current as DigitToken).Text);
                    if (!enumerator.MoveNext()) goto EndFast;
                }
            }
            else
            {
                throw new ParseException("Literal number must begin with a digit.", enumerator.Current);
            }

            if (enumerator.Current is SymbolicToken && // decimal place
                (enumerator.Current as SymbolicToken).Type == SymbolicToken.SymbolicType.DecimalPoint)
            {
                builder.Append('.');
                if (!enumerator.MoveNext()) goto EndFast;

                if (enumerator.Current is DigitToken) // fractional component
                {
                    while (enumerator.Current != null && enumerator.Current is DigitToken)
                    {
                        builder.Append((enumerator.Current as DigitToken).Text);
                        if (!enumerator.MoveNext()) goto EndFast;
                    }
                }
                else
                {
                    throw new ParseException("Decimal point in a literal number must be followed by a digit.", enumerator.Current);
                }
            }

            if (enumerator.Current is SymbolicToken && // exponent
                (enumerator.Current as SymbolicToken).Type == SymbolicToken.SymbolicType.Exp10)
            {
                builder.Append('e');
                if (!enumerator.MoveNext()) goto EndFast;
                if(enumerator.Current is OperationToken) // symbol
                {
                    OperationToken.OperationType operation = (enumerator.Current as OperationToken).Operation;
                    if (operation == OperationToken.OperationType.Add)
                        builder.Append('+');
                    else if (operation == OperationToken.OperationType.Subtract)
                        builder.Append('-');
                    else
                        throw new ParseException("Invalid symbol here - must be a +, - or digit.", enumerator.Current);
                    if (!enumerator.MoveNext()) goto EndFast;
                }

                if (enumerator.Current is DigitToken) // exponent component
                {
                    while (enumerator.Current != null && enumerator.Current is DigitToken)
                    {
                        builder.Append((enumerator.Current as DigitToken).Text);
                        if (!enumerator.MoveNext()) goto EndFast;
                    }
                }
                else
                {
                    throw new ParseException("Exponentiation symbol in a literal number must be followed by a digit, optionally preceded by a symbol.", enumerator.Current);
                }
            }

            if(enumerator.Current is SymbolicToken &&
                (enumerator.Current as SymbolicToken).Type == SymbolicToken.SymbolicType.Percent)
            {
                scaleFactor /= 100.0;
                if (!enumerator.MoveNext()) goto EndFast;
            }
        EndFast:
            return new ConstantParseTreeNode(Double.Parse(builder.ToString()) * scaleFactor);
        }

        protected ParseTreeNode ParseAtomic(IEnumerator<Token> enumerator)
        {
            ParseTreeNode currentNode = null;
            if (enumerator.Current is DigitToken)
            {
                currentNode = ParseLiteral(enumerator);
            }
            while (enumerator.Current != null && enumerator.Current is IParsable)
            {
                if (currentNode == null)
                {
                    currentNode = (enumerator.Current as IParsable).Parse();
                }
                else
                {
                    currentNode = new BinaryParseTreeNode(
                        MultiplyEvaluator,
                        currentNode,
                        (enumerator.Current as IParsable).Parse());
                }
                if (!enumerator.MoveNext()) goto EndFast;
            }
        EndFast:
            return currentNode;
        }

        public static UnaryEvaluator NegationEvaluator = new UnaryEvaluator(x => -x, "-{0}");
        protected ParseTreeNode ParseUnary(IEnumerator<Token> enumerator)
        {
            bool positive = true;
            while (enumerator.Current != null && enumerator.Current is OperationToken)
            {
                OperationToken current = enumerator.Current as OperationToken;

                if (current.Operation == OperationToken.OperationType.Add)
                {
                    // do nothing
                }
                else if (current.Operation == OperationToken.OperationType.Subtract)
                {
                    positive = !positive;
                }
                else
                {
                    break;
                }
                if (!enumerator.MoveNext()) goto EndFast;
            }
            if (positive)
                return ParseAtomic(enumerator);
            else
                return new UnaryParseTreeNode(NegationEvaluator, ParseAtomic(enumerator));
        EndFast:
            throw new ParseException("Unexpected end of expression.", enumerator.Current);
        }

        public static readonly BinaryEvaluator MultiplyEvaluator = new BinaryEvaluator((l, r) => l * r, "{0}*{1}");
        public static readonly BinaryEvaluator DivideEvaluator = new BinaryEvaluator((l, r) => l / r, "{0}/{1}");
        protected ParseTreeNode ParseProduction(IEnumerator<Token> enumerator)
        {
            ParseTreeNode currentNode = ParseUnary(enumerator);
            while (enumerator.Current != null && enumerator.Current is OperationToken)
            {
                OperationToken current = enumerator.Current as OperationToken;

                if (current.Operation == OperationToken.OperationType.Multiply)
                {
                    if (!enumerator.MoveNext()) goto EndFast;
                    currentNode = new BinaryParseTreeNode(MultiplyEvaluator, currentNode, ParseUnary(enumerator));
                }
                else if (current.Operation == OperationToken.OperationType.Divide)
                {
                    if (!enumerator.MoveNext()) goto EndFast;
                    currentNode = new BinaryParseTreeNode(DivideEvaluator, currentNode, ParseUnary(enumerator));
                }
                else
                {
                    break;
                }
            }
            return currentNode;
        EndFast:
            throw new ParseException("Unexpected end of expression.", enumerator.Current);
        }

        public static readonly BinaryEvaluator AddEvaluator = new BinaryEvaluator((l, r) => l + r, "{0}+{1}");
        public static readonly BinaryEvaluator SubtractEvaluator = new BinaryEvaluator((l, r) => l - r, "{0}-{1}");
        protected ParseTreeNode ParseSummation(IEnumerator<Token> enumerator)
        {
            ParseTreeNode currentNode = ParseProduction(enumerator);
            while (enumerator.Current != null && enumerator.Current is OperationToken)
            {
                OperationToken current = enumerator.Current as OperationToken;

                if (current.Operation == OperationToken.OperationType.Add)
                {
                    if (!enumerator.MoveNext()) goto EndFast;
                    currentNode = new BinaryParseTreeNode(AddEvaluator, currentNode, ParseProduction(enumerator));
                }
                else if (current.Operation == OperationToken.OperationType.Subtract)
                {
                    if (!enumerator.MoveNext()) goto EndFast;
                    currentNode = new BinaryParseTreeNode(SubtractEvaluator, currentNode, ParseProduction(enumerator));
                }
                else
                {
                    break;
                }
            }
            return currentNode;
        EndFast:
            throw new ParseException("Unexpected end of expression.", enumerator.Current);
        }

        public ParseTreeNode Parse()
        {
            IEnumerator<Token> enumerator = ((IEnumerable<Token>)this).GetEnumerator();
            enumerator.Reset();
            if (enumerator.MoveNext())
            {
                ParseTreeNode tree = ParseSummation(enumerator);
                enumerator.Reset();

                return tree;
            }
            else
            {
                throw new ParseException("Child token empty.", Parent);
            }
        }
    }
}
