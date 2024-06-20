using System.ComponentModel.DataAnnotations;

namespace BookStory.Presentation.Requests;

internal class BooksRequest
{
    [Required, Length(3, 30)]
    public required string Title { get; set; }

    [Required]
    public required decimal Price { get; set; }

    [Required]
    public int Bookstand { get; set; }

    [Required]
    public int Shelf { get; set; }

    [Required]
    public Author[] Authors { get; set; } = [];

    public record Author([Required] string FirstName, [Required] string LastName);
}
