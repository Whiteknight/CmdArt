using System.Collections.Generic;

namespace CmdArt.Rendering.Images
{
    public interface IPixelSource
    {
        IEnumerable<ConsolePixel> GetPixels();
    }
}