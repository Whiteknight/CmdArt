using CmdArt.Colors;
using System;
using System.Collections.Generic;

namespace CmdArt.Rendering.Images
{
    public class ColorsPixelSource : IPixelSource
    {
        public IEnumerable<ConsolePixel> GetPixels()
        {
            ConsoleColor[] colors = {
                ConsoleColor.Magenta,
                ConsoleColor.Red,
                ConsoleColor.Yellow,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.Blue
            };

            for (int i = 0; i < colors.Length; i++)
            {
                // Get the pure colors
                for (int j = 1; j < 12; j++)
                {
                    ConsolePixel p = GetPureColorPixel(colors[i], j);
                    yield return p;
                }

                // Get direct pure-color blends
                for (int j = 0; j < 4; j++)
                {
                    ConsolePixel p = GetBlendColorPixel(colors[i], colors[(i + 1) % colors.Length], j);
                    yield return p;

                    p = GetBlendColorPixel(colors[i].MakeDark(), colors[(i + 1) % colors.Length].MakeDark(), j);
                    yield return p;
                }

                // Get diagonal bright-dark color blends
                for (int j = 0; j < 4; j++)
                {
                    ConsolePixel p = GetBlendColorPixel(colors[i], colors[(i + 1) % colors.Length].MakeDark(), j);
                    yield return p;

                    p = GetBlendColorPixel(colors[i], colors[(i + colors.Length - 1) % colors.Length].MakeDark(), j);
                    yield return p;
                }

                // Get Gray-Blends
                for (int j = 0; j < 4; j++)
                {
                    ConsolePixel p = GetBlendColorPixel(colors[i], ConsoleColor.Gray, j);
                    yield return p;

                    p = GetBlendColorPixel(colors[i].MakeDark(), ConsoleColor.DarkGray, j);
                    yield return p;
                }
            }

            // Add in some special non-adjacent blends where appropriate
            for (int j = 0; j < 4; j++)
            {
                ConsolePixel p = GetBlendColorPixel(ConsoleColor.Green, ConsoleColor.Blue, j);
                yield return p;

                p = GetBlendColorPixel(ConsoleColor.DarkGreen, ConsoleColor.DarkBlue, j);
                yield return p;
            }

            // Add some "brown" shades
            ConsolePixel brown = GetBlendColorPixel(ConsoleColor.Red, ConsoleColor.Green, 2);
            yield return brown;
            brown = GetBlendColorPixel(ConsoleColor.DarkRed, ConsoleColor.DarkGreen, 2);
            yield return brown;
        }

        static ConsolePixel GetPureColorPixel(ConsoleColor baseColor, int i)
        {
            if (i > 13)
                i = 13;
            if (i < 0)
                i = 0;
            const string blocks = " \x2591\x2592\x2593 \x2591\x2592\x2593 \x2591\x2592\x2593 ";
            ConsoleColor bright = baseColor.MakeBright();
            ConsoleColor dark = baseColor.MakeDark();
            ConsoleColor[] scales = { ConsoleColor.Black, dark, bright, ConsoleColor.White, ConsoleColor.White };
            int gidx = i / 4;
            ConsoleColor bg = scales[gidx];
            ConsoleColor fg = scales[gidx + 1];
            char c = blocks[i];
            return new ConsolePixel(new Palette(bg, fg), c);
        }

        private static ConsolePixel GetBlendColorPixel(ConsoleColor c1, ConsoleColor c2, int i)
        {
            if (i > 4)
                i = 4;
            if (i < 0)
                i = 0;
            // TODO: Use the named constants from the Unicode or AsciiCodePage437 classes
            const string blocks = " \x2591\x2592\x2593";
            if (i == 4)
                return new ConsolePixel(new Palette(c2, c1), ' ');
            return new ConsolePixel(new Palette(c1, c2), blocks[i]);
        }
    }
}