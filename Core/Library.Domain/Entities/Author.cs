using Library.Domain.Exceptions;

namespace Library.Domain.Entities;

/// <summary>
/// Entidad Author que representa un autor en el catálogo bibliográfico.
/// </summary>
public sealed class Author
{
    public int Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    // Colección de navegación inversa (un autor tiene muchos libros)
    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    /// <summary>
    /// Constructor privado sin parámetros requerido por EF Core.
    /// </summary>
    private Author() { }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Author"/> con validaciones aplicadas.
    /// </summary>
    public Author(string firstName, string lastName)
    {
        ApplyFirstNameRules(firstName);
        ApplyLastNameRules(lastName);

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    /// <summary>
    /// Nombre completo del autor (calculado).
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    // -------------------------------------------------------------------------
    // Reglas de validación privadas
    // -------------------------------------------------------------------------

    private static void ApplyFirstNameRules(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BusinessRuleException("El nombre del autor no puede ser nulo o vacío.");

        if (firstName.Trim().Length > 100)
            throw new BusinessRuleException("El nombre del autor no puede superar los 100 caracteres.");
    }

    private static void ApplyLastNameRules(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new BusinessRuleException("El apellido del autor no puede ser nulo o vacío.");

        if (lastName.Trim().Length > 100)
            throw new BusinessRuleException("El apellido del autor no puede superar los 100 caracteres.");
    }
}
