using System.ComponentModel.DataAnnotations;

namespace SistemaOficina.DTOs
{
    public class AgendamentoCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string NomeCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca do carro é obrigatória.")]
        public string MarcaCarro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo do carro é obrigatório.")]
        public string ModeloCarro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de serviço é obrigatório.")]
        public string TipoServico { get; set; } = string.Empty;
    }
}
