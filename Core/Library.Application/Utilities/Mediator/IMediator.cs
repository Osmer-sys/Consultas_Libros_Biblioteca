namespace Library.Application.Utilities.Mediator;

/// <summary>
/// Contrato del Mediador. Despacha solicitudes al manejador correspondiente.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Envía una solicitud al manejador registrado y retorna la respuesta.
    /// </summary>
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
