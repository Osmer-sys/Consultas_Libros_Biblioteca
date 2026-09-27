namespace Library.Application.Utilities.Mediator;

/// <summary>
/// Contrato para el manejador de una solicitud de tipo <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">Tipo de solicitud que implementa <see cref="IRequest{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">Tipo de la respuesta producida.</typeparam>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Maneja la solicitud de forma asíncrona.
    /// </summary>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
