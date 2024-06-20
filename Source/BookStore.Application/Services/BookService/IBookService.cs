using BookStore.Application.Dto;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public interface IBookService
{
    public Task<IReadOnlyCollection<BookDto>?> GetAllAsync();
    public Task<BookDto> AddAsync(string title, decimal price, int bookstand, int shelf, ICollection<Author> authors);
}
