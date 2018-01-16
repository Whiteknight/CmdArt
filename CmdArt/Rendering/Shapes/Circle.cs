using CmdArt.Screen;
using System;

namespace CmdArt.Rendering.Shapes
{
    public class Circle : IRenderable
    {
        private readonly ILocation _center;
        private readonly uint _radius;
        private readonly char _symbol;
        private readonly byte _color;

        // TODO: Bool flag to fill or not
        public Circle(ILocation center, uint radius, char symbol, byte color)
        {
            if (center == null)
                throw new ArgumentNullException(nameof(center));

            _center = center ?? new Location(0, 0);
            _radius = radius;
            _symbol = symbol;
            _color = color;
        }

        public Circle(ILocation center, uint radius, char symbol, Palette color)
            : this(center, radius, symbol, color.ByteValue)
        {
        }

        public void RenderTo(IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            uint x = _radius;
            uint y = 0;
            int err = 0;

            while (x >= y)
            {
                buffer.Set(_center.Left + x, _center.Top + y, _color, _symbol);
                buffer.Set(_center.Left + y, _center.Top + x, _color, _symbol);
                buffer.Set(_center.Left - y, _center.Top + x, _color, _symbol);
                buffer.Set(_center.Left - x, _center.Top + y, _color, _symbol);
                buffer.Set(_center.Left - x, _center.Top - y, _color, _symbol);
                buffer.Set(_center.Left - y, _center.Top - x, _color, _symbol);
                buffer.Set(_center.Left + y, _center.Top - x, _color, _symbol);
                buffer.Set(_center.Left + x, _center.Top - y, _color, _symbol);

                y += 1;
                err += 1 + 2 * (int)y;
                if (2 * (err - x) + 1 > 0)
                {
                    x -= 1;
                    err += 1 - 2 * (int)x;
                }
            }
        }
    }
}