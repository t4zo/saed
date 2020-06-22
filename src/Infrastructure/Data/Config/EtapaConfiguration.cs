using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class EtapaConfiguration : IEntityTypeConfiguration<Etapa>
    {
        public void Configure(EntityTypeBuilder<Etapa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Normativa)
                .IsRequired();

            builder.HasOne(etapa => etapa.Segmento)
                .WithMany(segmento => segmento.Etapas)
                .HasForeignKey(etapa => etapa.SegmentoId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
