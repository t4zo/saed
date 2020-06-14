using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class EtapaConfiguration : IEntityTypeConfiguration<Etapa>
    {
        public void Configure(EntityTypeBuilder<Etapa> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(etapa => etapa.Segmento)
                .WithMany(segmento => segmento.Etapas)
                .HasForeignKey(etapa => etapa.SegmentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(etapa => etapa.Id);
        }
    }
}
