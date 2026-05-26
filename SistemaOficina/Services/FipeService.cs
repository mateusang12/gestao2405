using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SistemaOficina.Services
{
    public class FipeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FipeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1. Busca TODAS as marcas de carros direto da FIPE
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
                return new List<object>(); // Retorna lista vazia para o front-end não quebrar
            }
        }

        // 2. Busca os modelos baseado no código numérico da marca selecionada
        public async Task<object> BuscarModelosAsync(string marcaId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                // A URL exige o ID numérico (ex: "22" para Ford). O try-catch protege o app caso venha texto inválido
                var url = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{marcaId}/modelos";
                var response = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Abre o JSON bruto da FIPE para extrair apenas a propriedade interna "modelos"
                using var doc = JsonDocument.Parse(response);
                var modelosRaw = doc.RootElement.GetProperty("modelos").GetRawText();

                // Desserializa e entrega o array limpo [...] direto para o Controller repassar ao JavaScript
                return JsonSerializer.Deserialize<object>(modelosRaw, options);
            }
            catch (Exception ex)
            {
                // Registra o erro no console de Output do Visual Studio sem estourar uma tela preta de exceção no app
                System.Diagnostics.Debug.WriteLine($"[Erro FIPE] Falha ao buscar modelos para a marca '{marcaId}': {ex.Message}");
                return new List<object>(); // Retorna lista vazia segura
            }
        }
    }
}