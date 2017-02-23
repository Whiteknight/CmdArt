﻿using System.Collections.Generic;
using System.Drawing;
using CmdArt.Images.Sources;

namespace CmdArt.Images.Converters
{
    public class SearchBrushConverter : IBrushConverter
    {
        private readonly ConsolePixelRepository _repository;

        public SearchBrushConverter()
        {
            _repository = new ConsolePixelRepository(new List<IBrushSource> {
                new GrayscaleBrushSource(),
                new ColorsBrushSource()
            });
        }

        public ImageBrush CreateBrush(Color c)
        {
            return _repository.GetClosestPixel(c);
        }
    }
}