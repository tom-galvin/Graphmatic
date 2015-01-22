using Graphmatic.Interaction.Plotting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction
{
    public partial class DataSet : Resource, IEnumerable<double[]>, IPlottable
    {
        /// <summary>
        /// Returns the value of the given variable from each row in this data set.
        /// </summary>
        /// <param name="variable">The variable to extract from each row in the data set.</param>
        /// <returns>An enumerable collection of the value of the given variable in each row in the data set.</returns>
        public IEnumerable<double> RowSelect(char variable)
        {
            int index = IndexOfVariable(variable);
            if (index == -1)
                throw new IndexOutOfRangeException(String.Format("Variable {0} does not exist in the DataSet.", variable));

            return Data.Select(r => r[index]);
        }

        /// <summary>
        /// Returns a tuple containing the values of the given variables from each row in this data set.
        /// </summary>
        /// <param name="variable1">The first variable to extract from each row in the data set.</param>
        /// <param name="variable2">The second variable to extract from each row in the data set.</param>
        /// <returns>An enumerable collection of the value of the given variables in each row in the data set.</returns>
        public IEnumerable<Tuple<double, double>> RowSelect(char variable1, char variable2)
        {
            int index1 = IndexOfVariable(variable1);
            if (index1 == -1)
                throw new IndexOutOfRangeException(String.Format("Variable {0} does not exist in the DataSet.", variable1));

            int index2 = IndexOfVariable(variable2);
            if (index2 == -1)
                throw new IndexOutOfRangeException(String.Format("Variable {0} does not exist in the DataSet.", variable2));

            return Data.Select(r => new Tuple<double, double>(r[variable1], r[variable2]));
        }

        /// <summary>
        /// Calculates the unnormalized variances for two variables in the data set.
        /// </summary>
        /// <param name="variable1">The first variable for which to calculate the unnormalized variance.</param>
        /// <param name="variable2"></param>
        /// <param name="s11"></param>
        /// <param name="s22"></param>
        /// <param name="s12"></param>
        public void CalculateVariances(char variable1, char variable2, out double s11, out double s22, out double s12)
        {
            s11 = RowSelect(variable1).UnnormalizedVariance();
            s22 = RowSelect(variable2).UnnormalizedVariance();
            s12 = RowSelect(variable1, variable2).UnnormalizedCovariance();
        }

        public double Pmcc(char variable1, char variable2)
        {
            double s11, s22, s12;
            CalculateVariances(variable1, variable2, out s11, out s22, out s12);
            return s12 / Math.Sqrt(s11 * s22);
        }

        public Equation LinearRegression(char variable1, char variable2)
        {
            double s11, s22, s12;
            CalculateVariances(variable1, variable2, out s11, out s22, out s12);
            double b = 
        }
    }
}
