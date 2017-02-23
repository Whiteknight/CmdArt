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
            var bitmap = Image.FromFile(@"");
            ImageBufferBuilder builder = new ImageBufferBuilder(new PickOneImageSampler(), new SearchBrushConverter(), Color.Transparent);
            var imageBuffer = builder.Build((Bitmap)bitmap, Region.Window);
            var image = new CmdArt.Images.Image(imageBuffer, Location.Origin);

            // TODO: The image is stretched to fit the entire console. We need a method that allows
            // us to downsample while maintaining aspect ratio.

            var screen = new TerminalScreen();
            image.RenderTo(screen.Buffer);
            screen.Render();
            Console.ReadKey();

            screen.Buffer.Clear(Palette.Default);
            var window = screen.CreateNewWindow(new Region(3, 3, 24, 20));
            var buffer = screen.BufferFactory.CreateForTerminalScreen();
            image.RenderTo(buffer);
            window.SetSourceBuffer(buffer, new Location(28, 2));
            window.Render();
            screen.Render();
            Console.ReadKey();
        }
    }
}
