using ContactAPI.Middlewares;
using ContactApplication.Services;
using ContactCore.Interfaces;
using ContactInfrastructure.Data;
using ContactInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios y repositorios para la inyecci�n de dependencias
builder.Services.AddScoped<IContactFormRepository, ContactFormRepository>();
builder.Services.AddScoped<IContactFormService, ContactFormService>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();

// Configurar controladores y validadores
builder.Services.AddControllers();

// Agregar servicios de autenticaci�n y autorizaci�n
builder.Services.AddAuthorization();

// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Contact Form API",
        Version = "v1",
        Description = "API for managing contact form submissions.",
        Contact = new OpenApiContact
        {
            Name = "Support Team",
            Email = "support@example.com",
        }
    });
});

// Configuraci�n de CORS (opcional)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configuraci�n de logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Middleware de manejo de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configuraci�n de CORS (opcional)
app.UseCors("AllowAll");

// Middleware de redirecci�n HTTPS
app.UseHttpsRedirection();

// Middleware para Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact Form API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Middleware de autorizaci�n
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();