using System;
using System.Linq;
using CmdArt.Screen;

namespace CmdArt.Rendering.Strings
{
    public class ForwardScrollingText : IRenderable
    {
        private readonly ISize _size;
        private readonly CircularTextBuffer _textBuffer;

        public ForwardScrollingText(ISize size)
        {
            _size = size;
            _textBuffer = new CircularTextBuffer((int)size.Height);
        }

        public void RenderTo(IPixelBuffer buffer)
        {
            buffer.Clear();
            var lines = _textBuffer.ToList();
            for (int i = 0; i < buffer.Size.Height && i < lines.Count; i++)
                buffer.Set(0, (uint) i, buffer.DefaultPalette, lines[i]);
        }

        public void AddText(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                _textBuffer.AddLine("");
                return;
            }
            var lines = s.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.Length <= _size.Width)
                {
                    _textBuffer.AddLine(line);
                    continue;
                }
                var x = line;
                while (x.Length > _size.Width)
                {
                    var l = x.Substring(0, (int) _size.Width);
                    _textBuffer.AddLine(l);
                    x = x.Substring((int) _size.Width);
                }
                if (!string.IsNullOrEmpty(x))
                    _textBuffer.AddLine(x);
            }
        }
    }
}