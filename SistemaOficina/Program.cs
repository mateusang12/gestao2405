using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SistemaOficina.Data;
using SistemaOficina.Repositories;
using SistemaOficina.Services;

var builder = WebApplication.CreateBuilder(args);

// Substitui a linha do builder.Services.AddDbContext antiga por este bloco:
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    // Para testes locais sem variáveis de ambiente, usa o banco em memória
    builder.Services.AddDbContext<OficinaDbContext>(options =>
        options.UseInMemoryDatabase("OficinaDb"));
}
else
{
    // Em produção (Render), liga-se diretamente ao PostgreSQL do Supabase
    builder.Services.AddDbContext<OficinaDbContext>(options =>
        options.UseNpgsql(connectionString));
}

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddHttpClient();

builder.Services.AddScoped<IFipeService, FipeService>();

builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Sistema Oficina API v1");
        options.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

// Adicione isso logo antes de app.Run(); para criar as tabelas no Supabase automaticamente
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OficinaDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
