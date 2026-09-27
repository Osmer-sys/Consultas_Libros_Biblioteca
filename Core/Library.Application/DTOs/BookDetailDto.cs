namespace Library.Application.DTOs;

/// <summary>
/// DTO de detalle completo de un libro (usado en el Query "Obtener libro por ID" y "por categoría").
/// </summary>
public sealed record BookDetailDto(
    int Id,
    string Title,
    string Isbn,
    int PublicationYear,
    int AuthorId,
    string AuthorFullName,
    string AuthorFirstName,
    string AuthorLastName,
    int CategoryId,
    string CategoryName
);
