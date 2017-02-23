using System.Linq;
using CmdArt.Screen;

namespace CmdArt.Rendering.Strings
{
    public class Text : IRenderable
    {
        private readonly string[] _content;
        private readonly ILocation _contentLocation;

        public Text(string[] content, ILocation contentLocation)
        {
            _content = content;
            _contentLocation = contentLocation;
        }

        public void RenderTo(IPixelBuffer buffer)
        {
            string[] visibleLines = _content.Skip(_contentLocation.Top).Take(buffer.Size.Height).ToArray();
            for (int i = 0; i < visibleLines.Length; i++)
            {
                string s = visibleLines[i].Truncate(buffer.Size.Width);
                buffer.Set(0, i, s.PadRight(buffer.Size.Width, ' '));
            }
        }
    }
}
