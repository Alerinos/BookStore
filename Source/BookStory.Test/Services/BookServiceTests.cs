using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Xunit;

namespace BookStory.Test.Services;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IBooksRepository> _booksRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _booksRepositoryMock = new Mock<IBooksRepository>();

        _bookService = new BookService(
            _bookRepositoryMock.Object,
            _booksRepositoryMock.Object
        );
    }

    [Fact]
    public async Task GetAllAsync_ReturnsBooks()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Price = 10.5M, Bookstand = 1, Shelf = 1, Authors = new List<Author>() },
            new Book { Id = 2, Title = "Book 2", Price = 15.0M, Bookstand = 1, Shelf = 1, Authors = new List<Author>() }
        };
        var booksDto = books.Select(b => new BookDto(b)).ToList().AsReadOnly();

        _booksRepositoryMock.Setup(repo => repo.Include(It.IsAny<Expression<Func<Book, object>>>()))
            .Returns(_booksRepositoryMock.Object);
        _booksRepositoryMock.Setup(repo => repo.GetAsync<BookDto>(It.IsAny<CancellationToken>()))
            .ReturnsAsync(booksDto);

        // Act
        var result = await _bookService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task AddAsync_AddsBook()
    {
        // Arrange
        var authors = new List<Author> { new Author { FirstName = "First", LastName = "Last" } };
        var book = new Book
        {
            Title = "New Book",
            Price = 20.0M,
            Bookstand = 1,
            Shelf = 1,
            Authors = authors
        };
        var bookDto = new BookDto(book);

        _bookRepositoryMock.Setup(repo => repo.AddAsync<BookDto>(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookDto);

        // Act
        var result = await _bookService.AddAsync(book.Title, book.Price, book.Bookstand, book.Shelf, book.Authors);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Price, result.Price);
        Assert.Equal(book.Bookstand, result.Bookstand);
        Assert.Equal(book.Shelf, result.Shelf);
        Assert.Equal(book.Authors.Count, result.Authors.Count);
    }

    [Fact]
    public void Book_Title_Length_Validation()
    {
        // Arrange
        var book = new Book
        {
            Title = "AB",
            Price = 10.0M,
            Bookstand = 1,
            Shelf = 1,
            Authors = new List<Author> { new Author { FirstName = "First", LastName = "Last" } }
        };

        // Act
        var isValid = ValidationHelper.TryValidateObject(book, out var validationResults);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(Book.Title)) && v.ErrorMessage.Contains("Title must be between 3 and 30 characters long."));
    }

    [Fact]
    public void Book_Title_MaxLength_Validation()
    {
        // Arrange
        var book = new Book
        {
            Title = new string('A', 31),
            Price = 10.0M,
            Bookstand = 1,
            Shelf = 1,
            Authors = new List<Author> { new Author { FirstName = "First", LastName = "Last" } }
        };

        // Act
        var isValid = ValidationHelper.TryValidateObject(book, out var validationResults);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(Book.Title)) && v.ErrorMessage.Contains("Title must be between 3 and 30 characters long."));
    }

    [Fact]
    public void Book_Title_Valid_Length()
    {
        // Arrange
        var book = new Book
        {
            Title = "Valid Title",
            Price = 10.0M,
            Bookstand = 1,
            Shelf = 1,
            Authors = new List<Author> { new Author { FirstName = "First", LastName = "Last" } }
        };

        // Act
        var isValid = ValidationHelper.TryValidateObject(book, out var validationResults);

        // Assert
        Assert.True(isValid);
        Assert.DoesNotContain(validationResults, v => v.MemberNames.Contains(nameof(Book.Title)));
    }
}

public static class ValidationHelper
{
    public static bool TryValidateObject(object obj, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(obj, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
    }
}