using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Zonit.Extensions.Databases.SqlServer.Repositories;

namespace BookStory.Infrastructure.Repositories;

internal class OrdersRepository(IDbContextFactory<DatabaseContext> _context) : DatabasesRepository<Order, DatabaseContext>(_context), IOrdersRepository
{
}
