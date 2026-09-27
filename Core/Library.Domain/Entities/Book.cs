using Library.Domain.Common.ValueObjects;
using Library.Domain.Exceptions;

namespace Library.Domain.Entities;

/// <summary>
/// Entidad Book que representa un libro en el catálogo bibliográfico.
/// Agrega las relaciones con <see cref="Author"/> y <see cref="Category"/>.
/// </summary>
public sealed class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Isbn Isbn { get; private set; } = null!;
    public int PublicationYear { get; private set; }

    // Claves foráneas
    public int AuthorId { get; private set; }
    public int CategoryId { get; private set; }

    // Propiedades de navegación
    public Author Author { get; private set; } = null!;
    public Category Category { get; private set; } = null!;

    /// <summary>
    /// Constructor privado sin parámetros requerido por EF Core.
    /// </summary>
    private Book() { }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Book"/> con todas las validaciones de negocio aplicadas.
    /// </summary>
    /// <param name="title">Título del libro.</param>
    /// <param name="isbn">ISBN del libro.</param>
    /// <param name="publicationYear">Año de publicación.</param>
    /// <param name="authorId">Identificador del autor.</param>
    /// <param name="categoryId">Identificador de la categoría.</param>
    public Book(string title, Isbn isbn, int publicationYear, int authorId, int categoryId)
    {
        ApplyTitleRules(title);
        ApplyPublicationYearRules(publicationYear);
        ApplyRelationRules(authorId, categoryId);

        Title = title.Trim();
        Isbn = isbn ?? throw new BusinessRuleException("El ISBN no puede ser nulo.");
        PublicationYear = publicationYear;
        AuthorId = authorId;
        CategoryId = categoryId;
    }

    // -------------------------------------------------------------------------
    // Reglas de validación privadas
    // -------------------------------------------------------------------------

    private static void ApplyTitleRules(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new BusinessRuleException("El título del libro no puede ser nulo o vacío.");

        if (title.Trim().Length > 200)
            throw new BusinessRuleException("El título del libro no puede superar los 200 caracteres.");
    }

    private static void ApplyPublicationYearRules(int year)
    {
        if (year < 1000 || year > DateTime.UtcNow.Year + 1)
            throw new BusinessRuleException($"El año de publicación '{year}' no es válido.");
    }

    private static void ApplyRelationRules(int authorId, int categoryId)
    {
        if (authorId <= 0)
            throw new BusinessRuleException("El identificador del autor debe ser mayor que cero.");

        if (categoryId <= 0)
            throw new BusinessRuleException("El identificador de la categoría debe ser mayor que cero.");
    }
}
