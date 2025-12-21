using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContratosAPI.Attributes;

namespace ContratosAPI.Models
{
    public class Funcionario
    {
        private string _email;
        
        // Todos os atributos possuem validação de obrigatoriedade e valor

        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // **
        
        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome completo deve ter no máximo 200 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataNascimentoValidacao(IdadeMinima = 14, ErrorMessage = "Você deve ter pelo menos 14 anos")] // Menor aprendiz
        public DateTime DataNascimento { get; set; }
        
        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 caracteres")]
        public string CPF { get; set; }
        
        [Required(ErrorMessage = "Cidade e Estado são obrigatórios")]
        public CidadeEstado CidadeEstado { get; set; }
        
        [Required(ErrorMessage = "Contato é obrigatório")]
        public Contato Contato { get; set; }
        
        [Required(ErrorMessage = "Função é obrigatória")]
        [StringLength(200, ErrorMessage = "a função deve ter no máximo 200 caracteres")]
        
        // Um funcionário pode ter um ou vários contratos (1:N)
        public ICollection<Contrato>?  Contratos { get; set; }
    }
};