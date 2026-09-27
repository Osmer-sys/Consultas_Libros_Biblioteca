using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Robert C.", "Martin" },
                    { 2, "Martin", "Fowler" },
                    { 3, "Eric", "Evans" },
                    { 4, "Gang", "of Four" },
                    { 5, "Andrew S.", "Tanenbaum" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Libros sobre desarrollo de software y programación", "Programación" },
                    { 2, "Patrones, principios y diseño de sistemas", "Arquitectura de Software" },
                    { 3, "Fundamentos teóricos y algorítmicos", "Ciencias de la Computación" },
                    { 4, "Diseño, modelado y gestión de bases de datos", "Base de Datos" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Isbn", "AuthorId", "CategoryId", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 1, "978-0132350884", 1, 1, 2008, "Clean Code" },
                    { 2, "978-0134494166", 1, 2, 2017, "Clean Architecture" },
                    { 3, "978-0201485677", 2, 1, 1999, "Refactoring" },
                    { 4, "978-0321127426", 2, 2, 2002, "Patterns of Enterprise Application Architecture" },
                    { 5, "978-0321125217", 3, 2, 2003, "Domain-Driven Design" },
                    { 6, "978-0201633610", 4, 2, 1994, "Design Patterns" },
                    { 7, "978-0133591620", 5, 3, 2014, "Modern Operating Systems" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_FullName",
                table: "Authors",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
