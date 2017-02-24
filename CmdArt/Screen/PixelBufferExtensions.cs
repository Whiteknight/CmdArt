namespace CmdArt.Screen
{
    public static class PixelBufferExtensions
    {
        public static void Clear(this IPixelBuffer buffer, Palette palette)
        {
            buffer.Set(new Region(0, 0, buffer.Size), palette, ' ');
        }

        public static Palette GetColorPalette(this IPixelBuffer buffer, int left, int top)
        {
            byte b = buffer.Get(left, top).Color;
            return new Palette(b);
        }
    }
}