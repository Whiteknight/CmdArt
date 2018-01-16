namespace CmdArt.Screen
{
    public class PixelBuffer : IPixelBuffer
    {
        public ScreenPixel[,] Raw { get; }

        public PixelBuffer(uint width, uint height)
            : this(new Size(width, height))
        {
        }

        public PixelBuffer(ISize size)
        {
            if (size == null)
                throw new System.ArgumentNullException(nameof(size));
            if (size.IsZeroSize)
                throw new System.ArgumentOutOfRangeException(nameof(size));

            Size = size;
            Raw = new ScreenPixel[size.Width, size.Height];
            DefaultPalette = Palette.Default;
        }

        public ISize Size { get; }

        public Palette DefaultPalette { get; set; }

        public void Set(uint left, uint top, byte color, char c)
        {
            // Silently ignore update requests outside the buffer region
            if (IsOutsideBounds(left, top))
                return;

            // Silently ignore if the pixel is already the same, don't mark it updated.
            if (Raw[left, top].Color == color && Raw[left, top].Character == c)
                return;

            Raw[left, top].Color = color;
            Raw[left, top].Character = c;
            Raw[left, top].IsUpdated = true;
        }

        public void Set(uint left, uint top, byte color, string s)
        {
            for (uint i = 0; i < Size.Width && i < s.Length; i++)
            {
                Set(left + i, top, color, s[(int)i]);
            }
        }

        public void Set(Region region, byte color, char c)
        {
            for (uint j = region.Top; j < region.Top + region.Height && j < Size.Height; j++)
            {
                for (uint i = region.Left; i < region.Left + region.Width && i < Size.Width; i++)
                {
                    Set(i, j, color, c);
                }
            }
        }

        public void SetColor(uint left, uint top, byte color)
        {
            // Silently ignore update requests outside the buffer region
            if (IsOutsideBounds(left, top))
                return;

            // Silently ignore if the pixel is already the same, don't mark it updated.
            if (Raw[left, top].Color == color)
                return;

            Raw[left, top].Color = color;
            Raw[left, top].IsUpdated = true;
        }

        public void SetColor(Region region, byte color)
        {
            for (uint j = region.Top; j < region.Top + region.Height && j < Size.Height; j++)
            {
                for (uint i = region.Left; i < region.Left + region.Width && i < Size.Width; i++)
                {
                    SetColor(i, j, color);
                }
            }
        }

        public void SetCharacter(uint left, uint top, char c)
        {
            // Silently ignore update requests outside the buffer region
            if (IsOutsideBounds(left, top))
                return;

            // Silently ignore if the pixel is already the same, don't mark it updated.
            if (Raw[left, top].Character == c)
                return;

            Raw[left, top].Character = c;
            Raw[left, top].IsUpdated = true;
        }

        public void SetCharacter(Region region, char c)
        {
            for (uint j = region.Top; j < region.Top + region.Height && j < Size.Height; j++)
            {
                for (uint i = region.Left; i < region.Left + region.Width && i < Size.Width; i++)
                {
                    SetCharacter(i, j, c);
                }
            }
        }

        public void Unset(uint left, uint top)
        {
            Set(left, top, DefaultPalette.ByteValue, '\0');
        }

        public void Unset(Region region)
        {
            Set(region, DefaultPalette.ByteValue, '\0');
        }

        private bool IsOutsideBounds(uint left, uint top)
        {
            return left >= Size.Width || top >= Size.Height;
        }

        public bool IsVisible(uint left, uint top)
        {
            if (IsOutsideBounds(left, top))
                return false;
            return Raw[left, top].IsVisible;
        }

        public bool IsUpdated(uint left, uint top)
        {
            if (IsOutsideBounds(left, top))
                return false;
            return Raw[left, top].IsUpdated;
        }

        public void SetUpdated(uint left, uint top, bool updated)
        {
            if (IsOutsideBounds(left, top))
                return;
            Raw[left, top].IsUpdated = updated;
        }

        public ScreenPixel Get(uint left, uint top)
        {
            if (IsOutsideBounds(left, top))
                return ScreenPixel.Transparent;
            return Raw[left, top];
        }
    }
}
