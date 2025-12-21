using Microsoft.EntityFrameworkCore;
using ContratosAPI.Models;

namespace ContratosAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Contrato> Contratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuração da entidade Empresa
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasIndex(e => e.CNPJ).IsUnique();
                
                // Configuração de Contato como Owned Type
                entity.OwnsOne(e => e.Contato, contato =>
                {
                    contato.Property(c => c.Email).HasColumnName("email").IsRequired();
                    contato.Property(c => c.Telefone).HasColumnName("telefone").IsRequired();
                    contato.Property(c => c.TelefoneAlternativo).HasColumnName("telefone_alternativo");
                    contato.Property(c => c.Website).HasColumnName("website");
                    contato.Property(c => c.LinkedIn).HasColumnName("linkedin");
                    
                    // Índice único no Email
                    contato.HasIndex(c => c.Email).IsUnique();
                });
                
                // Configuração de Cidade/Estado como Owned Type
                entity.OwnsOne(e => e.CidadeEstado, cidadeEstado =>
                {
                    cidadeEstado.Property(c => c.Cidade).HasColumnName("cidade").IsRequired();
                    cidadeEstado.Property(c => c.Estado).HasColumnName("estado").IsRequired();
                });
            });
            
            // Configuração da entidade Funcionario
            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("Funcionario");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasIndex(e => e.CPF).IsUnique();
                
                // Configuração de Contato como Owned Type
                entity.OwnsOne(e => e.Contato, contato =>
                {
                    contato.Property(c => c.Email).HasColumnName("email").IsRequired();
                    contato.Property(c => c.Telefone).HasColumnName("telefone").IsRequired();
                    contato.Property(c => c.TelefoneAlternativo).HasColumnName("telefone_alternativo");
                    contato.Property(c => c.Website).HasColumnName("website");
                    contato.Property(c => c.LinkedIn).HasColumnName("linkedin");
                    
                    // Índice único no Email
                    contato.HasIndex(c => c.Email).IsUnique();
                });
                
                // Configuração de Cidade/Estado como Owned Type
                entity.OwnsOne(e => e.CidadeEstado, cidadeEstado =>
                {
                    cidadeEstado.Property(c => c.Cidade).HasColumnName("cidade").IsRequired();
                    cidadeEstado.Property(c => c.Estado).HasColumnName("estado").IsRequired();
                });
            });
            
            // Configuração da entidade Contrato
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");
                entity.Property(e => e.Id).HasColumnName("id");
                
                // Relacionamento: Contratante (sempre Empresa)
                entity.HasOne(c => c.Contratante)
                    .WithMany(c => c.ContratosComoContratante)
                    .HasForeignKey(c => c.ContratanteId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relacionamento: Contraente pode ser Empresa
                entity.HasOne(c => c.ContraenteEmpresa)
                    .WithMany(c => c.ContratosComoContraente)
                    .HasForeignKey(c => c.ContraenteEmpresaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
                
                // Relacionamento: Contraente pode ser Funcionário
                entity.HasOne(c => c.ContraenteFuncionario)
                    .WithMany(c => c.Contratos)
                    .HasForeignKey(c => c.ContraenteFuncionarioId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
                
                entity.Property(c => c.Precificacao)
                    .HasColumnName("precificacao")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });
        }
    }
}

