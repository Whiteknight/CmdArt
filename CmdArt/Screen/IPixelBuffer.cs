namespace CmdArt.Screen
{
    public interface IPixelBuffer
    {
        // TODO: Shrink this interface. Several of these methods and overloads can become extension methods
        ISize Size { get; }
        Palette DefaultPalette { get; set; }

        void Set(uint left, uint top, byte color, char c);
        void Set(uint left, uint top, byte color, string s);
        void Set(Region region, byte color, char c);

        void SetColor(uint left, uint top, byte color);
        void SetColor(Region region, byte color);

        void SetCharacter(uint left, uint top, char c);
        void SetCharacter(Region region, char c);

        void Unset(uint left, uint top);
        void Unset(Region region);

        bool IsVisible(uint left, uint top);

        bool IsUpdated(uint left, uint top);
        void SetUpdated(uint left, uint top, bool updated);

        ScreenPixel Get(uint left, uint top);
    }
}