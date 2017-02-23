using System.Drawing;

namespace CmdArt.Images.Samplers
{
    // Samples a region in a Bitmap to get a single Color value. This is used in cases where we need to down-sample
    // a large image before rendering in the console.
    public interface IImageSampler
    {
        Color GetSampleColor(ISize bufferSize, Bitmap bmp, int left, int top, Color bgColor);
    }
}
