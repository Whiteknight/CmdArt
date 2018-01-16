using CmdArt.Utilities;

namespace CmdArt.Screen
{
    public interface IPixelBufferFactory : IFactory<IPixelBuffer, ISize>
    {
        IPixelBuffer CreateForTerminalScreen();
    }
}