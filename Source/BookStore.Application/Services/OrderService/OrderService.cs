using BookStore.Application.Dto;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services;

internal class OrderService(
    IOrdersRepository _ordersRepository
    ) : IOrderService
{
    public async Task<(IReadOnlyCollection<OrderDto>?, int)> GetAllAsync(int page)
    {
        const int pageSize = 50;

        int skip = (page - 1) * pageSize;

        var orders = await _ordersRepository
            .Include(x => x.OrderLines)
            .Skip(skip)
            .Take(pageSize)
            .OrderByDescending(x => x.CreatedDate)
            .GetAsync<OrderDto>();

        var count = await _ordersRepository.GetCountAsync();
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return (orders, totalPages);
    }
}