using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Data;

public sealed class IncludableQueryOptions<TModel> : IIncludableQueryOptions<TModel>
    where TModel : class
{
    private Func<IQueryable<TModel>, IQueryable<TModel>> _applyOptions = x => x;

    public IIncludableQueryOptions<TModel> Include<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return ApplyFunc(x => x.Include(keySelector));
    }

    public IIncludableQueryOptions<TModel> OrderByAsc<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return ApplyFunc(x => x.OrderBy(keySelector));
    }

    public IIncludableQueryOptions<TModel> OrderByAsc(string property)
    {
        return ApplyFunc(x => x.OrderBy(property));
    }

    public IIncludableQueryOptions<TModel> OrderByDesc<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return ApplyFunc(x => x.OrderByDescending(keySelector));
    }

    public IIncludableQueryOptions<TModel> OrderByDesc(string property)
    {
        return ApplyFunc(x => x.OrderByDescending(property));
    }

    public IIncludableQueryOptions<TModel> Where(Expression<Func<TModel, bool>> predicate)
    {
        return ApplyFunc(x => x.Where(predicate));
    }

    public IQueryable<TModel> Apply(IQueryable<TModel> source)
    {
        return _applyOptions(source);
    }

    public IIncludableQueryOptions<TModel> AsNoTracking()
    {
        return ApplyFunc(x => x.AsNoTracking());
    }

    private IIncludableQueryOptions<TModel> ApplyFunc(Func<IQueryable<TModel>, IQueryable<TModel>> func)
    {
        var applyQuery = _applyOptions;
        _applyOptions = x => func(applyQuery(x));
        return this;
    }

    IQueryOptions<TModel> IQueryOptions<TModel>.OrderByDesc<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return OrderByDesc(keySelector);
    }

    IQueryOptions<TModel> IQueryOptions<TModel>.Where(Expression<Func<TModel, bool>> predicate)
    {
        return Where(predicate);
    }

    IQueryOptions<TModel> IQueryOptions<TModel>.OrderByAsc<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        return OrderByAsc(keySelector);
    }

    IQueryOptions<TModel> IQueryOptions<TModel>.AsNoTracking()
    {
        return AsNoTracking();
    }

    IQueryOptions<TModel> IQueryOptions<TModel>.OrderByAsc(string property)
    {
        return OrderByAsc(property);
    }

    IQueryOptions<TModel> IQueryOptions<TModel>.OrderByDesc(string property)
    {
        return OrderByDesc(property);
    }
}