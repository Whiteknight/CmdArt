using CmdArt.Colors;

namespace CmdArt.Screen
{
    public static class ConsoleWrapperExtensions
    {
        public static void SetColor(this IConsoleWrapper console, byte color)
        {
            var f = ConsoleColorUtilities.GetForeground(color);
            var b = ConsoleColorUtilities.GetBackground(color);
            console.SetColor(f, b);
        }

        public static void SetColor(this IConsoleWrapper console, Palette palette)
        {
            console.SetColor(palette.Foreground, palette.Background);
        }

        public static void SetCursorPosition(this IConsoleWrapper console, ILocation location)
        {
            console.SetCursorPosition(location.Left, location.Top);
        }

        public static void SetSize(this IConsoleWrapper console, ISize size)
        {
            console.SetSize(size.Width, size.Height);
        }
    }
}