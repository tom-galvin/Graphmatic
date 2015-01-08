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
                ((int)(red * 255.0)).Clamp(0, 255),
                ((int)(green * 255.0)).Clamp(0, 255),
                ((int)(blue * 255.00)).Clamp(0, 255),
                color.A);
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
    }
}
