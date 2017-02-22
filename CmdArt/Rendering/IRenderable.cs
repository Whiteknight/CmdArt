using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public interface IRenderable
    {
        void Render(IPixelBuffer buffer);
    }
}
