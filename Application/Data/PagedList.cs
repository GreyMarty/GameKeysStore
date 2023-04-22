using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class PagedList<T> : IPagedList<T>
{
    private readonly IReadOnlyList<T> _values;

    public PagedList(T[] data, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;

        _values = data;
    }

    public int Count => _values.Count;
    public int PageIndex { get; }
    public int PageSize { get; }

    public T this[int index] => _values[index];

    public IEnumerator<T> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class PageableCollectionExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize) 
    {
        var data = source
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArray();

        return new PagedList<T>(data, pageIndex, pageSize);
    }

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        var data = await source
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArrayAsync();

        return new PagedList<T>(data, pageIndex, pageSize);
    }
}