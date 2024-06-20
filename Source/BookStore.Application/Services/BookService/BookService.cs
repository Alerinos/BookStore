using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services;

public class BookService(
        IBookRepository _bookRepository,
        IBooksRepository _booksRepository
    ) : IBookService
{
    public async Task<IReadOnlyCollection<BookDto>?> GetAllAsync()
    {
        return await _booksRepository.Include(x => x.Authors).GetAsync<BookDto>();
    }

    public async Task<BookDto> AddAsync(string title, decimal price, int bookstand, int shelf, ICollection<Author> authors)
    {
        var model = new Book
        {
            Title = title,
            Price = price,
            Bookstand = bookstand,
            Shelf = shelf,
            Authors = authors
        };

        return await _bookRepository.AddAsync<BookDto>(model); 
    }
}
