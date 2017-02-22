namespace CmdArt.Screen
{
    public static class ScreenBufferExtensions
    {
        public static void Clear(this IScreenBuffer buffer, Palette palette)
        {
            buffer.Set(new Region(0, 0, buffer.Size), palette, ' ');
        }
    }
}