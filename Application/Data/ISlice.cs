namespace Application.Data;

public interface ISlice<T> : IReadOnlyCollection<T>
{
    public int Offset { get; }
    public int TotalCount { get; }
}
