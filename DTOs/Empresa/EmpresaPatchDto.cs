using System.ComponentModel.DataAnnotations;
using ContratosAPI.DTOs.Common;

namespace ContratosAPI.DTOs.Empresa
{
    // DTO para atualizar uma empresa existente
    public class EmpresaPatchDto
    {
        // ID não está aqui, pois vem da rota (ex: PATCH /api/empresas/5)
        [StringLength(200, ErrorMessage = "Razão Social deve ter no máximo 200 caracteres")]
        public string? RazaoSocial { get; set; }
        
        [StringLength(200, ErrorMessage = "Nome Fantasia deve ter no máximo 200 caracteres")]
        public string? NomeFantasia { get; set; }
        
        // CNPJ geralmente não é atualizado, mas se permitir:
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve ter 14 dígitos")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "CNPJ deve conter apenas números")]
        public string? CNPJ { get; set; }
        
        [StringLength(200, ErrorMessage = "O logradouro deve ter no máximo 200 caracteres")]
        public string? Logradouro { get; set; }
        
        [StringLength(10, MinimumLength = 1, ErrorMessage = "O número deve ter entre 1 e 10 caracteres")]
        public string? Numero { get; set; }
        
        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres")]
        public string? Complemento { get; set; }
        
        [Required(ErrorMessage = "Setor é obrigatório")]
        [StringLength(100, ErrorMessage = "O setor deve ter no máximo 100 caracteres")]
        public string? Setor { get; set; }
        
        [Required(ErrorMessage = "Informações de cidade e estado são obrigatórias")]
        public CidadeEstadoDto? CidadeEstado { get; set; }
        
        [Required(ErrorMessage = "Informações de contato são obrigatórias")]
        public ContatoDto? Contato { get; set; }
    }
}