using System.Collections;

namespace NewBlazor.Data
{
    public static class IEnumerableExtensions
    {
        public static bool Any(this IEnumerable enumerable)
        => enumerable.GetEnumerator().MoveNext();
    }
}
