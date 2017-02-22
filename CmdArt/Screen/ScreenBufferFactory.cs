using CmdArt.Utilities;

namespace CmdArt.Screen
{
    public class ScreenBufferFactory : IFactory<IScreenBuffer, ISize>
    {
        public IScreenBuffer Create(ISize arg)
        {
            return new ScreenBuffer(arg);
        }
    }
}
