using System;
using System.Collections.Generic;
using System.Drawing;
using CmdArt.Colors;

namespace CmdArt.Rendering.Images
{
    public class ConsolePixel
    {
        // percentage of the background color which shows with each glyph
        private static readonly Dictionary<char, byte> _percents = new Dictionary<char, byte> {
            { Unicode.Space, 100 },
            { Unicode.LightShade, 75 },
            { Unicode.MediumShade, 50 },
            { Unicode.DarkShade, 25 }
        };

        private readonly byte _percent;

        public ConsolePixel(Palette palette, char printableCharacter)
        {
            Palette = palette;
            PrintableCharacter = printableCharacter;

            _percent = _percents.ContainsKey(printableCharacter) ? _percents[printableCharacter] : (byte)0;

            Color color1 = palette.Background.ToColor();
            Color color2 = palette.Foreground.ToColor(); ;

            Color = color1.Blend(color2, (double)_percent);
        }

        public Palette Palette { get; private set; }
        public ConsoleColor BackgroundColor { get { return Palette.Background; } }
        public ConsoleColor ForegroundColor { get { return Palette.Foreground; } }
        public char PrintableCharacter { get; private set; }

        public ConsolePixel Invert()
        {
            return new ConsolePixel(Palette.Invert(), PrintableCharacter);
        }

        public Color Color { get; private set; }

        public int AsInt
        {
            get { return Color.GetRgbInt(); }
        }

        public bool IsGrayscale
        {
            get { return BackgroundColor.IsGrayscale() && (_percent == 0 || ForegroundColor.IsGrayscale()); }
        }
    }
} 