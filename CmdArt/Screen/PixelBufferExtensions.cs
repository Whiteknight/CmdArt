namespace CmdArt.Screen
{
    public static class PixelBufferExtensions
    {
        public static void Clear(this IPixelBuffer buffer)
        {
            buffer.Set(new Region(0, 0, buffer.Size), buffer.DefaultPalette, ' ');
        }

        public static void Clear(this IPixelBuffer buffer, Palette palette)
        {
            buffer.Set(new Region(0, 0, buffer.Size), palette, ' ');
        }

        public static Palette GetColorPalette(this IPixelBuffer buffer, int left, int top)
        {
            byte b = buffer.Get(left, top).Color;
            return new Palette(b);
        }

        public static void Set(this IPixelBuffer buffer, int left, int top, Palette palette, char c)
        {
            buffer.Set(left, top, palette.ByteValue, c);
        }

        public static void Set(this IPixelBuffer buffer, int left, int top, Palette palette, string s)
        {
            buffer.Set(left, top, palette.ByteValue, s);
        }

        public static void Set(this IPixelBuffer buffer, Region region, Palette palette, char c)
        {
            buffer.Set(region, palette.ByteValue, c);
        }

        public static void Set(this IPixelBuffer buffer, int left, int top, string s)
        {
            buffer.Set(left, top, buffer.DefaultPalette.ByteValue, s);
        }

        public static void SetColor(this IPixelBuffer buffer, int left, int top, Palette palette)
        {
            buffer.SetColor(left, top, palette.ByteValue);
        }

        public static void SetColor(this IPixelBuffer buffer, Region region, Palette palette)
        {
            buffer.SetColor(region, palette.ByteValue);
        }
    }
}