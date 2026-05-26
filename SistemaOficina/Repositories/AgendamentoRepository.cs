using SistemaOficina.Data;
using SistemaOficina.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SistemaOficina.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly OficinaDbContext _context;

        public AgendamentoRepository(OficinaDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.AddAsync(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Agendamento>> ObterTodosAsync()
        {
            return await _context.Agendamentos.ToListAsync();
        }
    }
}