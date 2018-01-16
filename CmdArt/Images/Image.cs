using CmdArt.Rendering;
using CmdArt.Screen;
using System.Drawing;

namespace CmdArt.Images
{
    public class Image : IRenderable
    {
        private readonly IImageBuffer _imageBuffer;
        private readonly ILocation _imageLocation;

        public Image(IImageBuffer imageBuffer)
        {
            if (imageBuffer == null)
                throw new System.ArgumentNullException(nameof(imageBuffer));

            _imageBuffer = imageBuffer;
        }

        public ISize Size => _imageBuffer.Size;

        public void RenderTo(IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            var imageFrame = _imageBuffer.GetBuffer(0);
            var brushes = imageFrame.GetRegionContents(new Region(Location.Origin, buffer.Size.Width, buffer.Size.Height));
            for (uint j = 0; j < buffer.Size.Height && j < imageFrame.TotalSize.Height; j++)
            {
                for (uint i = 0; i < buffer.Size.Width && i < imageFrame.TotalSize.Width; i++)
                {
                    buffer.Set(i, j, brushes[j, i].Palette, brushes[j, i].PrintableCharacter);
                }
            }
        }

        public void RenderTo(IPixelBuffer buffer, Region sourceRegion)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            var imageFrame = _imageBuffer.GetBuffer(0);
            var brushes = imageFrame.GetRegionContents(sourceRegion);
            for (uint j = 0; j < buffer.Size.Height &&  j < sourceRegion.Height && j < imageFrame.TotalSize.Height; j++)
            {
                for (uint i = 0; i < buffer.Size.Width && i < sourceRegion.Width && i < imageFrame.TotalSize.Width; i++)
                {
                    var brush = brushes[j, i];
                    if (brush == null)
                        continue;
                    buffer.Set(i, j, brush.Palette, brush.PrintableCharacter);
                }
            }
        }

        public static Image BuildFromImageFile(string fileName, ISize bufferSize, bool maintainAspectRatio = true)
        {
            if (fileName == null)
                throw new System.ArgumentNullException(nameof(fileName));

            if (bufferSize == null)
                throw new System.ArgumentNullException(nameof(bufferSize));

            var bitmap = System.Drawing.Image.FromFile(fileName);
            return BuildFromBitmap((Bitmap)bitmap, bufferSize, maintainAspectRatio);
        }

        public static Image BuildFromBitmap(Bitmap bitmap, ISize bufferSize, bool maintainAspectRatio = true)
        {
            if (bitmap == null)
                throw new System.ArgumentNullException(nameof(bitmap));

            if (bufferSize == null)
                throw new System.ArgumentNullException(nameof(bufferSize));

            // TODO: Use default singleton to avoid creating many instances
            var builder = new ImageBufferBuilder();
            var imageBuffer = builder.Build(bitmap, bufferSize, maintainAspectRatio);
            return new Image(imageBuffer);
        }
    }
}