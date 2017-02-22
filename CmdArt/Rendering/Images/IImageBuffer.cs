using System.Collections.Generic;

namespace CmdArt.Rendering.Images
{
    public interface IImageBuffer
    {
        Region Region { get; }
        IEnumerable<IImageFrame> Buffers { get; }
        int NumberOfBuffers { get; }
        IImageFrame GetBuffer(int bufferIdx);
    }
}