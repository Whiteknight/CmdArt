using System;
using CmdArt.Screen;

namespace CmdArt.Rendering.Shapes
{
    public class Line : IRenderable
    {
        private readonly ILocation _loc1;
        private readonly ILocation _loc2;
        private readonly char _symbol;

        public Line(ILocation loc1, ILocation loc2, char symbol)
        {
            _loc1 = loc1;
            _loc2 = loc2;
            _symbol = symbol;
        }

        public void Render(IPixelBuffer buffer)
        {
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
                ILocation temp = loc1;
                loc1 = _loc2;
                loc2 = temp;
            }
            int dx = loc2.Left - loc1.Left;
            int dy = Math.Abs(loc2.Top - loc1.Top);
            int error = dx / 2;
            int ystep = (loc1.Top < loc2.Top) ? 1 : -1;
            int y = loc1.Top;
            for (int x = loc1.Left; x <= loc2.Left; x++)
            {
                buffer.Set((steep ? y : x), (steep ? x : y), _symbol);
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
    }
}
