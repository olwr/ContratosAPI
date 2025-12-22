using System.ComponentModel.DataAnnotations;

namespace ContratosAPI.DTOs.Contrato
{
    /// DTO para atualizar um contrato existente
    public class ContratoPutDto
    {
        // Contratante e Contraente geralmente não mudam após criação
        
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo válido")]
        public int? TipoContratoId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um status válido")]
        public int? StatusContratoId { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Precificação deve ser maior que zero")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal? Precificacao { get; set; }
        
        [StringLength(500, ErrorMessage = "Condições de pagamento devem ter no máximo 500 caracteres")]
        public string? CondicoesPagamento { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataEmissao { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Validade { get; set; }
        
        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string? Descricao { get; set; }
    }
}