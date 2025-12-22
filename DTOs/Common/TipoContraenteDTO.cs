using System.ComponentModel.DataAnnotations;

namespace ContratosAPI.DTOs.Common
{
    // DTO para representar um tipo de contrato
    public class TipoContraenteDto
    {
        // ID de referência
        public int Id { get; set; }
        // Nome do tipo de contrato
        public string Nome { get; set; }
        // Descrição do tipo de contrato
        public string? Descricao { get; set; }
    }
}