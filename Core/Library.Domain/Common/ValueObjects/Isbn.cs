using Library.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Library.Domain.Common.ValueObjects;

/// <summary>
/// Value Object que encapsula el ISBN de un libro.
/// Es inmutable y contiene validaciones de formato ISBN-10 e ISBN-13.
/// </summary>
public sealed class Isbn : IEquatable<Isbn>
{
    // Patrón que acepta ISBN-10 e ISBN-13 (con o sin guiones)
    private static readonly Regex IsbnPattern =
        new(@"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$",
             RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    /// <summary>
    /// Constructor privado sin parámetros requerido por EF Core.
    /// </summary>
    private Isbn() { Value = string.Empty; }

    private Isbn(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea y valida una instancia de ISBN.
    /// </summary>
    /// <param name="value">Cadena con el ISBN a validar.</param>
    /// <returns>Nueva instancia de <see cref="Isbn"/>.</returns>
    /// <exception cref="BusinessRuleException">Si el ISBN es nulo, vacío o tiene formato inválido.</exception>
    public static Isbn Create(string value)
    {
        ApplyIsbnRules(value);
        return new Isbn(value.Trim());
    }

    /// <summary>
    /// Aplica las reglas de validación del ISBN.
    /// </summary>
    /// <exception cref="BusinessRuleException">Si el ISBN no cumple las reglas de negocio.</exception>
    private static void ApplyIsbnRules(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessRuleException("El ISBN no puede ser nulo o vacío.");

        if (value.Trim().Length > 20)
            throw new BusinessRuleException("El ISBN no puede superar los 20 caracteres.");
    }

    public override string ToString() => Value;

    public bool Equals(Isbn? other)
    {
        if (other is null) return false;
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => obj is Isbn other && Equals(other);

    public override int GetHashCode() => Value.ToUpperInvariant().GetHashCode();

    public static bool operator ==(Isbn? left, Isbn? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Isbn? left, Isbn? right) => !(left == right);
}
