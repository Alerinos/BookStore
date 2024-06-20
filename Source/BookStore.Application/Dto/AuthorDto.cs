using BookStore.Domain.Entities;

namespace BookStore.Application.Dto;

public class AuthorDto(Author x)
{
    public string FirstName { get; set; } = x.FirstName;
    public string LastName { get; set; } = x.LastName;
}
