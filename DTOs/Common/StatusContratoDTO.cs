using System.ComponentModel.DataAnnotations;

namespace ContratosAPI.DTOs.Common
{
    // DTO para representar o status de um contrato
    public class StatusContratoDto
    {
        // ID de referência
        public int Id { get; set; }
        // Nome do status do contrato
        public string Nome { get; set; }
        // Descrição do status do contrato
        public string? Descricao { get; set; }
    }
}