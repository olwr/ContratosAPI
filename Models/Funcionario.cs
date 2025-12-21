using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContratosAPI.Models
{
    public class Funcionario
    {
        // Todos os atributos possuem validação de obrigatoriedade e valor

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome fantasia deve ter no máximo 200 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }
    }
};