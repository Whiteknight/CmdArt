using System.Collections.Generic;

namespace CmdArt.Screen
{
    public class PixelBuffer : IPixelBuffer
    {
        private readonly ScreenPixel[,] _buffer;

        public PixelBuffer(int width, int height)
            : this(new Size(width, height))
        {
        }

        public PixelBuffer(ISize size)
        {
            Size = size;
            _buffer = new ScreenPixel[size.Width, size.Height];
            DefaultPalette = Palette.Default;
        }

        public ISize Size { get; }

        public Palette DefaultPalette { get; set; }

        public void Set(int left, int top, Palette palette, char c)
        {
            Set(left, top, palette.ByteValue, c);
        }

        public void Set(int left, int top, byte color, char c)
        {
            // Silently ignore update requests outside the buffer region
            if (IsOutsideBounds(left, top))
                return;

            // Silently ignore if the pixel is already the same, don't mark it updated.
            if (_buffer[left, top].Color == color && _buffer[left, top].Character == c)
                return;

            _buffer[left, top].Color = color;
            _buffer[left, top].Character = c;
            _buffer[left, top].IsUpdated = true;
        }

        public void Set(int left, int top, Palette palette, string s)
        {
            for (int i = 0; i < Size.Width && i < s.Length; i++)
            {
                Set(left + i, top, palette, s[i]);
            }
        }

        public void Set(Region region, Palette palette, char c)
        {
            byte color = palette.ByteValue;
            for (int j = region.Top; j < region.Top + region.Height && j < Size.Height; j++)
            {
                for (int i = region.Left; i < region.Left + region.Width && i < Size.Width; i++)
                {
                    Set(i, j, color, c);
                }
            }
        }

        public void Set(int left, int top, char c)
        {
            Set(left, top, DefaultPalette, c);
        }

        public void Set(int left, int top, string s)
        {
            Set(left, top, DefaultPalette, s);
        }

        public void Set(Region region, char c)
        {
            Set(region, DefaultPalette, c);
        }

        public void SetColor(int left, int top, Palette palette)
        {
            var color = palette.ByteValue;

            SetColor(left, top, color);
        }

        private void SetColor(int left, int top, byte color)
        {
            // Silently ignore update requests outside the buffer region
            if (IsOutsideBounds(left, top))
                return;

            // Silently ignore if the pixel is already the same, don't mark it updated.
            if (_buffer[left, top].Color == color)
                return;

            _buffer[left, top].Color = color;
            _buffer[left, top].IsUpdated = true;
        }

        public void SetColor(Region region, Palette palette)
        {
            byte color = palette.ByteValue;
            for (int j = region.Top; j < region.Top + region.Height && j < Size.Height; j++)
            {
                for (int i = region.Left; i < region.Left + region.Width && i < Size.Width; i++)
                {
                    SetColor(i, j, color);
                }
            }
        }

        public void SetCharacter(int left, int top, char c)
        {
            // Silently ignore update requests outside the buffer region
            if (IsOutsideBounds(left, top))
                return;

            // Silently ignore if the pixel is already the same, don't mark it updated.
            if (_buffer[left, top].Character == c)
                return;

            _buffer[left, top].Character = c;
            _buffer[left, top].IsUpdated = true;
        }

        public void SetCharacter(Region region, char c)
        {
            for (int j = region.Top; j < region.Top + region.Height && j < Size.Height; j++)
            {
                for (int i = region.Left; i < region.Left + region.Width && i < Size.Width; i++)
                {
                    SetCharacter(i, j, c);
                }
            }
        }

        public IEnumerable<ILocation> AllVisible()
        {
            for (int j = 0; j < Size.Height; j++)
            {
                for (int i = 0; i < Size.Width; i++)
                {
                    if (_buffer[i, j].IsVisible)
                        yield return new Location(i, j);
                }
            }
        }

        public void Unset(int left, int top)
        {
            Set(left, top, DefaultPalette, '\0');
        }

        public void Unset(Region region)
        {
            Set(region, DefaultPalette, '\0');
        }

        private bool IsOutsideBounds(int left, int top)
        {
            return left < 0 || top < 0 || left >= Size.Width || top >= Size.Height;
        }

        public bool IsVisible(int left, int top)
        {
            if (IsOutsideBounds(left, top))
                return false;
            return _buffer[left, top].IsVisible;
        }

        public bool IsUpdated(int left, int top)
        {
            if (IsOutsideBounds(left, top))
                return false;
            return _buffer[left, top].IsUpdated;
        }

        public void SetUpdated(int left, int top, bool updated)
        {
            if (IsOutsideBounds(left, top))
                return;
            _buffer[left, top].IsUpdated = updated;
        }

        public byte GetColorByte(int left, int top)
        {
            if (IsOutsideBounds(left, top))
                return DefaultPalette.ByteValue;
            return _buffer[left, top].Color;
        }

        public char GetCharacter(int left, int top)
        {
            if (IsOutsideBounds(left, top))
                return '\0';
            return _buffer[left, top].Character;
        }

        //public void ForeachVisible(Action<ScreenPixel> act)
        //{
        //    for (int j = 0; j < Size.Height; j++)
        //    {
        //        for (int i = 0; i < Size.Width; i++)
        //        {
        //            if (_buffer[i, j].Character != '\0')
        //            {
        //                act(_buffer[i, j]);
        //            }
        //        }
        //    }
        //}
    }
}
