using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContratosAPI.Attributes;

namespace ContratosAPI.Models
{
    // ENUM usado para manter o padrão e validação dos tipos e status de contratos
    public enum TipoContrato
    {
        [Display(Name = "Selecione o tipo do contrato")]
        NaoSelecionado = 0,
        CLT,
        MEI,
        TEMPORARIO,
        AUTONOMO,
        ESTAGIO,
        APRENDIZ // Menor e Jovem
    }

    public enum StatusContrato
    {
        [Display(Name = "Selecione o status do contrato")]
        NaoSelecionado = 0,
        ATIVO,
        ENCERRADO,
        RESCINDIDO
    }

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
        public int? ContraenteEmpresaId { get; set; }
        public int? ContraenteFuncionarioId { get; set; }
        
        [ForeignKey("ContraenteEmpresaId")]
        public Empresa? ContraenteEmpresa { get; set; }

        [ForeignKey("ContraenteFuncionarioId")]
        public Funcionario? ContraenteFuncionario { get; set; }
        // **
        
        [Required(ErrorMessage = "Tipo do contrato é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo válido")]
        [EnumDataType(typeof(TipoContrato))]
        public TipoContrato TipoContrato { get; set; }
        
        [Required(ErrorMessage = "Precificação é obrigatória")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Precificação deve ser maior que zero")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precificacao { get; set; }
        
        [Required(ErrorMessage = "Condições de pagamento são obrigatórias")]
        [StringLength(500, ErrorMessage = "Condições de pagamento devem ter no máximo 500 caracteres")]
        public string CondicoesPagamento { get; set; }
        
        [Required(ErrorMessage = "Status do contrato é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um status válido")]
        [EnumDataType(typeof(StatusContrato))]
        public StatusContrato StatusContrato { get; set; }
        
        [Required(ErrorMessage = "Data de emissão é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEmissao { get; set; } = DateTime.Today;
     
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Validade {get; set; }
    }
}