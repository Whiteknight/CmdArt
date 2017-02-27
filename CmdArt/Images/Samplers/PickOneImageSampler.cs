using CmdArt.Colors;
using System.Drawing;

namespace CmdArt.Images.Samplers
{
    public class PickOneImageSampler : IImageSampler
    {
        // TODO: The "pixels" in the console aren't usually square. Create a sampler that accounts for a 
        // rectangular pixel
        public Color GetSampleColor(ISize bufferSize, Bitmap bitmap, int left, int top, Color bgColor)
        {
            if (bufferSize == null)
                throw new System.ArgumentNullException(nameof(bufferSize));

            if (bitmap == null)
                throw new System.ArgumentNullException(nameof(bitmap));

            double x = ((double)bitmap.Size.Width / bufferSize.Width) * left;
            double y = ((double)bitmap.Size.Height / bufferSize.Height) * top;

            Color c = bitmap.GetPixel((int)x, (int)y);
            return c.BlendTransparency(bgColor);
        }
    }
}