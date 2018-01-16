using CmdArt.Screen;
using System;

namespace CmdArt.Rendering.Shapes
{
    public class Line : IRenderable
    {
        private readonly ILocation _loc1;
        private readonly ILocation _loc2;
        private readonly char _symbol;
        private readonly byte _color;

        public Line(ILocation loc1, ILocation loc2, char symbol, byte color)
        {
            if (loc1 == null)
                throw new ArgumentNullException(nameof(loc1));

            if (loc2 == null)
                throw new ArgumentNullException(nameof(loc2));

            _loc1 = loc1;
            _loc2 = loc2;
            _symbol = symbol;
            _color = color;
        }

        public Line(ILocation loc1, ILocation loc2, char symbol, Palette palette)
            : this(loc1, loc2, symbol, palette.ByteValue)
        {
        }

        // TODO: Constructor take a string, and print the characters of the string along the line.

        public void RenderTo(IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            var loc1 = _loc1;
            var loc2 = _loc2;
            bool steep = Math.Abs(_loc2.Top - _loc1.Top) > Math.Abs(_loc2.Left - _loc1.Left);
            if (steep)
            {
                loc1 = new Location(loc1.Top, loc1.Left);
                loc2 = new Location(loc2.Top, loc2.Left);
            }
            if (loc1.Left > loc2.Left)
            {
                var temp = loc1;
                loc1 = _loc2;
                loc2 = temp;
            }
            int dx = (int)loc2.Left - (int)loc1.Left;
            int dy = Math.Abs((int)loc2.Top - (int)loc1.Top);
            int error = dx / 2;
            int ystep = (loc1.Top < loc2.Top) ? 1 : -1;
            uint y = loc1.Top;
            for (uint x = loc1.Left; x <= loc2.Left; x++)
            {
                buffer.Set((steep ? y : x), (steep ? x : y), _color, _symbol);
                error = error - dy;
                if (error < 0)
                {
                    y = (uint)((int)y + ystep);
                    error += dx;
                }
            }
        }
    }
}
