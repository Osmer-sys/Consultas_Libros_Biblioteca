using Library.Domain.Entities;

namespace Library.Application.Contracts.Repositories;

/// <summary>
/// Interfaz genérica de repositorio base.
/// Solo operaciones de lectura (esta versión es QUERY-ONLY).
/// </summary>
/// <typeparam name="TEntity">Tipo de la entidad del dominio.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>Obtiene todos los registros de la entidad.</summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Obtiene una entidad por su identificador primario.</summary>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
