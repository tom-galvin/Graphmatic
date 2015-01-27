using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Graphmatic.Interaction.Plotting
{
    /// <summary>
    /// Provides extension methods relating to plotting graphs.
    /// </summary>
    public static class PlotExtensionMethods
    {
        /// <summary>
        /// Clamps an integer between two values.
        /// </summary>
        /// <param name="i">The integer to clamp.</param>
        /// <param name="min">The clamp upper-bound.</param>
        /// <param name="max">The clamp lower-bound.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(this int i, int min, int max)
        {
            return
                i < min ? min :
                i > max ? max :
                i;
        }

        /// <summary>
        /// Warps a color by applying a power transformation to its color components.
        /// <para/>
        /// This is like a gamma correction, but for each color channel.
        /// </summary>
        /// <param name="color">The color to warp.</param>
        /// <param name="power">The power index.</param>
        /// <returns>The warped color.</returns>
        public static Color ColorWarp(this Color color, double power)
        {
            double red = (double)color.R / 255.0;
            double green = (double)color.G / 255.0;
            double blue = (double)color.B / 255.0;

            red = Math.Pow(red, power);
            green = Math.Pow(green, power);
            blue = Math.Pow(blue, power);

            return Color.FromArgb(
                color.A,
                ((int)(red * 255.0)).Clamp(0, 255),
                ((int)(green * 255.0)).Clamp(0, 255),
                ((int)(blue * 255.00)).Clamp(0, 255));
        }

        /// <summary>
        /// Creates a new Color from an existing Color using a new alpha component.
        /// </summary>
        /// <param name="color">The template color.</param>
        /// <param name="alpha">The alpha value to use.</param>
        /// <returns>The new Color with the given alpha component.</returns>
        public static Color ColorAlpha(this Color color, double alpha)
        {
            return Color.FromArgb(
                ((int)(alpha * 255.0)).Clamp(0, 255),
                color.R,
                color.G,
                color.B);
        }

        /// <summary>
        /// Gets the graph label suffix for a given axis type.
        /// <para/>
        /// For example, degrees have a degree symbol (°) suffix, and radians have none.
        /// </summary>
        /// <param name="axisType">The graph axis type for which to return the suffix.</param>
        /// <returns>The graph label suffix for a given axis type.</returns>
        public static string AxisTypeExtension(this GraphAxisType axisType)
        {
            switch (axisType)
            {
                case GraphAxisType.Degrees:
                    return "°";
                case GraphAxisType.Grads:
                    return "g";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Gets the graph label scale for a gixen axis type.
        /// <para/>
        /// For example, labels in degrees are scaled by a factor of 180/pi, and radians are not scaled.
        /// </summary>
        /// <param name="axisType">The graph axis type for which to return the scale.</param>
        /// <returns>The graph label scale for a gixen axis type.</returns>
        public static double AxisTypeLabelScale(this GraphAxisType axisType)
        {
            switch (axisType)
            {
                case GraphAxisType.Degrees:
                    return 180.0 / Math.PI;
                case GraphAxisType.Grads:
                    return 200.0 / Math.PI;
                default:
                    return 1.0;
            }
        }

        /// <summary>
        /// Gets the graph grid scale for a gixen axis type.
        /// <para/>
        /// For example, degrees and grads are scaled by a factor of 2*pi/5 to make major intervals line
        /// up with the full angle around a circle, and radians are left unscaled.
        /// </summary>
        /// <param name="axisType">The graph grid type for which to return the scale.</param>
        /// <returns>The grid scale for a gixen axis type.</returns>
        public static double AxisTypeGridScale(this GraphAxisType axisType)
        {
            switch (axisType)
            {
                case GraphAxisType.Degrees:
                    return 2 * Math.PI / 5;
                case GraphAxisType.Grads:
                    return 2 * Math.PI / 5;
                default:
                    return 1.0;
            }
        }
    }
}
