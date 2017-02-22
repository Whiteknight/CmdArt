using System;
using System.Text;
using RichCmd.Colors;

namespace RichCmd.Screen
{
    public class ConsoleScreen
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
