using System;

namespace CmdArt.Rendering.Images
{
    public interface IImageFrameBuilder : IDisposable
    {
        int NumberOfBuffers { get; }
        IImageFrame Build(int bufferIdx);
    }
}