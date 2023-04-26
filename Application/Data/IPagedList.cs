namespace Application.Data;

public interface IPagedList<out T> : IReadOnlyCollection<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
}