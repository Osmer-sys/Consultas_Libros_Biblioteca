# Consultas_Libros_Biblioteca
Aplicación de catálogo para la consulta centralizada y organizada de libros, autores y categorías literarias. El sistema está diseñado exclusivamente para la recuperación de información (operaciones de lectura/Queries).

## 🛠️ Tecnologías y Arquitectura
- **Plataforma:** .NET / C#
- **Arquitectura:** Clean Architecture (CA) y Domain-Driven Design (DDD).
- **Patrón de Consultas:** CQRS (únicamente capas y handlers de tipo Query).
- **Persistencia:** Entity Framework Core y SQL Server.

## 📋 Casos de Uso (Queries)
1. **Consultar todos los libros:** Obtiene el listado completo (Id, Título, ISBN, Año de publicación, Autor y Categoría).
2. **Consultar libro por ID:** Detalle completo de un libro específico incluyendo su autor y categoría.
3. **Consultar libros por categoría:** Filtrado de libros pertenecientes a una categoría dada.
