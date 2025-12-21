using Microsoft.EntityFrameworkCore;
// using ContratosAPI.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configura a conexão com database (MariaDB)
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
        Description = "API para a gestão de empresas, funcionários e contratos"
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
app.UseAuthorization();
app.MapControllers();
app.Run();