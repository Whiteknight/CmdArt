namespace CmdArt.Screen
{
    public class PixelBufferFactory : IPixelBufferFactory
    {
        private readonly IConsoleWrapper _console;

        public PixelBufferFactory(IConsoleWrapper console)
        {
            if (console == null)
                throw new System.ArgumentNullException(nameof(console));

            _console = console;
        }

        public IPixelBuffer Create(ISize arg)
        {
            return new PixelBuffer(arg);
        }

        public IPixelBuffer CreateForTerminalScreen()
        {
            return new PixelBuffer(_console.WindowRegion);
        }
    }
}
