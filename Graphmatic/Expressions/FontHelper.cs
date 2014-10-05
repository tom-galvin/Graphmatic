using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphmatic.Expressions
{
    public static class FontHelper
    {
        private static Rectangle[] largeChars;
        private static Rectangle[] smallChars;

        static FontHelper()
        {
            largeChars = new Rectangle[100];
            smallChars = new Rectangle[100];
            for (int i = 0; i < 100; i++)
            {
                largeChars[i] = new Rectangle(1 + (i % 10) * 6, (i / 10) * 9, 5, 9);
                smallChars[i] = new Rectangle(1 + (i % 10) * 6, (i / 10) * 6, 5, 6);
            }
        }

        public static Rectangle RectOf(int index, bool small)
        {
            return small ? smallChars[index] : largeChars[index];
        }

        public static Rectangle RectOf(char c, bool small)
        {
            return RectOf(Properties.Resources.FontChars.IndexOf(c), small);
        }

        public static void DrawPixelString(this Graphics g, string s, bool small, int x, int y)
        {
            int currentX = x;
            foreach (char c in s)
            {
                Rectangle sourceRect = RectOf(c, small);
                Rectangle destRect = new Rectangle(currentX, y, sourceRect.Width, sourceRect.Height);
                g.DrawImage(small ? Properties.Resources.SmallFont : Properties.Resources.LargeFont, destRect, sourceRect, GraphicsUnit.Pixel);
                currentX += destRect.Width + 1;
            }
        }
    }
}
