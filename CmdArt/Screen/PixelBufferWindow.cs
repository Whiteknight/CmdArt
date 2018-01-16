namespace CmdArt.Screen
{
    public class PixelBufferWindow : IPixelBuffer
    {
        private readonly IPixelBuffer _inner;
        private readonly ILocation _innerLocation;

        public PixelBufferWindow(IPixelBuffer inner, ILocation innerLocation, ISize size)
        {
            _inner = inner;
            _innerLocation = innerLocation;
            Size = size;
        }

        public ISize Size { get; }

        public Palette DefaultPalette
        {
            get { return _inner.DefaultPalette; }
            set { }
        }

        public void Set(uint left, uint top, byte color, char c)
        {
            _inner.Set(_innerLocation.Left + left, _innerLocation.Top + top, color, c);
        }

        public void Set(uint left, uint top, byte color, string s)
        {
            _inner.Set(_innerLocation.Left + left, _innerLocation.Top + top, color, s);
        }

        public void Set(Region region, byte color, char c)
        {
            _inner.Set(region.Offset(_innerLocation), color, c);
        }

        public void SetColor(uint left, uint top, byte color)
        {
            _inner.SetColor(_innerLocation.Left + left, _innerLocation.Top + top, color);
        }

        public void SetColor(Region region, byte color)
        {
            _inner.SetColor(region.Offset(_innerLocation), color);
        }

        public void SetCharacter(uint left, uint top, char c)
        {
            _inner.SetCharacter(_innerLocation.Left + left, _innerLocation.Top + top, c);
        }

        public void SetCharacter(Region region, char c)
        {
            _inner.SetCharacter(region.Offset(_innerLocation), c);
        }

        public void Unset(uint left, uint top)
        {
            _inner.Unset(_innerLocation.Left + left, _innerLocation.Top + top);
        }

        public void Unset(Region region)
        {
            _inner.Unset(region.Offset(_innerLocation));
        }

        public bool IsVisible(uint left, uint top)
        {
            return _inner.IsVisible(_innerLocation.Left + left, _innerLocation.Top + top);
        }

        public bool IsUpdated(uint left, uint top)
        {
            return _inner.IsUpdated(_innerLocation.Left + left, _innerLocation.Top + top);
        }

        public void SetUpdated(uint left, uint top, bool updated)
        {
            _inner.SetUpdated(_innerLocation.Left + left, _innerLocation.Top + top, updated);
        }

        public ScreenPixel Get(uint left, uint top)
        {
            return _inner.Get(_innerLocation.Left + left, _innerLocation.Top + top);
        }
    }
}