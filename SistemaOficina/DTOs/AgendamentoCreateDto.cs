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

        [Required(ErrorMessage = "A marca é obrigatória.")]
        public string MarcaCarro { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string ModeloCarro { get; set; }

        [Required(ErrorMessage = "A localidade é obrigatória.")]
        public string Localidade { get; set; }

        [Required(ErrorMessage = "O tipo de serviço é obrigatório.")]
        public string TipoServico { get; set; }

        [Required(ErrorMessage = "A data e horário são obrigatórios.")]
        public DateTime DataAgendamento { get; set; }
    }
}
