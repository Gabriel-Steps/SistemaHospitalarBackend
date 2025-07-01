using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaHospitalar.Core.Entities;

namespace SistemaHospitalar.Infra.Configurations
{
    public class PrecricaoConfiguration : IEntityTypeConfiguration<Prescricao>
    {
        public void Configure(EntityTypeBuilder<Prescricao> builder)
        {
            builder.ToTable("Prescricoes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Medicamento)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Posologia)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(p => p.Prontuario)
                .WithMany(pr => pr.Prescricoes)
                .HasForeignKey(p => p.ProntuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
