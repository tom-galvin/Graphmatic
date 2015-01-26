using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic
{
    /// <summary>
    /// Represents a method that derives the class name of deserialized data from the serialized equivalent.
    /// </summary>
    /// <param name="xml">The XML representation of the serialized data.</param>
    /// <returns>The deserialized form of the XML.</returns>
    public delegate string TypeNameDerivation(XElement xml);

    /// <summary>
    /// Exposes methods for easier deserialization of data from XML.
    /// </summary>
    public static class SerializationExtensionMethods
    {
        /// <summary>
        /// A type name factory that derives a resource name from an XML element.
        /// </summary>
        public static readonly TypeNameDerivation ResourceName = xml => String.Format("{0}", xml.Name);

        /// <summary>
        /// A type name factory that derives an annotation name from an XML element.
        /// </summary>
        public static readonly TypeNameDerivation AnnotationName = xml => String.Format("{0}", xml.Name);

        /// <summary>
        /// A type name factory that derives a plottable resource name from an XML element.
        /// </summary>
        public static readonly TypeNameDerivation PlottableName = xml => String.Format("{0}", xml.Name);

        /// <summary>
        /// A type name factory that derives a token name from an XML element.
        /// </summary>
        public static readonly TypeNameDerivation TokenName = xml => String.Format("{0}Token", xml.Name);

        /// <summary>
        /// Deserializes a class from XML data using reflection.
        /// This isn't the prettiest method ever, but it reduces the need for needless
        /// deserialization factories cluttering the rest of the codebase.<para/>
        /// If the correct type cannot be found then this method will throw a <c>System.Exception</c>.<para/>
        /// I was hesitant to use the stock .NET Serialization as my code needed to be "complex" per the AQA
        /// specification. This will be changed and completely ripped out after my coursework has been marked
        /// to make the code cleaner.
        /// </summary>
        /// <typeparam name="T">The type of class to deserialize.</typeparam>
        /// <param name="xml">The XML to deserialize.</param>
        /// <param name="typeNameDerivation">The method to use for deriving a type name from the XML data.</param>
        /// <returns>The deserialized form of the data.</returns>
        public static T Deserialize<T>(this XElement xml, TypeNameDerivation typeNameDerivation) where T : class
        {
            string typeName = typeNameDerivation(xml);
            try
            {

                Type type = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(a => a.GetTypes().Where(t => t.Name.EndsWith(typeName)))
                    .First();

                object initializedObject = type.GetConstructor(new Type[] { typeof(XElement) }).Invoke(new object[] { xml });
                return initializedObject as T;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(String.Format("Unknown {0} type: {1}", typeof(T).Name, typeName), ex);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
