using System;

namespace CmdArt.Images
{
    public interface IImageFrameBuilder : IDisposable
    {
        int NumberOfBuffers { get; }
        IImageFrame Build(int bufferIdx);
    }
}