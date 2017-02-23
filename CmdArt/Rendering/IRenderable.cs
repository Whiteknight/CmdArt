using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public interface IRenderable
    {
        void RenderTo(IPixelBuffer buffer);
    }
}
