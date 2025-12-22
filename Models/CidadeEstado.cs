using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

// Classe para a reutilização de código entre Empresa e Funcionário
namespace ContratosAPI.Models
{
    [Owned]
    public class CidadeEstado
    {
        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(200, ErrorMessage = "O nome da cidade deve ter no máximo 200 caracteres")]
        public string Cidade { get; set; }

        // FK
        [Required(ErrorMessage = "Estado é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um estado válido")]
        public int EstadoId { get; set; }
    }
}