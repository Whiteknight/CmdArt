using System.Drawing;

namespace CmdArt.Images.Converters
{
    public class InvertBrushConverter : IBrushConverter
    {
        private readonly IBrushConverter _inner;

        public InvertBrushConverter(IBrushConverter inner)
        {
            _inner = inner;
        }

        public ImageBrush CreateBrush(Color c)
        {
            return _inner.CreateBrush(c).Invert();
        }
    }
}