using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public interface IDecoration
    {
        Region RenderTo(IPixelBuffer buffer, Region region);
    }
}