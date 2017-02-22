using CmdArt.Colors;
using System;

namespace CmdArt.Screen
{
    public interface IConsoleWrapper
    {
        void SetForAsciiGraphics();
        void SetColor(byte color);
        void SetCursorPosition(int i, int j);
        void Write(char c);
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

        public void SetCursorPosition(int i, int j)
        {
            Console.SetCursorPosition(i, j);
        }

        public void Write(char c)
        {
            Console.Write(c);
        }
    }
}
