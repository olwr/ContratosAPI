using ContratosAPI.DTOs.Common;

namespace ContratosAPI.DTOs.Contrato
{
    /// DTO para retornar dados completos de um contrato
    public class ContratoResponseDto
    {
        public int Id { get; set; }
        
        // Contratante expandido
        public int ContratanteId { get; set; }
        public string ContratanteNome { get; set; }
        public string ContratanteCNPJ { get; set; }
        
        // Contraente expandido
        public int ContraenteId { get; set; }
        public int TipoContraenteId { get; set; }
        public string TipoContraenteNome { get; set; }
        public string ContraenteNome { get; set; }
        public string ContraenteDocumento { get; set; } // CPF ou CNPJ
        
        // Tipo e Status expandidos
        public int TipoContratoId { get; set; }
        public string TipoContratoNome { get; set; }
        public string? TipoContratoDescricao { get; set; }
        public int StatusContratoId { get; set; }
        public string StatusContratoNome { get; set; }
        public string? StatusContratoDescricao { get; set; }
        
        // Dados do contrato
        public decimal Precificacao { get; set; }
        public string CondicoesPagamento { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? Validade { get; set; }
        public string? Descricao { get; set; }
        
        // Informações calculadas
        public bool EstaVencido { get; set; }
        public int DiasAtivo { get; set; }
    }
}