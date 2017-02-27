namespace CmdArt
{
    public interface ILocation
    {
        int Left { get; }
        int Top { get; }
    }

    public struct Location : ILocation
    {
        public Location(int left, int top)
            : this()
        {
            Top = top;
            Left = left;
        }

        public int Left { get; }
        public int Top { get; }

        public static ILocation Origin => new Location(0, 0);
    }
}