using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SistemaOficina.Data;
using SistemaOficina.Repositories;
using SistemaOficina.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OficinaDbContext>(options =>
    options.UseInMemoryDatabase("OficinaDb"));

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

app.Run();
