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
    public class DataSet : Resource, IEnumerable<double[]>, IPlottable
    {
        public char[] Variables
        {
            get;
            protected set;
        }

        public override string Type
        {
            get
            {
                return "Data Set";
            }
        }

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

        private List<double[]> Data;

        public DataSet(params char[] variableNames)
            : base()
        {
            Variables = new char[variableNames.Length];
            for (int i = 0; i < variableNames.Length; i++)
                Variables[i] = variableNames[i];
            Data = new List<double[]>();
        }

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

        public void AddVariable(int index, char variableName, double defaultValue)
        {
            Variables = Insert(Variables, index, variableName);
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = Insert(Data[i], index, defaultValue);
            }
        }

        public void RemoveVariable(int index)
        {
            Variables = Remove(Variables, index);
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = Remove(Data[i], index);
            }
        }

        public void SwapVariables(int index1, int index2)
        {
            Variables = Swap(Variables, index1, index2);
            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = Swap(Data[i], index1, index2);
            }
        }

        public override Image GetResourceIcon(bool large)
        {
            return large ?
                Properties.Resources.DataSet32 :
                Properties.Resources.DataSet16;
        }

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

        public void Remove(int index)
        {
            Data.RemoveAt(index);
        }

        public void Clear()
        {
            Data.Clear();
        }

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

        private int IndexOfVariable(char variable)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i] == variable)
                    return i;
            }
            return -1;
        }

        public const float DataPointPenWidth = 2f;
        public const int DataPointCrossSize = 3;
        public void PlotOnto(Graph graph, Graphics graphics, Size graphSize, PlottableParameters plotParams, GraphParameters graphParams)
        {
            Pen dataPointPen = new Pen(plotParams.PlotColor, DataPointPenWidth);

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

            foreach (double[] row in Data)
            {
                double horizontal = row[horizontalVariableIndex],
                       vertical = row[verticalVariableIndex];

                int graphX, graphY;
                graph.ToImageSpace(
                    graphSize, graphParams,
                    horizontal, vertical,
                    out graphX, out graphY);

                graphics.DrawLine(dataPointPen, graphX - DataPointCrossSize, graphY - DataPointCrossSize, graphX + DataPointCrossSize, graphY + DataPointCrossSize);
                graphics.DrawLine(dataPointPen, graphX - DataPointCrossSize, graphY + DataPointCrossSize, graphX + DataPointCrossSize, graphY - DataPointCrossSize);
            }
        }


        public bool CanPlot(char variable1, char variable2)
        {
            return
                IndexOfVariable(variable1) != -1 &&
                IndexOfVariable(variable2) != -1;
        }
    }
}
