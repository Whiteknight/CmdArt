using System;
using System.Text;

namespace RichCmd
{
    public struct Region : ISize, ILocation
    {
        private readonly int _left;
        private readonly int _top;
        private readonly int _width;
        private readonly int _height;

        public Region(int left, int top, int width, int height)
        {
            _left = left;
            _top = top;
            _width = width;
            _height = height;
        }

        public Region(int left, int top, ISize size)
        {
            _left = left;
            _top = top;
            _width = size.Width;
            _height = size.Height;
        }

        public Region(ILocation location, int width, int height)
        {
            _left = location.Left;
            _top = location.Top;
            _width = width;
            _height = height;
        }

        public Region(ILocation location, ISize size)
        {
            _left = location.Left;
            _top = location.Top;
            _width = size.Width;
            _height = size.Height;
        }

        public int Left { get { return _left; } }
        public int Top { get { return _top; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public static Region None
        {
            get { return new Region(); }
        }

        public static Region Window
        {
            get { return new Region(0, 0, Console.WindowWidth, Console.WindowHeight - 1); }
        }

        public static Region WindowMax
        {
            get { return new Region(0, 0, Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        public static Region WindowMaxVertical
        {
            get { return new Region(0, 0, Console.WindowWidth, Console.LargestWindowHeight); }
        }

        public Region RelativeToAbsolute(Region relative)
        {
            return new Region(Left + relative.Left, Top + relative.Top, Math.Min(Width - relative.Left, relative.Width), Math.Min(Height - relative.Top, relative.Height));
        }

        public Region Adjust(int leftDelta, int topDelta, int widthDelta, int heightDelta)
        {
            return new Region(Left + leftDelta, Top + topDelta, Width + widthDelta, Height + heightDelta);
        }

        public Region Offset(ILocation location)
        {
            return new Region(Left + location.Left, Top + location.Top, Width, Height);
        }

        public Region Move(int leftDelta, int topDelta)
        {
            return new Region(Left + leftDelta, Top + topDelta, Width, Height);
        }

        public Region ShrinkToFit(int width, int height)
        {
            if (Height < height)
                height = Height;
            if (Width < width)
                width = Width;
            return new Region(Left, Top, width, height);
        }

        public bool Equals(ISize other)
        {
            if (other == null)
                return false;
            return Height == other.Height && Width == other.Width;
        }
        // TODO: ==ISize and !=ISize

        public override string ToString()
        {
            return string.Format("({0},{1}) {2}x{3}", Left, Top, Width, Height);
        }

        public string ToString(string fmt)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < fmt.Length; i++)
            {
                switch (fmt[i])
                {
                    case '\\':
                        i++;
                        builder.Append(fmt[i]);
                        break;
                    case 'L':
                        builder.Append(Left);
                        break;
                    case 'T':
                        builder.Append(Top);
                        break;
                    case 'W':
                        builder.Append(Width);
                        break;
                    case 'H':
                        builder.Append(Height);
                        break;
                    default:
                        builder.Append(fmt[i]);
                        break;
                }
            }
            return builder.ToString();
        }

        public bool Overlaps(Region other)
        {
            if (other.Left + other.Width < Left ||
                other.Top + other.Height < Top ||
                other.Left > Left + Width ||
                other.Top > Top + Height)
                return false;
            return true;
        }
    }
}