﻿using RichCmd.Screen;

namespace RichCmd.Rendering.Shapes
{
    public class Rectangle : IRenderable
    {
        private readonly Region _region;
        private readonly char _symbol;

        // TODO: Bool flag to fill or not
        public Rectangle(Region region)
            : this(region, ' ')
        {
        }

        public Rectangle(Region region, char symbol)
        {
            _region = region;
            _symbol = symbol;
        }

        public void Render(IScreenBuffer buffer)
        {
            // Rectangle render logic is in IScreenBuffer
            buffer.Set(_region, _symbol);
        }
    }
}
