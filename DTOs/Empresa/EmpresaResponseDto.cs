using ContratosAPI.DTOs.Common;

namespace ContratosAPI.DTOs.Empresa
{
    /// DTO para retornar dados completos de uma empresa
    public class EmpresaResponseDto
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }

        public string Setor { get; set; }

        // Cidade e Estado expandidos
        public string Cidade { get; set; }
        public int EstadoId { get; set; }
        public string EstadoSigla { get; set; }
        public string EstadoNome { get; set; }

        // Contato expandido
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? TelefoneAlternativo { get; set; }
        public string? Website { get; set; }
        public string? LinkedIn { get; set; }

        // Opcional: incluir estatísticas
        public int TotalContratos { get; set; }
    }
}