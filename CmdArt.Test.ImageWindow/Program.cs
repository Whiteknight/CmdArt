using CmdArt.Screen;
using System;

namespace CmdArt.Test.ImageWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = new TerminalScreen();

            var fileName = @"Media\MonaLisa.jpg";
            var image = CmdArt.Images.Image.BuildFromImageFile(fileName, new Size(100, 50));

            var window = screen.CreateNewWindow(new Region(5, 5, 20, 16));
            var buffer = screen.BufferFactory.Create(image.Size);
            window.SetSourceBuffer(buffer, new Location(7, 4));

            image.RenderTo(window.SourceBuffer);
            screen.Render(includeWindows: true);

            Console.ReadKey();
        }
    }
}
