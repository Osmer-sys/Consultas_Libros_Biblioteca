using Library.Application.Contracts.Repositories;
using Library.Application.UseCases.Books.Queries;
using Library.Application.Utilities.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.Extensions;

/// <summary>
/// Clase de extensión para registrar los servicios de la capa Application en el contenedor DI.
/// </summary>
public static class ApplicationServicesExtensions
{
    /// <summary>
    /// Registra el <see cref="IMediator"/> (<see cref="SimpleMediator"/>) y todos los
    /// <see cref="IRequestHandler{TRequest,TResponse}"/> de la capa Application.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Mediador
        services.AddScoped<IMediator, SimpleMediator>();

        // Handlers de queries de libros
        services.AddScoped<
            IRequestHandler<GetAllBooksQuery, IReadOnlyList<Library.Application.DTOs.BookListItemDto>>,
            GetAllBooksQueryHandler>();

        services.AddScoped<
            IRequestHandler<GetBookByIdQuery, Library.Application.DTOs.BookDetailDto?>,
            GetBookByIdQueryHandler>();

        services.AddScoped<
            IRequestHandler<GetBooksByCategoryQuery, IReadOnlyList<Library.Application.DTOs.BookDetailDto>>,
            GetBooksByCategoryQueryHandler>();

        return services;
    }
}
