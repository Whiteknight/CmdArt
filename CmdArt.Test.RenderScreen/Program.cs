using CmdArt.Screen;
using System;

namespace CmdArt.Test.RenderScreen
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = new TerminalScreen();
            screen.Buffer.Set(new Region(5, 5, 10, 10), new Palette((byte)0x56), '1');
            screen.Render();
            Console.ReadKey();

            screen.Buffer.Clear(Palette.Default);
            var window = screen.Buffer.CreateWindow(new Region(8, 8, 12, 12));
            window.Set(window.GetFillRegion(), Palette.Default, '2');
            for (uint i = 0; i < 12; i++)
                window.Set(i, i, new Palette(ConsoleColor.Yellow, ConsoleColor.DarkRed), '3');

            screen.Render(true);
            Console.ReadKey();
        }
    }
}
