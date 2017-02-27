using CmdArt.Screen;
using System.Linq;

namespace CmdArt.Rendering.Strings
{
    public class Text : IRenderable
    {
        private readonly string[] _content;
        private readonly ILocation _contentLocation;

        public Text(string[] content, ILocation contentLocation)
        {
            if (content == null)
                throw new System.ArgumentNullException(nameof(content));

            if (contentLocation == null)
                throw new System.ArgumentNullException(nameof(contentLocation));

            _content = content;
            _contentLocation = contentLocation;
        }

        public void RenderTo(IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));
            // TODO: We need an ability to word-wrap, controlled by a flag. We can implement that here, or
            // expect the _content to implement that already
            string[] visibleLines = _content.Skip(_contentLocation.Top).Take(buffer.Size.Height).ToArray();
            for (int i = 0; i < visibleLines.Length; i++)
            {
                string s = visibleLines[i].Truncate(buffer.Size.Width);
                buffer.Set(0, i, s.PadRight(buffer.Size.Width, ' '));
            }
        }
    }
}
