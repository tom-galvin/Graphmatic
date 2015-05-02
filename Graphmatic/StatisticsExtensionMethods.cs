using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphmatic
{
    /// <summary>
    /// Provides methods to assist in performing statistical calculation on
    /// <c>Graphmatic.Interaction.DataSet</c> objects.
    /// </summary>
    public static class StatisticsExtensionMethods
    {
        /// <summary>
        /// Calculates the mean of a given enumerable set of data.
        /// </summary>
        /// <param name="data">The data for which to calculate the mean.</param>
        /// <returns>Returns the mean of the given data.</returns>
        public static double Mean(this IEnumerable<double> data)
        {
            int count = 0;
            double accumulator = 0;

            foreach (double row in data)
            {
                count += 1;
                accumulator += row;
            }

            return accumulator / count;
        }

        /// <summary>
        /// Calculates the double mean of a given enumerable set of data.
        /// </summary>
        /// <param name="data">The data for which to calculate the mean.</param>
        /// <returns>Returns the double mean of the given data.</returns>
        public static Tuple<double, double> Mean(this IEnumerable<Tuple<double, double>> data)
        {
            int count = 0;
            double accumulator1 = 0, accumulator2 = 0;

            foreach (Tuple<double, double> row in data)
            {
                count += 1;
                accumulator1 += row.Item1;
                accumulator2 += row.Item2;
            }

            return new Tuple<double, double>(accumulator1 / count, accumulator2 / count);
        }

        /// <summary>
        /// Calculates the unnormalized variance of a given enumerable set of data.
        /// </summary>
        /// <param name="data">The data for which to calculate the unnormalized variance.</param>
        /// <returns>Returns the unnormalized variance of the given data.</returns>
        public static double UnnormalizedVariance(this IEnumerable<double> data)
        {
            double mean = data.Mean();
            return data
                .Select(x =>
                {
                    double meanOffset = x - mean;
                    return meanOffset * meanOffset;
                })
                .Sum();
        }

        /// <summary>
        /// Calculates the unnormalized covariance of a given enumerable set of data.
        /// </summary>
        /// <param name="data">The data for which to calculate the unnormalized covariance.</param>
        /// <returns>Returns the unnormalized covariance of the given data.</returns>
        public static double UnnormalizedCovariance(this IEnumerable<Tuple<double, double>> data)
        {
            Tuple<double, double> mean = data.Mean();
            return data
                .Select(x =>
                {
                    double meanOffset1 = x.Item1 - mean.Item1;
                    double meanOffset2 = x.Item2 - mean.Item2;
                    return meanOffset1 * meanOffset2;
                })
                .Sum();
        }
    }
}
