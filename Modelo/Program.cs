using Modelo.Domain.Interfaces;
using Modelo.Infrastructure.Data;
using Modelo.Infrastructure.Repositories;
using Modelo.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Obtenha a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registre o DbConnectionHelper com a string de conexão
builder.Services.AddSingleton(new DbConnectionHelper(connectionString));

// Registre os serviços e repositórios
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IAssuntoService, AssuntoService>();
builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();

var app = builder.Build();

// Configure o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar o middleware HTTP.
app.UseHttpsRedirection();

// Aplica a política de CORS
app.UseCors("AllowAllOrigins");

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
