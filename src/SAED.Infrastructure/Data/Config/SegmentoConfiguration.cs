using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class SegmentoConfiguration : IEntityTypeConfiguration<Segmento>
    {
        public void Configure(EntityTypeBuilder<Segmento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Sigla)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.CursoId)
                .IsRequired();

            builder.HasOne(segmento => segmento.Curso)
                .WithMany(curso => curso.Segmentos)
                .HasForeignKey(segmento => segmento.CursoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}