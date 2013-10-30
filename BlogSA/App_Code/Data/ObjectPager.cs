using System.Collections.Generic;

/// <summary>
/// Summary description for ObjectPager
/// </summary>
public class ObjectPager
{
    public int PageCount<T>(IList<T> source, int pageSize)
    {
        return (source.Count / pageSize) + (source.Count % pageSize > 0 ? 1 : 0);
    }

    public IEnumerable<T> GetPage<T>(IList<T> source, int startIndex, int length)
    {
        for (int i = startIndex; i < startIndex + length && i < source.Count; i++)
        {
            yield return source[i];
        }
    }
}