using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdArt.Rendering.Boarders;
using CmdArt.Rendering.Strings;
using CmdArt.Screen;

namespace CmdArt.Test.TextScroll
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = new TerminalScreen();

            var frame = Boarder.SolidDoubleLine();
            var window = frame.RenderTo(screen.Buffer, new Region(5, 3, 20, 7));
            var textArea = new ForwardScrollingText(window.Size);
            textArea.AddText("this is a line");
            textArea.AddText("this is line 2");
            textArea.RenderTo(window);
            screen.Render();
            Console.ReadKey();

            for (int i = 0; i < 5; i++)
            {
                textArea.AddText("this is line " + (i + 3));
                textArea.RenderTo(window);
                screen.Render();
                Console.ReadKey();
            }
        }
    }
}
