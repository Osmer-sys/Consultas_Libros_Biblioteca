using Library.Application.Contracts.Repositories;
using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Application.Utilities.Mediator;

namespace Library.Application.UseCases.Books.Queries;

/// <summary>
/// Manejador para <see cref="GetBooksByCategoryQuery"/>.
/// Retorna la lista de libros filtrados por categoría.
/// </summary>
public sealed class GetBooksByCategoryQueryHandler
    : IRequestHandler<GetBooksByCategoryQuery, IReadOnlyList<BookDetailDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksByCategoryQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<BookDetailDto>> HandleAsync(
        GetBooksByCategoryQuery request,
        CancellationToken cancellationToken = default)
    {
        var books = await _bookRepository.GetByCategoryAsync(request.CategoryId, cancellationToken);

        return books
            .Select(book => book.ToDetailDto())
            .ToList()
            .AsReadOnly();
    }
}
