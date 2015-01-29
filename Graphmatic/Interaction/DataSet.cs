using Graphmatic.Interaction.Plotting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graphmatic.Interaction
{
    /// <summary>
    /// Represents a numerical data set in a document.
    /// </summary>
    public partial class DataSet : Resource, IEnumerable<double[]>, IPlottable
    {
        /// <summary>
        /// Gets an array of the variables that each row (tuple) in this data set has.
        /// </summary>
        public char[] Variables
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the string <c>"Data Set"</c>.
        /// </summary>
        public override string Type
        {
            get
            {
                return "Data Set";
            }
        }

        /// <summary>
        /// Gets a row (tuple) from this data set with the given zero-based <paramref name="index"/>.<para/>
        /// The row is returned as an array of doubles containing the variables given in the order contained in the <c>Variables</c> property.
        /// </summary>
        /// <param name="index">The index of the row to return.</param>
        /// <returns>The row with the index given in <paramref name="index"/> as an array of doubles.</returns>
        public double[] this[int index]
        {
            get
            {
                return Data[index];
            }
            set
            {
                Data[index] = value;
            }
        }

        /// <summary>
        /// The actual list of data that this data set contains.
        /// </summary>
        private List<double[]> Data;

        /// <summary>
        /// Initialize a new instance of the <c>Graphmatic.Interaction.DataSet</c> class with the given variables.
        /// </summary>
        /// <param name="variableNames">The variables that each row (tuple/member) of this data set contains.</param>
        public DataSet(params char[] variableNames)
            : base()
        {
            Variables = new char[variableNames.Length];
            for (int i = 0; i < variableNames.Length; i++)
                Variables[i] = variableNames[i];
            Data = new List<double[]>();
        }

        /// <summary>
        /// Inserts <paramref name="value"/> into a resized clone of <paramref name="array"/> at the index given in
        /// <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">The type of the objects in <paramref name="array"/>.</typeparam>
        /// <param name="array">The array to insert into.</param>
        /// <param name="index">The index to insert at.</param>
        /// <param name="value">The value to insert.</param>
        private T[] Insert<T>(T[] array, int index, T value)
        {
            T[] newArray = new T[array.Length + 1];
            for (int i = 0; i < array.Length + 1; i++)
            {
                if (i < index)
                {
                    newArray[i] = array[i];
                }
                else if (i == index)
                {
                    newArray[i] = value;
                }
                else
                {
                    newArray[i] = array[i - 1];
                }
            }
            return newArray;
        }

        /// <summary>
        /// Removes the value at index <paramref name="index"/> from a shallow copy of <paramref name="array"/>.
        /// </summary>
        /// <typeparam name="T">The type of the objects in <paramref name="array"/>.</typeparam>
        /// <param name="array">The array to remove from.</param>
        /// <param name="index">The index to remove at.</param>
        private T[] Remove<T>(T[] array, int index)
        {
            T[] newArray = new T[array.Length - 1];
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (i < index)
                {
                    newArray[i] = array[i];
                }
                else
                {
                    newArray[i] = array[i + 1];
                }
            }
            return newArray;
        }

        /// <summary>
        /// Swaps the values at <paramref name="index1"/> and <paramref name="index2"/> in a shallow copy of <paramref name="array"/>.
        /// </summary>
        /// <typeparam name="T">The type of the objects in <paramref name="array"/>.</typeparam>
        /// <param name="array">The array to swap values in.</param>
        /// <param name="index1">The indices at which to swap values.</param>
        /// <param name="index2">The indices at which to swap values.</param>
        private T[] Swap<T>(T[] array, int index1, int index2)
        {
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index1)
                {
                    newArray[i] = array[index2];
                }
                else if (i == index2)
                {
                    newArray[i] = array[index1];
                }
                else
                {
                    newArray[i] = array[i];
                }
            }
            return newArray;
        }

        /// <summary>
        /// Initialize a new empty instance of the <c>Graphmatic.Interaction.DataSet</c> class from serialized XML data.
        /// </summary>
        /// <param name="xml">The XML data to use for deserializing this Resource.</param>
        public DataSet(XElement xml)
            : base(xml)
        {
            var variables = xml.Element("Variables").Elements("Variable");
            Variables = new char[variables.Count()];
            Dictionary<char, int> variableIndices = new Dictionary<char, int>();
            int i = 0;
            foreach (XElement variable in variables)
            {
                Variables[i] = variable.Attribute("Name").Value[0];
                variableIndices[Variables[i]] = i;
                i++;
            }

            var data = xml.Element("Data");
            Data = new List<double[]>();
            foreach (XElement row in data.Elements("Row"))
            {
                double[] rowData = new double[Variables.Length];
                foreach (XElement column in row.Elements("Column"))
                {
                    var variable = column.Attribute("Name").Value[0];
                    if (variableIndices.ContainsKey(variable))
                    {
                        rowData[variableIndices[variable]] = Double.Parse(column.Attribute("Value").Value);
                    }
                    else
                    {
                        throw new IOException(
                            "Unknown variable " + variable +
                            " referenced in Data Set " + Name + ".");
                    }
                }
                Data.Add(rowData);
            }
        }

        /// <summary>
        /// Adds a variable to this data set, and adjusts existing data points accordingly.
        /// </summary>
        /// <param name="index">The index in the order of variable to insert the new variable.</param>
        /// <param name="variableName">The name of the variable to insert.</param>
        /// <param name="defaultValue">The default value of the variable in existing data points.</param>
        public void AddVariable(int index, char variableName, double defaultValue)
        {
            Variables = Insert(Variables, index, variableName);
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = Insert(Data[i], index, defaultValue);
            }
        }

        /// <summary>
        /// Removes a variable from the data set, removing its value from existing data points accordingly.
        /// This operation is destructive, so be careful.
        /// </summary>
        /// <param name="index">The index of the variable to remove.</param>
        public void RemoveVariable(int index)
        {
            Variables = Remove(Variables, index);
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = Remove(Data[i], index);
            }
        }

        /// <summary>
        /// Swaps the order of two variables in the data set, and adjusts existing data points accordingly.
        /// </summary>
        /// <param name="index1">The indices of the two variables to swap.</param>
        /// <param name="index2">The indices of the two variables to swap.</param>
        public void SwapVariables(int index1, int index2)
        {
            Variables = Swap(Variables, index1, index2);
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = Swap(Data[i], index1, index2);
            }
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
                Properties.Resources.DataSet32 :
                Properties.Resources.DataSet16;
        }

        /// <summary>
        /// Converts this object to its equivalent serialized XML representation.
        /// </summary>
        /// <returns>The serialized representation of this Graphmatic object.</returns>
        public override XElement ToXml()
        {
            XElement baseElement = base.ToXml();
            baseElement.Name = "DataSet";
            baseElement.Add(
                new XElement("Variables",
                    Variables.Select(v => new XElement("Variable", new XAttribute("Name", v.ToString())))),
                new XElement("Data",
                    Data.Select(r => new XElement("Row",
                        r.Select((c, i) => new XElement("Column",
                            new XAttribute("Name", Variables[i]),
                            new XAttribute("Value", c)))))));
            return baseElement;
        }

        /// <summary>
        /// Adds a data point to the data set.
        /// </summary>
        /// <param name="data">The data set to add, as an array of doubles.</param>
        public void Add(params double[] data)
        {
            if (data.Length != Variables.Length)
            {
                throw new ArgumentException("Data length must be same as the number of arguments.");
            }
            else
            {
                Data.Add(data);
            }
        }

        /// <summary>
        /// Removes a data point from the data set.
        /// This operation is destructive, so be careful.
        /// </summary>
        /// <param name="index">The zero-based index in the data set of the data point to remove.</param>
        public void Remove(int index)
        {
            Data.RemoveAt(index);
        }

        /// <summary>
        /// Removes all data points from the data set.
        /// This operation is destructive, so be careful.
        /// </summary>
        public void Clear()
        {
            Data.Clear();
        }

        /// <summary>
        /// Sets the internal list containing all of the data points to a new list. This is used when the 
        /// data set is edited by a Windows Forms control so that the data set can be updated more efficiently.
        /// </summary>
        /// <param name="data">The list containing the new data representing the data set in memory.</param>
        internal void Set(List<double[]> data)
        {
            Data = data;
        }

        public IEnumerator<double[]> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Data).GetEnumerator();
        }

        /// <summary>
        /// Returns the zero-based index of a variable in the order of variables in this data set.
        /// </summary>
        /// <param name="variable">The variable for which to find the index.</param>
        private int IndexOfVariable(char variable)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i] == variable)
                    return i;
            }
            return -1;
        }
        
        /// <summary>
        /// The width of the crosses used to plot data sets onto a graph.
        /// </summary>
        public const float DataPointPenWidth = 2f;

        /// <summary>
        /// The size of the strokes used to draw the crosses for plotting data sets on a graph.
        /// </summary>
        public const int DataPointCrossSize = 3;

        /// <summary>
        /// Plots this DataSet onto <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">The Graph to plot this IPlottable onto.</param>
        /// <param name="graphics">The GDI+ drawing surface to use for plotting this IPlottable.</param>
        /// <param name="graphSize">The size of the Graph on the screen. This is a property of the display rather than the
        /// graph and is thus not included in the graph's parameters.</param>
        /// <param name="plotParams">The parameters used to plot this IPlottable.</param>
        /// <param name="resolution">The plotting resolution to use. This does not have an effect for data sets.</param>
        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, PlotResolution resolution)
        {
            if (resolution == PlotResolution.Resize) return;

            int horizontalVariableIndex = IndexOfVariable(graph.Parameters.HorizontalAxis),
                verticalVariableIndex = IndexOfVariable(graph.Parameters.VerticalAxis);

            if (horizontalVariableIndex == -1)
                throw new Exception("This data set cannot be plotted over the " +
                                    graph.Parameters.HorizontalAxis.ToString() +
                                    " variable, as it does not contain such a variable.");

            if (verticalVariableIndex == -1)
                throw new Exception("This data set cannot be plotted over the " +
                                    graph.Parameters.VerticalAxis.ToString() +
                                    " variable, as it does not contain such a variable.");

            using (Pen dataPointPen = new Pen(plotParams.PlotColor, DataPointPenWidth))
            {

                foreach (double[] row in Data)
                {
                    double horizontal = row[horizontalVariableIndex],
                           vertical = row[verticalVariableIndex];

                    int graphX, graphY;
                    graph.ToImageSpace(
                        graphSize,
                        horizontal, vertical,
                        out graphX, out graphY);

                    graphics.DrawLine(dataPointPen, graphX - DataPointCrossSize, graphY - DataPointCrossSize, graphX + DataPointCrossSize, graphY + DataPointCrossSize);
                    graphics.DrawLine(dataPointPen, graphX - DataPointCrossSize, graphY + DataPointCrossSize, graphX + DataPointCrossSize, graphY - DataPointCrossSize);
                }
            }
        }

        /// <summary>
        /// Determines whether this plottable object can be plotted using the specific given variables.
        /// This returns true if both <paramref name="variable1"/> and <paramref name="variable2"/> exist in
        /// the list of variables contained in this data set, which is done by calling <c>IndexOfVariable</c>.
        /// </summary>
        /// <param name="variable1">A variable plotted on an axis of a Graph.</param>
        /// <param name="variable2">A variable plotted on an axis of a Graph.</param>
        /// <returns>Returns whether this data set can be plotted over the given variables.</returns>
        public bool CanPlot(char variable1, char variable2)
        {
            return
                IndexOfVariable(variable1) != -1 &&
                IndexOfVariable(variable2) != -1;
        }
    }
}
