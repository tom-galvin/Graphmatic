using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    /// <summary>
    /// Exposes helper methods for the display of the calculation font.
    /// </summary>
    public static class FontHelperExtensionMethods
    {
        /// <summary>
        /// The large character set.
        /// </summary>
        private static Rectangle[] LargeChars;
        /// <summary>
        /// The small character set.
        /// </summary>
        private static Rectangle[] SmallChars;

        /// <summary>
        /// Initializes the sizes of the character set bounding boxes in the font images.
        /// </summary>
        static FontHelperExtensionMethods()
        {
            LargeChars = new Rectangle[100];
            SmallChars = new Rectangle[100];
            for (int i = 0; i < 100; i++)
            {
                LargeChars[i] = new Rectangle(1 + (i % 10) * 6, (i / 10) * 9, 5, 9);
                SmallChars[i] = new Rectangle(1 + (i % 10) * 6, (i / 10) * 6, 5, 6);
            }
        }

        /// <summary>
        /// Gets the rectangle (in the font image) of a given character determined by its <paramref name="index"/> in the character set.
        /// </summary>
        /// <param name="index">The character's index in the character set.</param>
        /// <param name="small">Whether to get the small character set rectangle, or the large character set rectangle.</param>
        /// <returns>Returns the bounding rectangle in the font image of the given character.</returns>
        public static Rectangle RectOf(int index, bool small)
        {
            return small ? SmallChars[index] : LargeChars[index];
        }

        /// <summary>
        /// Gets the rectangle (in the font image) of a given character determined by its <paramref name="index"/> in the character set.
        /// </summary>
        /// <param name="c">The character to determine the rectangle of.</param>
        /// <param name="small">Whether to get the small character set rectangle, or the large character set rectangle.</param>
        /// <returns>Returns the bounding rectangle in the font image of the given character.</returns>
        public static Rectangle RectOf(char c, bool small)
        {
            return RectOf(Properties.Resources.FontChars.IndexOf(c), small);
        }

        /// <summary>
        /// Gets the image of a character in the character set.<para/>
        /// This is not used for drawing of characters in the expression display as that would be
        /// nastily inefficient. Instead this is just for the display of symbols on UI buttons.
        /// </summary>
        /// <param name="c">The character to get the image of.</param>
        /// <param name="small">Whether to use the small or large character set.</param>
        /// <param name="scale">The pixel scale of the image. This must be a value greater than 1, or an <c>ArgumentException</c> is thrown.</param>
        /// <returns>Returns the image of <paramref name="c"/> in the <paramref name="small"/> or large character set,
        /// with the given pixel <paramref name="scale"/>.</returns>
        public static Bitmap GetCharacterImage(char c, bool small, int scale)
        {
            if (scale < 1) throw new ArgumentException("Character's pixel scale must not be less than one.");
            Rectangle sourceRect = RectOf(c, small);
            sourceRect.X -= 1;
            sourceRect.Width += 1;
            Rectangle destRect = new Rectangle(
                1,
                1,
                sourceRect.Width * scale,
                sourceRect.Height * scale);
            Bitmap characterImage = new Bitmap(sourceRect.Width * scale + 2, sourceRect.Height * scale + 2);
            Graphics graphics = Graphics.FromImage(characterImage);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.DrawImage(
                small ?
                    Properties.Resources.SmallFont :
                    Properties.Resources.LargeFont,
                destRect,
                sourceRect,
                GraphicsUnit.Pixel);
            return characterImage;
        }

        /// <summary>
        /// Writes a string to a given GDI drawing surface using the Graphmatic calculation font.
        /// </summary>
        /// <param name="graphics">The GDI+ drawing surface to draw onto.</param>
        /// <param name="str">The string to draw.</param>
        /// <param name="small">Whether to use the small or large character set.</param>
        /// <param name="x">The X co-ordinate to draw to on <paramref name="graphics"/>.</param>
        /// <param name="y">The Y co-ordinate to draw to on <paramref name="graphics"/>.</param>
        public static void DrawExpressionString(this Graphics graphics, string str, bool small, int x, int y)
        {
            int currentX = x;
            foreach (char c in str)
            {
                Rectangle sourceRect = RectOf(c, small);
                Rectangle destRect = new Rectangle(currentX, y, sourceRect.Width, sourceRect.Height);
                graphics.DrawImage(small ? Properties.Resources.SmallFont : Properties.Resources.LargeFont, destRect, sourceRect, GraphicsUnit.Pixel);
                currentX += destRect.Width + 1;
            }
        }
    }
}
