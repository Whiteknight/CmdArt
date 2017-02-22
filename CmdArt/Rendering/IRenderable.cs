using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public interface IRenderable
    {
        void Render(IScreenBuffer buffer);
    }
}
