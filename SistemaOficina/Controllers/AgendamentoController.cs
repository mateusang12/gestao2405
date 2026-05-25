using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SistemaOficina.DTOs;
using SistemaOficina.Models;

namespace SistemaOficina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AgendamentoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1. Busca TODAS as marcas de carros direto da FIPE
        [HttpGet("marcas")]
        public async Task<IActionResult> GetMarcasFipe()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                var url = "https://parallelum.com.br/fipe/api/v1/carros/marcas";
                var response = await client.GetStringAsync(url);

                // Desserializa garantindo que o mapeamento respeite a estrutura original da FIPE
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var marcas = JsonSerializer.Deserialize<object>(response, options);

                return Ok(marcas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar marcas na FIPE", detalhe = ex.Message });
            }
        }

        // 2. Busca os modelos baseado no código da marca selecionada
        [HttpGet("marcas/{marcaId}/modelos")]
        public async Task<IActionResult> GetModelosFipe(string marcaId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                var url = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{marcaId}/modelos";
                var response = await client.GetStringAsync(url);

                // Em vez de usar JsonDocument, vamos extrair a estrutura usando uma classe dinâmica anônima.
                // Isso padroniza o JSON que sai do C# para o JavaScript
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                using var doc = JsonDocument.Parse(response);
                var modelosRaw = doc.RootElement.GetProperty("modelos").GetRawText();

                // Converte a propriedade "modelos" em um objeto puro que o .NET consegue entregar perfeitamente
                var listaModelos = JsonSerializer.Deserialize<object>(modelosRaw, options);

                return Ok(listaModelos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar modelos na FIPE", detalhe = ex.Message });
            }
        }

        // 3. Os 10 serviços mais procurados
        [HttpGet("servicos-oficina")]
        public IActionResult GetServicosOficina()
        {
            var servicos = new[]
            {
                "Troca de Óleo e Filtro",
                "Manutenção/Troca de Freios (Pastilhas e Discos)",
                "Alinhamento e Balanceamento de Rodas",
                "Troca de Bateria",
                "Revisão do Sistema de Ar Condicionado",
                "Diagnóstico de Injeção Eletrônica (Luz da Injeção)",
                "Troca de Amortecedores e Suspensão",
                "Substituição de Velas de Ignição",
                "Troca da Correia Dentada",
                "Revisão Geral Preventiva"
            };
            return Ok(servicos);
        }

        [HttpPost]
        public IActionResult CriarAgendamento([FromBody] AgendamentoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoAgendamento = new Agendamento
            {
                Id = new Random().Next(1, 1000),
                NomeCliente = dto.NomeCliente,
                Telefone = dto.Telefone,
                MarcaCarro = dto.MarcaCarro,
                ModeloCarro = dto.ModeloCarro,
                TipoServico = dto.TipoServico,
                DataAgendamento = dto.DataAgendamento
            };

            return Created(string.Empty, new { mensagem = "Agendamento realizado com sucesso!", dados = novoAgendamento });
        }
    }
}