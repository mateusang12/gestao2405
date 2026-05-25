using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaOficina.DTOs
{
    public class AgendamentoCreateDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "A marca do veículo é obrigatória.")]
        public string MarcaCarro { get; set; }

        [Required(ErrorMessage = "O modelo do veículo é obrigatório.")]
        public string ModeloCarro { get; set; }

        [Required(ErrorMessage = "O tipo de serviço é obrigatório.")]
        public string TipoServico { get; set; }

        // ADICIONE ESTAS LINHAS ABAIXO:
        [Required(ErrorMessage = "A data e o horário do agendamento são obrigatórios.")]
        public DateTime DataAgendamento { get; set; }
    }
}
