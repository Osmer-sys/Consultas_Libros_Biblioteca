using Library.Application.DTOs;
using Library.Application.UseCases.Books.Queries;
using Library.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

/// <summary>
/// Controlador para la consulta del catálogo bibliográfico.
/// Expone únicamente operaciones de lectura (GET).
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los libros del catálogo (Id, Título, ISBN, Año, Autor, Categoría).
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de libros con datos básicos.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllBooksQuery();
        var result = await _mediator.SendAsync(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene el detalle completo de un libro por su ID.
    /// </summary>
    /// <param name="id">Identificador del libro.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Detalle del libro o 404 si no se encuentra.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(id);
        var result = await _mediator.SendAsync(query, cancellationToken);

        if (result is null)
            return NotFound(new { Message = $"No se encontró el libro con ID {id}." });

        return Ok(result);
    }

    /// <summary>
    /// Obtiene todos los libros pertenecientes a una categoría específica.
    /// </summary>
    /// <param name="categoryId">Identificador de la categoría.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de libros de la categoría indicada.</returns>
    [HttpGet("category/{categoryId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<BookDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCategory(int categoryId, CancellationToken cancellationToken)
    {
        var query = new GetBooksByCategoryQuery(categoryId);
        var result = await _mediator.SendAsync(query, cancellationToken);

        if (!result.Any())
            return NotFound(new { Message = $"No se encontraron libros para la categoría con ID {categoryId}." });

        return Ok(result);
    }
}
