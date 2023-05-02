using System.Linq.Expressions;

namespace Application.Data;

public interface IQueryOptions<TModel>
{
    public IQueryOptions<TModel> OrderByAsc<TKey>(Expression<Func<TModel, TKey>> keySelector);
    public IQueryOptions<TModel> OrderByAsc(string property);

    public IQueryOptions<TModel> OrderByDesc<TKey>(Expression<Func<TModel, TKey>> keySelector);
    public IQueryOptions<TModel> OrderByDesc(string property);

    public IQueryOptions<TModel> Where(Expression<Func<TModel, bool>> predicate);

    public IQueryOptions<TModel> AsNoTracking();

    public IQueryable<TModel> Apply(IQueryable<TModel> source);
}