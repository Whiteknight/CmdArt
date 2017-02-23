using System;
using System.Collections.Generic;

namespace CmdArt.Images.Sources
{
    public class GrayscaleBrushSource : IBrushSource
    {
        #region Implementation of IPixelSource

        public IEnumerable<ImageBrush> GetPixels()
        {
            for (int i = 0; i < 13; i++)
            {
                ImageBrush p = GetGreyscalePixel(i);
                yield return p;
            }
        }

        private static ImageBrush GetGreyscalePixel(int i)
        {
            if (i > 12)
                i = 12;
            if (i < 0)
                i = 0;

            ConsoleColor[] greyscales = { ConsoleColor.Black, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White };
            const string blocks = " \x2591\x2592\x2593 \x2591\x2592\x2593 \x2591\x2592\x2593 ";
            int gidx = i / 4;
            ConsoleColor bg = greyscales[gidx];
            ConsoleColor fg = greyscales[gidx + 1];
            char c = blocks[i];
            return new ImageBrush(new Palette(bg, fg), c);
        }

        #endregion
    }
}