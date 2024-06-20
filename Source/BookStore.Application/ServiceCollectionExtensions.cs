using BookStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookStoryApplication(this IServiceCollection services)
    {
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IOrderService, OrderService>();

        return services;
    }
}