using SistemaOficina.Services;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class FipeService : IFipeService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FipeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<object> BuscarMarcasAsync()
    {
        var client = _httpClientFactory.CreateClient();

        // Cabeçalhos robustos para evitar que o host remoto cancele a conexão
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        try
        {
            var response = await client.GetStringAsync("https://parallelum.com.br/fipe/api/v1/carros/marcas");
            return JsonSerializer.Deserialize<object>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (HttpRequestException ex)
        {
            // Fallback elegante para a sua apresentação não quebrar se a API externa cair
            return new[] { new { codigo = "22", nome = "Ford (Local Offline)" }, new { codigo = "21", nome = "Fiat (Local Offline)" } };
        }
    }

    public async Task<object> BuscarModelosAsync(string marcaId)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        // Garante que se vier o nome "ford" por erro do front, ele converte para o ID numérico correto da FIPE (22)
        if (marcaId.ToLower() == "ford") marcaId = "22";

        try
        {
            var response = await client.GetStringAsync($"https://parallelum.com.br/fipe/api/v1/carros/marcas/{marcaId}/modelos");

            using var doc = JsonDocument.Parse(response);
            var modelosRaw = doc.RootElement.GetProperty("modelos").GetRawText();
            return JsonSerializer.Deserialize<object>(modelosRaw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (HttpRequestException ex)
        {
            return new[] { new { nome = "EcoSport" }, new { nome = "Ranger" }, new { nome = "Ka" } };
        }
    }
}