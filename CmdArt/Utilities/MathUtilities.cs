using System;
using System.Linq;

namespace RichCmd.Utilities
{
    public static class MathUtilities
    {
        public static T Min<T>(params T[] values)
            where T : IComparable<T>
        {
            return values.Min();
        }
    }
}
