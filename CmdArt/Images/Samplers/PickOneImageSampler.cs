using CmdArt.Colors;
using System.Drawing;

namespace CmdArt.Images.Samplers
{
    public class PickOneImageSampler : IImageSampler
    {
        // TODO: The "pixels" in the console aren't usually square. Create a sampler that accounts for a 
        // rectangular pixel
        public Color GetSampleColor(ISize bufferSize, Bitmap bmp, int left, int top, Color bgColor)
        {
            double x = ((double)bmp.Size.Width / bufferSize.Width) * left;
            double y = ((double)bmp.Size.Height / bufferSize.Height) * top;

            Color c = bmp.GetPixel((int)x, (int)y);
            return c.BlendTransparency(bgColor);
        }
    }
}