using System;

namespace SistemaOficina.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
        public string MarcaCarro { get; set; }
        public string ModeloCarro { get; set; }
        public string TipoServico { get; set; }

        // ADICIONE ESTA LINHA ABAIXO:
        public DateTime DataAgendamento { get; set; }
    }
}