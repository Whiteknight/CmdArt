using System.Drawing;

namespace CmdArt.Rendering.Images
{
    public interface IPixelConverter
    {
        ConsolePixel CreatePixel(Color c);
    }
}