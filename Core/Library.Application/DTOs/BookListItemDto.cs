namespace Library.Application.DTOs;

/// <summary>
/// DTO para listar libros con datos básicos (usado en el Query "Obtener todos los libros").
/// </summary>
public sealed record BookListItemDto(
    int Id,
    string Title,
    string Isbn,
    int PublicationYear,
    string AuthorFullName,
    string CategoryName
);
