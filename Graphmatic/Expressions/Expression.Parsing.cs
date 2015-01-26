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
        /// <summary>
        /// Parses a literal numeric value. This will correctly accept decimal places and positive/negative exponents.<para/>
        /// This works in a bit of a cheaty way, but it saves me some time. The entered literal is converted into a string representing
        /// the value (turning the <c>*10^</c> operator into the character <c>e</c>) and then parsed using the .NET framework,
        /// leaving the conversion from token sequence to double up to Double.Parse.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
        protected ConstantParseTreeNode ParseLiteral(IEnumerator<Token> enumerator)
        {
            StringBuilder builder = new StringBuilder();
            double scaleFactor = 1;

            if (enumerator.Current is DigitToken) // integer component
            {
                while (enumerator.Current != null && enumerator.Current is DigitToken)
                {
                    builder.Append((enumerator.Current as DigitToken).Text);
                    if (!enumerator.MoveNext()) break;
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
                if (enumerator.MoveNext())
                {

                    if (enumerator.Current is DigitToken) // fractional component
                    {
                        while (enumerator.Current != null && enumerator.Current is DigitToken)
                        {
                            builder.Append((enumerator.Current as DigitToken).Text);
                            if (!enumerator.MoveNext()) break;
                        }
                    }
                    else
                    {
                        throw new ParseException("Decimal point in a literal number must be followed by a digit.", enumerator.Current);
                    }
                }
            }

            if (enumerator.Current is SymbolicToken && // exponent
                (enumerator.Current as SymbolicToken).Type == SymbolicToken.SymbolicType.Exp10)
            {
                builder.Append('e');
                if (!enumerator.MoveNext())
                    throw new ParseException("Expected exponent after exponent symbol.", enumerator.Current);
                if(enumerator.Current is OperationToken) // symbol
                {
                    OperationToken.OperationType operation = (enumerator.Current as OperationToken).Operation;
                    if (operation == OperationToken.OperationType.Add)
                        builder.Append('+');
                    else if (operation == OperationToken.OperationType.Subtract)
                        builder.Append('-');
                    else
                        throw new ParseException("Invalid symbol here - must be a +, - or digit.", enumerator.Current);
                    enumerator.MoveNext();
                }

                if (enumerator.Current is DigitToken) // exponent component
                {
                    while (enumerator.Current != null && enumerator.Current is DigitToken)
                    {
                        builder.Append((enumerator.Current as DigitToken).Text);
                        if (!enumerator.MoveNext()) break;
                    }
                }
                else
                {
                    throw new ParseException("Exponent symbol in a literal number must be followed by a digit, optionally preceded by a symbol.", enumerator.Current);
                }
            }

            if(enumerator.Current is SymbolicToken &&
                (enumerator.Current as SymbolicToken).Type == SymbolicToken.SymbolicType.Percent) // divide by 100 when the percentage sign appears
            {
                scaleFactor /= 100.0;
                enumerator.MoveNext();
            }

            return new ConstantParseTreeNode(Double.Parse(builder.ToString()) * scaleFactor);
        }

        /// <summary>
        /// Parses an atomic production.<para/>
        /// An atomic production takes the EBNF production:
        /// <code>&lt;atomic&gt; := &lt;literal&gt; { &lt;token&gt; } { &lt;exp&gt; }</code><para/>
        /// This means that, in an equation such as <c>y=4x√2</c> the three tokens '4', 'x' and '√2' are correctly broken
        /// down into three separate multiplications. This is so the algebraic multiplication short-hand of omitting the
        /// cross symbol still works. Lastly, this also parses any exponents.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
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
                if (!enumerator.MoveNext()) break;
            }
            while (enumerator.Current != null && enumerator.Current is ExpToken)
            {
                currentNode = new BinaryParseTreeNode(
                    ExpEvaluator,
                    currentNode,
                    (enumerator.Current as ExpToken).Power.Parse());
                if (!enumerator.MoveNext()) break;
            }
            return currentNode;
        }


        /// <summary>
        /// An evaluator for the exponent (power) operator.
        /// </summary>
        public static readonly BinaryEvaluator ExpEvaluator = new BinaryEvaluator((powBase, powPower) => Math.Pow(powBase, powPower), "pow[{1}]({0})");

        /// <summary>
        /// An evaluator for negation, such as when an atomic value is negated with the unary - operator.
        /// </summary>
        public static readonly UnaryEvaluator NegationEvaluator = new UnaryEvaluator(x => -x, "-{0}");

        /// <summary>
        /// Parses unary operators, such as anything prefixed with + and -.<para/>
        /// This correctly swallows any additional operators so as to avoid any huge unnecessary chains of unary negation.
        /// This means an expression like <c>y=--++--+--++--++++--x</c> will correctly reduce to <c>y=x</c>.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
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
                if (!enumerator.MoveNext())
                    throw new ParseException("Unexpected end of expression.", enumerator.Current);
            }
            if (positive)
                return ParseAtomic(enumerator);
            else
                return new UnaryParseTreeNode(NegationEvaluator, ParseAtomic(enumerator));
        }

        /// <summary>
        /// An evaluator for multiplication.
        /// </summary>
        public static readonly BinaryEvaluator MultiplyEvaluator = new BinaryEvaluator((l, r) => l * r, "{0}*{1}");
        /// <summary>
        /// An evaluator for division.
        /// </summary>
        public static readonly BinaryEvaluator DivideEvaluator = new BinaryEvaluator((l, r) => l / r, "{0}/{1}");
        /// <summary>
        /// Parses a production production (I might have called it that on purpose.)<para/>
        /// Basically, this handles multiplication and division.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
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

        /* 'I believe that by presenting such a view I am not in fact disagreeing sharply
         * with Dijkstra's ideas, since he recently wrote the following: "Please don't
         * fall into the trap of believing that I am terribly dogmatical about [the go to
         * statement]. I have the uncomfortable feeling that others are making a religion
         * out of it, as if the conceptual problems of programming could be solved by a
         * single trick, by a simple form of coding discipline!"'
         * 
         * - Donald E. Knuth
         * 
         * Goto isn't evil here, as it avoids nasty temporary variables everywhere. */

        /// <summary>
        /// An evaluator for addition.
        /// </summary>
        public static readonly BinaryEvaluator AddEvaluator = new BinaryEvaluator((l, r) => l + r, "{0}+{1}");
        /// <summary>
        /// An evaluator for subtraction.
        /// </summary>
        public static readonly BinaryEvaluator SubtractEvaluator = new BinaryEvaluator((l, r) => l - r, "{0}-{1}");
        /// <summary>
        /// Parses a summation production, handling addition and subtraction.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
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

        /// <summary>
        /// An evaluator for the equals sign in an equation.<para/>
        /// Behind the scenes, this is just a subtraction.
        /// </summary>
        public static readonly BinaryEvaluator EqualsEvaluator = new BinaryEvaluator((l, r) => l - r, "{0}={1}");
        protected ParseTreeNode ParseEquation(IEnumerator<Token> enumerator)
        {
            ParseTreeNode leftNode = ParseSummation(enumerator), rightNode;
            SymbolicToken current = enumerator.Current as SymbolicToken;
            if (current != null && current.Type == SymbolicToken.SymbolicType.Equals)
            {
                if (!enumerator.MoveNext())
                    throw new ParseException("Unexpected end of equation.", enumerator.Current);
                rightNode = ParseSummation(enumerator);
            }
            else
            {
                throw new ParseException("Unexpected symbol in equation. Have you used an equals sign?", enumerator.Current);
            }
            if(enumerator.MoveNext())
                throw new ParseException("Unexpected symbol after equation.", enumerator.Current);
            if (leftNode == null || rightNode == null)
            {
                throw new ParseException("Equation must have both a left-hand side and a right-hand side.", enumerator.Current);
            }
            return new BinaryParseTreeNode(EqualsEvaluator, leftNode, rightNode);
        }

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <param name="equationParse">Whether to parse as an equation (ie. including the equals sign) or not.</param>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse(bool equationParse)
        {
            IEnumerator<Token> enumerator = ((IEnumerable<Token>)this).GetEnumerator();
            enumerator.Reset();
            if (enumerator.MoveNext())
            {
                ParseTreeNode tree = equationParse ?
                    ParseEquation(enumerator) :
                    ParseSummation(enumerator);
                enumerator.Reset();

                return tree;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses this token into a <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing
        /// the sequence of calculations needed to evaluate this expression.
        /// </summary>
        /// <returns>A <c>Graphmatic.Expressions.Parsing.ParseTreeToken</c> representing a syntax tree
        /// for this token and any children.</returns>
        public ParseTreeNode Parse()
        {
            return Parse(false);
        }
    }
}
