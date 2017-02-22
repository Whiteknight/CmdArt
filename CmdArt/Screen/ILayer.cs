using System.Collections.Generic;
using CmdArt.Rendering;

namespace CmdArt.Screen
{
    public interface ILayer
    {
        string Name { get; }
        ILocation Offset { get; }
        ISize Size { get; }
        IPixelBuffer Buffer { get; }
        void MoveTo(ILocation newLocation);
        IList<IRenderable> Renderables { get; }
        IList<IDecoration> LayerDecorations { get; }
        void Render();
    }
}