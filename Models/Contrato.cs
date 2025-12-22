using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContratosAPI.Attributes;

namespace ContratosAPI.Models
{
    [ContraenteValidacao]
    public class Contrato
    {
        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        // Chaves estrangeiras
        // Contratante (Sempre Empresa)
        [Required(ErrorMessage = "Id do Contratante é obrigatório (EMPRESA)")]
        public int ContratanteId { get; set; }
        
        [ForeignKey("ContratanteId")]
        public Empresa Contratante { get; set; }
        
        // Contraente (Empresa OU Funcionário)
        [Required(ErrorMessage = "Contraente é obrigatório")]
        public int ContraenteId { get; set; }
        
        // FK para TipoContraente (tabela de referência)
        [Required(ErrorMessage = "Tipo de contraente é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de contraente válido")]
        public int TipoContraenteId { get; set; }
        
        [ForeignKey("TipoContraenteId")]
        [Range(1, 2, ErrorMessage = "Tipo de contraente inválido (1=Empresa, 2=Funcionário)")]
        public TipoContraente TipoContraente { get; set; }
        
        [Required(ErrorMessage = "Tipo do contrato é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo válido")]
        public int TipoContratoId { get; set; }
        
        [ForeignKey("TipoContratoId")]
        public TipoContrato TipoContrato { get; set; }
        
        [Required(ErrorMessage = "Status do contrato é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um status válido")]
        public int StatusContratoId { get; set; }
        
        [ForeignKey("StatusContratoId")]
        public StatusContrato StatusContrato { get; set; }
        // **
        
        [Required(ErrorMessage = "Precificação é obrigatória")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Precificação deve ser maior que zero")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precificacao { get; set; }
        
        [Required(ErrorMessage = "Condições de pagamento são obrigatórias")]
        [StringLength(500, ErrorMessage = "Condições de pagamento devem ter no máximo 500 caracteres")]
        public string CondicoesPagamento { get; set; }
        
        [Required(ErrorMessage = "Data de emissão é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEmissao { get; set; } = DateTime.Today;
     
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Validade {get; set; }
        
        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string? Descricao { get; set; }
    }
}