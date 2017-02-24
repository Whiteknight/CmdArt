using CmdArt.Screen;
using System;
using System.IO;

namespace CmdArt.Test.ImageRender
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = new TerminalScreen();

            foreach (var file in Directory.EnumerateFiles("Media", "*.jpg"))
            {
                RenderImage(screen, file);
                Console.ReadKey();
                screen.Buffer.Clear(Palette.Default);
                screen.Render();
            }

            // TODO: Show example where pushing arrowkeys moves the location of the window in the screen (but keeps the source location fixed)
            // TODO: Show example where pushing arrowkeys moves the source location, but keeps the window fixed in the screen
            // TODO: Show example where pushing arrowkeys moves the window and the source location
        }

        private static void RenderImage(TerminalScreen screen, string fileName)
        {
            var image = CmdArt.Images.Image.BuildFromImageFile(fileName, new Size(100, 50));

            screen.ResizeConsole(image.Size);
            image.RenderTo(screen.Buffer);
            screen.Render(force: true);
        }
    }
}
