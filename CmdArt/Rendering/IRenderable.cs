using RichCmd.Screen;

namespace RichCmd.Rendering
{
    public interface IRenderable
    {
        void Render(IScreenBuffer buffer);
    }
}
