using SistemaOficina.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaOficina.Repositories
{
    public interface IAgendamentoRepository
    {
        Task AdicionarAsync(Agendamento agendamento);
        Task<List<Agendamento>> ObterTodosAsync();
    }
}
