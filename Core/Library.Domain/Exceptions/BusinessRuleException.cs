namespace Library.Domain.Exceptions;

/// <summary>
/// Excepción que representa la violación de una regla de negocio del dominio.
/// </summary>
public sealed class BusinessRuleException : Exception
{
    public BusinessRuleException(string message)
        : base(message)
    {
    }

    public BusinessRuleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
