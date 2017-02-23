using System.Collections.Generic;
using System.Drawing;
using CmdArt.Images.Sources;

namespace CmdArt.Images.Converters
{
    public class GreyscaleBrushConverter : IBrushConverter
    {
        private readonly ConsolePixelRepository _repository;

        public GreyscaleBrushConverter()
        {
            _repository = new ConsolePixelRepository(new List<IBrushSource> {
                new GrayscaleBrushSource()
            });
        }

        public ImageBrush CreateBrush(Color c)
        {
            return _repository.GetClosestPixel(c);
        }
    }
}