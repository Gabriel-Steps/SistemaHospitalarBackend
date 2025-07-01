using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaHospitalar.Core.Entities;

namespace SistemaHospitalar.Infra.Configurations
{
    public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Mensagem)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.DataCriacao)
                .IsRequired();

            builder.Property(n => n.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(n => n.Paciente)
                .WithMany()
                .HasForeignKey(n => n.IdPaciente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
