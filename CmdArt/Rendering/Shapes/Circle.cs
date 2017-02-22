using System;
using RichCmd.Screen;

namespace RichCmd.Rendering.Shapes
{
    public class Circle : IRenderable
    {
        private readonly ILocation _center;
        private readonly int _radius;
        private readonly char _symbol;

        // TODO: Bool flag to fill or not
        public Circle(ILocation center, int radius, char symbol)
        {
            _center = center ?? new Location(0, 0);
            _radius = Math.Abs(radius);
            _symbol = symbol;
        }

        public void Render(IScreenBuffer buffer)
        {
            int x = _radius;
            int y = 0;
            int err = 0;
            

            while (x >= y)
            {
                buffer.Set(_center.Left + x, _center.Top + y, _symbol);
                buffer.Set(_center.Left + y, _center.Top + x, _symbol);
                buffer.Set(_center.Left - y, _center.Top + x, _symbol);
                buffer.Set(_center.Left - x, _center.Top + y, _symbol);
                buffer.Set(_center.Left - x, _center.Top - y, _symbol);
                buffer.Set(_center.Left - y, _center.Top - x, _symbol);
                buffer.Set(_center.Left + y, _center.Top - x, _symbol);
                buffer.Set(_center.Left + x, _center.Top - y, _symbol);

                y += 1;
                err += 1 + 2 * y;
                if (2 * (err - x) + 1 > 0)
                {
                    x -= 1;
                    err += 1 - 2 * x;
                }
            }
        }
    }
}