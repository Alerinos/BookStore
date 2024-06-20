using BookStore.Application.Dto;

namespace BookStory.Presentation.Responses;

internal class OrdersResponse
{
    // Można tutaj zwrócić więcej danych, takich jak data, status, ilość (np. do leazy loading) itp.
    public IReadOnlyCollection<OrderDto>? Data { get; set; }
    public int TotalPage { get; set; }
}
