using System.Collections.Generic;

namespace CmdArt.Images
{
    public interface IImageBuffer
    {
        ISize Size { get; }
        IEnumerable<IImageFrame> Buffers { get; }
        int NumberOfBuffers { get; }
        IImageFrame GetBuffer(int bufferIdx);
    }
}