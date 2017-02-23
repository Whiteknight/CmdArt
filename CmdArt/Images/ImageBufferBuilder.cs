using CmdArt.Images.Converters;
using CmdArt.Images.Samplers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace CmdArt.Images
{
    public class ImageBufferBuilder
    {
        private readonly IImageSampler _sampler;
        private readonly IBrushConverter _converter;
        private readonly Color _transparencyColor;

        public ImageBufferBuilder(IImageSampler sampler, IBrushConverter converter, Color transparencyColor)
        {
            _sampler = sampler;
            _converter = converter;
            _transparencyColor = transparencyColor;
        }

        public ImageBuffer Build(Bitmap bmp, Region targetSize)
        {
            if (targetSize.Height <= 0 || targetSize.Width <= 0)
                throw new Exception("Width and height must be strictly positive");

            IImageFrameBuilder imageFrameBuilder = new ImageFrameBuilder(bmp, targetSize, _sampler, _converter, _transparencyColor);
            return new ImageBuffer(imageFrameBuilder, targetSize);
        }

        private class ImageFrameBuilder : IImageFrameBuilder
        {
            private Bitmap _bmp;
            private readonly Region _region;
            private IImageSampler _sampler;
            private IBrushConverter _converter;
            private readonly Color _transparencyColor;
            private FrameDimension _frameDimension;

            public ImageFrameBuilder(Bitmap bmp, Region region, IImageSampler sampler, IBrushConverter converter, Color transparencyColor)
            {
                _bmp = bmp;
                _region = region;
                _sampler = sampler;
                _converter = converter;
                _transparencyColor = transparencyColor;

                _frameDimension = new FrameDimension(bmp.FrameDimensionsList[0]);
                NumberOfBuffers = bmp.GetFrameCount(_frameDimension);
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                if (_bmp != null)
                {
                    _bmp.Dispose();
                    _bmp = null;
                    _frameDimension = null;
                    _sampler = null;
                    _converter = null;
                }
            }

            #endregion

            #region Implementation of IImageFrameBuilder

            public int NumberOfBuffers { get; }

            public IImageFrame Build(int bufferIdx)
            {
                return BuildBuffer(_region, _bmp, _frameDimension, bufferIdx);
            }

            #endregion

            private IImageFrame BuildBuffer(Region size, Bitmap bmp, FrameDimension frameDimension, int idx)
            {
                ImageBrush[,] buffer = new ImageBrush[size.Height, size.Width];
                bmp.SelectActiveFrame(frameDimension, idx);

                for (int i = 0; i < size.Height; i++)
                {
                    for (int j = 0; j < size.Width; j++)
                    {
                        Color c = _sampler.GetSampleColor(size, bmp, j, i, _transparencyColor);
                        ImageBrush brush = _converter.CreateBrush(c);
                        buffer[i, j] = brush;
                    }
                }

                Dictionary<string, object> properties = null;

                // TODO: Move PropertyItem parsing out into separate classes? Do existing libraries exist that might
                // do this for us?
                // TODO: Are there any other per-frame properties we want?
                PropertyItem gifFrameTimeProp = bmp.PropertyItems.FirstOrDefault(pi => pi.Id == 0x5100);
                if (gifFrameTimeProp != null)
                {
                    byte[] b = gifFrameTimeProp.Value;
                    long value = b[idx * 4] | (b[(idx * 4) + 1] << 8) | (b[(idx * 4) + 2] << 16) | (b[(idx * 4) + 3] << 24);
                    value = value * 10; // gif frame times are in centi-seconds, not milli-seconds
                    properties = new Dictionary<string, object>();
                    properties.Add(ImagePropertyConstants.GifFrameTimeMs, value);
                }

                return new ImageFrame(size, buffer, properties);
            }
        }

        //private static Dictionary<int, string> framePropertiesToFetch = new Dictionary<int, string> {
        //    { 0x5100, ImagePropertyConstants.GifFrameTimeMs }
        //};

        //private Dictionary<string, object> GetFrameProperties(Bitmap bmp)
        //{
        //    IList<PropertyItem> items = bmp.PropertyItems.Where(pi => framePropertiesToFetch.ContainsKey(pi.Id)).ToList();
        //    if (!items.Any())
        //        return null;


        //}

        //private long Parse

        private class ImageFrame : IImageFrame
        {
            private readonly ImageBrush[,] _buffer;
            private readonly IReadOnlyDictionary<string, object> _properties;
            public Region TotalRegion { get; private set; }

            public ImageFrame(Region size, ImageBrush[,] buffer, IReadOnlyDictionary<string, object> properties)
            {
                _buffer = buffer;
                _properties = properties;
                TotalRegion = size;
            }

            public ImageBrush[,] GetRegionContents(Region region)
            {
                ImageBrush[,] brushes = new ImageBrush[region.Height, region.Width];

                for (int i = 0; i < region.Height && (i + region.Top) < TotalRegion.Height; i++)
                {
                    for (int j = 0; j < region.Width && (j + region.Left) < TotalRegion.Width; j++)
                    {
                        brushes[i, j] = _buffer[i + region.Top, j + region.Left];
                    }
                }

                return brushes;
            }

            //public event EventHandler<ContentChangedEventArgs> ContentChanged;

            public object GetProperty(string propertyName)
            {
                if (_properties == null)
                    return null;
                if (!_properties.ContainsKey(propertyName))
                    return null;
                return _properties[propertyName];
            }
        }
    }
}
