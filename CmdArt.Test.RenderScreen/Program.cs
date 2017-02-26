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
            var window = screen.CreateNewWindow(new Region(8, 8, 12, 12));
            window.SourceBuffer.SetCharacter(window.SourceRegion, '2');
            for (int i = 0; i < 12; i++)
                window.SourceBuffer.Set(i, i, 0xE0, '3');

            screen.Render(true);
            Console.ReadKey();
        }
    }
}
