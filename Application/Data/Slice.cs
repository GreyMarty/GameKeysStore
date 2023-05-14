using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Application.Data;

public class Slice<T> : ISlice<T>
{
    private readonly IReadOnlyCollection<T> _items;

    public int Offset { get; }

    public int TotalCount { get; }

    public int Count => _items.Count;

    public Slice(IReadOnlyCollection<T> items, int offset, int totalCount)
    {
        _items = items;
        Offset = offset;
        TotalCount = totalCount;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class SliceExtentions 
{
    public static async Task<ISlice<T>> ToSliceAsync<T>(this IQueryable<T> source, int offset, int count, CancellationToken cancellationToken = default) 
    {
        var data = await source
            .Skip(offset)
            .Take(count)
            .ToArrayAsync(cancellationToken);

        var totalCount = data.Count();

        return new Slice<T>(data, offset, totalCount);
    }
}