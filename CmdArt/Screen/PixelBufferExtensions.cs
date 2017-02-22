namespace CmdArt.Screen
{
    public static class PixelBufferExtensions
    {
        public static void Clear(this IPixelBuffer buffer, Palette palette)
        {
            buffer.Set(new Region(0, 0, buffer.Size), palette, ' ');
        }
    }
}