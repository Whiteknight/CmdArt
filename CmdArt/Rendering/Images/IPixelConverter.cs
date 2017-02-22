using System.Drawing;

namespace RichCmd.Rendering.Images
{
    public interface IPixelConverter
    {
        ConsolePixel CreatePixel(Color c);
    }
}