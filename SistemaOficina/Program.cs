using Microsoft.EntityFrameworkCore; // ADICIONE ESTE USING NO TOPO
using Scalar.AspNetCore;
using SistemaOficina.Data;
using SistemaOficina.Repositories;
using SistemaOficina.Services;

var builder = WebApplication.CreateBuilder(args);

// ADICIONE ESTA LINHA AQUI (Configura o banco de dados temporário na memória):
builder.Services.AddDbContext<OficinaDbContext>(options =>
    options.UseInMemoryDatabase("OficinaDb"));

// Configura os Controladores
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// ... resto do seu Program.cs continua igualzinho ...

// 2. Ativa o mecanismo do HttpClientFactory para consumo da API FIPE
builder.Services.AddHttpClient();

// 3. INJEÇÃO DE DEPENDÊNCIA: Registra as novas pastas/classes no sistema
builder.Services.AddScoped<FipeService>();
builder.Services.AddScoped<AgendamentoRepository>();

// 4. Ativa o gerador de documentação OpenAPI (.NET 10 nativo)
builder.Services.AddOpenApi();

var app = builder.Build();

// 5. Mapeia a rota visual do Swagger se estiver em ambiente de desenvolvimento
// Se houver a linha do Scalar (app.MapScalarApiReference();), pode apagá-la.

if (app.Environment.IsDevelopment())
{
    // 1. Mantém o gerador do JSON ativo
    app.MapOpenApi();

    // 2. Ativa o Swagger UI clássico apontando para o JSON correto do .NET 10
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Sistema Oficina API v1");
        options.RoutePrefix = "swagger"; // Define que a rota de acesso será /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Permite renderizar a sua interface frontend (index.html, css, js) da pasta wwwroot
app.UseStaticFiles();

app.MapControllers();

app.Run();
