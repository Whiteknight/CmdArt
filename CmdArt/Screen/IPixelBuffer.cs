namespace CmdArt.Screen
{
    public interface IPixelBuffer
    {
        // TODO: Shrink this interface. Several of these methods and overloads can become extension methods
        ISize Size { get; }
        Palette DefaultPalette { get; set; }

        void Set(int left, int top, byte color, char c);
        void Set(int left, int top, byte color, string s);
        void Set(Region region, byte color, char c);

        void SetColor(int left, int top, byte color);
        void SetColor(Region region, byte color);

        void SetCharacter(int left, int top, char c);
        void SetCharacter(Region region, char c);

        void Unset(int left, int top);
        void Unset(Region region);

        bool IsVisible(int left, int top);

        bool IsUpdated(int left, int top);
        void SetUpdated(int left, int top, bool updated);

        ScreenPixel Get(int left, int top);
    }
}