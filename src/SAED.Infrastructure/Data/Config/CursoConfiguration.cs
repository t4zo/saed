using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(curso => curso.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Sigla)
                .HasMaxLength(8)
                .IsRequired();
        }
    }
}