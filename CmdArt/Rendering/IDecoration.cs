using CmdArt.Screen;

namespace CmdArt.Rendering
{
    // TODO: Remove the Decoration abstraction and replace with an IFilter which can alter the
    // render pattern of a Window object
    // TODO: Remove or replace most of the existing decoration classes, Boarders, etc
    public interface IDecoration
    {
        Region InnerRegion(Region region);

        Region Render(IPixelBuffer buffer, Region region);
    }
}