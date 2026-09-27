using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

/// <summary>
/// Configuración Fluent API de la entidad <see cref="Author"/>.
/// </summary>
public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(100);

        // Índice compuesto para evitar duplicados de nombre completo
        builder.HasIndex(a => new { a.FirstName, a.LastName })
            .HasDatabaseName("IX_Authors_FullName");
    }
}
