using RichCmd.Screen;

namespace RichCmd.Rendering
{
    public interface IDecoration
    {
        Region InnerRegion(Region region);

        Region Render(IScreenBuffer buffer, Region region);
    }
}