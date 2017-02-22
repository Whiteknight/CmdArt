using System;

namespace RichCmd.Rendering
{
    [Flags]
    public enum DecorationSide
    {
        None = 0,
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        All = 15
    }
}