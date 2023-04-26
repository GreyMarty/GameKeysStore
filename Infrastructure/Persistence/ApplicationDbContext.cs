using Application;
using Domain.Entities;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using PhysicalFile = Domain.Entities.PhysicalFile;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<SystemRequirements> SystemRequirements { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PhysicalFile> Files { get; set; }
    public DbSet<Image> Images { get; set; }

    public ApplicationDbContext(DbContextOptions options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddIndexes();
    }
}