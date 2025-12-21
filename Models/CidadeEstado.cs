using System.ComponentModel.DataAnnotations;

// Classe Abstrata para a reutilização de código entre Empresa e Funcionário
namespace ContratosAPI.Models
{
    // ENUM usado para manter o padrão e validação dos Estados
    public enum Estado
    {
        [Display(Name = "Selecione um estado")]
        NaoSelecionado = 0,
        AC = 1,
        AL = 2,
        AP = 3,
        AM = 4,
        BA = 5,
        CE = 6,
        DF = 7,
        ES = 8,
        GO = 9,
        MA = 10,
        MT = 11,
        MS = 12,
        MG = 13,
        PA = 14,
        PB = 15,
        PR = 16,
        PE = 17,
        PI = 18,
        RJ = 19,
        RN = 20,
        RS = 21,
        RO = 22,
        RR = 23,
        SC = 24,
        SP = 25,
        SE = 26,
        TO = 27
    }

    public abstract class CidadeEstado
    {
        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(200, ErrorMessage = "O nome da cidade deve ter no máximo 200 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um estado válido")]
        [EnumDataType(typeof(Estado))]
        public Estado Estado { get; set; }
    }
}

