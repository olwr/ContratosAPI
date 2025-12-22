namespace ContratosAPI.DTOs.Funcionario
{
    /// DTO para listar funcionários (versão resumida)
    public class FuncionarioListDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public string Funcao { get; set; }
        public string Cidade { get; set; }
        public string EstadoSigla { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}