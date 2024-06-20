using BookStore.Application.Dto;

namespace BookStore.Application.Services;

public interface IOrderService
{
    public Task<(IReadOnlyCollection<OrderDto>?, int)> GetAllAsync(int page);
}
