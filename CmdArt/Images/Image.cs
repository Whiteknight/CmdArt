using CmdArt.Rendering;
using CmdArt.Screen;

namespace CmdArt.Images
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

        public void RenderTo(IPixelBuffer buffer)
        {
            IImageFrame imageFrame = _imageBuffer.GetBuffer(0);
            ImageBrush[,] brushes = imageFrame.GetRegionContents(new Region(_imageLocation.Left, _imageLocation.Top, buffer.Size.Width, buffer.Size.Height));
            for (int j = 0; j < buffer.Size.Height && j < imageFrame.TotalRegion.Height; j++)
            {
                for (int i = 0; i < buffer.Size.Width && i < imageFrame.TotalRegion.Width; i++)
                {
                    buffer.Set(i, j, brushes[j, i].Palette, brushes[j, i].PrintableCharacter);
                }
            }
        }
    }
}