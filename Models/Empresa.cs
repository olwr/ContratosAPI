using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContratosAPI.Models
{
    public class Empresa
    {
        // Todos os atributos possuem validação de obrigatoriedade e valor

        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // **

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
        [StringLength(10, MinimumLength = 1, ErrorMessage = "O número deve ter entre 1 e 10 caracteres")]
        public string Numero { get; set; }

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres")]
        public string? Complemento { get; set; }
        
        [Required(ErrorMessage = "Cidade e Estado são obrigatórios")]
        public CidadeEstado CidadeEstado { get; set; }
        
        [Required(ErrorMessage = "Contato é obrigatório")]
        public Contato Contato { get; set; }
        
        [Required(ErrorMessage = "Setor é obrigatório")]
        [StringLength(100, ErrorMessage = "O setor deve ter no máximo 100 caracteres")]
        public string Setor { get; set; }
        
        // Uma empresa pode ter um ou vários contratos (1:N)
        public ICollection<Contrato>?  ContratosComoContratante { get; set; }
    }
};