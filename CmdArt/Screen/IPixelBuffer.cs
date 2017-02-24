using System.Collections.Generic;

namespace CmdArt.Screen
{
    public interface IPixelBuffer
    {
        // TODO: Shrink this interface. Several of these methods and overloads can become extension methods
        ISize Size { get; }
        Palette DefaultPalette { get; set; }
        IEnumerable<ILocation> AllVisible();

        void Set(int left, int top, Palette palette, char c);
        void Set(int left, int top, byte color, char c);
        void Set(int left, int top, Palette palette, string s);
        void Set(Region region, Palette palette, char c);

        void Set(int left, int top, char c);
        void Set(int left, int top, string s);
        void Set(Region region, char c);

        void SetColor(int left, int top, Palette palette);
        void SetColor(Region region, Palette palette);

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