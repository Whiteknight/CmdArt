using System.Drawing;

namespace CmdArt.Rendering.Images
{
    public interface IImageSampler
    {
        Color GetSampleColor(Region bufferSize, Bitmap bmp, int left, int top, Color bgColor);
    }
}
