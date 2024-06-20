using System.ComponentModel.DataAnnotations;

namespace BookStory.Presentation.Requests;

/*
 * Tutaj można zrobić ilość wyświetlanych wyników oraz jaki zakres ma się pokazywać. Przydatne do lazy loadingu.
 * Zastosowałem prostą paginację, gdzie domyślnie pokazuje się pierwsza strona.
 */

internal class OrdersRequest
{
    [Required]
    public int Page { get; set; } = 1;
}
