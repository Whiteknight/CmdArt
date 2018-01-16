namespace CmdArt
{
    public struct Location : ILocation
    {
        public Location(uint left, uint top)
            : this()
        {
            Top = top;
            Left = left;
        }

        public uint Left { get; }
        public uint Top { get; }

        public static ILocation Origin => new Location(0, 0);
    }
}