using CmdArt.Utilities;

namespace CmdArt.Screen
{
    public class PixelBufferFactory : IFactory<IPixelBuffer, ISize>
    {
        // TODO: Method to create a PixelBuffer for the current console screen size
        public IPixelBuffer Create(ISize arg)
        {
            return new PixelBuffer(arg);
        }
    }
}
