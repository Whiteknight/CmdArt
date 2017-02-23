using CmdArt.Colors;
using CmdArt.Screen;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CmdArt.Images
{
    public class ImageBrush
    {
        // percentage of the background color which shows with each glyph
        private static readonly Dictionary<char, byte> _percents = new Dictionary<char, byte> {
            { Unicode.Space, 100 },
            { Unicode.LightShade, 75 },
            { Unicode.MediumShade, 50 },
            { Unicode.DarkShade, 25 }
        };

        private readonly byte _percent;

        public ImageBrush(Palette palette, char printableCharacter)
        {
            Palette = palette;
            PrintableCharacter = printableCharacter;

            _percent = _percents.ContainsKey(printableCharacter) ? _percents[printableCharacter] : (byte)0;

            Color color1 = palette.Background.ToColor();
            Color color2 = palette.Foreground.ToColor(); ;

            Color = color1.Blend(color2, (double)_percent);
        }

        public Palette Palette { get; }
        public ConsoleColor BackgroundColor => Palette.Background;
        public ConsoleColor ForegroundColor => Palette.Foreground;
        public char PrintableCharacter { get; }

        public ImageBrush Invert()
        {
            return new ImageBrush(Palette.Invert(), PrintableCharacter);
        }

        public Color Color { get; }

        public int AsInt => Color.GetRgbInt();

        public bool IsGrayscale => BackgroundColor.IsGrayscale() && (_percent == 0 || ForegroundColor.IsGrayscale());

        public ScreenPixel ToScreenPixel()
        {
            return new ScreenPixel()
            {
                Color = Palette.ByteValue,
                Character = PrintableCharacter,
                IsUpdated = true
            };
        }
    }
}