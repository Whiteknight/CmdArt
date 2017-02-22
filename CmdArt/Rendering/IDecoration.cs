using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public interface IDecoration
    {
        Region InnerRegion(Region region);

        Region Render(IScreenBuffer buffer, Region region);
    }
}