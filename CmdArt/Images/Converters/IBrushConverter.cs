using System.Drawing;

namespace CmdArt.Images.Converters
{
    // IBrushConverter converts a System.Drawing.Color to an ImageBrush
    // This conversion is lossy, so different algorithms will produce different results with
    // different performance characteristics
    public interface IBrushConverter
    {
        ImageBrush CreateBrush(Color c);
    }
}