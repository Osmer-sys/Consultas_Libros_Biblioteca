namespace Library.Application.Utilities.Mediator;

/// <summary>
/// Marcador genérico para una solicitud que produce una respuesta de tipo <typeparamref name="TResponse"/>.
/// Implementado por todos los Queries y Commands.
/// </summary>
/// <typeparam name="TResponse">Tipo de la respuesta esperada.</typeparam>
public interface IRequest<TResponse>
{
}
