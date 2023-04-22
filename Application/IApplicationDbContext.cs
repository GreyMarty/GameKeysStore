using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Developer> Developers { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Key> Keys { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Purchase> Purchases { get; set; }

    public DbSet<SystemRequirements> SystemRequirements { get; set; }

    public int SaveChanges();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}