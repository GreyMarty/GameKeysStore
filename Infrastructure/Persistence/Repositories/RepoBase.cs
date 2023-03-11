using System.Linq.Expressions;
using Domain.Data;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public abstract class RepoBase<TModel> : IRepo<TModel>
    where TModel : EntityBase
{
    protected readonly IApplicationDbContext _context;
    protected readonly Func<IApplicationDbContext, DbSet<TModel>> _sourceGetter;

    public int Count => Source.Count();
    public bool IsReadOnly => false;

    protected DbSet<TModel> Source => _sourceGetter(_context);

    protected RepoBase(IApplicationDbContext context, Func<IApplicationDbContext, DbSet<TModel>> sourceGetter)
    {
        _context = context;
        _sourceGetter = sourceGetter;
    }

    public TModel Add(TModel entity)
    {
        Source.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public void Clear()
    {
        Source.RemoveRange(Source);
        _context.SaveChanges();
    }

    public bool Remove(TModel entity)
    {
        return Remove(entity.Id);
    }

    public bool Remove(int id)
    {
        var sourceEntity = Get(id);

        if (sourceEntity is null) return false;

        Source.Remove(sourceEntity);
        _context.SaveChanges();
        return true;
    }

    public IQueryable<TModel> GetAll(Action<IQueryOptions<TModel>>? configureOptions = null)
    {
        return ApplyOptions(configureOptions);
    }

    public TModel? Get(int id, Action<IQueryOptions<TModel>>? configureOptions = null)
    {
        var source = ApplyOptions(configureOptions);

        return source.FirstOrDefault(e => e.Id == id);
    }

    public TModel? Get(Expression<Func<TModel, bool>> query, Action<IQueryOptions<TModel>>? configureOptions = null)
    {
        var source = ApplyOptions(configureOptions);

        return source.FirstOrDefault(query);
    }

    public TModel Update(TModel entity)
    {
        if (!Any(e => e.Id == entity.Id)) throw new EntityDoesNotExistException();

        Source.Update(entity);
        _context.SaveChanges();

        return entity;
    }

    public bool Any(Expression<Func<TModel, bool>> predicate)
    {
        return Source.Any(predicate);
    }

    private IQueryable<TModel> ApplyOptions(Action<IQueryOptions<TModel>>? configureOptions)
    {
        var options = new QueryOptions<TModel>();
        if (configureOptions is not null) configureOptions(options);

        return options.Apply(Source);
    }
}