namespace CmdArt.Screen
{
    public struct ScreenPixel
    {
        public bool IsUpdated { get; set; }
        public byte Color { get; set; }
        public char Character { get; set; }

        public bool IsVisible
        {
            get { return Character != '\0'; }
        }
    }
}