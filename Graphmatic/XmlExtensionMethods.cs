using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphmatic
{
    public static class XmlExtensionMethods
    {
        public static T Get<T>(this XElement element, string name, T defaultValue = default(T))
        {
            var e = element.Element(name);
            if (e == null) return defaultValue;

            if (typeof(T) == typeof(string))
            {
                return (T)(object)e.Value;
            }
            else if (typeof(T) == typeof(int))
            {
                return (T)(object)int.Parse(e.Value);
            }
            else if (typeof(T) == typeof(double))
            {
                return (T)(object)double.Parse(e.Value);
            }
            else if (typeof(T) == typeof(bool))
            {
                return (T)(object)bool.Parse(e.Value);
            }
            else if (typeof(T) == typeof(float))
            {
                return (T)(object)float.Parse(e.Value);
            }
            else if (typeof(T) == typeof(Color))
            {
                return (T)(object)Color.FromArgb(int.Parse(e.Value));
            }
            else
            {
                throw new NotSupportedException("Unsupported type " + typeof(T).Name);
            }
        }

        public static void Set<T>(this XElement element, string name, T value)
        {
            string v = value.ToString();

            if (typeof(T) == typeof(Color))
            {
                v = ((Color)(object)value).ToArgb().ToString();
            }

            element.SetElementValue(name, v);
        }
    }
}
