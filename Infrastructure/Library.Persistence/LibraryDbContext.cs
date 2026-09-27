using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence;

/// <summary>
/// DbContext principal de la aplicación que expone los <see cref="DbSet{T}"/> del dominio.
/// Carga las configuraciones Fluent API desde las clases que implementan
/// <see cref="IEntityTypeConfiguration{TEntity}"/> en el mismo ensamblado.
/// Incluye datos semilla (Data Seeding) para pruebas.
/// </summary>
public sealed class LibraryDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Category> Categories => Set<Category>();

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Carga automáticamente todas las clases IEntityTypeConfiguration<T> del ensamblado
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);

        // -----------------------------------------------------------------------
        // DATA SEEDING: datos iniciales para pruebas
        // -----------------------------------------------------------------------
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Categorías
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Programación", Description = "Libros sobre desarrollo de software y programación" },
            new { Id = 2, Name = "Arquitectura de Software", Description = "Patrones, principios y diseño de sistemas" },
            new { Id = 3, Name = "Ciencias de la Computación", Description = "Fundamentos teóricos y algorítmicos" },
            new { Id = 4, Name = "Base de Datos", Description = "Diseño, modelado y gestión de bases de datos" }
        );

        // Autores
        modelBuilder.Entity<Author>().HasData(
            new { Id = 1, FirstName = "Robert C.", LastName = "Martin" },
            new { Id = 2, FirstName = "Martin", LastName = "Fowler" },
            new { Id = 3, FirstName = "Eric", LastName = "Evans" },
            new { Id = 4, FirstName = "Gang", LastName = "of Four" },
            new { Id = 5, FirstName = "Andrew S.", LastName = "Tanenbaum" }
        );

        // Libros (usando HasData con tipo anónimo para evitar dependencia del constructor)
        modelBuilder.Entity<Book>().HasData(
            new { Id = 1, Title = "Clean Code", PublicationYear = 2008, AuthorId = 1, CategoryId = 1 },
            new { Id = 2, Title = "Clean Architecture", PublicationYear = 2017, AuthorId = 1, CategoryId = 2 },
            new { Id = 3, Title = "Refactoring", PublicationYear = 1999, AuthorId = 2, CategoryId = 1 },
            new { Id = 4, Title = "Patterns of Enterprise Application Architecture", PublicationYear = 2002, AuthorId = 2, CategoryId = 2 },
            new { Id = 5, Title = "Domain-Driven Design", PublicationYear = 2003, AuthorId = 3, CategoryId = 2 },
            new { Id = 6, Title = "Design Patterns", PublicationYear = 1994, AuthorId = 4, CategoryId = 2 },
            new { Id = 7, Title = "Modern Operating Systems", PublicationYear = 2014, AuthorId = 5, CategoryId = 3 }
        );

        // ISBN por separado usando OwnsOne seed (se define en BookConfiguration)
    }
}
