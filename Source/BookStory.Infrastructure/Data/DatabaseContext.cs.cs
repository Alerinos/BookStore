using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStory.Infrastructure.Data;

/*
 * dotnet ef migrations add BookStory_v1 
 */
internal class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var name = modelBuilder.Entity(entity.Name).Metadata.ClrType.Name;
            modelBuilder.Entity(entity.Name).ToTable($"BookStore.{name}");
        };

        // Można też atrybutem w domenie ale trzeba zaciągać zależności EF
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .Property(x => x.Price)
            .HasColumnType("decimal(19, 4)");

        modelBuilder.Entity<OrderLine>()
            .Property(x => x.Price)
            .HasColumnType("decimal(19, 4)");

        base.OnModelCreating(modelBuilder);
    }
}
