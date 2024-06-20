using BookStore.Domain.Entities;

namespace BookStore.Application.Dto;

public class OrderDto(Order x)
{
    public Guid Id { get; set; } = x.Id;
    public DateTime CreatedDate { get; set; } = x.CreatedDate;
    public IReadOnlyCollection<OrderLineDto> OrderLines { get; set; } = x.OrderLines.Select(x => new OrderLineDto(x)).ToList();
}