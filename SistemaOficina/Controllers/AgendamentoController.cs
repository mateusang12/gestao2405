using Microsoft.AspNetCore.Mvc;
using SistemaOficina.DTOs;
using SistemaOficina.Models;

namespace SistemaOficina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        // Mock ou simulador de dados de carros vendidos no Brasil (2010 - 2026)
        [HttpGet("carros-brasil")]
        public IActionResult GetCarrosBrasil()
        {
            var dadosCarros = new List<object>
            {
                new { Marca = "Fiat", Modelos = new[] { "Strada", "Palio", "Uno", "Argo", "Mobi", "Toro" } },
                new { Marca = "Volkswagen", Modelos = new[] { "Gol", "Polo", "Fox", "T-Cross", "Saveiro", "Tera" } },
                new { Marca = "Chevrolet", Modelos = new[] { "Onix", "Prisma", "Celta", "Tracker", "S10" } },
                new { Marca = "Hyundai", Modelos = new[] { "HB20", "Creta", "HB20S" } },
                new { Marca = "Toyota", Modelos = new[] { "Corolla", "Hilux", "Etios" } },
                new { Marca = "Ford", Modelos = new[] { "Ka", "Fiesta", "EcoSport" } },
                new { Marca = "BYD", Modelos = new[] { "Dolphin Mini", "Dolphin", "Song Plus" } }
            };

            return Ok(dadosCarros);
        }

        // Os 10 serviços mais procurados em oficinas mecânicas
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

            // Aqui você salvaria no banco via Data/OficinaContext. Exemplo simulado:
            var novoAgendamento = new Agendamento
            {
                Id = new Random().Next(1, 1000), // Apenas para simulação
                NomeCliente = dto.NomeCliente,
                Telefone = dto.Telefone,
                MarcaCarro = dto.MarcaCarro,
                ModeloCarro = dto.ModeloCarro,
                TipoServico = dto.TipoServico
            };

            return Created(string.Empty, new { mensagem = "Agendamento realizado com sucesso!", dados = novoAgendamento });
        }
    }
}
