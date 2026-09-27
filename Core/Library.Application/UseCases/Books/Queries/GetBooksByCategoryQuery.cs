using Library.Application.DTOs;
using Library.Application.Utilities.Mediator;

namespace Library.Application.UseCases.Books.Queries;

/// <summary>
/// Query para obtener todos los libros filtrados por categoría.
/// </summary>
public sealed record GetBooksByCategoryQuery(int CategoryId) : IRequest<IReadOnlyList<BookDetailDto>>;
