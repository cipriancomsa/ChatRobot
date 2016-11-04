namespace Utils.Collection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExt
    {
        public static bool ContainsString(this List<string> list, string value, bool ignoreCase = false)
        {
            return ignoreCase
                ? list.Any(s => s.Equals(value, StringComparison.OrdinalIgnoreCase))
                : list.Contains(value);
        }
    }
}