namespace BookStore.Domain.Entities;

public class OrderLine
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
