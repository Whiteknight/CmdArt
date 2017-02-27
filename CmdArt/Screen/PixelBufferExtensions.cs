namespace CmdArt.Screen
{
    public static class PixelBufferExtensions
    {
        public static void Clear(this IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.Set(new Region(0, 0, buffer.Size), buffer.DefaultPalette, ' ');
        }

        public static void Clear(this IPixelBuffer buffer, Palette palette)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.Set(new Region(0, 0, buffer.Size), palette, ' ');
        }

        public static Palette GetColorPalette(this IPixelBuffer buffer, int left, int top)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            byte b = buffer.Get(left, top).Color;
            return new Palette(b);
        }

        public static void Set(this IPixelBuffer buffer, int left, int top, Palette palette, char c)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.Set(left, top, palette.ByteValue, c);
        }

        public static void Set(this IPixelBuffer buffer, int left, int top, Palette palette, string s)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.Set(left, top, palette.ByteValue, s);
        }

        public static void Set(this IPixelBuffer buffer, Region region, Palette palette, char c)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.Set(region, palette.ByteValue, c);
        }

        public static void Set(this IPixelBuffer buffer, int left, int top, string s)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.Set(left, top, buffer.DefaultPalette.ByteValue, s);
        }

        public static void SetColor(this IPixelBuffer buffer, int left, int top, Palette palette)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.SetColor(left, top, palette.ByteValue);
        }

        public static void SetColor(this IPixelBuffer buffer, Region region, Palette palette)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            buffer.SetColor(region, palette.ByteValue);
        }
    }
}