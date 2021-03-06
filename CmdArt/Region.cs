﻿using System;
using System.Text;

namespace CmdArt
{
    public struct Region : ISize, ILocation
    {
        public Region(uint left, uint top, uint width, uint height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public Region(uint left, uint top, ISize size)
        {
            Left = left;
            Top = top;
            Width = size.Width;
            Height = size.Height;
        }

        public Region(ILocation location, uint width, uint height)
        {
            Left = location.Left;
            Top = location.Top;
            Width = width;
            Height = height;
        }

        public Region(ILocation location, ISize size)
        {
            Left = location.Left;
            Top = location.Top;
            Width = size.Width;
            Height = size.Height;
        }

        public uint Left { get; }
        public uint Top { get; }
        public uint Width { get; }
        public uint Height { get; }

        public static Region None => new Region();

        public ISize RegionSize => new Size(Width, Height);
        public ILocation RegionLocation => new Location(Left, Top);

        public bool IsZeroSize => Width == 0 || Height == 0;

        public Region RelativeToAbsolute(Region relative)
        {
            return new Region(Left + relative.Left, Top + relative.Top, Math.Min(Width - relative.Left, relative.Width), Math.Min(Height - relative.Top, relative.Height));
        }

        public Region Adjust(int leftDelta, int topDelta, int widthDelta, int heightDelta)
        {
            return new Region((uint)(Left + leftDelta), (uint)(Top + topDelta), (uint)(Width + widthDelta), (uint)(Height + heightDelta));
        }

        public Region Offset(ILocation location)
        {
            return new Region(Left + location.Left, Top + location.Top, Width, Height);
        }

        public Region Move(int leftDelta, int topDelta)
        {
            return new Region((uint)(Left + leftDelta), (uint)(Top + topDelta), Width, Height);
        }

        public Region ShrinkToFit(uint width, uint height)
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
        // TODO: IEquatable<Region>

        public override string ToString()
        {
            return $"({Left},{Top}) {Width}x{Height}";
        }

        public string ToString(string fmt)
        {
            var builder = new StringBuilder();
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

        public bool IsCompletelyContainedBy(Region other)
        {
            return other.CompletelyContains(this);
        }

        public bool CompletelyContains(Region other)
        {
            if (Left <= other.Left &&
                Top <= other.Top &&
                Left + Width >= other.Left + other.Width &&
                Top + Height >= other.Top + other.Height)
                return true;
            return false;
        }
    }
}