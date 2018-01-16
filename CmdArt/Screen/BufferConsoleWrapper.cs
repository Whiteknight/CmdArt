using CmdArt.Colors;
using System;

namespace CmdArt.Screen
{
    // Dummy IConsoleWrapper with an IPixelBuffer backend for testing purposes.
    public class BufferConsoleWrapper : IConsoleWrapper
    {
        private byte _currentColor;
        private uint _left;
        private uint _top;
        private uint _width;
        private uint _height;

        public BufferConsoleWrapper()
        {
            _currentColor = Palette.Default.ByteValue;
            _left = 0;
            _top = 0;
            _width = 80;
            _height = 24;
            Buffer = new PixelBuffer(_width, _height);
        }

        public void SetForAsciiGraphics()
        {
        }

        public void SetColor(ConsoleColor foreground, ConsoleColor background)
        {
            _currentColor = ConsoleColorUtilities.ColorsToByte(foreground, background);
        }

        public void SetCursorPosition(int i, int j)
        {
            if (i > 0)
                _left = (uint)i;
            if (j > 0)
                _top = (uint)j;
        }

        public void SetSize(int width, int height)
        {
            Buffer = new PixelBuffer(_width, _height);
        }

        public void Write(char c)
        {
            Buffer.Set(_left, _top, _currentColor, c);
        }

        public Region WindowRegion => new Region(0, 0, _width, _height);
        public Region WindowMaxRegion => new Region(0, 0, 200, 100);
        public Region WindowMaxVerticalRegion => new Region(0, 0, _width, 100);

        public Palette GetCurrentPalette()
        {
            return new Palette(_currentColor);
        }

        public IPixelBuffer Buffer { get; private set; }
    }
}