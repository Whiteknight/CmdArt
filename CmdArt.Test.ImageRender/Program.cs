using CmdArt.Images;
using CmdArt.Images.Converters;
using CmdArt.Images.Samplers;
using CmdArt.Screen;
using System;
using System.Drawing;
using Image = System.Drawing.Image;

namespace CmdArt.Test.ImageRender
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitmap = Image.FromFile(@"MonaLisa.jpg");
            ImageBufferBuilder builder = new ImageBufferBuilder(new PickOneImageSampler(), new SearchBrushConverter(), Color.Transparent);
            var size = Size.FitButMaintainAspectRatio(Region.WindowMax, bitmap.Width, bitmap.Height);

            // TODO: IConsoleWrapper method to set the console to a size
            Console.WindowWidth = size.Width;
            Console.WindowHeight = size.Height;

            var imageBuffer = builder.Build((Bitmap)bitmap, size);
            var image = new CmdArt.Images.Image(imageBuffer, Location.Origin);

            // TODO: The image is stretched to fit the entire console. We need a method that allows
            // us to downsample while maintaining aspect ratio.

            var screen = new TerminalScreen();
            image.RenderTo(screen.Buffer);
            screen.Render();
            Console.ReadKey();

            screen.Buffer.Clear(Palette.Default);
            var window = screen.CreateNewWindow(new Region(3, 3, 24, 29));
            var buffer = screen.BufferFactory.CreateForTerminalScreen();
            image.RenderTo(buffer);
            window.SetSourceBuffer(buffer, new Location(20, 8));
            window.Render();
            screen.Render();
            Console.ReadKey();

            // TODO: Show example where pushing arrowkeys moves the location of the window in the screen (but keeps the source location fixed)
            // TODO: Show example where pushing arrowkeys moves the source location, but keeps the window fixed in the screen
            // TODO: Show example where pushing arrowkeys moves the window and the source location
        }
    }
}
