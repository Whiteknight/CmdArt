using CmdArt.Rendering.Boarders;
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

            var frame = Boarder.SolidDoubleLine();
            var imageRegion = frame.RenderTo(screen.Buffer, new Region(4, 4, 22, 18));
            var window = screen.CreateNewWindow(imageRegion);
            var buffer = screen.BufferFactory.Create(image.Size);
            window.SetSourceBuffer(buffer, new Location(7, 4));

            image.RenderTo(window.SourceBuffer);
            screen.Render(includeWindows: true);

            Console.ReadKey();
        }
    }
}
