using Modelo.Domain.Interfaces;
using Modelo.Infrastructure.Data;
using Modelo.Infrastructure.Repositories;
using Modelo.Services;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Registra os serviços no contêiner
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); // Adiciona suporte a controladores para Web API

        // Registra os serviços personalizados
        services.AddScoped<DbConnectionHelper>(provider =>
            new DbConnectionHelper(Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<ILivroService, LivroService>();
    }

    // Configura o pipeline HTTP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Certifique-se de mapear os controladores
        });
    }
}
