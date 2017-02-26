using CmdArt.Screen;

namespace CmdArt.Rendering
{
    public class SelectionMarker : IDecoration
    {
        private readonly char _glyph;
        private readonly DecorationSide _side;
        private readonly Palette _palette;

        public SelectionMarker(Palette palette, char glyph = ' ', DecorationSide side = DecorationSide.Left)
        {
            _glyph = glyph;
            _side = side;
            _palette = palette;
        }

        public Region InnerRegion(Region region)
        {
            // TODO: _side
            return region.RelativeToAbsolute(new Region(2, 0, region.Width - 2, region.Height));
        }

        public Region RenderTo(IPixelBuffer buffer, Region region)
        {
            buffer.Set(region.Left, region.Top, _palette, _glyph);
            return InnerRegion(region);
        }
    }
}
