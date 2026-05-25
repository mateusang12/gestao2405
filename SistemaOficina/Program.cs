var builder = WebApplication.CreateBuilder(args);

// Configura os controladores e força o JSON a manter os nomes originais das propriedades
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Ativa a fábrica de conexões HTTP
builder.Services.AddHttpClient();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles(); // Importante para rodar o index.html da wwwroot

app.MapControllers();

app.Run();
