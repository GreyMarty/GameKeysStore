using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Data;

public interface IQueryOptions<TModel>
    where TModel : EntityBase
{
    public IQueryOptions<TModel> Include<TKey>(Expression<Func<TModel, TKey>> keySelector);

    public IQueryOptions<TModel> OrderByAsc<TKey>(Expression<Func<TModel, TKey>> keySelector);

    public IQueryOptions<TModel> OrderByDesc<TKey>(Expression<Func<TModel, TKey>> keySelector);

    public IQueryable<TModel> Apply(IQueryable<TModel> source);
}