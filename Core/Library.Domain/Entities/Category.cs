using Library.Domain.Exceptions;

namespace Library.Domain.Entities;

/// <summary>
/// Entidad Category que representa una categoría o género literario en el catálogo.
/// </summary>
public sealed class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    // Colección de navegación inversa (una categoría tiene muchos libros)
    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    /// <summary>
    /// Constructor privado sin parámetros requerido por EF Core.
    /// </summary>
    private Category() { }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Category"/> con validaciones aplicadas.
    /// </summary>
    public Category(string name, string? description = null)
    {
        ApplyNameRules(name);

        Name = name.Trim();
        Description = description?.Trim();
    }

    // -------------------------------------------------------------------------
    // Reglas de validación privadas
    // -------------------------------------------------------------------------

    private static void ApplyNameRules(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleException("El nombre de la categoría no puede ser nulo o vacío.");

        if (name.Trim().Length > 100)
            throw new BusinessRuleException("El nombre de la categoría no puede superar los 100 caracteres.");
    }
}
