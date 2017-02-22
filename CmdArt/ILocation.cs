﻿namespace RichCmd
{
    public interface ILocation
    {
        int Left { get; }
        int Top { get;  }
    }

    public struct Location : ILocation
    {
        public Location(int left, int top)
            : this()
        {
            Top = top;
            Left = left;
        }

        public int Left { get; private set; }
        public int Top { get; private set; }

        public static ILocation Origin
        {
            get { return new Location(0, 0); }
        }
    }
}