using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

// Classe para a reutilização de código entre Empresa e Funcionário
namespace ContratosAPI.Models
{
    [Owned]
    public class Contato
{
    private string? _email;

    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(254)]
    public string Email
    {
        // Normalização para minúsculas
        get => _email;
        set => _email = value?.ToLowerInvariant();
    }
    
    [Required(ErrorMessage = "Telefone é obrigatório")]
    [RegularExpression("^\\+55(1[1-9]|2[1-8]|3[1-5]|4[1-9]|5[1-5]|6[1-9]|7[1-9]|8[1-9]|9[1-9])9\\d{8}$")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O número deve estar no formato: +5511912345678")]
    public string Telefone { get; set; }
    
    [Phone]
    [StringLength(15, ErrorMessage = "Telefone deve ter até 15 caracteres")]
    public string? TelefoneAlternativo { get; set; }
    
    [Url]
    [StringLength(200, ErrorMessage = "Url do website deve ter no máximo 200 caracteres")]
    public string? Website { get; set; }

    [StringLength(50, ErrorMessage = "Url do LinkedIn deve ter no máximo 50 caracteres")]
    public string? LinkedIn { get; set; }
}
}