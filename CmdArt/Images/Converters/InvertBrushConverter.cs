using System.Drawing;

namespace CmdArt.Images.Converters
{
    public class InvertBrushConverter : IBrushConverter
    {
        private readonly IBrushConverter _inner;

        public InvertBrushConverter(IBrushConverter inner)
        {
            if (inner == null)
                throw new System.ArgumentNullException(nameof(inner));

            _inner = inner;
        }

        public ImageBrush CreateBrush(Color c)
        {
            return _inner.CreateBrush(c).Invert();
        }
    }
}