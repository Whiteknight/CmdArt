using System;
using System.Collections.Generic;

namespace RichCmd.Rendering.Strings
{
    public static class StringExtensions
    {
        public static string Truncate(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            if (s.Length <= maxLength)
                return s;
            return s.Substring(0, maxLength);
        }

        public static string SubstringSafe(this string s, int start, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            if (start >= s.Length)
                return string.Empty;

            int length = Math.Min(maxLength, s.Length - start);
            if (length == s.Length)
                return s;
            return s.Substring(start, length);
        }

        public static IEnumerable<int> AllIndexesOf(this string source, string find)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(find))
                yield break;
            int i = 0;
            while (i >= 0)
            {
                i = source.IndexOf(find, i, System.StringComparison.Ordinal);
                if (i < 0)
                    break;
                if (i >= 0)
                    yield return i;
                i++;
            }
        }
    }
}
