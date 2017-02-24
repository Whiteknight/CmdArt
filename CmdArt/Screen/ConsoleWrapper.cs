using CmdArt.Colors;
using System;

namespace CmdArt.Screen
{
    public interface IConsoleWrapper
    {
        void SetForAsciiGraphics();
        void SetColor(byte color);
        void SetColor(Palette palette);
        void SetCursorPosition(int i, int j);
        void Write(char c);

        Region WindowRegion { get; }
        Region WindowMaxRegion { get; }
        Region WindowMaxVerticalRegion { get; }

        Palette GetCurrentPalette();
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public void SetForAsciiGraphics()
        {
            //Console.OutputEncoding = Encoding.GetEncoding(437);
            Console.CursorVisible = false;
        }

        public void SetColor(byte color)
        {
            Console.ForegroundColor = ConsoleColorUtilities.GetForeground(color);
            Console.BackgroundColor = ConsoleColorUtilities.GetBackground(color);
        }

        public void SetColor(Palette palette)
        {
            Console.ForegroundColor = palette.Foreground;
            Console.BackgroundColor = palette.Background;
        }

        public Palette GetCurrentPalette()
        {
            return new Palette(Console.ForegroundColor, Console.BackgroundColor);
        }

        public void SetCursorPosition(int i, int j)
        {
            Console.SetCursorPosition(i, j);
        }

        public void Write(char c)
        {
            Console.Write(c);
        }

        public Region WindowRegion => new Region(0, 0, Console.WindowWidth, Console.WindowHeight - 1);

        public Region WindowMaxRegion => new Region(0, 0, Console.LargestWindowWidth, Console.LargestWindowHeight);

        public Region WindowMaxVerticalRegion => new Region(0, 0, Console.WindowWidth, Console.LargestWindowHeight);
    }
}
