using System.Linq.Expressions;

namespace Application.Data;

public interface IIncludableQueryOptions<TModel> : IQueryOptions<TModel>
{
    public IIncludableQueryOptions<TModel> Include<TKey>(Expression<Func<TModel, TKey>> keySelector);

    public new IIncludableQueryOptions<TModel> OrderByAsc<TKey>(Expression<Func<TModel, TKey>> keySelector);
    public new IIncludableQueryOptions<TModel> OrderByAsc(string property);

    public new IIncludableQueryOptions<TModel> OrderByDesc<TKey>(Expression<Func<TModel, TKey>> keySelector);
    public new IIncludableQueryOptions<TModel> OrderByDesc(string property);

    public new IIncludableQueryOptions<TModel> Where(Expression<Func<TModel, bool>> predicate);

    public new IIncludableQueryOptions<TModel> AsNoTracking();
}