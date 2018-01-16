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
            screen.Console.SetForAsciiGraphics();

            // Load the bitmap into memory
            var fileName = @"Media\MonaLisa.jpg";
            var image = CmdArt.Images.Image.BuildFromImageFile(fileName, new Size(50, 50));

            // Resize the window and show the whole image, for reference
            screen.ResizeConsole(new Size(52, 52));
            screen.Buffer.Set(4, 50, Palette.Default, "Press any key to frame her face");
            image.RenderTo(screen.Buffer);
            screen.Render();
            Console.ReadKey();

            // Clear the image so we can show just a part of it
            screen.Buffer.Clear();

            // Render a pretty frame
            var frame = Boarder.SolidDoubleLine();
            var window = frame.RenderTo(screen.Buffer, new Region(6, 3, 22, 18));
            //screen.Render();

            // Render the image, but just the page of the face which fits in the frame 
            image.RenderTo(window, new Region(7, 4, window.Size.Width, window.Size.Height));
            screen.Buffer.Set(4, 50, Palette.Default, "Press any key to move her around");
            screen.Render();
            Console.ReadKey();

            // Clear the screen again, and re-render the boarder and image fragment elsewhere on the screen
            screen.Buffer.Clear();
            window = frame.RenderTo(screen.Buffer, new Region(16, 23, 22, 18));
            image.RenderTo(window, new Region(7, 4, window.Size.Width, window.Size.Height));
            screen.Buffer.Set(4, 50, Palette.Default, "Press any key to exit");
            screen.Render();
            Console.ReadKey();
        }
    }
}
