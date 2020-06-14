using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class SegmentoConfiguration : IEntityTypeConfiguration<Segmento>
    {
        public void Configure(EntityTypeBuilder<Segmento> builder)
        {
            builder.Property(x => x.Id);

            builder.HasMany(segmento => segmento.Etapas)
                .WithOne(ano => ano.Segmento)
                .HasForeignKey(ano => ano.SegmentoId);

            builder.HasOne(segmento => segmento.Curso)
                .WithMany(curso => curso.Segmentos)
                .HasForeignKey(segmento => segmento.CursoId);

            builder.HasKey(segmento => segmento.Id);
        }
    }
}
