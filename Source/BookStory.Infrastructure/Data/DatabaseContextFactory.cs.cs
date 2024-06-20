using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace BookStory.Infrastructure.Data;

/// <summary>
/// Only factory migration
/// </summary>
internal class DataBaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookStory;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

        return new DatabaseContext(optionsBuilder.Options);
    }
}