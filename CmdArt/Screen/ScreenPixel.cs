namespace CmdArt.Screen
{
    public struct ScreenPixel
    {
        public bool IsUpdated { get; set; }
        public byte Color { get; set; }
        public char Character { get; set; }

        public bool IsVisible => Character != '\0';

        public static ScreenPixel Transparent => new ScreenPixel
        {
            Character = '\0'
        };

        // TODO: Pack and unpack need testing.
        public int Pack()
        {
            return (int)Color | ((int)Character >> 8);
        }

        public void Unpack(int packed)
        {
            Color = (byte)(packed & 0xFF);
            Character = (char)((packed & 0x00FFFF00) << 8);
            IsUpdated = true;
        }

        public static ScreenPixel FromPackedInt(int packed)
        {
            var pixel = new ScreenPixel();
            pixel.Unpack(packed);
            return pixel;
        }
    }
}