using System.Linq.Expressions;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data;

public sealed class QueryOptions<TModel> : IQueryOptions<TModel>
    where TModel : EntityBase
{
    private Func<IQueryable<TModel>, IQueryable<TModel>> _applyQuery = x => x;

    public IQueryOptions<TModel> Include<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return ApplyFunc(x => x.Include(keySelector));
    }

    public IQueryOptions<TModel> OrderByAsc<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return ApplyFunc(x => x.OrderBy(keySelector));
    }

    public IQueryOptions<TModel> OrderByDesc<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return ApplyFunc(x => x.OrderByDescending(keySelector));
    }

    public IQueryable<TModel> Apply(IQueryable<TModel> source)
    {
        return _applyQuery(source);
    }

    private IQueryOptions<TModel> ApplyFunc(Func<IQueryable<TModel>, IQueryable<TModel>> func)
    {
        var applyQuery = _applyQuery;
        _applyQuery = x => func(applyQuery(x));
        return this;
    }
}