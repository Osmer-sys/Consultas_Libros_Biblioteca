using Library.Domain.Common.ValueObjects;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

/// <summary>
/// Configuración Fluent API de la entidad <see cref="Book"/>.
/// Define la tabla, claves, restricciones, Value Objects y relaciones.
/// </summary>
public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.PublicationYear)
            .IsRequired();

        // Value Object ISBN usando OwnsOne
        builder.OwnsOne(b => b.Isbn, isbnBuilder =>
        {
            isbnBuilder.Property(i => i.Value)
                .HasColumnName("Isbn")
                .IsRequired()
                .HasMaxLength(20);
        });

        // Relación con Author: muchos libros -> un autor
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación con Category: muchos libros -> una categoría
        builder.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // -----------------------------------------------------------------------
        // Seed data del ISBN (OwnsOne seed)
        // -----------------------------------------------------------------------
        builder.OwnsOne(b => b.Isbn).HasData(
            new { BookId = 1, Value = "978-0132350884" },
            new { BookId = 2, Value = "978-0134494166" },
            new { BookId = 3, Value = "978-0201485677" },
            new { BookId = 4, Value = "978-0321127426" },
            new { BookId = 5, Value = "978-0321125217" },
            new { BookId = 6, Value = "978-0201633610" },
            new { BookId = 7, Value = "978-0133591620" }
        );
    }
}
