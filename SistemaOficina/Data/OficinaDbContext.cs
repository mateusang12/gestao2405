using Microsoft.EntityFrameworkCore;
using SistemaOficina.Models;

namespace SistemaOficina.Data
{
    public class OficinaDbContext : DbContext
    {
        public OficinaDbContext(DbContextOptions<OficinaDbContext> options) : base(options) { }

        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}
