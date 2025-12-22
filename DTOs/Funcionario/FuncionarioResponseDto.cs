using ContratosAPI.DTOs.Common;
namespace ContratosAPI.DTOs.Funcionario
{
    /// <summary>
    /// DTO para retornar dados completos de um funcionário
    /// </summary>
    public class FuncionarioResponseDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; } // Calculado
        public string CPF { get; set; }
        public string Funcao { get; set; }
        
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