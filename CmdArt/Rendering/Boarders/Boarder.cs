using System;
using System.Diagnostics;
using CmdArt.Screen;

namespace CmdArt.Rendering.Boarders
{
    public class Boarder : IDecoration
    {
        private readonly IBoarderChars _chars;
        private readonly DecorationSide _sides;
        private readonly Palette _palette;
        private readonly string _title;
        private readonly bool _top;
        private readonly bool _left;
        private readonly bool _right;
        private readonly bool _bottom;

        public Boarder(IBoarderChars chars, Palette palette, DecorationSide sides = DecorationSide.All, string title = null)           
        {
            _chars = chars;
            _palette = palette;
            _sides = sides;
            _title = title;
            _top = (sides & DecorationSide.Top) == DecorationSide.Top;
            _left = (sides & DecorationSide.Left) == DecorationSide.Left;
            _right = (sides & DecorationSide.Right) == DecorationSide.Right;
            _bottom = (sides & DecorationSide.Bottom) == DecorationSide.Bottom;
        }

        public Boarder WithPalette(Palette palette)
        {
            return new Boarder(_chars, palette, _sides, _title);
        }

        public Boarder Partial(DecorationSide sides)
        {
            return new Boarder( _chars, _palette, sides, _title);
        }

        public Boarder WithTitle(string title)
        {
            return new Boarder(_chars, _palette, _sides, title);
        }

        public int Top { get { return _top ? 1 : 0;  } }
        public int Left { get { return _left ? 1 : 0; } }
        public int Right { get { return _right ? 1 : 0; } }
        public int Bottom { get { return _bottom ? 1 : 0; } }

        public Region InnerRegion(Region region)
        {
            if (region.Width < 3 || region.Height < 3)
                return region;
            return region.RelativeToAbsolute(new Region(Left, Top, region.Width - Left - Right, region.Height - Top - Bottom));
        }

        public Region RenderTo(IPixelBuffer buffer, Region region)
        {
            if (region.Width < 3 || region.Height < 3)
                return region;

            RenderTopBoarder(buffer, region, _palette);

            RenderLeftBoarder(buffer, region, _palette);
            RenderRightBoarder(buffer, region, _palette);

            RenderBottomBoarder(buffer, region, _palette);

            return InnerRegion(region);
        }

        private void RenderBottomBoarder(IPixelBuffer buffer, Region region, Palette palette)
        {
            if (_left || _bottom)
                buffer.Set(region.Left, region.Top + region.Height - 1, palette, _chars.LowerLeft);
            if (_bottom)
                buffer.Set(region.Left + 1, region.Top + region.Height - 1, palette, new string(_chars.Horizontal, region.Width - 2));
            if (_bottom || _right)
                buffer.Set(region.Left + region.Width - 1, region.Top + region.Height - 1, palette, _chars.LowerRight);
        }

        private void RenderRightBoarder(IPixelBuffer buffer, Region region, Palette palette)
        {
            if (_right)
            {
                for (int i = 1; i < region.Height - 1; i++)
                    buffer.Set(region.Left + region.Width - 1, region.Top + i, palette, _chars.Vertical);
            }
        }

        private void RenderLeftBoarder(IPixelBuffer buffer, Region region, Palette palette)
        {
            if (_left)
            {
                for (int i = 1; i < region.Height - 1; i++)
                    buffer.Set(region.Left, region.Top + i, palette, _chars.Vertical);
            }
        }

        private void RenderTopBoarder(IPixelBuffer buffer, Region region, Palette palette)
        {
            if (_top || _left)
                buffer.Set(region.Left, region.Top, palette, _chars.UpperLeft);
            if (_top)
            {
                string s;
                if (!string.IsNullOrEmpty(_title))
                {
                    int diff = region.Width - Left - Right - 2 - _title.Length;
                    s = new string(_chars.Horizontal, diff / 2) + _chars.TeeLeft + _title + _chars.TeeRight + new string(_chars.Horizontal, (diff / 2) + (diff % 2));
                }
                else
                    s = new string(_chars.Horizontal, region.Width - 2);
                buffer.Set(region.Left + 1, region.Top, palette, s);
            }
            if (_top || _right)
                buffer.Set(region.Left + region.Width - 1, region.Top, palette, _chars.UpperRight);
        }

        // TODO: Move this to a method on BoarderChars
        public static Boarder FromStringArray(string[] chars)
        {
            Debug.Assert(chars.Length == 3);
            int width = chars[0].Length;
            CustomBoarderChars bc;
            if (width == 3)
                bc = new CustomBoarderChars(chars[0][0], chars[0][2], chars[2][0], chars[2][2], chars[0][1], chars[1][0], chars[0][1], chars[0][1], ' ', ' ', ' ');
            else if (width == 6)
                bc = new CustomBoarderChars(chars[0][0], chars[0][5], chars[2][0], chars[2][5], chars[0][1], chars[1][0], chars[0][2], chars[0][3], ' ', ' ', ' ');
            else
                throw new Exception("Cannot understand string array");

            return new Boarder(bc, Palette.Default);
        }
        
        // TODO: Change all these to be subclasses.
        public static Boarder SolidDoubleLine()
        {
            return new Boarder(BoarderChars.Double, Palette.Default);
        }

        public static Boarder SolidSingleLine()
        {
            return new Boarder(BoarderChars.Single, Palette.Default);
        }

        public static Boarder DashedSingleLine()
        {
            string[] chars = new[] {
                "+-[]-+",
                "|    |",
                "+----+"
            };
            return FromStringArray(chars);
        }

        public static Boarder PointyIn()
        {
            string[] chars = new[] {
                "+v+",
                "> <",
                "+^+"
            };
            return FromStringArray(chars);
        }

        public static Boarder PointyOut()
        {
            string[] chars = new[] {
                "+^+",
                "< >",
                "+v+"
            };
            return FromStringArray(chars);
        }

        public static Boarder Invisible()
        {
            string[] chars = new[] {
                "   ",
                "   ",
                "   "
            };
            return FromStringArray(chars);
        }
    }
}