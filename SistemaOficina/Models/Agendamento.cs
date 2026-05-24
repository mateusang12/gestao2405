namespace SistemaOficina.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string MarcaCarro { get; set; } = string.Empty;
        public string ModeloCarro { get; set; } = string.Empty;
        public string TipoServico { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
