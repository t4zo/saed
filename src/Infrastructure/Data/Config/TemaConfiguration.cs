using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class TemaConfiguration : IEntityTypeConfiguration<Tema>
    {
        public void Configure(EntityTypeBuilder<Tema> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(tema => tema.Disciplina)
                .WithMany(disciplina => disciplina.Temas)
                .HasForeignKey(tema => tema.DisciplinaId);

            builder.HasKey(tema => tema.Id);
        }
    }
}
