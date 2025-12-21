using System.ComponentModel.DataAnnotations;

// Validação costumizada de data de nascimento válida
namespace ContratosAPI.Attributes
{
    public class DataNascimentoValidacao : ValidationAttribute
    {
        public int IdadeMinima { get; set; } = 0;
        private int IdadeMaxima { get; set; } = 125;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dataNascimento) return new ValidationResult("Data inválida");
            
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;

            if (dataNascimento > hoje)
                return new ValidationResult("Data de nascimento não pode ser no futuro");

            if (idade < IdadeMinima)
                return new ValidationResult($"Idade mínima: {IdadeMinima} anos");
                
            if (idade > IdadeMaxima)
                return new ValidationResult($"Idade máxima: {IdadeMaxima} anos");
            
            return ValidationResult.Success;
        }
    }
}