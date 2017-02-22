using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public class NoDecoration : IDecoration
    {
        public Region InnerRegion(Region region)
        {
            return region;
        }

        public Region Render(IPixelBuffer buffer, Region region)
        {
            return region;
        }
    }
}