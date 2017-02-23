using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CmdArt.Images
{
    // TODO: create a subclass that represents a projection of a cropped region in an ImageBuffer.
    // TODO: Define IImageBuffer in terms of a more generic type for holding any set of chars or
    // ConsolePixels, for use with more than just graphics

    // Effectively a read-only wrapper around ImageBufferSet
    public sealed class ImageBuffer : IImageBuffer, IDisposable
    {
        private readonly ImageFrameSet _imageFrames;

        public ImageBuffer(IImageFrameBuilder builder, ISize size)
        {
            _imageFrames = new ImageFrameSet(builder);
            Size = size;
            NumberOfBuffers = builder.NumberOfBuffers;
        }

        public ISize Size { get; }

        public IEnumerable<IImageFrame> Buffers => _imageFrames;

        public int NumberOfBuffers { get; }

        public IImageFrame GetBuffer(int bufferIdx)
        {
            return _imageFrames.GetBuffer(bufferIdx);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _imageFrames.Dispose();
        }

        #endregion

        private class ImageFrameSet : IEnumerable<IImageFrame>, IDisposable
        {
            private IImageFrameBuilder _builder;
            private readonly List<IImageFrame> _buffers;
            private readonly int _numberOfBuffers;
            private int _numberOfGeneratedBuffers;

            public ImageFrameSet(IImageFrameBuilder builder)
            {
                _builder = builder;
                _numberOfBuffers = builder.NumberOfBuffers;
                _numberOfGeneratedBuffers = 0;
                _buffers = Enumerable.Range(0, _numberOfBuffers).Select<int, IImageFrame>(x => null).ToList();
            }

            public IEnumerator<IImageFrame> GetEnumerator()
            {
                for (int i = 0; i < _numberOfBuffers; i++)
                    yield return GetBuffer(i);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IImageFrame GetBuffer(int bufferIdx)
            {
                if (bufferIdx < 0 || bufferIdx >= _numberOfBuffers)
                    throw new IndexOutOfRangeException("The buffer index is out of bounds");

                IImageFrame buffer = (bufferIdx < _buffers.Count) ? _buffers[bufferIdx] : null;
                if (buffer == null)
                {
                    buffer = _builder.Build(bufferIdx);
                    _buffers.Insert(bufferIdx, buffer);
                    _numberOfGeneratedBuffers++;
                    if (_numberOfGeneratedBuffers >= _numberOfBuffers && _buffers.All(ib => ib != null))
                    {
                        _builder.Dispose();
                        _builder = null;
                    }
                }
                return buffer;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                if (_builder == null)
                    return;
                _builder.Dispose();
                _builder = null;
            }

            #endregion
        }
    }
}