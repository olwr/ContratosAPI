namespace ContratosAPI.DTOs.Empresa
{
    /// DTO para listar empresas (versão resumida)
    public class EmpresaListDto
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Cidade { get; set; }
        public string EstadoSigla { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}