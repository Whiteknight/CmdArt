using CmdArt.Screen;

namespace CmdArt.Rendering.Images
{
    public class Image : IRenderable
    {
        private readonly IImageBuffer _imageBuffer;
        private readonly ILocation _imageLocation;

        public Image(IImageBuffer imageBuffer, ILocation imageLocation)
        {
            _imageBuffer = imageBuffer;
            _imageLocation = imageLocation;
        }

        public void Render(IScreenBuffer buffer)
        {
            IImageFrame imageFrame = _imageBuffer.GetBuffer(0);
            ConsolePixel[,] pixels = imageFrame.GetRegionContents(new Region(_imageLocation.Left, _imageLocation.Top, buffer.Size.Width, buffer.Size.Height));
            for (int j = 0; j < buffer.Size.Height && j < imageFrame.TotalRegion.Height; j++)
            {
                for (int i = 0; i < buffer.Size.Width && i < imageFrame.TotalRegion.Width; i++)
                {
                    buffer.Set(i, j, pixels[j, i].Palette, pixels[j, i].PrintableCharacter);
                }
            }
        }
    }
}