using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configuration;

public static class Indexes
{
    public static void AddIndexes(this ModelBuilder builder)
    {
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
    }
}