using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaHospitalar.Core.Entities;

namespace SistemaHospitalar.Infra.Configurations
{
    internal class ProntuarioConfiguration : IEntityTypeConfiguration<Prontuario>
    {
        public void Configure(EntityTypeBuilder<Prontuario> builder)
        {
            builder.ToTable("Prontuarios");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Diagnostico)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.ObservacoesMedicas)
                .HasMaxLength(2000);

            builder.HasOne(p => p.Consulta)
                .WithOne(c => c.Prontuario)
                .HasForeignKey<Prontuario>(p => p.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
