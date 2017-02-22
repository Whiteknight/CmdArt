using System.Collections.Generic;
using System.Drawing;

namespace CmdArt.Rendering.Images
{
    public class InvertBlockPixelConverter : IPixelConverter
    {
        private readonly ConsolePixelRepository _repository;

        public InvertBlockPixelConverter()
        {
            _repository = new ConsolePixelRepository(new List<IPixelSource> {
                new GrayscalePixelSource(),
                new ColorsPixelSource()
            });
        }

        public ConsolePixel CreatePixel(Color c)
        {
            return _repository.GetClosestPixel(c).Invert();
        }
    }
}