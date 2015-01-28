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
        /// This works in a bit of a cheaty way, but it saves some code. The entered literal is converted into a string representing
        /// the value (turning the <c>*10^</c> operator into the character <c>e</c>) and then parsed using the .NET framework,
        /// leaving the conversion from token sequence to double up to Double.Parse.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
        protected ConstantParseTreeNode ParseLiteral(ParserEnumerator enumerator)
        {
            StringBuilder builder = new StringBuilder();
            double scaleFactor = 1;

            if (enumerator.Check<DigitToken>()) // integer component
            {
                while (enumerator.Accept<DigitToken>())
                {
                    builder.Append((enumerator.Current as DigitToken).Text);
                }
            }
            else
            {
                throw new ParseException("Literal number must begin with a digit.", enumerator.Current);
            }

            if (enumerator.Accept<SymbolicToken>(t => t.Type == SymbolicToken.SymbolicType.DecimalPoint)) // decimal place
            {
                builder.Append('.');
                if (enumerator.Check<DigitToken>()) // fractional component
                {
                    while (enumerator.Accept<DigitToken>())
                    {
                        builder.Append((enumerator.Current as DigitToken).Text);
                    }
                }
                else
                {
                    throw new ParseException("Decimal point in a literal number must be followed by a digit.", enumerator.Current);
                }
            }

            if (enumerator.Accept<SymbolicToken>(t => t.Type == SymbolicToken.SymbolicType.Exp10)) // exponent
            {
                builder.Append('e');

                // optional sign after the '*10^' symbol
                if (enumerator.Accept<OperationToken>(t => t.Operation == OperationToken.OperationType.Add))
                    builder.Append('+');
                if (enumerator.Accept<OperationToken>(t => t.Operation == OperationToken.OperationType.Subtract))
                    builder.Append('-');

                if (enumerator.Check<DigitToken>())
                {
                    while (enumerator.Accept<DigitToken>())
                    {
                        builder.Append((enumerator.Current as DigitToken).Text);
                    }
                }
                else
                {
                    throw new ParseException("Exponent symbol in a literal number must be followed by a digit, optionally preceded by a symbol.", enumerator.Current);
                }
            }

            if (enumerator.Accept<SymbolicToken>(t => t.Type == SymbolicToken.SymbolicType.Percent)) // divide by 100 when the percentage sign appears
            {
                scaleFactor /= 100.0;
            }

            return new ConstantParseTreeNode(Double.Parse(builder.ToString()) * scaleFactor);
        }

        /// <summary>
        /// An evaluator for the exponent (power) operator.
        /// </summary>
        public static readonly BinaryEvaluator ExpEvaluator = new BinaryEvaluator((powBase, powPower) => Math.Pow(powBase, powPower), "pow[{1}]({0})");

        /// <summary>
        /// Parses a series of <c>ExpToken</c>s, and returns the combined power.
        /// This function takes advantage of the mathematical identity: <c>(a^b)^c=a^(b*c)</c>. Successive <c>ExpToken</c>s
        /// have their powers multiplied together such to return a single power to which to raise the previous expression to.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns the power that the token preceding the parsed <c>ExpToken</c>s should be raised to.</returns>
        protected ParseTreeNode ParseExponent(ParserEnumerator enumerator)
        {
            if (enumerator.Accept<ExpToken>())
            {
                ParseTreeNode exponentNode = (enumerator.Current as ExpToken).Power.Parse();
                while (enumerator.Accept<ExpToken>())
                {
                    exponentNode = new BinaryParseTreeNode(
                        MultiplyEvaluator,
                        exponentNode,
                        (enumerator.Current as ExpToken).Power.Parse());
                }
                return exponentNode;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses an atomic production.<para/>
        /// An atomic production can be represented as EBNF like:
        /// <code>&lt;atomic&gt; := [ &lt;literal&gt; ] { &lt;exp&gt; } { &lt;token&gt; { &lt;exp&gt; } }</code><para/>
        /// This means that, in an equation such as <c>y=4x√2</c> the three tokens '4', 'x' and '√2' are correctly broken
        /// down into three separate multiplications. This is so the algebraic multiplication short-hand of omitting the
        /// cross symbol still works. Lastly, this also parses any exponents.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this sub-expression in the order it should be evaluated.</returns>
        protected ParseTreeNode ParseAtomic(ParserEnumerator enumerator)
        {
            ParseTreeNode currentNode = null;
            // parse the preceding literal, if there is one
            if (enumerator.Check<DigitToken>())
            {
                currentNode = ParseLiteral(enumerator);
            }
            if (enumerator.Check<ExpToken>())
            {
                if (currentNode != null) // we can't exponentiate a non-existent literal
                {
                    currentNode = new BinaryParseTreeNode(
                        ExpEvaluator,
                        currentNode,
                        ParseExponent(enumerator));
                }
                else
                {
                    throw new ParseException("Expected a valid token before an exponent.", enumerator.Current);
                }
            }

            while (enumerator.Accept<Token>(t => t is IParsable))
            {
                // parse any other successive tokens
                ParseTreeNode currentNodeSubtoken = (enumerator.Current as IParsable).Parse();

                if (enumerator.Check<ExpToken>())
                {
                    currentNodeSubtoken = new BinaryParseTreeNode(
                        ExpEvaluator,
                        currentNodeSubtoken,
                        ParseExponent(enumerator));
                }

                if (currentNode == null)
                {
                    // if the current node is null, create it
                    currentNode = currentNodeSubtoken;
                }
                else
                {
                    // otherwise, multiply the existing current node with the new subnode
                    currentNode = new BinaryParseTreeNode(
                        MultiplyEvaluator,
                        currentNode,
                        currentNodeSubtoken);
                }
            }

            if (currentNode == null)
            {
                throw new ParseException("Expected a valid token.", enumerator.Current);
            }
            return currentNode;
        }

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
        protected ParseTreeNode ParseUnary(ParserEnumerator enumerator)
        {
            bool positive = true;
            while (enumerator.Accept<OperationToken>(t =>
                t.Operation == OperationToken.OperationType.Add ||
                t.Operation == OperationToken.OperationType.Subtract))
            {
                OperationToken current = enumerator.Current as OperationToken;

                if (current.Operation == OperationToken.OperationType.Subtract)
                {
                    positive = !positive;
                } // do nothing if the operation token is positive
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
        protected ParseTreeNode ParseProduction(ParserEnumerator enumerator)
        {
            ParseTreeNode currentNode = ParseUnary(enumerator);
            while (enumerator.Accept<OperationToken>(t =>
                t.Operation == OperationToken.OperationType.Multiply ||
                t.Operation == OperationToken.OperationType.Divide))
            {
                OperationToken current = enumerator.Current as OperationToken;

                if (current.Operation == OperationToken.OperationType.Multiply)
                {
                    currentNode = new BinaryParseTreeNode(MultiplyEvaluator, currentNode, ParseUnary(enumerator));
                }
                else if (current.Operation == OperationToken.OperationType.Divide)
                {
                    currentNode = new BinaryParseTreeNode(DivideEvaluator, currentNode, ParseUnary(enumerator));
                }
            }
            return currentNode;
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
        protected ParseTreeNode ParseSummation(ParserEnumerator enumerator)
        {
            ParseTreeNode currentNode = ParseProduction(enumerator);
            while (enumerator.Accept<OperationToken>(t =>
                t.Operation == OperationToken.OperationType.Add ||
                t.Operation == OperationToken.OperationType.Subtract))
            {
                OperationToken current = enumerator.Current as OperationToken;

                if (current.Operation == OperationToken.OperationType.Add)
                {
                    currentNode = new BinaryParseTreeNode(AddEvaluator, currentNode, ParseProduction(enumerator));
                }
                else if (current.Operation == OperationToken.OperationType.Subtract)
                {
                    currentNode = new BinaryParseTreeNode(SubtractEvaluator, currentNode, ParseProduction(enumerator));
                }
            }
            return currentNode;
        }

        /// <summary>
        /// An evaluator for the equals sign in an equation.<para/>
        /// Behind the scenes, this is just a subtraction.
        /// </summary>
        public static readonly BinaryEvaluator EqualsEvaluator = new BinaryEvaluator((l, r) => l - r, "{0}={1}");

        /// <summary>
        /// Parses an equation production, handling the equals sign.
        /// </summary>
        /// <param name="enumerator">An enumerator containing the current location in the Expression of the parser.</param>
        /// <returns>Returns a parse tree node representing this equation.<para/>
        /// The equals (=) symbol is essentially equivalent to a subtraction (-) symbol with lower precedence, at least in
        /// the context of evaluating the equation.</returns>
        protected ParseTreeNode ParseEquation(ParserEnumerator enumerator)
        {
            ParseTreeNode leftNode = ParseSummation(enumerator), rightNode;
            SymbolicToken current = enumerator.Current as SymbolicToken;
            if (enumerator.Accept<SymbolicToken>(t => t.Type == SymbolicToken.SymbolicType.Equals))
            {
                rightNode = ParseSummation(enumerator);
            }
            else
            {
                throw new ParseException("Equals sign (=) expected in equation.", enumerator.Current);
            }

            if (!enumerator.EndReached)
                throw new ParseException("Unexpected symbol after equation.", enumerator.Current);
            if (leftNode == null || rightNode == null)
            {
                throw new ParseException("Equation is incomplete.", enumerator.Current);
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
            ParserEnumerator enumerator = new ParserEnumerator(this);
            ParseTreeNode tree = equationParse ?
                    ParseEquation(enumerator) :
                    ParseSummation(enumerator);
            return tree;
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
