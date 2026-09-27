using Library.Application.Contracts.Repositories;
using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Application.Utilities.Mediator;

namespace Library.Application.UseCases.Books.Queries;

/// <summary>
/// Manejador para <see cref="GetAllBooksQuery"/>.
/// Retorna la lista de todos los libros con datos básicos del autor y categoría.
/// </summary>
public sealed class GetAllBooksQueryHandler
    : IRequestHandler<GetAllBooksQuery, IReadOnlyList<BookListItemDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<BookListItemDto>> HandleAsync(
        GetAllBooksQuery request,
        CancellationToken cancellationToken = default)
    {
        var books = await _bookRepository.GetAllWithDetailsAsync(cancellationToken);

        return books
            .Select(book => book.ToListItemDto())
            .ToList()
            .AsReadOnly();
    }
}
