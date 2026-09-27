using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappers;

/// <summary>
/// Métodos de extensión para mapear la entidad <see cref="Book"/> a los DTOs de Application.
/// </summary>
public static class BookMapperExtensions
{
    /// <summary>
    /// Convierte un <see cref="Book"/> con navegación cargada en un <see cref="BookListItemDto"/>.
    /// </summary>
    public static BookListItemDto ToListItemDto(this Book book)
    {
        return new BookListItemDto(
            Id: book.Id,
            Title: book.Title,
            Isbn: book.Isbn.Value,
            PublicationYear: book.PublicationYear,
            AuthorFullName: book.Author?.FullName ?? string.Empty,
            CategoryName: book.Category?.Name ?? string.Empty
        );
    }

    /// <summary>
    /// Convierte un <see cref="Book"/> con navegación cargada en un <see cref="BookDetailDto"/>.
    /// </summary>
    public static BookDetailDto ToDetailDto(this Book book)
    {
        return new BookDetailDto(
            Id: book.Id,
            Title: book.Title,
            Isbn: book.Isbn.Value,
            PublicationYear: book.PublicationYear,
            AuthorId: book.AuthorId,
            AuthorFullName: book.Author?.FullName ?? string.Empty,
            AuthorFirstName: book.Author?.FirstName ?? string.Empty,
            AuthorLastName: book.Author?.LastName ?? string.Empty,
            CategoryId: book.CategoryId,
            CategoryName: book.Category?.Name ?? string.Empty
        );
    }
}
