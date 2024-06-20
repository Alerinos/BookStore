using BookStore.Domain.Entities;

namespace BookStore.Application.Dto;

public class BookDto(Book book)
{
    public int Id { get; set; } = book.Id;
    public string Title { get; set; } = book.Title;
    public decimal Price { get; set; } = book.Price;
    public int Bookstand { get; set; } = book.Bookstand;
    public int Shelf { get; set; } = book.Shelf;
    public IReadOnlyCollection<AuthorDto> Authors { get; set; } = book.Authors.Select(x => new AuthorDto(x)).ToList();
}
