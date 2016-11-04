namespace Utils.Collection
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExt
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] items)
        {
            return source.Concat(items);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, params T[] items)
        {
            return items.Concat(source);
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            var buffer = new List<T>(batchSize);

            foreach (T item in source)
            {
                buffer.Add(item);

                if (buffer.Count >= batchSize)
                {
                    yield return buffer;
                    buffer = new List<T>();
                }
            }
            if (buffer.Count >= 0)
            {
                yield return buffer;
            }
        }
    }
}