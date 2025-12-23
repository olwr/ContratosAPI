using AutoMapper;
using ContratosAPI.Models;
using ContratosAPI.DTOs.Empresa;
using ContratosAPI.DTOs.Funcionario;
using ContratosAPI.DTOs.Contrato;
using ContratosAPI.DTOs.Common;

namespace ContratosAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // ========== ENTIDADES DE REFERÊNCIA ==========
            CreateMap<Estado, EstadoDto>();
            CreateMap<TipoContrato, TipoContratoDto>();
            CreateMap<StatusContrato, StatusContratoDto>();
            CreateMap<TipoContraente, TipoContraenteDto>();
            
            // ========== MAPEAMENTOS DE TIPOS ANINHADOS ==========
            
            // CidadeEstadoDto <-> CidadeEstado
            CreateMap<CidadeEstadoDto, CidadeEstado>();
            CreateMap<CidadeEstado, CidadeEstadoDto>();
            
            // ContatoDto <-> Contato
            CreateMap<ContatoDto, Contato>();
            CreateMap<Contato, ContatoDto>();

            // ========== EMPRESA ==========
            // Empresa → EmpresaResponseDto
            CreateMap<Empresa, EmpresaResponseDto>()
                .ForMember(dest => dest.Cidade,
                    opt => opt.MapFrom(src => src.CidadeEstado.Cidade))
                .ForMember(dest => dest.EstadoId,
                    opt => opt.MapFrom(src => src.CidadeEstado.EstadoId))
                .ForMember(dest => dest.EstadoSigla,
                    opt => opt.Ignore()) // Carregar do banco se necessário
                .ForMember(dest => dest.EstadoNome,
                    opt => opt.Ignore()) // Carregar do banco se necessário
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Contato.Email))
                .ForMember(dest => dest.Telefone,
                    opt => opt.MapFrom(src => src.Contato.Telefone))
                .ForMember(dest => dest.TelefoneAlternativo,
                    opt => opt.MapFrom(src => src.Contato.TelefoneAlternativo))
                .ForMember(dest => dest.Website,
                    opt => opt.MapFrom(src => src.Contato.Website))
                .ForMember(dest => dest.LinkedIn,
                    opt => opt.MapFrom(src => src.Contato.LinkedIn))
                .ForMember(dest => dest.TotalContratos,
                    opt => opt.Ignore()); // Calcular separadamente
            
            // Empresa → EmpresaListDto
            CreateMap<Empresa, EmpresaListDto>()
                .ForMember(dest => dest.Cidade,
                    opt => opt.MapFrom(src => src.CidadeEstado.Cidade))
                .ForMember(dest => dest.EstadoSigla,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Contato.Email))
                .ForMember(dest => dest.Telefone,
                    opt => opt.MapFrom(src => src.Contato.Telefone));
            
            // EmpresaCreateDto → Empresa
            CreateMap<EmpresaCreateDto, Empresa>();
            // EmpresaPutDto → Empresa
            CreateMap<EmpresaPutDto, Empresa>();
            // EmpresaPatchDto → Empresa
            CreateMap<EmpresaPatchDto, Empresa>();

            // ========== FUNCIONÁRIO ==========
            // Funcionario → FuncionarioResponseDto
            CreateMap<Funcionario, FuncionarioResponseDto>()
                .ForMember(dest => dest.Idade,
                    opt => opt.MapFrom(src => CalcularIdade(src.DataNascimento)))
                .ForMember(dest => dest.Cidade,
                    opt => opt.MapFrom(src => src.CidadeEstado.Cidade))
                .ForMember(dest => dest.EstadoId,
                    opt => opt.MapFrom(src => src.CidadeEstado.EstadoId))
                .ForMember(dest => dest.EstadoSigla,
                    opt => opt.Ignore())
                .ForMember(dest => dest.EstadoNome,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Contato.Email))
                .ForMember(dest => dest.Telefone,
                    opt => opt.MapFrom(src => src.Contato.Telefone))
                .ForMember(dest => dest.TelefoneAlternativo,
                    opt => opt.MapFrom(src => src.Contato.TelefoneAlternativo))
                .ForMember(dest => dest.Website,
                    opt => opt.MapFrom(src => src.Contato.Website))
                .ForMember(dest => dest.LinkedIn,
                    opt => opt.MapFrom(src => src.Contato.LinkedIn))
                .ForMember(dest => dest.TotalContratos,
                    opt => opt.Ignore());

            // Funcionario → FuncionarioListDto
            CreateMap<Funcionario, FuncionarioListDto>()
                .ForMember(dest => dest.Idade,
                    opt => opt.MapFrom(src => CalcularIdade(src.DataNascimento)))
                .ForMember(dest => dest.Cidade,
                    opt => opt.MapFrom(src => src.CidadeEstado.Cidade))
                .ForMember(dest => dest.EstadoSigla,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Contato.Email))
                .ForMember(dest => dest.Telefone,
                    opt => opt.MapFrom(src => src.Contato.Telefone));

            // FuncionarioCreateDto → Funcionario
            CreateMap<FuncionarioCreateDto, Funcionario>();
            // FuncionarioPutDto → Funcionario
            CreateMap<FuncionarioPutDto, Funcionario>();
            // FuncionarioPatchDto → Funcionario
            CreateMap<FuncionarioPatchDto, Funcionario>();

            // ========== CONTRATO ==========
            // Contrato → ContratoResponseDto
            CreateMap<Contrato, ContratoResponseDto>()
                .ForMember(dest => dest.ContratanteNome,
                    opt => opt.MapFrom(src => src.Contratante.RazaoSocial))
                .ForMember(dest => dest.ContratanteCNPJ,
                    opt => opt.MapFrom(src => src.Contratante.CNPJ))
                .ForMember(dest => dest.TipoContraenteNome,
                    opt => opt.MapFrom(src => src.TipoContraente.Nome))
                .ForMember(dest => dest.ContraenteNome,
                    opt => opt.Ignore()) // Carregar dinamicamente
                .ForMember(dest => dest.ContraenteDocumento,
                    opt => opt.Ignore()) // Carregar dinamicamente
                .ForMember(dest => dest.TipoContratoNome,
                    opt => opt.MapFrom(src => src.TipoContrato.Nome))
                .ForMember(dest => dest.TipoContratoDescricao,
                    opt => opt.MapFrom(src => src.TipoContrato.Descricao))
                .ForMember(dest => dest.StatusContratoNome,
                    opt => opt.MapFrom(src => src.StatusContrato.Nome))
                .ForMember(dest => dest.StatusContratoDescricao,
                    opt => opt.MapFrom(src => src.StatusContrato.Descricao))
                .ForMember(dest => dest.EstaVencido,
                    opt => opt.MapFrom(src => src.Validade.HasValue && src.Validade.Value < DateTime.Today))
                .ForMember(dest => dest.DiasAtivo,
                    opt => opt.MapFrom(src =>
                        (DateTime.Today - src.DataEmissao).Days));

            // Contrato → ContratoListDto
            CreateMap<Contrato, ContratoListDto>()
                .ForMember(dest => dest.ContratanteNome,
                    opt => opt.MapFrom(src => src.Contratante.RazaoSocial))
                .ForMember(dest => dest.ContraenteNome,
                    opt => opt.Ignore())
                .ForMember(dest => dest.TipoContratoNome,
                    opt => opt.MapFrom(src => src.TipoContrato.Nome))
                .ForMember(dest => dest.StatusContratoNome,
                    opt => opt.MapFrom(src => src.StatusContrato.Nome))
                .ForMember(dest => dest.EstaVencido,
                    opt => opt.MapFrom(src =>
                        src.Validade.HasValue && src.Validade.Value < DateTime.Today));

            // ContratoCreateDto → Contrato
            CreateMap<ContratoCreateDto, Contrato>();
            // ContratoPutDto → Contrato
            CreateMap<ContratoPutDto, Contrato>();
            // ContratoPatchDto → Contrato
            CreateMap<ContratoPatchDto, Contrato>();
        }

        private static int CalcularIdade(DateTime dataNascimento)
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
    }
}