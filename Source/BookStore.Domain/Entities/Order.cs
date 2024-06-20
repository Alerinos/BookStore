namespace BookStore.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; } = [];
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}