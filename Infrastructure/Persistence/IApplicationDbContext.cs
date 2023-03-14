using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

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
}