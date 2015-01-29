using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Parsing;
using Graphmatic.Interaction.Plotting;

namespace Graphmatic.Interaction
{
    /// <summary>
    /// Represents a plottable equation in a Graphmatic document.
    /// </summary>
    [GraphmaticObject]
    public partial class Equation : Resource, IPlottable
    {
        /// <summary>
        /// Gets the <c>Graphmatic.Expressions.Expression representing this equation in an editable form.</c>
        /// </summary>
        public Expression Expression
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the parsed form of this expression that is better suited to repeated evaluation during the plotting process.
        /// </summary>
        public ParseTreeNode ParseTree
        {
            get;
            set;
        }

        public override string Type
        {
            get
            {
                return "Equation";
            }
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Equation</c> class with no tokens in it.
        /// </summary>
        public Equation()
            : this(new Expression(null))
        {
        }

        /// <summary>
        /// Initialize a new instance of the <c>Graphmatic.Interaction.Equation</c> class from the given expression,
        /// representing an equation that describes this <c>Graphmatic.Interaction.Equation</c>.
        /// </summary>
        public Equation(Expression expression)
            : base()
        {
            Expression = expression;
            if (Expression.Count == 0)
            {
                // create a dummy parse tree equivalent to '0=1'
                // meaning it won't be plotted but it won't throw any exceptions either
                ParseTree =
                    new BinaryParseTreeNode(Expression.EqualsEvaluator,
                        new ConstantParseTreeNode(0),
                        new ConstantParseTreeNode(1));
            }
            else
            {
                ParseTree = Expression.Parse();
            }
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.Equation</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public Equation(XElement xml)
            : base(xml)
        {
            Expression = new Expression(null, xml.Element("Expression").Elements());
            Parse();
        }

        /// <summary>
        /// Parses the Expression representing this Equation and stores the resulting parse tree into the <c>ParseTree</c> property.
        /// </summary>
        public void Parse()
        {
            ParseTree = Expression.Parse(true);
        }

        /// <summary>
        /// Gets the resource icon describing this resource type in the user interface.<para/>
        /// This will return different icons if overriden by a derived type.
        /// </summary>
        /// <param name="large">Whether to return the large icon or not. Large icons are 32*32 and
        /// small icons are 16*16.</param>
        public override Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.Equation32 :
                Properties.Resources.Equation16;
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "Equation";
            baseElement.Add(
                new XElement("Expression", Expression.ToXml()));
            return baseElement;
        }
    }
}
