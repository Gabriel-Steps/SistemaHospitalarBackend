using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaHospitalar.Core.Entities;


namespace SistemaHospitalar.Infra.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataNascimento)
                .IsRequired();

            builder.Property(p => p.Genero)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.CPF)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(p => p.CPF).IsUnique();

            builder.Property(p => p.PlanoSaude)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.NumeroCarteiraPlano)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
