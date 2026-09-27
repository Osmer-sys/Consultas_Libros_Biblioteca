using Library.Application.Contracts.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio de libros utilizando EF Core.
/// Incluye las operaciones de consulta con carga de entidades relacionadas (Author, Category).
/// </summary>
public sealed class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(LibraryDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Book>> GetAllWithDetailsAsync(
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Category)
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Book?> GetByIdWithDetailsAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Book>> GetByCategoryAsync(
        int categoryId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Where(b => b.CategoryId == categoryId)
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);
    }
}
