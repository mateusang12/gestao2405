using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SistemaOficina.Data;
using SistemaOficina.Repositories;
using SistemaOficina.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados (PostgreSQL / Em Memória)
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
if (string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<OficinaDbContext>(options =>
        options.UseInMemoryDatabase("OficinaDb"));
}
else
{
    builder.Services.AddDbContext<OficinaDbContext>(options =>
        options.UseNpgsql(connectionString));
}

// 2. Configuração de CORS (Liberar acesso para o Frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Injeção de Dependências e Controladores
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddHttpClient();
builder.Services.AddScoped<IFipeService, FipeService>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddOpenApi();

var app = builder.Build();

// -------------------------------------------------------------
// CONFIGURAÇÃO DO PIPELINE DE REQUISIÇÕES (MIDDLEWARES)
// -------------------------------------------------------------

// 4. ATIVAR O CORS (Obrigatório vir logo no início!)
app.UseCors("PermitirTudo");

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
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

// 5. Garantir que as tabelas sejam criadas no Supabase automaticamente
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OficinaDbContext>();
    dbContext.Database.EnsureCreated();
}

// 6. Iniciar a API de fato (Apenas um app.Run no final do arquivo!)
app.Run();
