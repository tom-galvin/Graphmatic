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
    /// Exposes methods for easier deserialization of data from XML.
    /// </summary>
    public static class SerializationExtensionMethods
    {
        /// <summary>
        /// A dictionary associating XML element names with their respective deserialized .NET types.
        /// </summary>
        private static Dictionary<string, Type> GraphmaticObjects = null;

        /// <summary>
        /// Registers all objects in the current AppDomain with the GraphmaticObjectAttribute as objects
        /// that can be deserialized by the <c>Deserialize&lt;T&gt;</c> method, where the XML element name
        /// is registered as the class name.
        /// </summary>
        internal static void RegisterGraphmaticObjects()
        {
            if (GraphmaticObjects == null)
            {
                GraphmaticObjects = new Dictionary<string, Type>();
                IEnumerable<Type> graphmaticObjectTypes = AppDomain.CurrentDomain   // get the AppDomain
                        .GetAssemblies()                                            // all assemblies including future potential plugins (!!)
                        .SelectMany(a => a.GetTypes()                               // all types in those assemblies
                                          .Where(t => t.GetCustomAttributes(        // get attributes on those types...
                                              typeof(GraphmaticObjectAttribute),    // that are GraphmaticObjectAttributes
                                              false).Length > 0)                    // length > 0, ie. does the object have a GraphmaticObjectAttribute?
                                   );                                               // if so, it's a Graphmatic object - add it to the array

                foreach (Type type in graphmaticObjectTypes)
                {
                    GraphmaticObjects.Add(type.Name, type);
                }
            }
            else
            {
                throw new InvalidOperationException("Graphmatic object types have already been registered for this instance.");
            }
        }

        /// <summary>
        /// Deserializes a class from XML data using reflection.
        /// This isn't the prettiest method ever, but it reduces the need for needless deserialization factories
        /// cluttering the rest of the codebase.<para/> If the correct type cannot be found then this method will
        /// throw a <c>System.Exception</c>.<para/> I was hesitant to use the stock .NET Serialization as my code
        /// needed to be "complex" per the AQA specification. This will (ideally) be changed and completely ripped
        /// out after my coursework has been marked to make the code cleaner.<para/>
        /// If Graphmatic object types have not yet been registered, this will throw an <c>InvalidOperationException</c>.
        /// </summary>
        /// <typeparam name="T">The type of class to deserialize.</typeparam>
        /// <param name="xml">The XML to deserialize.</param>
        /// <returns>The deserialized form of the data.</returns>
        public static T Deserialize<T>(this XElement xml) where T : class
        {
            if (GraphmaticObjects == null)
                throw new InvalidOperationException(
                    "Graphmatic object types have not yet been registered for this instance. Call the " +
                    "Graphmatic.SerializationExtensionMethods.RegisterGraphmaticObjects() method first.");

            string typeName = xml.Name.ToString();
            
            if(!GraphmaticObjects.ContainsKey(typeName))
                throw new Exception(String.Format("Unknown {0} type: {1}", typeof(T).Name, typeName));

            try
            {
                object initializedObject = GraphmaticObjects[typeName] // get the type
                    .GetConstructor(new Type[] { typeof(XElement) })   // get the constructor accepting an XElement
                    .Invoke(new object[] { xml });                     // invoke it
                return initializedObject as T;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
