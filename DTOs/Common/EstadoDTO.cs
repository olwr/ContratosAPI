using System.ComponentModel.DataAnnotations;

namespace ContratosAPI.DTOs.Common
{
    // DTO para representar um Estado
    public class EstadoDto
    {
        // ID de referência
        public int Id { get; set; }
        // Sigla do Estado
        public string Sigla { get; set; }
        // Nome completo do Estado
        public string Nome { get; set; }
    }
}