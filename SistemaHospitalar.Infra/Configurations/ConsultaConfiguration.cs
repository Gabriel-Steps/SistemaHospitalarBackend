using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaHospitalar.Core.Entities;

namespace SistemaHospitalar.Infra.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consultas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Observacoes)
                .HasMaxLength(1000);

            builder.HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId);

            builder.HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId);

            builder.HasOne(c => c.Prontuario)
                .WithOne(p => p.Consulta)
                .HasForeignKey<Consulta>(c => c.PronturarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
