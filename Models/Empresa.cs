using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContratosAPI.Models
{
    // ENUM usado para manter o padrão e validação dos Estados
    public enum Estado
    {
        [Display(Name = "Selecione um estado")]
        NaoSelecionado = 0,
        AC = 1,
        AL = 2,
        AP = 3,
        AM = 4,
        BA = 5,
        CE = 6,
        DF = 7,
        ES = 8,
        GO = 9,
        MA = 10,
        MT = 11,
        MS = 12,
        MG = 13,
        PA = 14,
        PB = 15,
        PR = 16,
        PE = 17,
        PI = 18,
        RJ = 19,
        RN = 20,
        RS = 21,
        RO = 22,
        RR = 23,
        SC = 24,
        SP = 25,
        SE = 26,
        TO = 27
    }

    public class Empresa
    {
        private string _email;
        
        // Todos os atributos possuem validação de obrigatoriedade e valor

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Razão social é obrigatória")]
        [StringLength(200, ErrorMessage = "A razão social deve ter no máximo 200 caracteres")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Nome fantasia é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome fantasia deve ter no máximo 200 caracteres")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter 14 caracteres")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [StringLength(200, ErrorMessage = "O logradouro deve ter no máximo 200 caracteres")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Número é obrigatório (use 'S/N' se não houver)")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Número deve ter entre 1 e 10 caracteres")]
        public string Numero { get; set; }

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(200, ErrorMessage = "O nome da cidade deve ter no máximo 200 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um estado válido")]
        [EnumDataType(typeof(Estado))]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O número deve estar no formato: +5511912345678")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(254)]
        public string Email
        {
            // Normalização para minúsculas
            get => _email;
            set => _email = value.ToLowerInvariant();
        }
        
        [Required(ErrorMessage = "Setor é obrigatório")]
        [StringLength(100, ErrorMessage = "O setor deve ter no máximo 100 caracteres")]
        public string Setor { get; set; }
        
        // Uma empresa pode ter um ou vários contratos (1:N)
        public ICollection<Contrato>  Contratos { get; set; }
    }
};