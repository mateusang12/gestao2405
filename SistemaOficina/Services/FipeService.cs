using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SistemaOficina.Services
{
    
    public class FipeService : IFipeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FipeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> BuscarMarcasAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                var url = "https://parallelum.com.br/fipe/api/v1/carros/marcas";
                var response = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<object>(response, options);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Erro FIPE] Falha ao buscar marcas: {ex.Message}");
                return new List<object>();
            }
        }

        public async Task<object> BuscarModelosAsync(string marcaId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                var url = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{marcaId}/modelos";
                var response = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                using var doc = JsonDocument.Parse(response);
                var modelosRaw = doc.RootElement.GetProperty("modelos").GetRawText();

                return JsonSerializer.Deserialize<object>(modelosRaw, options);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Erro FIPE] Falha ao buscar modelos para a marca '{marcaId}': {ex.Message}");
                return new List<object>();
            }
        }
    }
}