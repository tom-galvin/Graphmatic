using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions.Parsing
{
    /// <summary>
    /// Represents a singular node in the parse tree with no children and returning the value of a given variable.
    /// </summary>
    public class VariableParseTreeNode : ParseTreeNode
    {
        /// <summary>
        /// Gets the name of the variable to return upon evaluation.
        /// </summary>
        public char Variable
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the VariableParseTreeNode class, with the given variable name.
        /// </summary>
        /// <param name="variableIdentifier">The name of the variable to return upon evaluation.</param>
        public VariableParseTreeNode(char variableIdentifier)
        {
            Variable = variableIdentifier;
        }

        /// <summary>
        /// Evaluate this parse tree node with the given variable values.
        /// </summary>
        /// <param name="variables">The variable values to use in calculation.</param>
        /// <returns>Returns the result of the evaluation.</returns>
        public override double Evaluate(Dictionary<char, double> variables)
        {
            try
            {
                return variables[Variable];
            }
            catch (KeyNotFoundException e)
            {
                throw new EvaluationException("No variable " + Variable + " is defined.", e);
            }
        }

        /// <summary>
        /// Convert this parse tree node into a string representation of the syntax it represents.
        /// </summary>
        /// <returns>A string representation of the syntax it represents.</returns>
        public override string ToString()
        {
            return Variable.ToString();
        }
    }
}
