using Modelo.Domain.Interfaces;
using Modelo.Infrastructure.Data;
using Modelo.Infrastructure.Repositories;
using Modelo.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao cont�iner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtenha a string de conex�o do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registre o DbConnectionHelper com a string de conex�o
builder.Services.AddSingleton(new DbConnectionHelper(connectionString));

// Registre os servi�os e reposit�rios
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

var app = builder.Build();

// Configure o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure o pipeline de requisi��es
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
