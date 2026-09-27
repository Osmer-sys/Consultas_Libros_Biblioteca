using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.Utilities.Mediator;

/// <summary>
/// Implementación simple del patrón Mediador usando reflexión para resolver
/// los <see cref="IRequestHandler{TRequest,TResponse}"/> desde el contenedor de DI.
/// </summary>
public sealed class SimpleMediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Construye el tipo genérico del handler: IRequestHandler<TRequest, TResponse>
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        // Resuelve el handler desde el contenedor DI
        var handler = _serviceProvider.GetRequiredService(handlerType);

        // Invoca HandleAsync mediante reflexión
        var method = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync))
            ?? throw new InvalidOperationException(
                $"No se encontró el método HandleAsync en el handler para '{requestType.Name}'.");

        var task = (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken })!;
        return await task.ConfigureAwait(false);
    }
}
