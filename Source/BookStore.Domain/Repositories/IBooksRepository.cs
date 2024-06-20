using BookStore.Domain.Entities;
using Zonit.Extensions.Databases;

namespace BookStore.Domain.Repositories;

public interface IBooksRepository : IDatabasesRepository<Book>
{
    /* Użyłem własnej biblioteki:
     * https://github.com/Zonit/Zonit.Extensions.Databases
     * Można tutaj zaimplementować dodatkową funkcjonalność
     */
}
