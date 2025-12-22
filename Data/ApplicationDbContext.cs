using Microsoft.EntityFrameworkCore;
using ContratosAPI.Models;

namespace ContratosAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        // Tabelas fundamentais
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        // Tabelas associativas
        public DbSet<Contrato> Contratos { get; set; }

        // Tabelas de referência
        public DbSet<TipoContrato> TiposContrato { get; set; }
        public DbSet<StatusContrato> StatusContratos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoContraente> TiposContraente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade TiposContrato
            modelBuilder.Entity<TipoContrato>(entity =>
            {
                entity.ToTable("TipoContrato");

                // Propriedades básicas
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
                entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired(false);

                entity.HasKey(e => e.Id);
            });

            // Configuração da entidade StatusContrato
            modelBuilder.Entity<StatusContrato>(entity =>
            {
                entity.ToTable("StatusContrato");

                // Propriedades básicas
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
                entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired(false);

                entity.HasKey(e => e.Id);
            });

            // Configuração da entidade Estado
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");

                // Propriedades básicas
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
                entity.Property(e => e.Sigla).HasColumnName("sigla").HasMaxLength(2).IsRequired();
                entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Sigla).IsUnique();
            });

            // Configuração da entidade TipoContraente
            modelBuilder.Entity<TipoContraente>(entity =>
            {
                entity.ToTable("TipoContraente");

                // Propriedades básicas
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
                entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired(false);

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Nome).IsUnique();
            });

            // Configuração da entidade Empresa
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");

                // Propriedades básicas
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.RazaoSocial).HasColumnName("razao_social").HasMaxLength(200).IsRequired();
                entity.Property(e => e.NomeFantasia).HasColumnName("nome_fantasia").HasMaxLength(200).IsRequired();
                entity.Property(e => e.CNPJ).HasColumnName("cnpj").HasMaxLength(14).IsRequired();
                entity.Property(e => e.Logradouro).HasColumnName("logradouro").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
                entity.Property(e => e.Complemento).HasColumnName("complemento").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.Setor).HasColumnName("setor").HasMaxLength(100).IsRequired();

                // PK
                entity.HasKey(e => e.Id);

                // Índice único
                entity.HasIndex(e => e.CNPJ).IsUnique();

                // Configuração de Contato como Owned Type
                entity.OwnsOne(e => e.Contato, contato =>
                {
                    contato.Property(c => c.Email).HasColumnName("email").HasMaxLength(254).IsRequired();
                    contato.Property(c => c.Telefone).HasColumnName("telefone").HasMaxLength(14).IsRequired();
                    contato.Property(c => c.TelefoneAlternativo).HasColumnName("telefone_alternativo").HasMaxLength(15);
                    contato.Property(c => c.Website).HasColumnName("website").HasMaxLength(200);
                    contato.Property(c => c.LinkedIn).HasColumnName("linkedin").HasMaxLength(50);
                });

                // Configuração de Cidade/Estado como Owned Type
                entity.OwnsOne(e => e.CidadeEstado, cidadeEstado =>
                {
                    cidadeEstado.Property(c => c.Cidade).HasColumnName("cidade").HasMaxLength(200).IsRequired();
                    cidadeEstado.Property(c => c.EstadoId).HasColumnName("estado_id").IsRequired();
                });
            });

            // Configuração da entidade Funcionario
            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("Funcionario");

                // Propriedades básicas
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.CPF).HasColumnName("cpf").HasMaxLength(11).IsRequired();
                entity.Property(e => e.NomeCompleto).HasColumnName("nome_completo").HasMaxLength(200).IsRequired();
                entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento").HasColumnType("date")
                    .IsRequired();
                entity.Property(e => e.Funcao).HasColumnName("funcao").HasMaxLength(150).IsRequired();

                // PK
                entity.HasKey(e => e.Id);

                // Índice único
                entity.HasIndex(e => e.CPF).IsUnique();

                // Configuração de Contato como Owned Type
                entity.OwnsOne(e => e.Contato, contato =>
                {
                    contato.Property(c => c.Email).HasColumnName("email").HasMaxLength(254).IsRequired();
                    contato.Property(c => c.Telefone).HasColumnName("telefone").HasMaxLength(14).IsRequired();
                    contato.Property(c => c.TelefoneAlternativo).HasColumnName("telefone_alternativo").HasMaxLength(15);
                    contato.Property(c => c.Website).HasColumnName("website").HasMaxLength(200);
                    contato.Property(c => c.LinkedIn).HasColumnName("linkedin").HasMaxLength(50);
                });

                // Configuração de Cidade/Estado como Owned Type
                entity.OwnsOne(e => e.CidadeEstado, cidadeEstado =>
                {
                    cidadeEstado.Property(c => c.Cidade).HasColumnName("cidade").HasMaxLength(200).IsRequired();
                    cidadeEstado.Property(c => c.EstadoId).HasColumnName("estado_id").IsRequired();
                });
            });

            // Configuração da entidade Contrato
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                // PK
                entity.HasKey(e => e.Id);


                // Propriedades básicas
                entity.Property(e => e.ContratanteId).HasColumnName("contratante_id").IsRequired();
                entity.Property(e => e.TipoContratoId).HasColumnName("tipo_contrato_id").IsRequired();
                entity.Property(e => e.StatusContratoId).HasColumnName("status_id").IsRequired();

                // Contraente polimórfico - apenas as colunas
                entity.Property(e => e.ContraenteId).HasColumnName("contraente_id").IsRequired();
                entity.Property(e => e.TipoContraenteId).HasColumnName("tipo_contraente_id").IsRequired();
                
                entity.Property(c => c.Precificacao).HasColumnName("precificacao").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.CondicoesPagamento).HasColumnName("condicoes_pagamento").HasMaxLength(500).IsRequired();
                entity.Property(e => e.DataEmissao).HasColumnName("data_emissao").IsRequired();
                entity.Property(e => e.Validade).HasColumnName("validade").IsRequired(false);
                entity.Property(e => e.Descricao).HasColumnName("descricao").HasMaxLength(1000).IsRequired(false);

                // Relacionamentos
                entity.HasOne(e => e.Contratante)
                    .WithMany(e => e.ContratosComoContratante)
                    .HasForeignKey(c => c.ContratanteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // FK para tabelas de referência
                entity.HasOne(e => e.TipoContrato)
                    .WithMany(e => e.Contratos)
                    .HasForeignKey(e => e.TipoContratoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(e => e.StatusContrato)
                    .WithMany(e => e.Contratos)
                    .HasForeignKey(e => e.StatusContratoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(c => c.TipoContraente)
                    .WithMany(t => t.Contratos)
                    .HasForeignKey(c => c.TipoContraenteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            // Dados das tabelas de referência
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed TipoContrato
            modelBuilder.Entity<TipoContrato>().HasData(
                new TipoContrato { Id = 1, Nome = "CLT", Descricao = "Consolidação das Leis do Trabalho" },
                new TipoContrato { Id = 2, Nome = "MEI", Descricao = "Microempreendedor Individual" },
                new TipoContrato { Id = 3, Nome = "Temporário", Descricao = "Contrato Temporário" },
                new TipoContrato { Id = 4, Nome = "Autônomo", Descricao = "Profissional Autônomo" },
                new TipoContrato { Id = 5, Nome = "Estágio", Descricao = "Contrato de Estágio" },
                new TipoContrato { Id = 6, Nome = "Aprendiz", Descricao = "Jovem ou menor Aprendiz" }
            );

            // Seed StatusContrato
            modelBuilder.Entity<StatusContrato>().HasData(
                new StatusContrato { Id = 1, Nome = "Ativo", Descricao = "Contrato em vigor" },
                new StatusContrato { Id = 2, Nome = "Encerrado", Descricao = "Contrato finalizado normalmente" },
                new StatusContrato { Id = 3, Nome = "Rescindido", Descricao = "Contrato rescindido" }
            );

            // Seed TipoContraente
            modelBuilder.Entity<TipoContraente>().HasData(
                new TipoContraente { Id = 1, Nome = "Empresa", Descricao = "Pessoa Jurídica" },
                new TipoContraente { Id = 2, Nome = "Funcionário", Descricao = "Pessoa Física" }
            );

            // Seed Estados
            modelBuilder.Entity<Estado>().HasData(
                new Estado { Id = 1, Sigla = "AC", Nome = "Acre" },
                new Estado { Id = 2, Sigla = "AL", Nome = "Alagoas" },
                new Estado { Id = 3, Sigla = "AP", Nome = "Amapá" },
                new Estado { Id = 4, Sigla = "AM", Nome = "Amazonas" },
                new Estado { Id = 5, Sigla = "BA", Nome = "Bahia" },
                new Estado { Id = 6, Sigla = "CE", Nome = "Ceará" },
                new Estado { Id = 7, Sigla = "DF", Nome = "Distrito Federal" },
                new Estado { Id = 8, Sigla = "ES", Nome = "Espírito Santo" },
                new Estado { Id = 9, Sigla = "GO", Nome = "Goiás" },
                new Estado { Id = 10, Sigla = "MA", Nome = "Maranhão" },
                new Estado { Id = 11, Sigla = "MT", Nome = "Mato Grosso" },
                new Estado { Id = 12, Sigla = "MS", Nome = "Mato Grosso do Sul" },
                new Estado { Id = 13, Sigla = "MG", Nome = "Minas Gerais" },
                new Estado { Id = 14, Sigla = "PA", Nome = "Pará" },
                new Estado { Id = 15, Sigla = "PB", Nome = "Paraíba" },
                new Estado { Id = 16, Sigla = "PR", Nome = "Paraná" },
                new Estado { Id = 17, Sigla = "PE", Nome = "Pernambuco" },
                new Estado { Id = 18, Sigla = "PI", Nome = "Piauí" },
                new Estado { Id = 19, Sigla = "RJ", Nome = "Rio de Janeiro" },
                new Estado { Id = 20, Sigla = "RN", Nome = "Rio Grande do Norte" },
                new Estado { Id = 21, Sigla = "RS", Nome = "Rio Grande do Sul" },
                new Estado { Id = 22, Sigla = "RO", Nome = "Rondônia" },
                new Estado { Id = 23, Sigla = "RR", Nome = "Roraima" },
                new Estado { Id = 24, Sigla = "SC", Nome = "Santa Catarina" },
                new Estado { Id = 25, Sigla = "SP", Nome = "São Paulo" },
                new Estado { Id = 26, Sigla = "SE", Nome = "Sergipe" },
                new Estado { Id = 27, Sigla = "TO", Nome = "Tocantins" }
            );
        }
    }
}