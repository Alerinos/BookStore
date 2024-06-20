using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;

public class Book
{
    public int Id { get; set; } // Dałbym tutaj Guid

    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 30 characters long.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Bookstand must be a positive integer.")]
    public int Bookstand { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Shelf must be a positive integer.")]
    public int Shelf { get; set; }

    public ICollection<Author> Authors { get; set; } = [];
}