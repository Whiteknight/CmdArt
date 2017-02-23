using System.Collections.Generic;

namespace CmdArt.Images.Sources
{
    // An IBrushSource produces a sequence of possible brushes. Different use cases require different
    // sets of brushes, and larger sets bring a performance penalty for most applications.
    public interface IBrushSource
    {
        IEnumerable<ImageBrush> GetPixels();
    }
}