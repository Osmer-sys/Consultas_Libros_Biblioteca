using Library.Application.Contracts.Repositories;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistence.Extensions;

/// <summary>
/// Clase de extensión para registrar los servicios de la capa Persistence en el contenedor DI.
/// </summary>
public static class PersistenceServicesExtensions
{
    /// <summary>
    /// Registra el <see cref="LibraryDbContext"/> con SQL Server y los repositorios.
    /// La cadena de conexión se obtiene desde la clave <c>DefaultConnection</c> en la configuración.
    /// </summary>
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'DefaultConnection' no está configurada en appsettings.json.");

        services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                }));

        // Repositorios
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}
