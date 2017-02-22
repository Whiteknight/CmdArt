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

        public int Left { get; private set; }
        public int Top { get; private set; }

        public static ILocation Origin => new Location(0, 0);
    }
}