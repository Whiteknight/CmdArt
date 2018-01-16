using CmdArt.Colors;

namespace CmdArt.Screen
{
    public static class ConsoleWrapperExtensions
    {
        public static void SetColor(this IConsoleWrapper console, byte color)
        {
            if (console == null)
                throw new System.ArgumentNullException(nameof(console));

            var f = ConsoleColorUtilities.GetForeground(color);
            var b = ConsoleColorUtilities.GetBackground(color);
            console.SetColor(f, b);
        }

        public static void SetColor(this IConsoleWrapper console, Palette palette)
        {
            if (console == null)
                throw new System.ArgumentNullException(nameof(console));

            console.SetColor(palette.Foreground, palette.Background);
        }

        public static void SetCursorPosition(this IConsoleWrapper console, ILocation location)
        {
            if (console == null)
                throw new System.ArgumentNullException(nameof(console));

            console.SetCursorPosition((int)location.Left, (int)location.Top);
        }

        public static void SetSize(this IConsoleWrapper console, ISize size)
        {
            if (console == null)
                throw new System.ArgumentNullException(nameof(console));

            if (size == null)
                throw new System.ArgumentNullException(nameof(size));

            console.SetSize((int)size.Width, (int)size.Height);
        }
    }
}