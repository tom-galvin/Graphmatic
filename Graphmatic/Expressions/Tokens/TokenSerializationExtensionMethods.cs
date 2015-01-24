using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a method that deserializes a token from its XML representation.
    /// </summary>
    /// <param name="xml">THe XML representation of the token.</param>
    /// <returns>The deserialized form of the token.</returns>
    public delegate Token TokenDeserializationFactory(XElement xml);

    /// <summary>
    /// Provides methods to assist in (de)serialization of serialized Graphmatic expressions.
    /// </summary>
    public static class TokenSerializationExtensionMethods
    {
        /// <summary>
        /// A list of deserializing constructors for tokens with differing XML element names.
        /// </summary>
        private static Dictionary<string, TokenDeserializationFactory> TokenDeserializers = new Dictionary<string, TokenDeserializationFactory>
        {
            { "Operation", xml => new OperationToken(xml) },
            { "Digit", xml => new DigitToken(xml) },
            { "Exp", xml => new ExpToken(xml) },
            { "Fraction", xml => new FractionToken(xml) },
            { "Root", xml => new RootToken(xml) },
            { "Log", xml => new LogToken(xml) },
            { "Constant", xml => new ConstantToken(xml) },
            { "Symbolic", xml => new SymbolicToken(xml) },
            { "Absolute", xml => new AbsoluteToken(xml) }, // must be ABOVE function to deserialize correctly
            { "Function", xml => new FunctionToken(xml) },
            { "Variable", xml => new VariableToken(xml) },
        };

        /// <summary>
        /// Deserializes a token from its XML representation.
        /// </summary>
        /// <param name="xml">The XML representation of a token to deserialize.</param>
        /// <returns>The deserialized form of the given XML.</returns>
        public static Token FromXml(XElement xml)
        {
            string elementName = xml.Name.LocalName;
            if (!TokenDeserializers.ContainsKey(elementName))
                throw new IOException("Unknown token type: " + elementName);
            return TokenDeserializers[elementName](xml);
        }
    }
}
