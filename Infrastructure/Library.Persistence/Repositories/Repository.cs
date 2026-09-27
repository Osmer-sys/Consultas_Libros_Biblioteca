using Library.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

/// <summary>
/// Implementación genérica del repositorio base usando Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">Tipo de entidad del dominio.</typeparam>
public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly LibraryDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(LibraryDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    /// <inheritdoc />
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(new object[] { id }, cancellationToken);
    }
}
