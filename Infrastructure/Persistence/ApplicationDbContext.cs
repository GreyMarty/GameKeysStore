using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleMembership> RoleMemberships { get; set; }

    public ApplicationDbContext(DbContextOptions options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasIndex(e => e.UserName)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();

        builder.Entity<Game>()
            .HasIndex(e => e.Name)
            .IsUnique();

        builder.Entity<Key>()
            .HasIndex(e => e.KeyString)
            .IsUnique();

        builder.Entity<Platform>()
            .HasIndex(e => e.Name)
            .IsUnique();

        builder.Entity<Developer>()
            .HasIndex(e => e.Name)
            .IsUnique();

        builder.Entity<Role>()
            .HasIndex(e => e.Name)
            .IsUnique();
    }
}