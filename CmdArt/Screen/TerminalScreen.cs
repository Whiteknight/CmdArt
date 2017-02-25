using System;
using System.Collections.Generic;
using System.Linq;

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
            Windows = new List<TerminalScreenWindow>();
        }

        public IConsoleWrapper Console { get; }
        public ICollection<TerminalScreenWindow> Windows { get; }
        public ScreenRenderer Renderer { get; }
        public IPixelBufferFactory BufferFactory { get; }
        public IPixelBuffer Buffer { get; private set; }

        public void Render(bool includeWindows = false, bool force = false)
        {
            if (includeWindows)
            {
                foreach (var window in Windows)
                    window.Render();
            }
            Renderer.Render(Buffer, Console, force);
        }

        public TerminalScreenWindow CreateNewWindow(Region region)
        {
            if (!Console.WindowRegion.CompletelyContains(region))
                throw new ArgumentOutOfRangeException(nameof(region), "Specified region must be entirely contained within the screen");
            if (Windows.Any(existing => existing.ScreenRegion.Overlaps(region)))
                throw new ArgumentOutOfRangeException(nameof(region), "Specified region may not overlap with any existing window regions");

            var window = new TerminalScreenWindow(this, region);
            Windows.Add(window);
            return window;
        }

        public void ResizeConsole(ISize size, bool preserveContents = false)
        {
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
