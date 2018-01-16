using System;

namespace CmdArt.Screen
{
    public class TerminalScreen
    {
        public TerminalScreen(IConsoleWrapper consoleWrapper = null, IPixelBufferFactory bufferFactory = null)
        {
            Console = consoleWrapper ?? new ConsoleWrapper();
            BufferFactory = bufferFactory ?? new PixelBufferFactory(Console);
            Buffer = BufferFactory.CreateForTerminalScreen();
            Renderer = new ScreenRenderer();
        }

        public IConsoleWrapper Console { get; }
        public ScreenRenderer Renderer { get; }
        public IPixelBufferFactory BufferFactory { get; }
        public IPixelBuffer Buffer { get; private set; }

        public void Render(bool force = false)
        {
            Renderer.Render(Buffer, Console, force);
        }

        public void ResizeConsole(ISize size, bool preserveContents = false)
        {
            if (size == null)
                throw new ArgumentNullException(nameof(size));

            Console.SetSize(size);
            var buffer = BufferFactory.CreateForTerminalScreen();
            if (preserveContents)
            {
                // TODO: Copy from old buffer to new buffer
            }
            Buffer = buffer;
        }
    }
}
