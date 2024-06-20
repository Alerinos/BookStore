using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStory.Presentation.Requests;
using BookStory.Presentation.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BookStory.Presentation;

public static class MiddlewareExtensions
{
    public static IEndpointRouteBuilder UseApi(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/books", async ([FromServices] IBookService bookService) => 
        {
            return new BooksResponse {
                Data = await bookService.GetAllAsync()
            };
        }).RequireAuthorization("read");


        builder.MapGet("/api/orders", async (int page, [FromServices] IOrderService orderService) => 
        {
            var orders = await orderService.GetAllAsync(page);

            return new OrdersResponse { 
                Data = orders.Item1,
                TotalPage = orders.Item2
            };

        }).RequireAuthorization("read");


        // Rozbił bym to na dwa requesty, jeden dodanie książki a potem edycja jej i dodanie szczegółów takich jak np autor
        builder.MapPost("/api/book", async ([FromBody] BooksRequest book, [FromServices] IBookService bookService) =>
        {
            var author = book
                .Authors
                .Select(x => new Author 
                { 
                    FirstName = x.FirstName, 
                    LastName = x.LastName 
                }).ToList();

            try
            {
                var add = await bookService
                    .AddAsync(book.Title, book.Price, book.Bookstand, book.Shelf, author);

                return Results.Ok(new BookResponse { Data = add });
            }
            catch (Exception e)
            {
                // TODO: Obsłużyć błędy itp.
                return Results.Problem(e.Message);
            }

        }).RequireAuthorization("read");

        return builder;
    }
}