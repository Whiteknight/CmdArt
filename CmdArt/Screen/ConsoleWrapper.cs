using System;

namespace CmdArt.Screen
{
    public interface IConsoleWrapper
    {
        void SetForAsciiGraphics();
        void SetColor(ConsoleColor foreground, ConsoleColor background);
        void SetCursorPosition(int i, int j);
        void SetSize(int width, int height);
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

        public void SetColor(ConsoleColor foreground, ConsoleColor background)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public Palette GetCurrentPalette()
        {
            return new Palette(Console.ForegroundColor, Console.BackgroundColor);
        }

        public void SetCursorPosition(int i, int j)
        {
            Console.SetCursorPosition(i, j);
        }

        public void SetSize(int width, int height)
        {
            if (width > 0)
                Console.WindowWidth = width > Console.LargestWindowWidth ? Console.LargestWindowWidth : width;
            if (height > 0)
                Console.WindowHeight = height > Console.LargestWindowHeight ? Console.LargestWindowHeight : height;
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
