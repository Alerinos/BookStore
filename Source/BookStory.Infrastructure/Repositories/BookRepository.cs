using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStory.Infrastructure.Data;
using Zonit.Extensions.Databases.SqlServer.Repositories;

namespace BookStory.Infrastructure.Repositories;

internal class BookRepository(DatabaseContext _context) : DatabaseRepository<Book, int>(_context), IBookRepository
{
}
