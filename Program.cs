using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ContratosAPI.Data;
using ContratosAPI.Mappings;
using ContratosAPI.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configura a conexão com database (MariaDB)
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configura AutoMapper
builder.Services.AddAutoMapper(typeof (AutoMapperProfile));

// Configura os Controlllers
builder.Services.AddControllers();

// Configura Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(gen =>
{
    gen.SwaggerDoc("v1", new()
    {
        Title = "ContratosAPI",
        Version = "v1",
        Description = "API para a gestão de empresas, funcionários e contratos",
    });

    // Incluir comentários XML
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    gen.IncludeXmlComments(xmlPath);

    // gen.EnableAnnotations();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

WebApplication app = builder.Build();

// Pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();