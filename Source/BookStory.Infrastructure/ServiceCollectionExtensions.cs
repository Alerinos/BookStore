using BookStore.Domain.Repositories;
using BookStory.Infrastructure.Data;
using BookStory.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions;

namespace BookStory.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookStoryInfrastructure(this IServiceCollection services)
    {
        services.AddDbSqlServer<DatabaseContext>();

        services.AddTransient<IBookRepository, BookRepository>();
        services.AddTransient<IBooksRepository, BooksRepository>();
        services.AddTransient<IOrdersRepository, OrdersRepository>();

        return services;
    }
}