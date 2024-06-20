using BookStore.Application.Dto;

namespace BookStory.Presentation.Responses;

internal class BooksResponse
{
    // Można tutaj zwrócić więcej danych, takich jak data, status, ilość (np. do leazy loading) itp.
    public IReadOnlyCollection<BookDto>? Data { get; set; } 
}
