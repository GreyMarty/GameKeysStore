using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class PagedList<T> : IPagedList<T>
{
    private readonly IReadOnlyList<T> _values;

    public int Count => _values.Count;
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public T this[int index] => _values[index];

    public PagedList(T[] data, int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;

        _values = data;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class PagedListExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize) 
    {
        var data = source
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArray();

        var totalCount = source.Count();

        return new PagedList<T>(data, pageIndex, pageSize, totalCount);
    }

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        var data = await source
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArrayAsync();

        var totalCount = await source.CountAsync();

        return new PagedList<T>(data, pageIndex, pageSize, totalCount);
    }
}