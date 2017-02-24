using CmdArt.Colors;
using CmdArt.Images.Sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CmdArt.Images
{
    public class ConsolePixelRepository
    {
        private readonly IReadOnlyList<ImageBrush> _grayscalePixels;
        private readonly Dictionary<int, ImageBrush> _pixelCache;

        public ConsolePixelRepository(IEnumerable<IBrushSource> sources)
        {
            Dictionary<int, ImageBrush> pixels = new Dictionary<int, ImageBrush>();
            Dictionary<int, ImageBrush> gsPixels = new Dictionary<int, ImageBrush>();
            foreach (ImageBrush p in sources.SelectMany(s => s.GetPixels()))
            {
                if (!pixels.ContainsKey(p.AsInt))
                    pixels.Add(p.AsInt, p);
                if (p.IsGrayscale && !gsPixels.ContainsKey(p.AsInt))
                    gsPixels.Add(p.AsInt, p);
            }

            AllPixels = pixels.Values.ToList();
            _grayscalePixels = gsPixels.Values.ToList();
            _pixelCache = new Dictionary<int, ImageBrush>();
        }

        public ImageBrush GetClosestPixel(Color c)
        {
            // TODO: make rounding configurable?
            // TODO: method to clear cache?
            c = c.Round();
            int key = c.GetRgbInt();

            if (_pixelCache.ContainsKey(key))
                return _pixelCache[key];

            IReadOnlyList<ImageBrush> pixels = AllPixels;
            if (c.IsGrayscale() && _grayscalePixels.Any())
                pixels = _grayscalePixels;

            var w = pixels
                .Select(p => new
                {
                    Pixel = p,
                    Distance = c.DistanceTo(p.Color)
                })
                .OrderBy(x => x.Distance)
                .ToList();
            ImageBrush brush = w[0].Pixel;

            _pixelCache.Add(key, brush);
            return brush;
        }

        public ImageBrush GetFurthestPixel(Color c)
        {
            return GetClosestPixel(c.Invert());
        }

        public IEnumerable<ImageBrush> RelatedColors(ConsoleColor cc)
        {
            ConsoleColor c1 = cc.MakeBright();
            return AllPixels.Where(p => p.BackgroundColor.MakeBright() == c1 || p.ForegroundColor.MakeBright() == c1);
        }

        public IReadOnlyList<ImageBrush> AllPixels { get; }
    }
}
