using System.Collections.Generic;
using System.Drawing;

namespace RichCmd.Rendering.Images
{
    public class GreyscalePixelConverter : IPixelConverter
    {
        private readonly ConsolePixelRepository _repository;

        public GreyscalePixelConverter()
        {
            _repository = new ConsolePixelRepository(new List<IPixelSource> {
                new GrayscalePixelSource(),
                new ColorsPixelSource()
            });
        }

        public ConsolePixel CreatePixel(Color c)
        {
            return _repository.GetClosestPixel(c);
        }
    }
}