using System.Collections.Generic;
using System.Linq;

namespace RichCmd.Utilities
{
    public static class StringUtilities
    {
        public static IEnumerable<string> WrapString(string str, int chunkSize)
        {
            int numChunks = str.Length / chunkSize;

            IEnumerable<string> wholeChunks = Enumerable.Range(0, numChunks)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
            foreach (string chunk in wholeChunks)
            {
                yield return chunk;
            }

            int rem = str.Length % chunkSize;
            if (rem > 0)
                yield return str.Substring(numChunks * chunkSize);
        }

        public static IEnumerable<string> WrapStringSmart(string str, int lineWidth)
        {
            // TODO: Scan the string and attempt to break lines on whitespace. 
            return null;
        }
    }
}
