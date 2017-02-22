using CmdArt.Utilities;

namespace CmdArt.Screen
{
    public interface IPixelBufferFactory : IFactory<IPixelBuffer, ISize>
    {
        IPixelBuffer CreateForTerminalScreen();
    }

    public class PixelBufferFactory : IPixelBufferFactory
    {
        public IPixelBuffer Create(ISize arg)
        {
            return new PixelBuffer(arg);
        }

        public IPixelBuffer CreateForTerminalScreen()
        {
            return new PixelBuffer(Region.Window);
        }
    }
}
