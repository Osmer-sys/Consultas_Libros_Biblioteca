using Library.Application.Contracts.Repositories;
using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Application.Utilities.Mediator;

namespace Library.Application.UseCases.Books.Queries;

/// <summary>
/// Manejador para <see cref="GetBookByIdQuery"/>.
/// Retorna el detalle completo de un libro o <c>null</c> si no se encontró.
/// </summary>
public sealed class GetBookByIdQueryHandler
    : IRequestHandler<GetBookByIdQuery, BookDetailDto?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <inheritdoc />
    public async Task<BookDetailDto?> HandleAsync(
        GetBookByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetByIdWithDetailsAsync(request.BookId, cancellationToken);

        return book?.ToDetailDto();
    }
}
