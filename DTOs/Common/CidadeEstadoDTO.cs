using System.ComponentModel.DataAnnotations;

namespace ContratosAPI.DTOs.Common
{
    public class CidadeEstadoDto
    {
        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(200, ErrorMessage = "O nome da cidade deve ter no máximo 200 caracteres")]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage = "Estado é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um estado válido")]
        public int EstadoId { get; set; }
    }
}

