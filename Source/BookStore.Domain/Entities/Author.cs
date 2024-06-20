using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;

public class Author
{
    public Guid Id { get; set; }

    public string Name => $"{FirstName} {LastName}";

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 50 characters long.")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 50 characters long.")]
    public string LastName { get; set; } = string.Empty;
}