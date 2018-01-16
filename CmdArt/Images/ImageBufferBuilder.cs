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

        public ImageBufferBuilder(IImageSampler sampler = null, IBrushConverter converter = null, Color? transparencyColor = null)
        {
            _sampler = sampler ?? new PickOneImageSampler();
            _converter = converter ?? new SearchBrushConverter();
            _transparencyColor = transparencyColor.GetValueOrDefault(Color.Transparent);
        }

        public ImageBuffer Build(Bitmap bitmap, ISize targetSize, bool maintainAspectRatio = true)
        {
            if (targetSize.Height <= 0 || targetSize.Width <= 0)
                throw new Exception("Width and height must be strictly positive");

            ISize size;
            if (maintainAspectRatio)
                size = Size.FitButMaintainAspectRatio(targetSize, (uint)bitmap.Width, (uint)bitmap.Height);
            else
                size = targetSize;


            var imageFrameBuilder = new ImageFrameBuilder(bitmap, size, _sampler, _converter, _transparencyColor);
            return new ImageBuffer(imageFrameBuilder, size);
        }

        private class ImageFrameBuilder : IImageFrameBuilder
        {
            private Bitmap _bmp;
            private readonly ISize _targetSize;
            private IImageSampler _sampler;
            private IBrushConverter _converter;
            private readonly Color _transparencyColor;
            private FrameDimension _frameDimension;

            public ImageFrameBuilder(Bitmap bmp, ISize targetSize, IImageSampler sampler, IBrushConverter converter, Color transparencyColor)
            {
                _bmp = bmp;
                _targetSize = targetSize;
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
                return BuildBuffer(_targetSize, _bmp, _frameDimension, bufferIdx);
            }

            #endregion

            private IImageFrame BuildBuffer(ISize size, Bitmap bmp, FrameDimension frameDimension, int idx)
            {
                var buffer = new ImageBrush[size.Height, size.Width];
                bmp.SelectActiveFrame(frameDimension, idx);

                for (var i = 0; i < size.Height; i++)
                {
                    for (var j = 0; j < size.Width; j++)
                    {
                        var c = _sampler.GetSampleColor(size, bmp, j, i, _transparencyColor);
                        buffer[i, j] = _converter.CreateBrush(c);
                    }
                }

                Dictionary<string, object> properties = null;

                // TODO: Move PropertyItem parsing out into separate classes? Do existing libraries exist that might
                // do this for us?
                // TODO: Are there any other per-frame properties we want?
                var gifFrameTimeProp = bmp.PropertyItems.FirstOrDefault(pi => pi.Id == 0x5100);
                if (gifFrameTimeProp != null)
                {
                    var b = gifFrameTimeProp.Value;
                    long value = b[idx * 4] | (b[(idx * 4) + 1] << 8) | (b[(idx * 4) + 2] << 16) | (b[(idx * 4) + 3] << 24);
                    value = value * 10; // gif frame times are in centi-seconds, not milli-seconds
                    properties = new Dictionary<string, object>();
                    properties.Add(ImagePropertyConstants.GifFrameTimeMs, value);
                }

                return new ImageFrame(size, buffer, properties);
            }
        }

        private class ImageFrame : IImageFrame
        {
            private readonly ImageBrush[,] _buffer;
            private readonly IReadOnlyDictionary<string, object> _properties;
            public ISize TotalSize { get; }

            public ImageFrame(ISize size, ImageBrush[,] buffer, IReadOnlyDictionary<string, object> properties)
            {
                _buffer = buffer;
                _properties = properties;
                TotalSize = size;
            }

            public ImageBrush[,] GetRegionContents(Region region)
            {
                var brushes = new ImageBrush[region.Height, region.Width];

                for (var i = 0; i < region.Height && (i + region.Top) < TotalSize.Height; i++)
                {
                    for (var j = 0; j < region.Width && (j + region.Left) < TotalSize.Width; j++)
                    {
                        brushes[i, j] = _buffer[i + region.Top, j + region.Left];
                    }
                }

                return brushes;
            }

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
