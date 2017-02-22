using System.Drawing;

namespace RichCmd.Rendering.Images
{
    public interface IImageSampler
    {
        Color GetSampleColor(Region bufferSize, Bitmap bmp, int left, int top, Color bgColor);
    }
}
