using System;
using System.Collections.Generic;
using System.Linq;

namespace RichCmd
{
    public interface ISize : IEquatable<ISize>
    {
        int Width { get; }
        int Height { get;  }
    }

    public class Size : ISize
    {
        public Size(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public static Size Maximum(IReadOnlyList<ISize> sizes)
        {
            int height = sizes.Max(s => s.Height);
            int width = sizes.Max(s => s.Width);
            return new Size(width, height);
        }

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
