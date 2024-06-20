using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using Moq;
using System.Linq.Expressions;

namespace BookStory.Test.Services;

public class OrderServiceTests
{
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _ordersRepositoryMock = new Mock<IOrdersRepository>();

        _orderService = new OrderService(
            _ordersRepositoryMock.Object
        );
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOrdersAndTotalPages()
    {
        // Arrange
        int page = 1;
        int pageSize = 50;
        int skip = (page - 1) * pageSize;

        var orders = new List<Order>
        {
            new Order { CreatedDate = DateTime.Now, OrderLines = new List<OrderLine>() },
            new Order { CreatedDate = DateTime.Now, OrderLines = new List<OrderLine>() }
        };
        var ordersDto = orders.Select(o => new OrderDto(o)).ToList().AsReadOnly();
        int totalOrdersCount = 120;

        _ordersRepositoryMock.Setup(repo => repo.Include(It.IsAny<Expression<Func<Order, object>>>()))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.Skip(skip))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.Take(pageSize))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.OrderByDescending(It.IsAny<Expression<Func<Order, object>>>()))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.GetAsync<OrderDto>(It.IsAny<CancellationToken>()))
            .ReturnsAsync(ordersDto);
        _ordersRepositoryMock.Setup(repo => repo.GetCountAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOrdersCount);

        // Act
        var (result, totalPages) = await _orderService.GetAllAsync(page);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(3, totalPages);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyOrdersWhenNoOrdersExist()
    {
        // Arrange
        int page = 1;
        int pageSize = 50;
        int skip = (page - 1) * pageSize;

        var ordersDto = new List<OrderDto>().AsReadOnly();
        int totalOrdersCount = 0;

        _ordersRepositoryMock.Setup(repo => repo.Include(It.IsAny<Expression<Func<Order, object>>>()))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.Skip(skip))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.Take(pageSize))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.OrderByDescending(It.IsAny<Expression<Func<Order, object>>>()))
            .Returns(_ordersRepositoryMock.Object);
        _ordersRepositoryMock.Setup(repo => repo.GetAsync<OrderDto>(It.IsAny<CancellationToken>()))
            .ReturnsAsync(ordersDto);
        _ordersRepositoryMock.Setup(repo => repo.GetCountAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOrdersCount);

        // Act
        var (result, totalPages) = await _orderService.GetAllAsync(page);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.Equal(0, totalPages);
    }
}