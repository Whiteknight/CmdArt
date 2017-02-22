namespace CmdArt.Rendering.Boarders
{
    public interface IBoarderChars
    {
        char UpperLeft { get; }
        char UpperRight { get; }
        char LowerLeft { get; }
        char LowerRight { get; }
        char Horizontal { get; }
        char Vertical { get; }
        char TeeLeft { get; }
        char TeeRight { get; }
        char TeeTop { get; }
        char TeeBottom { get; }
        char Cross { get; }
    }

    public abstract class BoarderChars : IBoarderChars
    {
        public char UpperLeft { get; protected set; }
        public char UpperRight { get; protected set; }
        public char LowerLeft { get; protected set; }
        public char LowerRight { get; protected set; }
        public char Horizontal { get; protected set; }
        public char Vertical { get; protected set; }
        public char TeeLeft { get; protected set; }
        public char TeeRight { get; protected set; }
        public char TeeTop { get; protected set; }
        public char TeeBottom { get; protected set; }
        public char Cross { get; protected set; }

        public static IBoarderChars Double { get
        {
            //return new AsciiCodePage437DoubleBoarderChars();
            return new UnicodeDoubleBoarderChars();
        } }
        public static IBoarderChars Single { get { return new SingleBoarderChars(); } }
        public static IBoarderChars Arithmetic { get { return new ArithmeticBoarderChars(); } }
    }

    public class CustomBoarderChars : BoarderChars
    {
        public CustomBoarderChars(char upperLeft, char upperRight, char lowerLeft, char lowerRight, char horizontal, char vertical, char titleLeft, char titleRight, char teeTop, char teeBottom, char cross)
        {
            Cross = cross;
            TeeBottom = teeBottom;
            TeeTop = teeTop;
            TeeRight = titleRight;
            TeeLeft = titleLeft;
            Vertical = vertical;
            Horizontal = horizontal;
            LowerRight = lowerRight;
            LowerLeft = lowerLeft;
            UpperRight = upperRight;
            UpperLeft = upperLeft;
        }
    }

    public class AsciiCodePage437DoubleBoarderChars : BoarderChars
    {
        public AsciiCodePage437DoubleBoarderChars()
        {
            Cross = AsciiCodePage437.DoubleBoarderCross;
            TeeBottom = AsciiCodePage437.DoubleBoarderTeeBottom;
            TeeTop = AsciiCodePage437.DoubleBoarderTeeTop;
            TeeRight = AsciiCodePage437.DoubleBoarderTeeRight;
            TeeLeft = AsciiCodePage437.DoubleBoarderTeeLeft;
            Vertical = AsciiCodePage437.DoubleBoarderVertical;
            Horizontal = AsciiCodePage437.DoubleBoarderHorizontal;
            LowerRight = AsciiCodePage437.DoubleBoarderLowerRight;
            LowerLeft = AsciiCodePage437.DoubleBoarderLowerLeft;
            UpperRight = AsciiCodePage437.DoubleBoarderUpperRight;
            UpperLeft = AsciiCodePage437.DoubleBoarderUpperLeft;
        }
    }

    public class UnicodeDoubleBoarderChars : BoarderChars
    {
        public UnicodeDoubleBoarderChars()
        {
            Cross = Unicode.DoubleBoarderCross;
            TeeBottom = Unicode.DoubleBoarderTeeBottom;
            TeeTop = Unicode.DoubleBoarderTeeTop;
            TeeRight = Unicode.DoubleBoarderTeeRight;
            TeeLeft = Unicode.DoubleBoarderTeeLeft;
            Vertical = Unicode.DoubleBoarderVertical;
            Horizontal = Unicode.DoubleBoarderHorizontal;
            LowerRight = Unicode.DoubleBoarderLowerRight;
            LowerLeft = Unicode.DoubleBoarderLowerLeft;
            UpperRight = Unicode.DoubleBoarderUpperRight;
            UpperLeft = Unicode.DoubleBoarderUpperLeft;
        }
    }

    public class SingleBoarderChars : BoarderChars
    {
        public SingleBoarderChars()
        {
            Cross = AsciiCodePage437.SingleBoarderCross;
            TeeBottom = AsciiCodePage437.SingleBoarderTeeBottom;
            TeeTop = AsciiCodePage437.SingleBoarderTeeTop;
            TeeRight = AsciiCodePage437.SingleBoarderTeeRight;
            TeeLeft = AsciiCodePage437.SingleBoarderTeeLeft;
            Vertical = AsciiCodePage437.SingleBoarderVertical;
            Horizontal = AsciiCodePage437.SingleBoarderHorizontal;
            LowerRight = AsciiCodePage437.SingleBoarderLowerRight;
            LowerLeft = AsciiCodePage437.SingleBoarderLowerLeft;
            UpperRight = AsciiCodePage437.SingleBoarderUpperRight;
            UpperLeft = AsciiCodePage437.SingleBoarderUpperLeft;
        }
    }

    public class ArithmeticBoarderChars : BoarderChars
    {
        public ArithmeticBoarderChars()
        {
            Cross = '+';
            TeeBottom = '+';
            TeeTop = '+';
            TeeRight = '+';
            TeeLeft = '+';
            Vertical = '|';
            Horizontal = '-';
            LowerRight = '+';
            LowerLeft = '+';
            UpperRight = '+';
            UpperLeft = '+';
        }
    }
}