using BookStore.Domain.Entities;
using Zonit.Extensions.Databases;

namespace BookStore.Domain.Repositories;

public interface IBookRepository : IDatabaseRepository<Book, int>
{
    /* Użyłem własnej biblioteki:
     * https://github.com/Zonit/Zonit.Extensions.Databases
     * Można tutaj zaimplementować dodatkową funkcjonalność
     */
}
