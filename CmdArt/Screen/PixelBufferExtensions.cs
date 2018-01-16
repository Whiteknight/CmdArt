using System;

namespace CmdArt.Screen
{
    public static class PixelBufferExtensions
    {
        public static Region GetFillRegion(this IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return new Region(Location.Origin, buffer.Size);
        }

        public static IPixelBuffer CreateWindow(this IPixelBuffer buffer, ILocation location, ISize size)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return new PixelBufferWindow(buffer, location, size);
        }

        public static IPixelBuffer CreateWindow(this IPixelBuffer buffer, Region region)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return new PixelBufferWindow(buffer, region.RegionLocation, region.RegionSize);
        }

        public static void Clear(this IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Set(new Region(0, 0, buffer.Size), buffer.DefaultPalette, ' ');
        }

        public static void Clear(this IPixelBuffer buffer, Palette palette)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Set(new Region(0, 0, buffer.Size), palette, ' ');
        }

        public static Palette GetColorPalette(this IPixelBuffer buffer, uint left, uint top)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            byte b = buffer.Get(left, top).Color;
            return new Palette(b);
        }

        public static void Set(this IPixelBuffer buffer, uint left, uint top, Palette palette, char c)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Set(left, top, palette.ByteValue, c);
        }

        public static void Set(this IPixelBuffer buffer, uint left, uint top, Palette palette, string s)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Set(left, top, palette.ByteValue, s);
        }

        public static void Set(this IPixelBuffer buffer, Region region, Palette palette, char c)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Set(region, palette.ByteValue, c);
        }

        public static void Set(this IPixelBuffer buffer, uint left, uint top, string s)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Set(left, top, buffer.DefaultPalette.ByteValue, s);
        }

        public static void SetColor(this IPixelBuffer buffer, uint left, uint top, Palette palette)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.SetColor(left, top, palette.ByteValue);
        }

        public static void SetColor(this IPixelBuffer buffer, Region region, Palette palette)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.SetColor(region, palette.ByteValue);
        }
    }
}