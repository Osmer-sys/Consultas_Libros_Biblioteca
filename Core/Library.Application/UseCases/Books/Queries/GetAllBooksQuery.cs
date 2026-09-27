using Library.Application.DTOs;
using Library.Application.Utilities.Mediator;

namespace Library.Application.UseCases.Books.Queries;

/// <summary>
/// Query para obtener todos los libros del catálogo con datos básicos.
/// </summary>
public sealed record GetAllBooksQuery() : IRequest<IReadOnlyList<BookListItemDto>>;
