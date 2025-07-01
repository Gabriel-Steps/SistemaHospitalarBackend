using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaHospitalar.Core.Entities;

namespace SistemaHospitalar.Infra.Configurations
{
    public class ExameConfiguration : IEntityTypeConfiguration<Exame>
    {
        public void Configure(EntityTypeBuilder<Exame> builder)
        {
            builder.ToTable("Exames");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Resultado)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.ArquivoUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(e => e.Prontuario)
                .WithMany(p => p.Exames)
                .HasForeignKey(e => e.ProntuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
