using System;
using System.Linq;

namespace CmdArt.Utilities
{
    public static class MathUtilities
    {
        public static T Min<T>(params T[] values)
            where T : IComparable<T>
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            return values.Min();
        }
    }
}
