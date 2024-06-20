using BookStore.Domain.Entities;

namespace BookStore.Application.Dto;

public class OrderLineDto(OrderLine x)
{
    public int Id { get; set; } = x.Id;
    public int BookId { get; set; } = x.BookId;
    public int Quantity { get; set; } = x.Quantity;
}
