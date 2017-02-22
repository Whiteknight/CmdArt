using System;
using System.Globalization;
using RichCmd.Colors;

namespace RichCmd
{
    public struct Palette : IEquatable<Palette>
    {
        private readonly byte _byteValue;
        private readonly ConsoleColor _foreground;
        private readonly ConsoleColor _background;
        // TODO: We should maintain a global source of randomness for all uses.
        private static readonly Random _random;

        static Palette()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public Palette(byte byteValue)
        {
            _byteValue = byteValue;
            _foreground = ConsoleColorUtilities.GetForeground(byteValue);
            _background = ConsoleColorUtilities.GetBackground(byteValue);
        }

        public Palette(ConsoleColor foreground, ConsoleColor background)
        {
            _background = background;
            _foreground = foreground;
            _byteValue = ConsoleColorUtilities.ColorsToByte(foreground, background);
        }

        public override string ToString()
        {
            return Foreground + " on " + Background;
        }

        public string ToString(string fmt)
        {
            if (fmt == "N")
                return ToString();
            if (fmt == "C")
                return Foreground + "," + Background;
            if (fmt == "B")
                return ByteValue.ToString("X");
            throw new Exception("Unknown ToString format " + fmt);
        }

        public byte ByteValue { get { return _byteValue; } }

        public ConsoleColor Foreground { get { return _foreground; } }
        public ConsoleColor Background { get { return _background; } }

        public static Palette Current
        {
            get { return new Palette(Console.ForegroundColor, Console.BackgroundColor); }
        }

        public static Palette Default
        {
            get { return new Palette(ConsoleColor.Gray, ConsoleColor.Black); }
        }

        public static Palette GetRandom()
        {
            return new Palette((byte)_random.Next(byte.MaxValue + 1));
        }

        public Palette Swap()
        {
            return new Palette(Background, Foreground);
        }

        public Palette Invert()
        {
            return new Palette(Foreground.Invert(), Background.Invert());
        }

        public void Set()
        {
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }

        public void With(Action act)
        {
            Palette current = Current;
            Set();
            try
            {
                act();
            }
            finally
            {
                current.Set();
            }
        }

        public bool Equals(Palette other)
        {
            return other.ByteValue == ByteValue;
        }

        public static Palette Parse(string s)
        {
            Palette palette;
            bool ok = TryParse(s, out palette);
            if (!ok)
                throw new Exception("Value not in a correct format");
            return palette;
        }

        public static bool TryParse(string s, out Palette palette)
        {
            string[] parts;
            if (s.Length == 2)
            {
                byte byteValue = Byte.Parse(s, NumberStyles.HexNumber);
                palette = new Palette(byteValue);
                return true;
            }
            if (s.Length == 4 && s.StartsWith("0x"))
            {
                byte byteValue = Byte.Parse(s.Substring(2), NumberStyles.HexNumber);
                palette = new Palette(byteValue);
                return true;
            }
            if (s.Contains(" on "))
                parts = s.Split(new string[] { " on " }, StringSplitOptions.None);
            else if (s.Contains(","))
                parts = s.Split(new string[] { "," }, StringSplitOptions.None);
            else
            {
                palette = new Palette();
                return false;
            }

            try
            {
                var foreground = (ConsoleColor)Enum.Parse(typeof (ConsoleColor), parts[0]);
                var background = (ConsoleColor)Enum.Parse(typeof (ConsoleColor), parts[1]);
                palette = new Palette(foreground, background);
                return true;
            }
            catch
            {
                palette = new Palette();
                return false;
            }
        }
    }
}