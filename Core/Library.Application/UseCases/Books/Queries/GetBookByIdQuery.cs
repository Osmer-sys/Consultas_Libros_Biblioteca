using Library.Application.DTOs;
using Library.Application.Utilities.Mediator;

namespace Library.Application.UseCases.Books.Queries;

/// <summary>
/// Query para obtener el detalle completo de un libro por su ID.
/// </summary>
public sealed record GetBookByIdQuery(int BookId) : IRequest<BookDetailDto?>;
