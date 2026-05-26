using System.Threading.Tasks;

namespace SistemaOficina.Services
{
    public interface IFipeService
    {
        Task<object> BuscarMarcasAsync();
        Task<object> BuscarModelosAsync(string marcaId);
    }
}
