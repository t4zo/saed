using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class EtapaDescritorConfiguration : IEntityTypeConfiguration<EtapaDescritor>
    {
        public void Configure(EntityTypeBuilder<EtapaDescritor> builder)
        {
            builder.HasOne(anoDescritor => anoDescritor.Etapa)
                .WithMany(ano => ano.EtapaDescritores)
                .HasForeignKey(anoDescritor => anoDescritor.EtapaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(anoDescritor => anoDescritor.Descritor)
                .WithMany(descritor => descritor.EtapaDescritores)
                .HasForeignKey(anoDescritor => anoDescritor.DescritorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(anoDescritor => new { anoDescritor.EtapaId, anoDescritor.DescritorId });
        }
    }
}
