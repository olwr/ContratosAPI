namespace ContratosAPI.DTOs.Contrato
{
    /// DTO para listar contratos (versão resumida)
    public class ContratoListDto
    {
        public int Id { get; set; }
        public string ContratanteNome { get; set; }
        public string ContraenteNome { get; set; }
        public string TipoContratoNome { get; set; }
        public string StatusContratoNome { get; set; }
        public decimal Precificacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public bool EstaVencido { get; set; }
    }
}