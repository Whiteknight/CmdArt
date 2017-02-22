using System;
using System.Collections.Generic;
using System.Linq;

namespace CmdArt.Screen
{
    public class TerminalScreen
    {
        public TerminalScreen(IConsoleWrapper consoleWrapper = null, IPixelBufferFactory bufferFactory = null)
        {
            BufferFactory = bufferFactory ?? new PixelBufferFactory();
            Buffer = BufferFactory.CreateForTerminalScreen();
            Renderer = new ScreenRenderer();
            Windows = new List<TerminalScreenWindow>();
            ConsoleWrapper = consoleWrapper ?? new ConsoleWrapper();
        }

        public IConsoleWrapper ConsoleWrapper { get; }
        public ICollection<TerminalScreenWindow> Windows { get; }
        public ScreenRenderer Renderer { get; }
        public IPixelBufferFactory BufferFactory { get; }
        public IPixelBuffer Buffer { get; }

        public void Render()
        {
            Renderer.Render(Buffer, ConsoleWrapper);
        }

        public TerminalScreenWindow CreateNewWindow(Region region)
        {
            if (!Region.Window.CompletelyContains(region))
                throw new ArgumentOutOfRangeException(nameof(region), "Specified region must be entirely contained within the screen");
            if (Windows.Any(existing => existing.ScreenRegion.Overlaps(region)))
                throw new ArgumentOutOfRangeException(nameof(region), "Specified region may not overlap with any existing window regions");

            var window = new TerminalScreenWindow(this, region);
            Windows.Add(window);
            return window;
        }
    }
}
