using Library.Application.Extensions;
using Library.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// Registro de servicios
// ============================================================

// Servicios de la capa Application (Mediator + Handlers)
builder.Services.AddApplicationServices();

// Servicios de la capa Persistence (DbContext SQL Server + Repositorios)
builder.Services.AddPersistenceServices(builder.Configuration);

// Controladores de la API
builder.Services.AddControllers();

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Library Catalog API",
        Version = "v1",
        Description = "API de consulta del catálogo bibliográfico: Libros, Autores y Categorías. " +
                      "Solo operaciones de lectura (CQRS - Query side)."
    });

    // Incluir comentarios XML si existen
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// ============================================================
// Pipeline HTTP
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Catalog API v1");
        options.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
