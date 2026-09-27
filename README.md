# 📚 Consultas_Libros_Biblioteca

Sistema de **consulta de catálogo bibliográfico** implementado con **Clean Architecture + DDD + CQRS** en .NET 8.  
Solo operaciones de lectura (Query side). Sin POST, PUT ni DELETE.

---

## 🏗️ Arquitectura

```
Consultas_Libros_Biblioteca/
├── Core/
│   ├── Library.Domain/               # Capa de Dominio (sin dependencias)
│   │   ├── Entities/
│   │   │   ├── Book.cs               # Entidad principal (aggregate root)
│   │   │   ├── Author.cs
│   │   │   └── Category.cs
│   │   ├── Common/ValueObjects/
│   │   │   └── Isbn.cs               # Value Object inmutable
│   │   └── Exceptions/
│   │       └── BusinessRuleException.cs
│   │
│   └── Library.Application/          # Capa de Aplicación (referencia Domain)
│       ├── Contracts/Repositories/
│       │   ├── IRepository.cs        # Repositorio genérico
│       │   └── IBookRepository.cs    # Contrato específico
│       ├── DTOs/
│       │   ├── BookListItemDto.cs
│       │   └── BookDetailDto.cs
│       ├── Mappers/
│       │   └── BookMapperExtensions.cs
│       ├── UseCases/Books/Queries/
│       │   ├── GetAllBooksQuery.cs + Handler
│       │   ├── GetBookByIdQuery.cs + Handler
│       │   └── GetBooksByCategoryQuery.cs + Handler
│       ├── Utilities/Mediator/
│       │   ├── IRequest.cs
│       │   ├── IRequestHandler.cs
│       │   ├── IMediator.cs
│       │   └── SimpleMediator.cs     # Implementación con Reflexión
│       └── Extensions/
│           └── ApplicationServicesExtensions.cs
│
├── Infrastructure/
│   └── Library.Persistence/          # Capa de Infraestructura (EF Core)
│       ├── Configurations/
│       │   ├── BookConfiguration.cs  # IEntityTypeConfiguration<Book>
│       │   ├── AuthorConfiguration.cs
│       │   └── CategoryConfiguration.cs
│       ├── Repositories/
│       │   ├── Repository.cs         # Implementación genérica
│       │   └── BookRepository.cs     # Implementación específica con Include
│       ├── Extensions/
│       │   └── PersistenceServicesExtensions.cs
│       └── LibraryDbContext.cs       # DbContext + Data Seeding
│
└── Presenters/
    └── Library.API/                  # ASP.NET Core Web API
        ├── Controllers/
        │   └── BooksController.cs    # 3 endpoints GET
        ├── Program.cs
        ├── appsettings.json
        └── appsettings.Development.json
```

---

## 🔌 Endpoints disponibles

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/books` | Todos los libros (Id, Título, ISBN, Año, Autor, Categoría) |
| `GET` | `/api/books/{id}` | Detalle completo de un libro por ID |
| `GET` | `/api/books/category/{categoryId}` | Libros filtrados por categoría |

Swagger UI disponible en: `https://localhost:{puerto}/` (raíz)

---

## ⚙️ Configuración de la base de datos

La cadena de conexión está en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LibraryDB;User Id=Sa;Password=2608ToDay+;TrustServerCertificate=True;"
  }
}
```

---

## 🗄️ Migraciones y Base de Datos

> **Requisito**: Tener el SDK de .NET 8 y la herramienta `dotnet-ef` instalados.

### Instalar dotnet-ef (si no está instalado)
```bash
dotnet tool install --global dotnet-ef
```

### Crear la migración inicial
```bash
dotnet ef migrations add InitialSchema \
  --project Infrastructure/Library.Persistence \
  --startup-project Presenters/Library.API \
  --output-dir Migrations
```

### Aplicar la migración (crear la BD y tablas + seed data)
```bash
dotnet ef database update \
  --project Infrastructure/Library.Persistence \
  --startup-project Presenters/Library.API
```

---

## 🔗 Configuración de Git y GitHub

### Vincular con el repositorio remoto
```bash
git remote add origin https://github.com/Osmer-sys/Consultas_Libros_Biblioteca.git
git branch -M master
git push -u origin master
```

### Commits sugeridos por convención
```bash
git add .
git commit -m "feat(domain): agregar entidades Book, Author, Category e ISBN Value Object"
git commit -m "feat(application): implementar CQRS con SimpleMediator y 3 use cases de consulta"
git commit -m "feat(persistence): configurar EF Core, LibraryDbContext y data seeding"
git commit -m "feat(api): agregar BooksController con endpoints GET de solo lectura"
git commit -m "chore(solution): actualizar solución .slnx y .gitignore"
```

---

## 📦 Paquetes NuGet utilizados

| Proyecto | Paquete |
|---------|---------|
| Library.Application | `Microsoft.Extensions.DependencyInjection` |
| Library.Persistence | `Microsoft.EntityFrameworkCore` |
| Library.Persistence | `Microsoft.EntityFrameworkCore.SqlServer` |
| Library.Persistence | `Microsoft.EntityFrameworkCore.Design` |
| Library.API | `Swashbuckle.AspNetCore` |
| Library.API | `Microsoft.AspNetCore.OpenApi` |

---

## 🌱 Datos de prueba (Seed Data)

La migración incluye datos iniciales:

**Categorías**: Programación, Arquitectura de Software, Ciencias de la Computación, Base de Datos  
**Autores**: Robert C. Martin, Martin Fowler, Eric Evans, Gang of Four, Andrew S. Tanenbaum  
**Libros**: Clean Code, Clean Architecture, Refactoring, PEAA, Domain-Driven Design, Design Patterns, Modern Operating Systems

---

## 🛠️ Cómo abrir en Visual Studio

1. Abrir Visual Studio 2025
2. `Archivo → Abrir → Proyecto/Solución`
3. Seleccionar `Consultas_Libros_Biblioteca.slnx`
4. Compilar con `Ctrl+Shift+B`
5. Establecer `Library.API` como proyecto de inicio
6. Ejecutar con `F5`
