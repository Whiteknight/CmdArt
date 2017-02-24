using CmdArt.Images;
using CmdArt.Images.Converters;
using CmdArt.Images.Samplers;
using CmdArt.Screen;
using System;
using System.Drawing;
using System.IO;
using Image = System.Drawing.Image;

namespace CmdArt.Test.ImageRender
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var file in Directory.EnumerateFiles("Media", "*.jpg"))
            {
                RenderImage(file);
                Console.ReadKey();
            }

            // TODO: Show example where pushing arrowkeys moves the location of the window in the screen (but keeps the source location fixed)
            // TODO: Show example where pushing arrowkeys moves the source location, but keeps the window fixed in the screen
            // TODO: Show example where pushing arrowkeys moves the window and the source location
        }

        private static void RenderImage(string fileName)
        {
            // TODO: This is too much work for basic setup. Create some helpers.
            var bitmap = Image.FromFile(fileName);
            ImageBufferBuilder builder = new ImageBufferBuilder(new PickOneImageSampler(), new SearchBrushConverter(), Color.Transparent);
            var size = Size.FitButMaintainAspectRatio(new Size(100, 55), bitmap.Width, bitmap.Height);

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
        }
    }
}
