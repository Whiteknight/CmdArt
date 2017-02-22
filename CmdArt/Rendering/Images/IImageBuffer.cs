using System.Collections.Generic;

namespace RichCmd.Rendering.Images
{
    public interface IImageBuffer
    {
        Region Region { get; }
        IEnumerable<IImageFrame> Buffers { get; }
        int NumberOfBuffers { get; }
        IImageFrame GetBuffer(int bufferIdx);
    }
}