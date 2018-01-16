using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public interface IDecoration
    {
        IPixelBuffer RenderTo(IPixelBuffer buffer, Region region);
    }
}