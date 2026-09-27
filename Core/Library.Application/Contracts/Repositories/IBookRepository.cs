using Library.Domain.Entities;

namespace Library.Application.Contracts.Repositories;

/// <summary>
/// Contrato específico del repositorio de libros.
/// Extiende <see cref="IRepository{Book}"/> con métodos de consulta adicionales.
/// </summary>
public interface IBookRepository : IRepository<Book>
{
    /// <summary>
    /// Obtiene todos los libros incluyendo Autor y Categoría.
    /// </summary>
    Task<IReadOnlyList<Book>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un libro por su ID incluyendo Autor y Categoría.
    /// </summary>
    Task<Book?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los libros de una categoría específica, incluyendo Autor y Categoría.
    /// </summary>
    Task<IReadOnlyList<Book>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
}
