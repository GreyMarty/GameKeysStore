using System.Linq.Expressions;
using Domain.Data;
using Domain.Entities;

namespace Domain.Repositories;

public interface IRepo<TModel>
    where TModel : EntityBase
{
    public IQueryable<TModel> GetAll(Action<IQueryOptions<TModel>>? configureOptions = null);

    public TModel? Get(int id, Action<IQueryOptions<TModel>>? configureOptions = null);
    public TModel? Get(Expression<Func<TModel, bool>> query, Action<IQueryOptions<TModel>>? configureOptions = null);

    public TModel Add(TModel entity);

    public TModel? Update(TModel entity);

    public bool Remove(TModel entity);
    public bool Remove(int id);

    public void Clear();

    public bool Any(Expression<Func<TModel, bool>> predicate);
}