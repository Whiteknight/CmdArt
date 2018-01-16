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
}