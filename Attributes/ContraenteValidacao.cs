using System.ComponentModel.DataAnnotations;
using ContratosAPI.Models;

// Validação costumizada de contraentes (Empresa ou Funcionário) válidos
namespace ContratosAPI.Attributes
{
    public class ContraenteValidacao : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not Contrato contrato)
               return ValidationResult.Success;

            if (contrato.ContraenteId <= 0)
            {
                return new ValidationResult(
                    "É necessário informar o ID do contraente",
                    new[] { nameof(Contrato.ContraenteId) });
            }

            if (contrato.TipoContraenteId <= 0)
            {
                return new ValidationResult(
                    "É necessário informar o tipo de contraente",
                    new[] { nameof(Contrato.TipoContraenteId) });
            }
           
            if (contrato.TipoContraenteId != 1 && contrato.TipoContraenteId != 2)
            {
                return new ValidationResult(
                    "Tipo de contraente inválido. Use 1 para Empresa ou 2 para Funcionário",
                    new[] { nameof(Contrato.TipoContraenteId) });
            }
           
            return ValidationResult.Success;
        }
    }
}