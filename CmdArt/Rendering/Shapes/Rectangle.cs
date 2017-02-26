using CmdArt.Screen;

namespace CmdArt.Rendering.Shapes
{
    public class Rectangle : IRenderable
    {
        private readonly Region _region;
        private readonly char _symbol;
        private readonly byte _color;

        // TODO: Bool flag to fill or not
        public Rectangle(Region region, byte color)
            : this(region, ' ', color)
        {
        }

        public Rectangle(Region region, char symbol, byte color)
        {
            _region = region;
            _symbol = symbol;
            _color = color;
        }

        public void RenderTo(IPixelBuffer buffer)
        {
            // Rectangle render logic is in the buffer
            buffer.Set(_region, _color, _symbol);
        }
    }
}
