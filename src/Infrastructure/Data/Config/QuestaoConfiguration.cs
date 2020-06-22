using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class QuestaoConfiguration : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Item)
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired();

            builder.Property(x => x.Html)
                .IsRequired(false);

            builder.HasOne(questao => questao.Descritor)
                .WithMany(descritor => descritor.Questoes)
                .HasForeignKey(questao => questao.DescritorId);
        }
    }
}
