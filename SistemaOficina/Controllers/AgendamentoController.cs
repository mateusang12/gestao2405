using Microsoft.AspNetCore.Mvc;
using SistemaOficina.DTOs;
using SistemaOficina.Models;
using SistemaOficina.Repositories;
using SistemaOficina.Services;
using System;
using System.Threading.Tasks;

namespace SistemaOficina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IFipeService _fipeService;
        private readonly IAgendamentoRepository _repository;

        public AgendamentoController(IFipeService fipeService, IAgendamentoRepository repository)
        {
            _fipeService = fipeService;
            _repository = repository;
        }

        [HttpGet("marcas")]
        public async Task<IActionResult> GetMarcas() => Ok(await _fipeService.BuscarMarcasAsync());

        [HttpGet("marcas/{marcaId}/modelos")]
        public async Task<IActionResult> GetModelos(string marcaId) => Ok(await _fipeService.BuscarModelosAsync(marcaId));

        [HttpGet("servicos-oficina")]
        public IActionResult GetServicosOficina()
        {
            return Ok(new[] {
                "Troca de Óleo e Filtro", "Manutenção/Troca de Freios", "Alinhamento e Balanceamento",
                "Troca de Bateria", "Revisão do Ar Condicionado", "Diagnóstico de Injeção Eletrônica",
                "Troca de Amortecedores", "Substituição de Velas", "Troca da Correia Dentada", "Revisão Geral Preventiva"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CriarAgendamento([FromBody] AgendamentoCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var novoAgendamento = new Agendamento
            {
                Id = new Random().Next(1, 1000),
                NomeCliente = dto.NomeCliente,
                Telefone = dto.Telefone,
                MarcaCarro = dto.MarcaCarro,
                ModeloCarro = dto.ModeloCarro,
                Localidade = dto.Localidade,
                TipoServico = dto.TipoServico,
                DataAgendamento = dto.DataAgendamento
            };

            await _repository.AdicionarAsync(novoAgendamento);
            return Created(string.Empty, new { mensagem = "Agendamento realizado com sucesso!", dados = novoAgendamento });
        }
    }
}