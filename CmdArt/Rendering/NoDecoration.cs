using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public class NoDecoration : IDecoration
    {
        public Region InnerRegion(Region region)
        {
            return region;
        }

        public IPixelBuffer RenderTo(IPixelBuffer buffer, Region region)
        {
            return buffer;
        }
    }
}