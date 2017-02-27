using System;

namespace CmdArt
{
    public interface ISize : IEquatable<ISize>
    {
        int Width { get; }
        int Height { get; }
        bool IsZeroSize { get; }
    }

    public class Size : ISize
    {
        public Size(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            Height = height;
            Width = width;
        }

        public int Width { get; }
        public int Height { get; }

        public static ISize FitButMaintainAspectRatio(ISize container, int startWidth, int startHeight)
        {
            if (startWidth == container.Width && startHeight == container.Height)
                return container;

            // Given a region of size (startWidth x startHeight) we need to maintain the aspect ratio
            // but shrink down so that the new region 
            double ri = ((double)startWidth / (double)startHeight);
            double rs = ((double)container.Width / (double)container.Height);
            if (rs > ri)
            {
                var newWidth = (int)(startWidth * ((double)container.Height / (double)startHeight));
                return new Size(newWidth, container.Height);
            }

            var newHeight = (int)(startHeight * ((double)container.Width / (double)startWidth));
            return new Size(container.Width, newHeight);
        }

        public bool IsZeroSize => Height == 0 || Width == 0;

        public bool Equals(ISize other)
        {
            if (other == null)
                return false;
            return Height == other.Height && Width == other.Width;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Width * 397) ^ Height;
            }
        }

        public static bool operator ==(Size left, Size right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Size)obj);
        }
    }
}
