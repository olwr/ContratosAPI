using System.ComponentModel.DataAnnotations;
using ContratosAPI.Attributes;
using ContratosAPI.DTOs.Common;

namespace ContratosAPI.DTOs.Funcionario
{
    /// DTO para criar um novo funcionário
    public class FuncionarioCreateDto
    {
        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome completo deve ter no máximo 200 caracteres")]
        public string NomeCompleto { get; set; }
        
        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataNascimentoValidacao(IdadeMinima = 14, ErrorMessage = "Você deve ter pelo menos 14 anos")] // Menor aprendiz
        public DateTime DataNascimento { get; set; }
        
        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas números")]
        public string CPF { get; set; }
        
        [Required(ErrorMessage = "Função é obrigatória")]
        [StringLength(150, ErrorMessage = "A função deve ter no máximo 150 caracteres")]
        public string Funcao { get; set; }
        
        [Required(ErrorMessage = "Informações de cidade e estado são obrigatórias")]
        public CidadeEstadoDto CidadeEstado { get; set; }
        
        [Required(ErrorMessage = "Informações de contato são obrigatórias")]
        public ContatoDto Contato { get; set; }
    }
}