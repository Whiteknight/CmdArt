using CmdArt.Screen;

namespace CmdArt.Rendering.Boarders
{
    public class DropShadow : IDecoration
    {
        private readonly Palette _palette;

        public DropShadow(Palette palette)
        {
            _palette = palette;
        }

        public Region InnerRegion(Region region)
        {
            return region.RelativeToAbsolute(new Region(0, 0, region.Width - 1, region.Height - 1));
        }

        public Region RenderTo(IPixelBuffer buffer, Region region)
        {
            for (int i = 1; i < region.Height - 1; i++)
            {
                buffer.Set(region.Left + region.Width - 1, region.Top + i, _palette, ' ');
            }
            buffer.Set(region.Left + 1, region.Top + region.Height - 1, _palette, new string(' ', region.Width - 1));

            return InnerRegion(region);
        }
    }
}
