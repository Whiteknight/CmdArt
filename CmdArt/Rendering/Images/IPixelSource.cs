using System.Collections.Generic;

namespace RichCmd.Rendering.Images
{
    public interface IPixelSource
    {
        IEnumerable<ConsolePixel> GetPixels();
    }
}