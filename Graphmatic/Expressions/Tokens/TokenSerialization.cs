﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic.Expressions.Tokens
{
    /// <summary>
    /// Represents a method that deserializes a token from its XML representation.
    /// </summary>
    /// <param name="parent">The parent expression of the token.</param>
    /// <param name="xml">THe XML representation of the token.</param>
    /// <returns>The deserialized form of the token.</returns>
    public delegate IToken TokenDeserializerFactory(Expression parent, XElement xml);

    /// <summary>
    /// Provides methods to assist in (de)serialization of serialized Graphmatic expressions.
    /// </summary>
    public static class TokenSerialization
    {
        /// <summary>
        /// A list of deserializing constructors for tokens with differing XML element names.
        /// </summary>
        private static Dictionary<string, TokenDeserializerFactory> TokenDeserializers = new Dictionary<string, TokenDeserializerFactory>
        {
            { "BinaryOperation", (parent, xml) => new BinaryOperationToken(parent, xml) },
            { "Digit", (parent, xml) => new DigitToken(parent, xml) },
            { "Exp", (parent, xml) => new ExpToken(parent, xml) },
            { "Fraction", (parent, xml) => new FractionToken(parent, xml) },
            { "Function", (parent, xml) => new FunctionToken(parent, xml) },
            { "Root", (parent, xml) => new RootToken(parent, xml) },
            { "Log", (parent, xml) => new LogToken(parent, xml) }
        };

        /// <summary>
        /// Deserializes a token from its XML representation.
        /// </summary>
        /// <param name="parent">The parent expression of the token.</param>
        /// <param name="xml">The XML representation of a token to deserialize.</param>
        /// <returns>The deserialized form of the given XML.</returns>
        public static IToken FromXml(Expression parent, XElement xml)
        {
            return TokenDeserializers[xml.Name.LocalName](parent, xml);
        }
    }
}
