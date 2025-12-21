using System.ComponentModel.DataAnnotations;

// Validação costumizada de contraentes (Empresa ou Funcionário) válidos
namespace ContratosAPI.Attributes
{
    public class ContraenteValidacao : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           object contrato = validationContext.ObjectInstance;
           int? empresaId = contrato.GetType().GetProperty("ContraenteEmpresaId")?.GetValue(contrato) as int?;
           int? funcionarioId = contrato.GetType().GetProperty("ContraenteFuncionarioId")?.GetValue(contrato) as int?;

           bool temEmpresa = empresaId.HasValue;
           bool temFuncionario = funcionarioId.HasValue;

           if (!temFuncionario && !temEmpresa)
               return new ValidationResult("É necessário informar um contraente (Empresa ou Funcionário)");

           if (temEmpresa && temFuncionario)
               return new ValidationResult("Informe apenas um tipo de contraente");
           
           return ValidationResult.Success;
        }
    }
}