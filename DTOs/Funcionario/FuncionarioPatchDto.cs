using System.ComponentModel.DataAnnotations;
using ContratosAPI.Attributes;
using ContratosAPI.DTOs.Common;

namespace ContratosAPI.DTOs.Funcionario
{
    /// DTO para atualizar um funcionário existente
    public class FuncionarioPatchDto
    {
        [StringLength(200, ErrorMessage = "O nome completo deve ter no máximo 200 caracteres")]
        public string NomeCompleto { get; set; }
        
        // Data de nascimento geralmente não é atualizada
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataNascimentoValidacao(IdadeMinima = 14, ErrorMessage = "Você deve ter pelo menos 14 anos")] // Menor aprendiz
        public DateTime DataNascimento { get; set; }
        
        // CPF geralmente não é atualizado
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas números")]
        public string CPF { get; set; }
        
        [StringLength(150, ErrorMessage = "A função deve ter no máximo 150 caracteres")]
        public string Funcao { get; set; }
        
        public CidadeEstadoDto CidadeEstado { get; set; }
        
        public ContatoDto Contato { get; set; }
    }
}